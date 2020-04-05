<template>
    <v-content>
        <v-container class="container--fluid">
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
                        <v-col sm="10" class="text-sm-right">
                            <v-btn class="mt-3" to="/Administration/projects/add-edit" color="primary">Add New
                                Project
                            </v-btn>
                        </v-col>
                    </v-row>
                    <v-row class="d-flex mb-3 ml-3">
                        <v-chip class="mr-2" v-for="(item,i) in filters" :key="i" close
                                @click:close="removeFromFilter(item)">
                            {{item.value}}
                        </v-chip>
                    </v-row>
                </template>

                <template v-slot:item.division="{item}">
                    <v-btn text @click.stop="addToFilter({field: 'division', value: item.division})" small>
                        {{item.division}}
                    </v-btn>
                </template>

                <template v-slot:item.name="{item}">
                    <v-list-item>
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

                <template class="d-flex" v-slot:item.statuses="{item}">
                    <v-tooltip :key="i" v-for="(status,i) in formattedStatuses(item.statuses)" bottom>
                        <template v-slot:activator="{on}">
                            <v-chip @click.stop="addToFilter({field: 'statuses', value: status.text})" class="mr-1"
                                    v-on="on" :color="status.color" dark>
                                {{ status.text }}
                            </v-chip>
                        </template>
                        <span>{{status.tooltip}}</span>
                    </v-tooltip>
                </template>

                <template v-slot:item.action="{item}">
                    <v-btn class="v-btn--outlined primary--text" :to="'/Administration/projects/add-edit?id=' + item.id"><v-icon left>edit</v-icon> Edit</v-btn>
                </template>

            </v-data-table>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component} from 'vue-property-decorator';
    import VueHelper from "@/helpers/VueHelper";
    import Axios from "axios";
    import _ from "lodash";
    import {handleAxiosError} from "@/helpers/HandleResponse";
    import {
        defaultFormattedStatus,
        defaultPayloadValue,
        FilteredItem,
        FormattedStatus,
        Payload,
        Project
    } from "@/components/Administration/Projects";
    import {PROJECTS_ENDPOINT} from "@/helpers/EndPoint";

    @Component
    export default class Projects extends Vue {
        drawer = null;
        search = "";
        filters: FilteredItem[] = [{field: null, value: null}];
        headers = [
            {
                text: "Division",
                value: "division",
                width: "7%"
            },
            {
                text: "Code",
                value: "code",
                width: "7%"
            },
            {
                text: "Project Name",
                value: "name",
                width: "19%"
            },
            {
                text: "Key Account Manager",
                value: "kam",
                width: "10%"
            },
            {
                text: "Delivery Responsible Name",
                value: "pic",
                width: "10%"
            },
            {
                text: "Statuses",
                value: "statuses",
            },
            {
                text: "Action",
                value: "action",
            }
        ];
        payload: Payload = defaultPayloadValue;
        items: Project[] = defaultPayloadValue.projects;
        
        formattedStatuses(statuses: string[]): FormattedStatus[]{
            const formattedStatuses = statuses.map(s => this.formatStatus(s));
            return formattedStatuses.filter(s => s.text !== defaultFormattedStatus.text);
        }

        async mounted() {
            this.filters = this.filters.filter(i => i.field !== null);
            await this.init()
        }

        async init() {
            try {
                const res = await Axios.get(PROJECTS_ENDPOINT);
                this.payload = res.data
            } catch (e) {
                handleAxiosError(e);
            }

            this.items = this.payload.projects;
            this.items = this.items.map(i => {
                i.kam = i.keyAccountManager;
                i.pic = i.deliveryResponsibleName;
                i.statuses = [];
                if (i.dodRequired) i.statuses = ["dod", ...i.statuses];
                if (i.phrRequired) i.statuses = ["phr", ...i.statuses];
                i.statuses = [i.projectStateType, ...i.statuses];
                if (i.dmrRequired) i.statuses = ["dmr", ...i.statuses];
                return i;
            })
        }

        addToFilter(item: FilteredItem) {
            if (!_.some(this.filters, {field: item.field, value: item.value})) {
                this.filters = [...this.filters, item];
                this.filter();
            }
        }

        removeFromFilter(item: any) {
            this.filters = _.filter(this.filters, i => !((i.field === item.field) && (i.value === item.value)));
            this.filter();
        }

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
        }

        formatStatus(status: string): FormattedStatus {
            if (status.includes("phr")) {
                const text = `phr`;
                return {
                    color: "lime",
                    text: text,
                    tooltip: "PHR Required"
                }
            } else if (status.includes("active")) {
                return {
                    color: "green",
                    text: "active",
                    tooltip: "Active"
                }
            } else if (status.includes("preparing")) {
                return {
                    color: "yellow",
                    text: "preparing",
                    tooltip: "Preparing"
                }
            } else if (status.includes("closed")) {
                return {
                    color: "red",
                    text: "closed",
                    tooltip: "Closed"
                }
            }
            else if (status.includes("dmr")) {
                return {
                    color: "indigo",
                    text: "dmr",
                    tooltip: "DMR Required"
                }
            }
            else if (status.includes("dod")) {
                return {
                    color: "teal",
                    text: "dod",
                    tooltip: "DoD Required"
                }
            } else return defaultFormattedStatus;
        }
    }

</script>