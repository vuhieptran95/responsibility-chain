<template>
    <v-content>
        <v-container class="mt-10">
            <v-data-table
                    :headers="headers"
                    :items="items"
                    :hide-default-footer="true"
                    class="elevation-1">
                <template v-slot:item.action="{item}">
                    <v-btn @@click.stop :href="getAddReportLink(item.name)" color="primary">Add
                        Report
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
    import {DIVISION_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError} from "@/helpers/HandleResponse";

    const defaultDivisionValue = {
        "divisions": [
            {
                "name": "",
                "managerName": ""
            }
        ]
    };

    @Component
    export default class DivisionIndex extends Vue {
        headers = [
            {
                text: "Division",
                value: "name"
            },
            {
                text: "Manager",
                value: "managerName"
            },
            {
                text: "Action",
                value: "action"
            }
        ];
        payload = defaultDivisionValue;
        items = defaultDivisionValue.divisions;

        mounted() {
            let res = axios.get(DIVISION_ENDPOINT + "index").then(res => {
                this.payload = res.data;
                this.items = this.payload.divisions;
            }).catch(handleAxiosError);
        };

        getAddReportLink(divisionName: string) {
            if (divisionName === "AMS 24/7") divisionName = "AMS247";
            return `/divisions/report?divisionName=${divisionName}`;
        }
    }
</script>
