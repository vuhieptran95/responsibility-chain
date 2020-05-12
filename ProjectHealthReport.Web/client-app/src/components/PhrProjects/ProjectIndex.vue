<template>
    <v-content>
        <v-container>
            <v-data-table
                    :items-per-page="20"
                    :headers="headers"
                    :search="search"
                    :items="items"
                    multi-sort
                    item-key="code"
                    :footer-props="{'items-per-page-options': [20, 50, 100, -1]}">
                <template v-slot:top>
                    <v-row class="d-flex">
                        <v-col sm="2">
                            <v-text-field v-model="search" label="Search" placeholder=" " append-icon="search"
                                          right>
                            </v-text-field>
                        </v-col>
                    </v-row>
                </template>

                <template v-slot:item.division="{item}">
                    <v-list-item text>
                        {{item.division}}
                    </v-list-item>
                </template>

                <template v-slot:item.name="{item}">
                    <v-btn small :to="'/phr/projects/add-edit?id=' + item.id">
                        {{item.name}}
                    </v-btn>
                </template>

                <template v-slot:item.kam="{item}">
                    <v-list-item>
                        {{item.kam}}
                    </v-list-item>
                </template>

                <template v-slot:item.pic="{item}">
                    <v-list-item>
                        {{item.pic}}
                    </v-list-item>
                </template>

                <template v-slot:item.action="{item}">
                    <v-btn class="v-btn--outlined mr-2 primary--text"
                           :to="'/phr/weekly-reports/add-edit?' + vueHelper.createQueryStringDefault(item.id, selectedYearWeek)">
                        <v-icon left>edit</v-icon>
                        Add/Edit Report
                    </v-btn>
                    <v-btn class="v-btn--outlined primary--text"
                           :href="'/WeeklyReports/GetGeneratedWeeklyReport?' + vueHelper.createQueryStringDefault(item.id, selectedYearWeek)"
                           target="_blank">
                        <v-icon left>visibility</v-icon>
                        View Report
                    </v-btn>
                </template>

            </v-data-table>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component} from 'vue-property-decorator';
    import VueHelper from "@/helpers/VueHelper";
    import axios from "axios";
    import _ from "lodash";
    import {defaultPayloadValue, Payload, Project} from "@/components/Administration/Projects";
    import {PROJECTS_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError} from "@/helpers/HandleResponse";

    @Component
    export default class ProjectIndex extends Vue {
        search = "";
        headers = [
            {
                text: "Division",
                value: "division",
                width: "14%"
            },
            {
                text: "Project Name",
                value: "name",
                width: "19%"
            },
            {
                text: "Key Account Manager",
                value: "kam",
                width: "15%"
            },
            {
                text: "Delivery Responsible Name",
                value: "pic",
                width: "15%"
            },
            {
                text: "Action",
                value: "action",
            }
        ];
        items: Project[] = defaultPayloadValue.projects;
        vueHelper = VueHelper;
        selectedYearWeek = this.vueHelper.getCurrentYearWeek();

        mounted() {
            axios.get(PROJECTS_ENDPOINT + "phr/project-index").then(res => {
                this.items = res.data.projects;
                this.items.forEach(i => {
                    i.kam = i.keyAccountManager;
                    i.pic = i.deliveryResponsibleName;
                })
            }).catch(handleAxiosError)
        }
    }
</script>
