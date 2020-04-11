﻿<template>
    <div id="weeklyReport">
        <v-content>
            <v-container>
                <h1 class="display-1 text-md-center">Weekly Report: Week {{report.selectedWeek}}
                    ({{vueHelper.formatMomentDate(report.firstWorkingDateOfWeek, "DD. MMM")}} -
                    {{vueHelper.formatMomentDate(report.lastWorkingDateOfWeek, "DD. MMM")}})</h1>

                <v-form ref="form" v-model="valid">
                    <v-row>
                        <v-col lg="12" xl="12">
                            <v-row>
                                <v-col md="3">
                                    <v-text-field :rules="vueHelper.requiredRules" readonly outlined
                                                  label="Project Name *"
                                                  hide-details
                                                  color="primary"
                                                  append-icon="edit"
                                                  @click:append="goToEditProject"
                                                  @click="goToEditProject"
                                                  v-model="report.projectName"/>
                                </v-col>
                                <v-col md="2">
                                    <v-select @change="getAllowedWeeks()" :rules="vueHelper.requiredRules" outlined
                                              hide-details
                                              label="Current year *" v-model="report.selectedYear"
                                              :items="report.years"/>
                                </v-col>
                                <v-col md="2">
                                    <v-select @change="handleViewSettingsChanged()" :rules="vueHelper.requiredRules"
                                              outlined
                                              hide-details
                                              label="Report week *" v-model="report.selectedWeek"
                                              :items="report.weeks"/>
                                </v-col>
                                <v-col md="2">
                                    <v-select @change="handleViewSettingsChanged()" :rules="vueHelper.requiredRules"
                                              outlined
                                              hide-details
                                              label="Previous weeks to display *" v-model="report.numberOfWeek"
                                              :items="report.numberOfWeeks"/>
                                </v-col>
                            </v-row>

                            <WeeklyReportStatus @status-change="handleStatusChange" :report="report"/>

                            <WeeklyReportBacklog @backlog-item-change="handleBacklogItemChange" :report="report"/>

                            <WeeklyReportQualityReport @quality-report-change="handleQualityReportChange"
                                                       :report="report"/>

                            <WeeklyReportDod v-if="report.dodRequired" @dod-records-change="handleDoDRecordsChange"
                                             ref="dod"
                                             @dod-change-metrics="handleDoDChangeMetrics"
                                             @report-link-change="handleReportLinkChange"
                                             :report="report"/>

                            <WeeklyReportAdditionalInfo
                                    @view-settings-change="handleAdditionalInfoViewSettingsChange"
                                    @additional-infos-change="handleAdditionalInfosChange"
                                    :issue-removed-ids-object="{issueRemovedIds: issueRemovedIds}"
                                    :report="report"/>

                            <div class="mt-4 d-flex justify-content-between">
                                <v-btn color="primary" v-on:click="addEditReport()">Save weekly report</v-btn>
                                <v-btn outlined color="primary" @click="getGeneratedReport()">Generate report with
                                    current settings
                                </v-btn>
                            </div>

                            <!--                            <WeeklyReportPopupNoInitial :report="report"/>-->
                            <v-row justify="center">
                                <v-dialog v-model="noInitialModalShow" persistent max-width="450">
                                    <v-card>
                                        <v-card-title class="headline">You cannot add weekly report yet!</v-card-title>
                                        <v-card-text>You need to specify <strong>Initial Items</strong> and/or <strong>Story
                                            Points</strong> first.
                                        </v-card-text>
                                        <v-card-actions>
                                            <v-spacer/>
                                            <v-btn color="primary darken-2" outlined
                                                   :to="getProjectDetails(report.projectId)" text
                                            >Edit this project
                                            </v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </v-dialog>
                            </v-row>
                        </v-col>
                    </v-row>
                </v-form>
            </v-container>
        </v-content>
    </div>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {
        AdditionalInfo,
        BacklogItem,
        defaultReport, DodLink, DodRecord,
        Issue, QualityReport,
        Report,
        Status
    } from "@/components/PhrWeeklyReports/WeeklyReport";
    import VueHelper from "@/helpers/VueHelper";
    import {HELPER_ENDPOINT, WEEKLYREPORT_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError, notify} from "@/helpers/HandleResponse";
    import axios from "axios";
    import _ from "lodash";
    import moment from "moment";
    import router from "@/router";
    import DisplayText from "@/components/PhrWeeklyReports/DisplayText.vue";
    import WeeklyReportBacklog from "@/components/PhrWeeklyReports/WeeklyReportBacklog.vue";
    import WeeklyReportStatus from "@/components/PhrWeeklyReports/WeeklyReportStatus.vue";
    import WeeklyReportQualityReport from "@/components/PhrWeeklyReports/WeeklyReportQualityReport.vue";
    import WeeklyReportAdditionalInfo from "@/components/PhrWeeklyReports/WeeklyReportAdditionalInfo.vue";
    import WeeklyReportPopupNoInitial from "@/components/PhrWeeklyReports/WeeklyReportPopupNoInitial.vue";
    import WeeklyReportDod from "@/components/PhrWeeklyReports/WeeklyReportDod.vue";

    @Component({
        components: {
            WeeklyReportDod,
            WeeklyReportPopupNoInitial,
            WeeklyReportAdditionalInfo,
            WeeklyReportQualityReport, WeeklyReportStatus, WeeklyReportBacklog, DisplayText
        }
    })
    export default class WeeklyReport extends Vue {
        report: Report = defaultReport;
        initialReport: Report = defaultReport;
        editorConfig = {
            language: 'en'
        };
        noInitialModalShow = false;
        vueHelper = VueHelper;
        valid = false;
        issueRemovedIds: Issue[] = [];
        dod = this.$refs.dod;

        async mounted() {
            await this.init();
        }

        async init() {
            this.issueRemovedIds = [];
            let queryString = window.location.href.split('?')[1];
            let url = `${WEEKLYREPORT_ENDPOINT + "phr"}?${queryString}`;

            try {
                let reportResponse = await axios.get(url);
                this.report = reportResponse.data;

                if (!this.report.status) {
                    this.report.status = defaultReport.status;
                } else {
                    if (this.report.status.milestoneDate)
                        this.report.status.milestoneDate = moment(this.report.status.milestoneDate).format("YYYY-MM-DD");
                }

                if (!this.report.backlogItem) this.report.backlogItem = defaultReport.backlogItem;
                else {
                    if (!this.report.backlogItem.storyPointsAdded) this.report.backlogItem.storyPointsAdded = 0;
                    if (!this.report.backlogItem.storyPointsRemaining) this.report.backlogItem.storyPointsRemaining = 0;
                }

                if (!this.report.qualityReport) this.report.qualityReport = defaultReport.qualityReport;

                this.report.additionalInfos = _.sortBy(this.report.additionalInfos, 'openedYearWeek');

                this.initialReport = _.cloneDeep(this.report);

                this.getAllowedWeeks();

                if (this.report.backlogItemListReadOnly.length === 0) {
                    this.noInitialModalShow = true
                }
            } catch (e) {
                handleAxiosError(e)
            }
        };

        addEditReport() {
// @ts-ignore
            if (!this.$refs.form.validate()) return;

            this.report.dodRecords = this.report.dodRecords.map(r => {
                if (r.value !== null) r.value = r.value.toString();
                return r;
            });

            this.report.metrics.forEach(m => {
                m.yearWeekValues.forEach(v => {
                        if (v.value !== null) v.value = v.value.toString();
                    }
                )
            });
            // let record = this.report.dodRecords.find(r => !r && r.value !== undefined);
            // if (record) {
            //     notify(`One of your DoD input values is out of range: ${record.tool} ${record.metricName}`, "error");
            //     return;
            // }

            if ((this.report.status.milestone && !this.report.status.milestoneDate) || (!this.report.status.milestone && this.report.status.milestoneDate)) {
                notify(`Milestone must have both Date and Content if is specified`, "error");
                return;
            }

            axios.post(WEEKLYREPORT_ENDPOINT + "phr", {report: this.report, issueRemovedIds: this.issueRemovedIds})
                .then(async res => {
                    let message = "Weekly Report is saved successfully";
                    notify(message, "success");
                    await this.init();
                })
                .catch(handleAxiosError)
        }

        async handleViewSettingsChanged() {
            await router.push({
                name: "PHRAddEditWeeklyReports",
                query: {
                    projectId: this.report.projectId.toString(),
                    year: this.report.selectedYear.toString(),
                    week: this.report.selectedWeek.toString(),
                    numberOfWeek: this.report.numberOfWeek.toString(),
                    numberOfWeekNotShowClosedItem: this.report.numberOfWeekNotShowClosedItem.toString()
                }
            });
            await this.init();
            // @ts-ignore
            this.$refs.form.resetValidation()
        }

        async goToEditProject() {
            await router.push({
                name: "PHRAddEditProject",
                query: {
                    id: this.report.projectId.toString()
                }
            })
        }

        getProjectDetails(projectId: number) {
            return `/Projects/AddEditProject?id=${projectId}`;
        };

        async getAllowedWeeks() {
            try {
                let allowedWeeksResponse = await axios.get(`${HELPER_ENDPOINT}allowed-weeks/${this.report.selectedYear}`);
                this.report.weeks = allowedWeeksResponse.data.allowedWeeks.reverse();
            } catch (e) {
                handleAxiosError(e)
            }
        };

        getGeneratedReport() {
            let queryString = window.location.href.split('?')[1];
            window.location.href = `/WeeklyReports/GetGeneratedWeeklyReport?${queryString}`;
        }

        handleBacklogItemChange(backlogItem: BacklogItem) {
            this.report.backlogItem = backlogItem;
        }

        handleStatusChange(status: Status) {
            this.report.status = status;
        }

        handleQualityReportChange(qualityReport: QualityReport) {
            this.report.qualityReport = qualityReport;
        }

        handleDoDRecordsChange(dodRecords: DodRecord[]) {
            this.report.dodRecords = dodRecords;
        }

        handleReportLinkChange(dodLink: DodLink) {
            this.report.dodRecords.forEach(d => {
                d.reportFileName = dodLink.reportFileName;
                d.linkToReport = dodLink.linkToReport;
            })
        }

        async handleDoDChangeMetrics() {
            let queryString = window.location.href.split('?')[1];
            let url = `${WEEKLYREPORT_ENDPOINT + "phr"}?${queryString}`;
            try {
                let reportResponse = await axios.get(url);
                this.report.metrics = reportResponse.data.metrics;
            } catch (e) {
                handleAxiosError(e)
            }
        }

        handleAdditionalInfosChange(additionalInfos: AdditionalInfo[], issueRemovedIds: Issue[]) {
            this.report.additionalInfos = additionalInfos;
            this.issueRemovedIds = issueRemovedIds;
        }

        async handleAdditionalInfoViewSettingsChange(numberOfWeekNotShowClosedItem: number) {
            this.report.numberOfWeekNotShowClosedItem = numberOfWeekNotShowClosedItem;
            await this.handleViewSettingsChanged();
        }
    }
</script>