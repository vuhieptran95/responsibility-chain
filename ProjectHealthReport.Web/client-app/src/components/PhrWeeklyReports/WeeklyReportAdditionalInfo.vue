<template>
    <v-row>
        <v-col>
            <v-row>
                <v-col md="3" class="pb-0"><p class="subtitle-1 mb-0 font-weight-bold">
                    Risk/Issues/Impediment</p></v-col>

            </v-row>
            <v-row>
                <v-col class="pb-0">
                    <v-row>
                        <v-col md="2">
                            <v-btn v-on:click="addNewIssue()" color="success">
                                Add new row
                            </v-btn>
                        </v-col>
                        <v-col md="3" class="pt-0">
                            <v-select hide-details
                                      :label="'Do not display closed items older than ' + report.numberOfWeekNotShowClosedItem + ' weeks'"
                                      @change="handleViewSettingsChanged"
                                      v-model="report.numberOfWeekNotShowClosedItem"
                                      :items="report.numberOfWeekNotShowClosedItems"/>
                        </v-col>
                    </v-row>
                    <table style="table-layout: fixed" id="tableAdditionalInfo"
                           class="table table-bordered">
                        <thead>
                        <tr>
                            <th width="9%">Week</th>
                            <th>Item</th>
                            <th>Impact</th>
                            <th>Action</th>
                            <th width="12%">Status</th>
                            <th width="7%"/>
                        </tr>
                        </thead>
                        <tbody>
                        <tr :key="i" v-for="(item, i) in report.additionalInfoListReadOnly">
                            <td>{{vueHelper.displayYearWeek(item.openedYearWeek)}}</td>
                            <td>{{item.item}}</td>
                            <td>{{item.impact}}</td>
                            <td>{{item.action}}</td>
                            <td>{{item.status}}</td>
                            <td/>
                        </tr>
                        <tr :key="i" v-for="(item, i) in report.additionalInfos"
                            v-if="item.openedYearWeek === currentYearWeek">
                            <td>
                                <DisplayText
                                        :text="vueHelper.displayYearWeek(item.openedYearWeek)"/>
                            </td>
                            <td>
                                <v-textarea hide-details auto-grow outlined rows="3"
                                            v-model="item.item"/>
                            </td>
                            <td>
                                <v-textarea hide-details auto-grow outlined rows="3"
                                            v-model="item.impact"/>
                            </td>
                            <td>
                                <v-textarea hide-details auto-grow outlined rows="3"
                                            v-model="item.action"/>
                            </td>
                            <td>
                                <v-select hide-details v-model="item.status" outlined
                                          :items="report.additionalInfoStatues"/>
                            </td>
                            <td class="text-center">
                                <v-btn small v-on:click="removeIssue(item)"
                                       class="white--text mt-2" color="red">
                                    <v-icon>delete</v-icon>
                                </v-btn>
                            </td>
                        </tr>
                        <tr v-else>
                            <td>
                                <DisplayText
                                        :text="vueHelper.displayYearWeek(item.openedYearWeek)"/>
                            </td>
                            <td>
                                <DisplayText :text="item.item"/>
                            </td>
                            <td>
                                <DisplayText :text="item.impact"/>
                            </td>
                            <td>
                                <DisplayText :text="item.action"/>
                            </td>
                            <td>
                                <v-select hide-details outlined
                                          :items="report.additionalInfoStatues"
                                          v-model="item.status"/>
                            </td>
                            <td/>
                        </tr>
                        </tbody>
                    </table>
                </v-col>
            </v-row>
        </v-col>
    </v-row>
</template>

<script lang="ts">
    import {Vue, Component, Prop, Watch} from 'vue-property-decorator'
    import {AdditionalInfo, Issue, Report} from "@/components/PhrWeeklyReports/WeeklyReport";
    import VueHelper from "@/helpers/VueHelper";
    import DisplayText from "@/components/PhrWeeklyReports/DisplayText.vue";

    @Component({
        components: {DisplayText}
    })
    export default class WeeklyReportAdditionalInfo extends Vue {
        @Prop() report!: Report;
        @Prop() additionalInfosReport!: AdditionalInfo[];
        @Prop() issueRemovedIdsObject!: any;

        vueHelper = VueHelper;

        get currentYearWeek() {
            return this.report.selectedYear * 100 + this.report.selectedWeek;
        }
        
        @Watch('report.additionalInfos', {deep: true, immediate: true})
        onAdditionalInfosChange(val: AdditionalInfo[], oldVal: AdditionalInfo[]) {
            this.$emit("additional-infos-change", val, this.issueRemovedIdsObject.issueRemovedIds);
        }
        
        addNewIssue() {
            let newIssue: AdditionalInfo = {
                id: 0,
                issueId: 0,
                item: "",
                impact: "",
                action: "",
                openedYearWeek: this.currentYearWeek,
                status: "Open",
                week: this.report.selectedWeek,
                year: this.report.selectedYear,
                yearWeek: 0
            };
            this.report.additionalInfos = [...this.report.additionalInfos, newIssue];
        }

        removeIssue(issue: AdditionalInfo) {
            if (issue.id !== 0) {
                this.issueRemovedIdsObject.issueRemovedIds = [...this.issueRemovedIdsObject.issueRemovedIds, {id: issue.id, issueId: issue.issueId}];
            }
            this.report.additionalInfos = this.report.additionalInfos.filter(i => i !== issue);
            this.$emit("additional-infos-change", this.report.additionalInfos, this.issueRemovedIdsObject.issueRemovedIds);
        }

        handleViewSettingsChanged() {
            this.$emit("view-settings-change", this.report.numberOfWeekNotShowClosedItem)
        }
    }

</script>