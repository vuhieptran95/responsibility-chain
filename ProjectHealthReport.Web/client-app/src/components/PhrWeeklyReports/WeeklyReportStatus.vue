<template>
    <div>
        <v-row>
            <v-col md="3">
                <v-select :rules="vueHelper.requiredRules" outlined label="Status *"
                          v-model="report.status.statusColor" :items="report.statuses"/>
            </v-col>
        </v-row>

        <v-row style="margin-top: -2rem">
            <v-col md="10">
                <p class="subtitle-1 mb-0 font-weight-bold">Project Status</p>
                <ckeditor name="Project_Status" :config="editorConfig"
                          v-model="report.status.projectStatus" rows="3"/>
            </v-col>
        </v-row>

        <v-row>
            <v-col md="3">
                <v-menu v-model="menuMilestone" :close-on-content-click="false" :nudge-right="40"
                        transition="scale-transition" offset-y min-width="290px">
                    <template v-slot:activator="{ on }">
                        <v-text-field clearable v-model="report.status.milestoneDate"
                                      label="Milestone Date" outlined append-icon="event"
                                      v-on="on"/>
                    </template>
                    <v-date-picker v-model="report.status.milestoneDate"
                                   @input="menuMilestone = false"/>
                </v-menu>
            </v-col>
            <v-col class="mt-2" md="2">
                <v-btn @click="handleCopyMilestoneFromLastWeek" outlined color="primary">Copy from
                    last week
                </v-btn>
            </v-col>
        </v-row>

        <v-row style="margin-top: -2rem">
            <v-col md="10">
                <p class="subtitle-1 mb-0 font-weight-bold">Milestone</p>
                <ckeditor name="Milestone" :config="editorConfig" v-model="report.status.milestone"
                          rows="3"/>
            </v-col>
        </v-row>

        <v-row>
            <v-col md="10">
                <p class="subtitle-1 mb-0 font-weight-bold">Retrospective Feedback</p>
                <ckeditor name="Retrospective_Feedback" :config="editorConfig"
                          v-model="report.status.retrospectiveFeedBack" rows="3"/>
            </v-col>
        </v-row>
    </div>
</template>

<script lang="ts">
    import {Vue, Component, Prop, Watch} from 'vue-property-decorator'
    import {BacklogItem, Report, Status} from "@/components/PhrWeeklyReports/WeeklyReport";
    import VueHelper from "@/helpers/VueHelper";
    import {WEEKLYREPORT_ENDPOINT} from "@/helpers/EndPoint";
    import axios from "axios";
    import moment from "moment";
    import {handleAxiosError} from "@/helpers/HandleResponse";

    @Component
    export default class WeeklyReportStatus extends Vue {
        @Prop() report!: Report;

        @Watch('report.status',{deep: true, immediate: true})
        onStatusChange(val: Status, oldVal: Status){
            this.$emit("status-change", val);
        }

        menuMilestone = false;
        
        editorConfig = {
            languague: 'en'
        };

        vueHelper = VueHelper;

        async handleCopyMilestoneFromLastWeek() {
            let selectedYear = this.report.selectedYear;
            let selectedWeek = this.report.selectedWeek;
            let selectedYearWeek = this.vueHelper.calculateYearWeek(selectedYear, selectedWeek);
            let lastYearWeek = this.vueHelper.getLastYearWeek(selectedYearWeek);
            let lastYear = this.vueHelper.calculateYear(lastYearWeek);
            let lastWeek = this.vueHelper.calculateWeek(lastYearWeek);

            let queryString = `projectId=${this.report.projectId}&year=${lastYear}&week=${lastWeek}&numberOfWeek=4&numberOfWeekNotShowClosedItem=2`;
            let url = `${WEEKLYREPORT_ENDPOINT}?${queryString}`;
            try {
                let reportResponse = await axios.get(url);
                this.report.status.milestone = reportResponse.data.status?.milestone;
                this.report.status.milestoneDate = reportResponse.data.status?.milestoneDate;
                if (this.report.status.milestoneDate)
                    this.report.status.milestoneDate = moment(this.report.status.milestoneDate).format("YYYY-MM-DD");
            } catch (e) {
                handleAxiosError(e)
            }
        }
    }

</script>