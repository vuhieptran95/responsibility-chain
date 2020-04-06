<template>
    <v-row>
        <v-col>
            <p class="subtitle-1 mb-0 font-weight-bold">Backlog items</p>
            <table id="tableBacklogItems" class="table table-bordered">
                <thead>
                <tr>
                    <th width="9%" class="text-center" rowspan="2">Week</th>
                    <th class="text-center" colspan="3">New Items Added this week</th>
                    <th class="text-center" colspan="2">Items Done this week</th>
                    <th width="17%" class="text-center" colspan="3">Remaining in Backlog</th>
                </tr>
                <tr>
                    <th class="font-weight-normal">Sprint</th>
                    <th class="font-weight-normal">Items <span style="color:red">*</span></th>
                    <th class="font-weight-normal">Story Points</th>
                    <th class="font-weight-normal">Items <span style="color:red">*</span></th>
                    <th class="font-weight-normal">Story Points</th>
                    <th class="font-weight-normal">Items</th>
                    <th class="font-weight-normal">Story Points</th>
                </tr>
                </thead>
                <tbody>
                <tr :key="i" v-for="(item,i) in report.backlogItemListReadOnly">
                    <td v-if="item.week === 0">Initial</td>
                    <td v-else>{{item.year}} - {{item.week}}</td>
                    <td v-if="item.sprint === 0"/>
                    <td v-else>{{item.sprint}}</td>
                    <td>{{item.itemsAdded}}</td>
                    <td>{{item.storyPointsAdded}}</td>
                    <td>{{item.itemsDone}}</td>
                    <td>{{item.storyPointsDone}}</td>
                    <td class="text-md-center">{{item.itemsRemaining}}</td>
                    <td class="text-md-center">{{item.storyPointsRemaining}}</td>
                </tr>
                <tr>
                    <td>
                        <DisplayText :text="`${report.selectedYear} - ${report.selectedWeek}`"/>
                    </td>
                    <td>
                        <v-text-field type="number" v-model.number="report.backlogItem.sprint"
                                      outlined :rules="[vueHelper.mustGreaterThanZero]"/>
                    </td>
                    <td>
                        <v-text-field min="0" type="number"
                                      v-model.number="report.backlogItem.itemsAdded" outlined
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero, itemsRemainingGreaterThanZero]"/>
                    </td>
                    <td>
                        <v-text-field min="0" type="number"
                                      v-model.number="report.backlogItem.storyPointsAdded"
                                      outlined
                                      :rules="[vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero, storyPointsRemainingGreaterThanZero]"/>
                    </td>
                    <td>
                        <v-text-field min="0" type="number"
                                      v-model.number="report.backlogItem.itemsDone" outlined
                                      :rules="[vueHelper.mustRequired, vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero, itemsRemainingGreaterThanZero]"/>
                    </td>
                    <td>
                        <v-text-field min="0" type="number"
                                      v-model.number="report.backlogItem.storyPointsDone"
                                      outlined
                                      :rules="[vueHelper.mustBeNumber, vueHelper.mustGreaterThanZero, storyPointsRemainingGreaterThanZero]"/>
                    </td>
                    <td class="text-md-center">
                        <DisplayText :text="backlogItemRemaining.items"/>
                    </td>
                    <td class="text-md-center">
                        <DisplayText :text="backlogItemRemaining.storyPoints"/>
                    </td>
                </tr>

                <tr>
                    <td>Average</td>
                    <td/>
                    <td>{{backlogItemAverage.averageItemAdded}}</td>
                    <td>{{backlogItemAverage.averageStoryPointsAdded}}</td>
                    <td>{{backlogItemAverage.averageItemDone}}</td>
                    <td>{{backlogItemAverage.averageStoryPointsDone}}</td>
                    <td/>
                    <td/>
                </tr>
                </tbody>
            </table>
            <v-label>* Initial values are not included in Average calculation.</v-label>
        </v-col>
    </v-row>
