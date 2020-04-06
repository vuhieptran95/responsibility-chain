﻿<template>
    <v-row>
        <v-col>
            <p class="subtitle-1 mb-0 font-weight-bold">Non functional DoDs</p>
            <v-btn @click="addEditDodModal = true" class="mb-3" :class="isAdd? 'success' : 'primary--text white'">
                {{isAdd?
                "Add" : "Edit"}} metrics for this week
            </v-btn>
            <table v-if="report.metrics.length > 0" class="table table-bordered">
                <thead>
                <tr>
                    <td colspan="2"/>
                    <td width="12%" v-for="yw in yearWeeks" :key="yw.yearWeek"
                        class="text-center font-weight-medium" :class="yw.class">
                        {{vueHelper.calculateYear(yw.yearWeek) + ' - ' +
                        vueHelper.calculateWeek(yw.yearWeek)}}
                    </td>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(metric,i) in metrics" :key="i">
                    <td v-if="metric.count" :rowspan="metric.count" style="vertical-align : middle;text-align:center;"
                        class="text-left subtitle-2 font-weight-bold">
                        <p>{{metric.tool}}</p>
                    </td>
                    <td class="subtitle-2"><p class="mt-2">{{metric.name}} {{metric.unit ? '(' + metric.unit + ')' :
                        ''}}</p></td>
                    <td class="text-center pl-0 pr-0" v-for="(val, i) in metric.yearWeekValues" :key="i">
                        <v-text-field
                                v-if="val.yearWeek === yearWeek && metric.valueType === vueHelper.valueTypeNumber && val.value != null"
                                :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber]" class="font-weight-bold centered-input"
                                :class="val.class"
                                min="0" type="number" dense v-model.number="val.value"/>
                        <v-text-field :rules="[vueHelper.mustRequired]"
                                      :class="val.class" class="font-weight-bold centered-input"
                                      v-else-if="val.yearWeek === yearWeek && metric.valueType === vueHelper.valueTypeText && val.value != null"
                                      type="text" dense v-model="val.value"/>
                        <v-select :rules="[vueHelper.mustRequired]" class="font-weight-bold centered-input"
                                  :class="val.class"
                                  v-else-if="val.yearWeek === yearWeek && metric.valueType === vueHelper.valueTypeSelect && val.value != null"
                                  :items="metric.selectValues.split(';')" type="text" dense v-model="val.value"/>
                        <p class="mt-2 font-weight-bold" :class="val.class"
                           v-else>{{val.value}}</p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <v-btn class="ma-2" outlined small fab color="indigo" @click="addEditReportLink = true">
                            <v-icon>mdi-pencil</v-icon>
                        </v-btn>
                        <span class="font-weight-bold">DoD Report Link</span></td>
                    <td v-for="link in dodLinks" :key="link.yearWeek" class="pl-0 pr-0 text-center"><p class="mt-2"><a
                            :href="link.linkToReport" target="_blank">{{link.reportFileName}}</a></p></td>
                </tr>
                </tbody>
            </table>
            <v-row justify="center">
                <v-dialog v-model="addEditReportLink" max-width="500">
                    <v-card>
                        <v-card-title class="headline">Add/Edit Report Link <span class="subtitle-1">(Metrics needs to be specified first)</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container>
                                <v-row>
                                    <v-col md="6">
                                        <v-text-field v-model="currentDodLink.reportFileName" label="File name"
                                                      outlined/>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col md="12">
                                        <v-text-field v-model="currentDodLink.linkToReport" label="Url to DoD report"
                                                      outlined/>
                                    </v-col>
                                </v-row>
                            </v-container>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer/>
                            <v-btn color="primary darken-2" outlined text
                                   @click="addEditDoDlinks">Save Report Link
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-row>
            <v-row justify="center">
                <v-dialog v-model="addEditDodModal" max-width="700">
                    <v-card>
                        <v-card-title class="headline">{{isAdd? "Add" : "Edit"}} metrics</v-card-title>
                        <v-card-text>
                            <v-container>
                                <v-row :key="group.tool" v-for="group in metricGroupsWatch">
                                    <v-col md="12">
                                        <p class="title mb-0">{{group.tool}}</p>
                                        <v-row class="flex-wrap">
                                            <v-checkbox :key="i" class="mx-2 fix-label"
                                                        v-model="metric.selected"
                                                        v-for="(metric,i) in group.metrics"
                                                        :label="metric.name"/>
                                        </v-row>
                                    </v-col>
                                </v-row>
                            </v-container>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer/>
                            <v-btn color="primary darken-2" outlined text
                                   @click="handleAddEditMetrics">Save metrics
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-row>
        </v-col>

    </v-row>
</template>

