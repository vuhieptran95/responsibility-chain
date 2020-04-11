<template>
    <div id="divisionReport">
        <h1 style="margin-bottom: 2rem;" class="text-center">Delivery Managers Weekly Report for
            {{payload.divisionName}} Division</h1>
        <v-app>
            <v-content>
                <v-container>
                    <v-btn
                            v-scroll="onScroll"
                            v-show="fab"
                            fab
                            dark
                            fixed
                            bottom
                            right
                            color="primary"
                            @@click="toTop"
                    >
                        <v-icon>keyboard_arrow_up</v-icon>
                    </v-btn>
                    <v-form ref="form" v-model="valid">
                        <v-data-table
                                :items-per-page="20"
                                :headers="headers"
                                :items="items"
                                :search="search"
                                :footer-props="{'items-per-page-options': [20, 50, 100, -1]}"
                                class="">
                            <template v-slot:top>
                                <v-row class="d-flex">
                                    <v-col sm="2">
                                        <v-text-field v-model="search" label="Search" placeholder=" "
                                                      append-icon="search"
                                                      right></v-text-field>
                                    </v-col>
                                    <v-col sm="2">
                                        <v-select
                                                :value="selectedYearWeek"
                                                :items="yearWeeks"
                                                :menu-props="{ top: true, offsetY: true }"
                                                @@change="handleCurrentReportWeekChange"
                                                label="Current Report Week"
                                        ></v-select>
                                    </v-col>
                                    <v-col sm="2">
                                        <p style="padding-top:1.2rem">{{workingDays}}</p>
                                    </v-col>
                                    <v-col class="text-right mt-2" sm="6">
                                        <v-btn @@click="handleSaveReport" color="primary" class="mr-2">Save Report
                                        </v-btn>

                                        <v-dialog
                                                v-model="dialogCopy"
                                                width="400"
                                        >
                                            <template v-slot:activator="{ on }">
                                                <v-btn v-on="on" class="mr-2">Copy from last weeks</v-btn>
                                            </template>

                                            <v-card>
                                                <v-card-title
                                                        class="headline grey lighten-2"
                                                        primary-title
                                                >
                                                    Copy Settings
                                                </v-card-title>

                                                <v-card-text>
                                                    <v-row>
                                                        <v-col sm="8">
                                                            <v-select
                                                                    v-model="selectedCopyYearWeek"
                                                                    :value="selectedCopyYearWeek"
                                                                    :items="yearWeeks"
                                                                    :menu-props="{ top: true, offsetY: true }"
                                                                    label="Select week to copy"
                                                            ></v-select>
                                                        </v-col>
                                                    </v-row>
                                                </v-card-text>

                                                <v-divider></v-divider>

                                                <v-card-actions>
                                                    <v-spacer></v-spacer>
                                                    <v-btn
                                                            color="primary"
                                                            text
                                                            @@click="handleCopyFromLastWeek"
                                                    >
                                                        Copy
                                                    </v-btn>
                                                </v-card-actions>
                                            </v-card>
                                        </v-dialog>
                                    </v-col>
                                </v-row>
                            </template>

                            <template v-slot:item.statuscolor="{item}">
                                <v-select v-model="item.statuscolor"
                                          :value="item.statuscolor"
                                          :items="statuses"
                                          required :rules="[v => !!v || 'Status is required']"
                                >

                                </v-select>
                            </template>

                            <template v-slot:item.projectstatus="{item}">
                                <ckeditor :config="editorConfig" v-model="item.projectstatus"></ckeditor>
                            </template>

                            <template v-slot:item.actions="{item}">
                                <ckeditor :config="editorConfig" v-model="item.actions"></ckeditor>
                            </template>

                        </v-data-table>
                    </v-form>
                </v-container>
            </v-content>
        </v-app>
    </div>
</template>

