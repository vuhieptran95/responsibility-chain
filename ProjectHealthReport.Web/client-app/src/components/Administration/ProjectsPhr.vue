<template>
    <v-content>
        <v-container class="container--fluid">
            <div>
                <v-data-table
                        :items-per-page="20"
                        :headers="headers"
                        :search="search"
                        :items="items"
                        :expanded.sync="expanded"
                        @click:row="handleRowExpand"
                        @item-selected="handleSelect"
                        @toggle-select-all="handleSelectAll"
                        @item-expanded="handleItemExpand"
                        multi-sort
                        show-select
                        item-key="name"
                        :footer-props="{'items-per-page-options': [20, 50, 100, -1]}"
                >
                    <template v-slot:top>
                        <v-row class="d-flex">
                            <v-col sm="2">
                                <v-text-field v-model="search" label="Search" placeholder=" " append-icon="search"
                                              right/>
                            </v-col>
                            <v-col sm="2">
                                <v-select
                                        :value="selectedYearWeek"
                                        :items="yearWeeks"
                                        :menu-props="{ top: true, offsetY: true }"
                                        @change="handleCurrentReportWeekChange"
                                        label="Current Report Week"
                                />
                            </v-col>
                            <v-col sm="2">
                                <p style="padding-top:1.2rem">{{workingDays}}</p>
                            </v-col>
                            <v-col class="mt-3 text-sm-right" sm="6">
                                <v-dialog
                                        v-model="dialog"
                                        width="700"
                                >
                                    <template v-slot:activator="{ on }">
                                        <v-btn v-on="on" color="blue" class="white--text">
                                            Export{{numberOfPdf}}PDF(s)
                                        </v-btn>
                                    </template>

                                    <v-card>
                                        <v-card-title
                                                class="headline grey lighten-2"
                                                primary-title
                                        >
                                            PDF Settings
                                        </v-card-title>

                                        <v-card-text>
                                            <v-row>
                                                <v-col sm="4">
                                                    <v-select
                                                            v-model="viewSettings.numberOfWeek"
                                                            :value="viewSettings.numberOfWeek"
                                                            :items="previousWeeks"
                                                            :menu-props="{ top: true, offsetY: true }"
                                                            label="Number of previous weeks to display"
                                                    />
                                                </v-col>

                                                <v-col class="ml-4" sm="4">
                                                    <v-select
                                                            v-model="viewSettings.numberOfWeekNotShowClosedItem"
                                                            :value="viewSettings.numberOfWeekNotShowClosedItem"
                                                            :items="notShowClosedItemWeeks"
                                                            :menu-props="{ top: true, offsetY: true }"
                                                            label="Do not display closed items older than ... weeks"
                                                    />
                                                </v-col>
                                            </v-row>
                                        </v-card-text>
                                        <v-divider/>
                                        <v-card-actions>
                                            <v-spacer/>
                                            <v-btn
                                                    color="primary"
                                                    text
                                                    target="_blank"
                                                    @click="handleExportPdfs"
                                            >
                                                Export PDFs
                                            </v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </v-dialog>
                            </v-col>
                        </v-row>
                        <v-row class="d-flex mb-3 ml-3">
                            <v-chip class="mr-2" :key="i" v-for="(item, i) in filters" close
                                    @click:close="removeFromFilter(item)">{{item.value}}
                            </v-chip>
                        </v-row>
                    </template>

                    <template v-slot:item.division="{item}">
                        <v-btn text @click.stop="addToFilter({field: 'division', value: item.division})" small>
                            {{item.division}}
                        </v-btn>
                    </template>

                    <template v-slot:item.name="{item}">
                        <v-list-item @click="dummyAction">
                            {{item.name}}
                        </v-list-item>
                    </template>

                    <template v-slot:item.kam="{item}">
                        <v-list-item @click.stop="addToFilter({field: 'kam', value: item.kam})">
                            {{item.kam}}
                        </v-list-item>
                    </template>

                    <template v-slot:item.pic="{item}">
                        <v-list-item @click.stop="addToFilter({field: 'pic', value: item.pic})">
                            {{item.pic}}
                        </v-list-item>
                    </template>

                    <template v-slot:item.currentstatuses="{item}">
                        <v-tooltip :key="i" v-for="(status, i) in item.currentstatuses"
                                   v-if="vueHelper.formatCurrentStatus(status)" bottom>
                            <template v-slot:activator="{on}">
                                <v-chip @click.stop="addToFilter({field: 'currentStatuses', value: status})"
                                        class="mr-1" v-on="on" :color="vueHelper.formatCurrentStatus(status).color"
                                        dark>{{
                                    vueHelper.formatCurrentStatus(status).text }}
                                </v-chip>
                            </template>
                            <span>{{vueHelper.formatCurrentStatus(status).tooltip}}</span>
                        </v-tooltip>
                    </template>

                    <template class="d-flex" v-slot:item.statuses="{item}">
                        <v-tooltip :key="status" v-for="status in item.statuses" v-if="formatStatus(status)" bottom>
                            <template v-slot:activator="{on}">
                                <v-chip @click.stop="addToFilter({field: 'statuses', value: status})" class="mr-1"
                                        v-on="on" :color="formatStatus(status).color" dark>{{
                                    formatStatus(status).text }}
                                </v-chip>
                            </template>
                            <span>{{formatStatus(status).tooltip}}</span>
                        </v-tooltip>
                    </template>

                    <template v-slot:item.action="{item}">
                        <v-btn :href="'/WeeklyReports/GetGeneratedWeeklyReport?' + vueHelper.createQueryStringDefault(item.id, selectedYearWeek)"
                               target="_blank" @click.stop="" small color="primary">
                            <v-icon dark>remove_red_eye</v-icon>
                        </v-btn>
                        <v-btn outlined class="ml-2 mr-2"
                               :to="'/WeeklyReports/AddEditWeeklyReport?' + vueHelper.createQueryStringDefault(item.id, selectedYearWeek)"
                               @click.stop="" small color="primary">
                            <v-icon dark>edit</v-icon>
                        </v-btn>
                        <v-btn small :to="'/Projects/AddEditProject?id='+ item.id">
                            <v-icon>edit</v-icon>
                        </v-btn>
                    </template>

                    <template v-slot:expanded-item="{item, headers}">
                        <td :colspan="headers.length">
                            <ProjectsPhrProjectTable @project-table-select="handleProjectTableSelect"
                                                     :selected-items="selectedItems" :project-id="item.id"/>
                        </td>
                    </template>
                </v-data-table>
            </div>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {HELPER_ENDPOINT, PROJECTS_ENDPOINT} from "@/helpers/EndPoint";
    import axios from "axios";
    import $ from "jquery";
    import _ from "lodash";
    import {handleAxiosError, notify, notifyWithOption} from "@/helpers/HandleResponse";
    import VueHelper, {defaultReportViewSettings} from "@/helpers/VueHelper";
    import moment from "moment";
    import ProjectsPhrProjectTable from "@/components/Administration/ProjectsPhrProjectTable.vue";

    const defaultAdminValue = {
        yearWeek: 0,
        projects: [
            {
                "id": 0,
                "name": "",
                "code": "",
                "division": "",
                "keyAccountManager": "",
                "kam": "",
                "deliveryResponsibleName": "",
                "pic": "",
                "currentStatuses": [],
                "currentstatuses": [],
                "statuses": [],
            }
        ]
    };

    @Component({
        components: {
            ProjectsPhrProjectTable
        }
    })
    export default class ProjectsPhr extends Vue {
        search = "";
        flag = "";
        expanded: any[] = [];
        filters = [{field: null, value: null}];
        selectedYearWeek = 0;
        yearWeeks = [
            {text: "", value: 0}
        ];
        selectedItems: any[] = [];
        payload = defaultAdminValue;
        items = defaultAdminValue.projects;
        headers = [
            {
                text: 'Division',
                align: 'left',
                value: 'division',
            },
            {
                text: 'Code',
                align: 'left',
                value: 'code',
            },
            {
                text: 'Project Name',
                align: 'left',
                value: 'name',
                width: "13%"
            },
            {
                text: 'Key Account Manager',
                align: 'left',
                value: 'kam',
            },
            {
                text: 'Delivery Responsible Name',
                align: 'left',
                value: 'pic',
            },
            {
                text: 'Statuses',
                align: 'left',
                value: 'currentstatuses',
            },
            {
                text: 'Missed',
                value: 'statuses',
            },
            {
                text: 'Action',
                align: 'left',
                value: 'action',
            }
        ];
        dialog = false;
        viewSettings = {
            numberOfWeek: 4,
            numberOfWeekNotShowClosedItem: 2
        };
        previousWeeks: number[] = [];
        notShowClosedItemWeeks: number[] = [];
        vueHelper = VueHelper;

        get workingDays() {
            let workingDays = this.vueHelper.getWorkingDays(this.selectedYearWeek);
            return `(${workingDays.startDay.format("DD. MMM")} - ${workingDays.endDay.format("DD. MMM")})`
        };

        get numberOfPdf() {
            let items = this.selectedItems.map(i => {
                return {projectId: i.projectId, yearWeek: i.yearWeek}
            });
            if (items.length > 0) {
                return ` ${_.uniqWith(items, _.isEqual).length} `;
            }
            return ` `;
        };

        async mounted() {
            //get last yearWeek
            this.previousWeeks = this.vueHelper.range(1, 50);
            this.notShowClosedItemWeeks = this.vueHelper.range(1, 10);
            let time = moment().subtract(1, 'weeks');
            this.selectedYearWeek = time.isoWeekYear() * 100 + time.isoWeek();
            this.filters = this.filters.filter(i => i.field !== null);
            axios.get(HELPER_ENDPOINT + "allowed-yearweeks/2020")
                .then(res => {
                    this.yearWeeks = res.data.map((yw: any) => this.vueHelper.formatYearWeek(yw));
                })
                .catch(handleAxiosError);

            await this.init();
        }

        async init() {
            this.filters = [];
            try {
                let response = await axios.get(PROJECTS_ENDPOINT + "phr/projects-with-weekly-status/year-week/" + this.selectedYearWeek);
                this.payload = response.data;
                _.forEach(this.payload.projects, p => {
                    p.kam = p.keyAccountManager;
                    p.pic = p.deliveryResponsibleName;
                    p.currentstatuses = p.currentStatuses;
                });
                this.items = this.payload.projects;
                this.expanded = [];
            } catch (e) {
                handleAxiosError(e);
            }

        };

        handleRowExpand(item: any, state: any) {
            if (state.isExpanded) {
                this.expanded = _.filter(this.expanded, i => i !== item)
                this.selectedItems = _.filter(this.selectedItems, i => i.isMain || i.projectId !== item.id)
            } else {
                this.expanded.push(item);
            }
        };

        // @ts-ignore
        handleItemExpand({item, value}) {
            if (!value) {
                this.selectedItems = _.filter(this.selectedItems, i => i.isMain || i.projectId !== item.id)
            }
        };

        async handleExportPdfs() {
            this.dialog = false;
            if (this.selectedItems.length === 0) return;

            let items = this.selectedItems.map(i => {
                return {projectId: i.projectId, yearWeek: i.yearWeek}
            });
            items = _.uniqWith(items, _.isEqual);

            let exportRequest = {
                viewSettings: this.viewSettings,
                reports: items
            };

            notifyWithOption("PDFs are being generated. Do not close this tab!", {autoHide: false, className: 'info'});

            axios.post(`/WeeklyReports/ExportMassPdfs`, exportRequest)
                .then(res => {
                    let url = `/WeeklyReports/GetPdfsZip?zipFile=${btoa(res.data)}`;
                    $('.notifyjs-wrapper').trigger('notify-hide');
                    window.location.href = url;
                })
                .catch(error => {
                    $('.notifyjs-wrapper').trigger('notify-hide');
                    handleAxiosError(error);
                });
        };

        addToFilter(item: any) {
            if (!_.some(this.filters, {field: item.field, value: item.value})) {
                this.filters = [...this.filters, item];
                this.filter();
            }
        };

        removeFromFilter(item: any) {
            this.filters = _.filter(this.filters, i => !((i.field === item.field) && (i.value === item.value)));
            this.filter();
        };

        filter() {
            this.items = this.payload.projects;
            for (let item of this.filters) {
                if (item.field === 'division') {
                    this.items = this.items.filter(i => i.division === item.value)
                } else if (item.field === 'kam') {
                    this.items = this.items.filter(i => i.keyAccountManager === item.value)
                } else if (item.field === 'pic') {
                    this.items = this.items.filter(i => i.deliveryResponsibleName === item.value)
                } else if (item.field === 'currentStatuses') {
                    this.items = this.items.filter(i => _.includes(i.currentStatuses, item.value))
                } else if (item.field === 'statuses') {
                    this.items = this.items.filter(i => _.includes(i.statuses, item.value))
                }
            }
        };

        // @ts-ignore
        handleSelect({item, value}) {
            let items = [item];
            this.handleSelectAll({items, value});
        };

        // @ts-ignore
        handleSelectAll({items, value}) {
            let selectedItems = items.map((i: any) => {
                return {projectId: i.id, yearWeek: this.selectedYearWeek, isMain: true}
            });

            if (value) {
                this.selectedItems = _.unionWith(this.selectedItems, selectedItems, _.isEqual)
            } else {
                this.selectedItems = _.differenceWith(this.selectedItems, selectedItems, _.isEqual)
            }
            console.log(this.selectedItems);
        };

        // @ts-ignore
        handleProjectTableSelect({items, value}) {
            if (value) {
                this.selectedItems = _.unionWith(this.selectedItems, items, _.isEqual)
            } else {
                this.selectedItems = _.differenceWith(this.selectedItems, items, _.isEqual)
            }
            console.log(this.selectedItems);
        };

        formatStatus(status: any) {
            if (status.includes("missed_times")) {
                let text = `missed (${status.split(':')[1]})`;
                return {
                    color: "red",
                    text: status.split(':')[1],
                    tooltip: status
                }
            } else return undefined;
        };

        async handleCurrentReportWeekChange(value: number) {
            this.selectedYearWeek = value;

            await this.init();
        };

        getAddEditReportLink(item: any) {
            let viewSettings = defaultReportViewSettings;
            viewSettings.projectId = item.id;
            viewSettings.year = this.vueHelper.calculateYear(this.selectedYearWeek);
            viewSettings.week = this.vueHelper.calculateWeek(this.selectedYearWeek);

            return "/WeeklyReports/AddEditWeeklyReport?" + this.vueHelper.createQueryString(viewSettings);
        }

        dummyAction() {

        }
    }

</script>