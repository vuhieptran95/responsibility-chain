<template>
    <v-container class="d-flex justify-content-center">
        <div style="width: 80%">
            <v-data-table
                    :items-per-page="20"
                    :headers="headers"
                    :search="search"
                    :items="items"
                    @item-selected="handleSelect"
                    @toggle-select-all="handleSelectAll"
                    multi-sort
                    show-select
                    item-key="yearWeek"
                    :footer-props="{'items-per-page-options': [20, 50, 100, -1]}"
            >
                <template v-slot:top>
                    <v-row class="d-flex">
                        <v-col sm="3">
                            <v-text-field v-model="search" label="Search" placeholder=" " append-icon="search" right/>
                        </v-col>
                    </v-row>
                </template>

                <template v-slot:item.yearweek="{item}" >
                    <td>{{vueHelper.calculateWeek(item.yearWeek)}} - {{vueHelper.calculateYear(item.yearWeek)}}</td>
                </template>

                <template v-slot:item.statuses="{item}">
                    <v-tooltip :key="status" v-for="status in item.statuses" v-if="vueHelper.formatCurrentStatus(status)" bottom>
                        <template v-slot:activator="{on}">
                            <v-chip class="mr-1" v-on="on" :color="vueHelper.formatCurrentStatus(status).color" dark>{{
                                vueHelper.formatCurrentStatus(status).text }}</v-chip>
                        </template>
                        <span>{{vueHelper.formatCurrentStatus(status).tooltip}}</span>
                    </v-tooltip>
                </template>

                <template v-slot:item.action="{item}">
                    <v-btn :href="'/WeeklyReports/GetGeneratedWeeklyReport?' + vueHelper.createQueryStringDefault(projectId, item.yearWeek)" target="_blank" @@click.stop small color="primary"><v-icon dark>remove_red_eye</v-icon></v-btn>
                    <v-btn class="ml-2" outlined :href="'/WeeklyReports/AddEditWeeklyReport?' + vueHelper.createQueryStringDefault(projectId, item.yearWeek)" target="_blank" @@click.stop small color="primary"><v-icon dark>edit</v-icon></v-btn>
                </template>

            </v-data-table>
        </div>
    </v-container>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {PROJECTS_ENDPOINT, WEEKLYREPORT_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError} from "@/helpers/HandleResponse";
    import axios from "axios"
    import VueHelper from "@/helpers/VueHelper";

    const defaultProjectValue = {
        "yearWeekStatuses": [
            {
                "yearWeek": 0,
                "statuses": [
                    ""
                ]
            }
        ]
    };

    @Component
    export default class ProjectsPhrProjectTable extends Vue {
        @Prop()
        projectId!: number;
        vueHelper = VueHelper;
        payload = defaultProjectValue;
        search = '';
        items = defaultProjectValue.yearWeekStatuses;
        headers = [
            {
                text: 'Report week',
                align: 'left',
                value: 'yearweek'
            },
            {
                text: 'Status',
                align: 'left',
                value: 'statuses'
            },
            {
                text: 'Action',
                align: 'left',
                value: 'action'
            }
        ];

        async mounted() {
            await this.init();
        };

        async init() {
            try {
                let response = await axios.get(PROJECTS_ENDPOINT + "phr/project-with-yearweeks-statuses/" + this.projectId);
                this.payload = response.data;
                this.items = this.payload.yearWeekStatuses;
            }
            catch (e) {
                handleAxiosError(e);
            }

        };
        // @ts-ignore
        handleSelect({item, value}){
            let items = [item];
            this.handleSelectAll({items, value});
        };
        // @ts-ignore
        handleSelectAll({items, value}){
            let selectedItems = items.map((i: any) => {
                return {projectId : this.projectId, yearWeek : i.yearWeek}
            });

            this.$emit('project-table-select', {items: selectedItems, value});
        };
    }

</script>