<script lang="ts">
    import {Component, Prop, Vue, Watch} from 'vue-property-decorator'
    import {
        DodLink,
        DodMetric,
        DodRecord,
        Report, Threshold, YearWeekValue
    } from "@/components/PhrWeeklyReports/WeeklyReport";
    import VueHelper from "@/helpers/VueHelper";
    import axios from "axios";
    import {DODS_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError} from "@/helpers/HandleResponse";
    import _ from "lodash";
    import DisplayText from "@/components/PhrWeeklyReports/DisplayText.vue";
    import evaluate from "ts-expression-evaluator";

    export interface GroupMetrics {
        tool: string;
        metrics: DodMetric[];
    }

    @Component({
        components: {DisplayText}
    })
    export default class WeeklyReportDod extends Vue {
        @Prop() report!: Report;

        vueHelper = VueHelper;
        addEditReportLink = false;
        addEditDodModal = false;
        dodRecords: DodRecord[] = [];
        metricGroups: GroupMetrics[] = [];
        thresholds: Threshold[] = [];
        metrics: DodMetric[] = [];
        yearWeeks: any[] = [];
        dodLinks: DodLink[] = [];
        currentDodLink: DodLink = {yearWeek: 0, reportFileName: null, linkToReport: null};

        @Watch('report.metrics', {deep: true, immediate: true})
        onReportChange(val: DodMetric[], oldVal: DodMetric[]) {
            if (_.isEqual(this.metrics, val))
                return;

            this.metrics = _.cloneDeep(val);
        }

        @Watch('report.numberOfWeek')
        async onNumberOfWeekChange() {
            await this.getDoDLinks()
        }

        @Watch('report.selectedWeek')
        async onSelectedWeekChange() {
            await this.getDoDLinks()
        }

        @Watch('currentDodLink', {deep: true, immediate: true})
        onCurrentDodLinkChange(val: DodLink) {
            this.$emit("report-link-change", val)
        }

        @Watch('metrics', {deep: true, immediate: true})
        onDoDReportChange(val: DodMetric[]) {
            let clone = _.cloneDeep(val);

            clone.forEach(m => {
                m.yearWeekValues.forEach(v => {
                    if (v.value !== null) {
                        v.class = this.appendClass(m.id, v.value)
                    }
                })
            });

            let yearWeekValues: YearWeekValue[] = [];
            clone.forEach(m => yearWeekValues.push(...m.yearWeekValues));

            let groups = _(yearWeekValues).groupBy('yearWeek').map((items, yearWeek) => ({
                yearWeek: yearWeek,
                items: items,
                class: ""
            })).value();

            groups.forEach(g => {
                if (g.items.every(i => i.value == null)) return;
                if (g.items.find(i => i.class === this.classifyColor("RED"))) {
                    g.class = "status-red";
                    return;
                }
                if (g.items.find(i => i.class === this.classifyColor("YELLOW"))) {
                    g.class = "status-yellow";
                    return;
                }
                g.class = "status-green";
            });

            let yearWeeks = groups.map(g => ({yearWeek: g.yearWeek, class: g.class}));

            this.yearWeeks = _.orderBy(yearWeeks, 'yearWeek', 'desc');

            if (_.isEqual(clone, val)) return;

            this.metrics = clone;

            let dodRecords: DodRecord[] = [];
            let yearWeek = this.vueHelper.calculateYearWeek(this.report.selectedYear, this.report.selectedWeek);
            val.forEach(m => {
                let ywv = m.yearWeekValues.find(i => i.yearWeek === yearWeek && i.value != null);
                if (ywv) {
                    dodRecords.push({
                        yearWeek: yearWeek,
                        metricId: m.id,
                        projectId: m.projectId,
                        value: ywv.value,
                        linkToReport: this.currentDodLink.linkToReport,
                        reportFileName: this.currentDodLink.reportFileName
                    })
                }
            });

            if (_.isEqual(this.dodRecords, dodRecords)) return;

            this.dodRecords = _.cloneDeep(dodRecords);

            this.$emit("dod-records-change", dodRecords);
        }


        appendClass(metricId: number, value: string): string {
            let thresholds = this.thresholds.filter(t => t.metricId === metricId);
            let color = "";
            thresholds.forEach(t => {
                if (t.metricValueType === this.vueHelper.valueTypeSelect) {
                    if (t.value === value) color = t.metricStatusName;
                } else if (t.metricValueType === this.vueHelper.valueTypeNumber && t.lowerBound !== null && t.upperBound !== null && t.lowerBoundOperator !== null && t.upperBoundOperator !== null && value !== "") {
                    let condition = evaluate(t.lowerBound.toString() + t.lowerBoundOperator.toString() + value.toString() + " && " + value.toString() + t.upperBoundOperator.toString() + t.upperBound.toString());
                    if (condition) {
                        color = t.metricStatusName;
                        return;
                    }
                }
            });
            return this.classifyColor(color);
        }

        classifyColor(color: string) {
            switch (color) {
                case "GREEN":
                    return "status-text-green";
                case "YELLOW":
                    return "status-text-yellow";
                case "RED":
                    return "status-text-red";
                default:
                    return "";
            }
        }


        get yearWeek() {
            return this.vueHelper.calculateYearWeek(this.report.selectedYear, this.report.selectedWeek)
        }

        async mounted() {
            await this.init();
            this.metrics.forEach(m => {
                m.yearWeekValues.forEach(v => {
                    if (v.value !== null) {
                        v.class = this.appendClass(m.id, v.value)
                    }
                })
            });
        }

        get isAdd() {
            return this.dodRecords.length === 0;
        }

        get metricGroupsWatch(): GroupMetrics[] {
            this.metricGroups.forEach(g => {
                g.metrics.forEach(m => {
                    if (this.dodRecords.find(r => r.metricId === m.id))
                        m.selected = true;
                })
            });

            return this.metricGroups;
        }

        async init() {
            try {
                let res = await axios.get(DODS_ENDPOINT + "metrics");
                this.metricGroups = res.data.metricGroups;
                this.metricGroups.forEach(g => {
                    g.metrics.forEach(m => {
                        m.thresholds.forEach(t => t.metricValueType = m.valueType);
                        this.thresholds.push(...m.thresholds)
                    })
                })

                await this.getDoDLinks();

            } catch (e) {
                handleAxiosError(e)
            }
        }

        async getDoDLinks() {
            try {
                let resLink = await axios
                    .get(DODS_ENDPOINT + `links?projectId=${this.report.projectId}&yearWeek=${this.yearWeek}&numberOfWeek=${this.report.numberOfWeek}`);
                this.dodLinks = resLink.data.reportLinks;
                if (this.dodLinks.length > 0) this.currentDodLink = this.dodLinks[0];
            } catch (e) {
                handleAxiosError(e)
            }
        }

        async addEditDoDlinks() {
            try {
                await axios.post(DODS_ENDPOINT + `links`, {
                    projectId: this.report.projectId,
                    yearWeek: this.yearWeek,
                    linkToReport: this.currentDodLink.linkToReport,
                    reportFileName: this.currentDodLink.reportFileName
                });
                this.addEditReportLink = false;
                await this.getDoDLinks();
            } catch (e) {
                handleAxiosError(e)
            }
        }

        handleAddEditMetrics() {
            this.addEditDodModal = false;
            let metrics: DodMetric[] = [];
            this.metricGroupsWatch.map(g => {
                metrics.push(...g.metrics)
            });

            let payload = metrics.map(m => {
                if (m.selected)
                    return {
                        projectId: this.report.projectId,
                        metricId: m.id,
                        value: "",
                        yearWeek: this.vueHelper.calculateYearWeek(this.report.selectedYear, this.report.selectedWeek)
                    };
                return null;
            });

            payload = payload.filter(i => i !== null);

            for (let metric of payload) {
                let record = this.dodRecords.find(r => r.metricId === metric?.metricId);
                if (record && metric)
                    metric.value = record.value !== null ? record.value.toString() : ""
            }

            axios.put(DODS_ENDPOINT + "dod-reports", {dodReports: payload}).then(async res => {
                this.$emit("dod-change-metrics");
                if (this.currentDodLink.reportFileName == null && this.currentDodLink.linkToReport == null) return;
                await this.addEditDoDlinks();
            }).catch(handleAxiosError)
        }

    }
</script>

<style>
    .centered-input input {
        text-align: center
    }
    
    .centered-input .v-select__selection.v-select__selection--comma{
        margin: auto;
        padding-left: 1rem;
    }
    
    .centered-input .v-select__selections input {
        display:none;
    }
    
    .fix-label label {
        margin-bottom: 0;
    }

    .status-text-red, .status-text-red.theme--light.v-input input, .status-text-red.theme--light.v-select .v-select__selection--comma {
        color: red;
    }

    .status-red {
        color: white;
        background-color: red;
    }

    .status-text-green, .status-text-green.theme--light.v-input input, .status-text-green.theme--light.v-select .v-select__selection--comma {
        color: #5d7745;
    }

    .status-green {
        color: white;
        background-color: #5d7745;
    }

    .status-text-yellow, .status-text-yellow.theme--light.v-input input, .status-text-yellow.theme--light.v-select .v-select__selection--comma {
        color: #c89800
    }

    .status-yellow {
        color: black;
        background-color: yellow;
    }

</style>