<script lang="ts">
    import {Vue, Component} from 'vue-property-decorator';
    import VueHelper from "@/helpers/VueHelper";
    import axios from "axios";
    import _ from "lodash";
    import {HELPER_ENDPOINT, WEEKLYREPORT_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError, notify} from "@/helpers/HandleResponse";

    const defaultDivisionReportValue = {
        "divisionName": "",
        "yearWeek": 0,
        "divisionProjectStatuses": [
            {
                projectId: 0,
                projectName: "",
                code: "",
                statusColor: null,
                statuscolor: null,
                projectStatus: null,
                projectstatus: null,
                actions: null,
                yearWeek: 0,
                statusId: 0
            }
        ],
        "divisionConcern": null,
        "divisionAvailableResources": null,
        "divisionFutureResources": null,
        "divisionSoonAvailableResources": null,
        "divisionUpdatedResources": null
    };

    @Component
    export default class DivisionReport extends Vue {
        headers = [
            {
                text: "Project Name",
                value: "projectName",
                width: "16%"
            },
            {
                text: "Code",
                value: "code",
                width: "7%"
            },
            {
                text: "Status",
                value: "statuscolor",
                width: "11%"
            },
            {
                text: "Description",
                value: "projectstatus",
                width: "33%",
            },
            {
                text: "Action",
                value: "actions",
                width: "33%",
            }
        ];
        editorConfig = {
            toolbar: [['Bold', 'Italic', 'Underline', 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Link']],
            height: 130,
            defaultLanguage: 'en',
            language: 'en'
        };
        yearWeeks = [];
        search = "";
        selectedYearWeek = 0;
        selectedPhrYearWeek = 0;
        selectedCopyYearWeek = 0;
        dialogPhr = false;
        dialogCopy = false;
        fab = false;
        valid = false;
        statuses = ["GREEN", "YELLOW", "RED"];
        payload = defaultDivisionReportValue;
        items = defaultDivisionReportValue.divisionProjectStatuses;
        vueHelper = VueHelper;

        async mounted() {
            this.selectedYearWeek = this.vueHelper.getCurrentYearWeek();
            this.selectedPhrYearWeek = this.vueHelper.getLastYearWeek(this.selectedYearWeek);
            this.selectedCopyYearWeek = this.vueHelper.getLastYearWeek(this.selectedYearWeek);
            await this.init();
        };

        get workingDays() {
            let workingDays = this.vueHelper.getWorkingDays(this.selectedYearWeek);
            return `(${workingDays.startDay.format("DD. MMM")} - ${workingDays.endDay.format("DD. MMM")})`
        };

        async init() {
            axios.get(`${HELPER_ENDPOINT}allowed-yearweeks/2020`).then(res => {
                this.yearWeeks = res.data.allowedYearWeeks.map((i: any) => {
                    i = this.vueHelper.formatYearWeek(i);
                    return i;
                });
            })
            .catch(handleAxiosError);

            let queryString = window.location.href.split('?')[1];
            let divisionName = queryString.split('=')[1];
            try {
                let res = await axios.get(`${WEEKLYREPORT_ENDPOINT}dmr/${divisionName}/${this.selectedYearWeek}`);
                this.payload = res.data;
                this.items = this.payload.divisionProjectStatuses;
                this.items = this.items.map((i: any) => {
                    i.statuscolor = i.statusColor ? i.statusColor : "";
                    i.projectstatus = i.projectStatus ? i.projectStatus : "";
                    return i;
                })
            } catch (e) {
                handleAxiosError(e);
            }
        };

        async handleSaveReport() {
            // @ts-ignore
            this.$refs.form.validate();

            let divisionProjectStatuses = this.items.map(i => {
                i.statusColor = i.statuscolor;
                i.projectStatus = i.projectstatus;
                return i;
            });

            await axios.post(WEEKLYREPORT_ENDPOINT + "dmr", {
                divisionName: this.payload.divisionName,
                divisionProjectStatuses: divisionProjectStatuses
            }).then(async res => {
                notify("Weekly report is saved successfully", "success");
                await this.init();
            })
                .catch(handleAxiosError)
        };

        async handleCurrentReportWeekChange(value: number) {
            this.selectedYearWeek = value;

            await this.init();
        };


        async handleCopyFromLastWeek() {
            let queryString = window.location.href.split('?')[1];
            let divisionName = queryString.split('=')[1];
            try {
                let res = await axios.get(`${WEEKLYREPORT_ENDPOINT}division/${divisionName}/year-week/${this.selectedCopyYearWeek}`);
                let statuses = res.data.divisionProjectStatuses;
                for (let i = 0; i < this.items.length; i++) {
                    let status = statuses.filter((s: any) => s.code === this.items[i].code)[0];
                    console.log(status);
                    if (status) {
                        this.items[i].statuscolor = status.statusColor;
                        this.items[i].projectstatus = status.projectStatus;
                        this.items[i].actions = status.actions;
                    }
                }
            } catch (e) {
                handleAxiosError(e)
            }
            this.dialogCopy = false;
        };

        onScroll(e: any) {
            if (typeof window === 'undefined') return
            const top = window.pageYOffset || e.target.scrollTop || 0
            this.fab = top > 20
        };

        toTop() {
            //@ts-ignore
            this.$vuetify.goTo(0)
        }
    }
</script>