</template>

<script lang="ts">
    import {Vue, Component, Prop, Watch} from 'vue-property-decorator'
    import VueHelper from "@/helpers/VueHelper";
    import {BacklogItem, Report} from "@/components/PhrWeeklyReports/WeeklyReport";
    import DisplayText from "@/components/PhrWeeklyReports/DisplayText.vue";
    import _ from "lodash";
    @Component({
        components: {DisplayText}
    })
    export default class WeeklyReportBacklog extends Vue {
        @Prop() report!: Report;
        
        vueHelper = VueHelper;
        
        @Watch('report.backlogItem',{deep: true, immediate: true})
        onBacklogItemChange(val: BacklogItem, oldVal: BacklogItem){
            this.$emit("backlog-item-change", val);
        }

        storyPointsRemainingGreaterThanZero(v: any) {
            if (this.backlogItemRemaining.storyPoints < 0) return "Story points remaining must be greater than 0";
            return true
        }

        itemsRemainingGreaterThanZero(v: any) {
            if (this.backlogItemRemaining.items < 0) return "Items remaining must be greater than 0";
            return true
        }

        get backlogItemRemaining() {
            let items = 0;
            let storyPoints = 0;

            let lastItem = this.report.backlogItemListReadOnly[this.report.backlogItemListReadOnly.length - 1];

            if (lastItem) {
                let latest = {items: lastItem.itemsRemaining, storyPoints: lastItem.storyPointsRemaining};
                items = latest.items + this.report.backlogItem.itemsAdded - this.report.backlogItem.itemsDone;

                if (this.report.backlogItem.storyPointsAdded !== null && this.report.backlogItem.storyPointsDone !== null)
                    storyPoints = latest.storyPoints + this.report.backlogItem.storyPointsAdded - this.report.backlogItem.storyPointsDone;
            }

            return {items, storyPoints};
        };

        get backlogItemAverage() {
            let calculatedItems = this.report.backlogItemListReadOnly.filter(i => i.week !== 0);
            let numberOfRows = calculatedItems.length;

            let totalItemsAdded = _.sumBy(calculatedItems, i => i.itemsAdded);
            let totalStoryPointsAdded = _.sumBy(calculatedItems, i => {
                if (i.storyPointsAdded) return i.storyPointsAdded;
                return 0;
            });
            let totalItemsDone = _.sumBy(calculatedItems, i => i.itemsDone);
            let totalStoryPointsDone = _.sumBy(calculatedItems, i => {
                if (i.storyPointsDone) return i.storyPointsDone;
                return 0;
            });

            if (numberOfRows > 0) {
                let averageItemAdded = (totalItemsAdded + this.report.backlogItem.itemsAdded) / (numberOfRows + 1);
                let averageStoryPointsAdded = 0;
                let averageItemDone = (totalItemsDone + this.report.backlogItem.itemsDone) / (numberOfRows + 1);
                let averageStoryPointsDone = 0;

                if (this.report.backlogItem.storyPointsAdded !== null)
                    averageStoryPointsAdded = (totalStoryPointsAdded + this.report.backlogItem.storyPointsAdded) / (numberOfRows + 1);

                if (this.report.backlogItem.storyPointsDone !== null)
                    averageStoryPointsDone = (totalStoryPointsDone + this.report.backlogItem.storyPointsDone) / (numberOfRows + 1);

                return {
                    averageItemAdded: _.floor(averageItemAdded, 1),
                    averageStoryPointsDone: _.floor(averageStoryPointsDone, 1),
                    averageItemDone: _.floor(averageItemDone, 1),
                    averageStoryPointsAdded: _.floor(averageStoryPointsAdded, 1)
                }
            }

            return {
                averageItemAdded: null,
                averageStoryPointsDone: null,
                averageItemDone: null,
                averageStoryPointsAdded: null
            }
        };
        
        
    }

</script>