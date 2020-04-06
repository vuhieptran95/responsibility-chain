<template>
    <v-row>
        <v-col>
            <p class="subtitle-1 mb-0 font-weight-bold">Quality Report</p>
            <table id="tableQualityReport" class="table table-bordered">
                <thead>
                <tr>
                    <th width="9%" class="text-center" rowspan="2">Week</th>
                    <th class="text-center" colspan="4">New bugs</th>
                    <th class="text-center" rowspan="2">Done bugs <span
                            style="color:red">*</span></th>
                    <th class="text-center" rowspan="2">Re-open bugs <span
                            style="color:red">*</span></th>
                    <th class="text-center" rowspan="2" width="14%">Remaining bugs <span
                            style="color:red">*</span></th>
                </tr>
                <tr>
                    <th class="font-weight-normal text-center">Total</th>
                    <th class="font-weight-normal text-center">Critical <span style="color:red">*</span>
                    </th>
                    <th class="font-weight-normal text-center">Major <span
                            style="color:red">*</span></th>
                    <th class="font-weight-normal text-center">Minor <span
                            style="color:red">*</span></th>
                </tr>
                </thead>
                <tbody>
                <tr :key="i" v-for="(item, i) in report.qualityReportListReadOnly">
                    <td>{{item.year}} - {{item.week}}</td>
                    <td>{{item.newBugs}}</td>
                    <td>{{item.criticalBugs}}</td>
                    <td>{{item.majorBugs}}</td>
                    <td>{{item.minorBugs}}</td>
                    <td>{{item.doneBugs}}</td>
                    <td>{{item.reOpenBugs}}</td>
                    <td class="text-md-center">{{item.remainingBugs}}</td>
                </tr>
                <tr>
                    <td>
                        <DisplayText :text="`${report.selectedYear} - ${report.selectedWeek}`"/>
                    </td>
                    <td>
                        <DisplayText :text="qualityReportRemaining.totalBugs"/>
                    </td>
                    <td>
                        <v-text-field outlined type="number" min="0"
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero]"
                                      v-model.number="report.qualityReport.criticalBugs"/>
                    </td>
                    <td>
                        <v-text-field outlined type="number" min="0"
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero]"
                                      v-model.number="report.qualityReport.majorBugs"/>
                    </td>
                    <td>
                        <v-text-field outlined type="number" min="0"
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero]"
                                      v-model.number="report.qualityReport.minorBugs"/>
                    </td>
                    <td>
                        <v-text-field outlined type="number" min="0"
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero]"
                                      v-model.number="report.qualityReport.doneBugs"/>
                    </td>
                    <td>
                        <v-text-field outlined type="number" min="0"
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero, reOpenBugsMustBeSmallerThanRemainingBugs]"
                                      v-model.number="report.qualityReport.reOpenBugs"/>
                    </td>
                    <td class="text-md-center">
                        <DisplayText :text="qualityReportRemaining.remainingBugs"/>
                    </td>
                </tr>
                </tbody>
            </table>
        </v-col>
    </v-row>
</template>

<script lang="ts">
    import {Vue, Component, Prop, Watch} from 'vue-property-decorator'
    import {QualityReport, Report, Status} from "@/components/PhrWeeklyReports/WeeklyReport";
    import VueHelper from "@/helpers/VueHelper";
    import DisplayText from "@/components/PhrWeeklyReports/DisplayText.vue";
    @Component({
        components: {DisplayText}
    })
    export default class WeeklyReportQualityReport extends Vue {
        @Prop() report!: Report;
        
        @Watch('report.qualityReport',{deep: true, immediate: true})
        onQualityReportChange(val: QualityReport, oldVal: QualityReport){
            this.$emit("quality-report-change", val);
        }
        
        vueHelper = VueHelper;

        get qualityReportRemaining() {
            let latestRemaining = 0;
            if (this.report.qualityReportListReadOnly.length > 0) {
                latestRemaining = this.report.qualityReportListReadOnly[this.report.qualityReportListReadOnly.length - 1].remainingBugs;
            }

            let totalBugs = this.report.qualityReport.criticalBugs + this.report.qualityReport.majorBugs + this.report.qualityReport.minorBugs;
            let remainingBugs = latestRemaining + totalBugs - this.report.qualityReport.doneBugs;

            this.report.qualityReport.remainingBugs = remainingBugs;

            return {
                totalBugs,
                remainingBugs
            };
        };

        reOpenBugsMustBeSmallerThanRemainingBugs(v: any) {
            if (this.report.qualityReport?.reOpenBugs && this.qualityReportRemaining.remainingBugs < this.report.qualityReport?.reOpenBugs)
                return "Re-open bugs must be smaller than Remaining bugs";
            return true
        }
    }

</script>