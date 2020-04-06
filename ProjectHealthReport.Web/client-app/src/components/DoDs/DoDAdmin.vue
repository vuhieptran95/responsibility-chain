﻿<template>
    <v-content>
        <v-container fluid>
            <v-row>
                <v-col md="2">
                    <v-row>
                        <v-col><v-btn @click="handleSaveMetrics" color="primary">Save metrics</v-btn></v-col>
                        <v-col><v-btn @click="selectedMetric = defaultMetric" color="primary">Add metric</v-btn></v-col>
                        <v-col><v-btn v-if="selectedGroup" @click="isToolManaged = true" color="secondary">Manage Tools</v-btn></v-col>
                    </v-row>
                    <v-card>
                        <h1 class="title ml-2">Tools</h1>
                        <v-list>
                            <v-list-item-group>
                        <draggable :list="metricGroups" @end="handleToolDragEnd">
                            <v-list-item v-for="(group,i) in metricGroups" :key="i" @click="selectedGroup = group">
                                <v-list-item-content>
                                    <v-list-item-title>Order: {{group.toolOrder}} - {{group.tool}}</v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </draggable>
                            </v-list-item-group>
                        </v-list>
                    </v-card>
                    <v-card class="mt-3" v-if="selectedGroup !== null">
                        <h1 class="title ml-2">{{selectedGroup.tool}} - Order: {{selectedGroup.toolOrder}}</h1>
                        <draggable :list="selectedGroup.metrics" @end="handleMetricDragEnd">
                            <v-list-item v-for="(metric,i) in selectedGroup.metrics" :key="i"
                                         @click="selectedMetric = metric">
                                <v-list-item-content>
                                    <v-list-item-title>Order: {{metric.order}} - {{metric.name}}</v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </draggable>
                    </v-card>
                </v-col>
                <v-col>
                    <v-row>
                        <v-col md="12" v-if="selectedGroup && isToolManaged === true">
                            <v-btn @click="isToolManaged = false">Close <span><v-icon>close</v-icon></span></v-btn>
                            <v-row>
                                <v-col md="2"><v-text-field outlined v-model="selectedGroup.tool" /></v-col>
                                <v-col md="1"><v-btn color="secondary" @click="handleRenameTool">Save Tool Name</v-btn></v-col>
                            </v-row>
                            <v-row>
                                <v-col md="2"><v-text-field outlined v-model="deletedTool" label="Type in tool name to delete"/></v-col>
                                <v-col md="2"><v-btn color="red" class="white--text" @click="handleDeleteTool">Delete Tool</v-btn></v-col>
                                <v-col md="2"><v-text-field outlined v-model="deletedMetric" label="Type in metric name to delete"/></v-col>
                                <v-col md="2"><v-btn color="red" class="white--text" @click="handleDeleteMetric">Delete Metric</v-btn></v-col>

                            </v-row>
                        </v-col>
                        <v-col md="12" v-if="selectedMetric">
                            <v-row>
                                <v-col md="4">
                                    <v-select outlined v-if="!isNewTool" class="font-weight-bold"
                                              :items="metricGroups.map(m => m.tool)" v-model="selectedMetric.tool" label="Tool *"/>
                                    <v-text-field v-if="isNewTool" class="font-weight-bold" v-model="selectedMetric.tool" outlined label="New Tool *" />
                                </v-col>
                                <v-col md="3"><v-checkbox v-if="selectedMetric === defaultMetric" v-model="isNewTool" label="Is New Tool?" /></v-col>
                            </v-row>
                            <v-col md="12">
                                <v-row>
                                    <v-col md="2">
                                        <v-text-field class="font-weight-bold" outlined v-model="selectedMetric.name" label="Metric Name"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-select :items="['Number', 'Select', 'Text']" outlined v-model="selectedMetric.valueType"
                                                  label="Value Type"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-text-field outlined v-model="selectedMetric.unit" label="Unit"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-text-field outlined v-model="selectedMetric.selectValues"
                                                      :disabled="selectedMetric.valueType === 'Number' || selectedMetric.valueType === 'Text'" label="Select Values"/>
                                    </v-col>
                                </v-row>
                            </v-col>
                            <v-col md="2">
                                <v-btn @click="handleAddThreshold">Add Threshold</v-btn>
                            </v-col>
                            <v-col md="12">
                                <v-row class="mt-0 mb-0" v-for="(threshold, i) in selectedMetric.thresholds" :key="i">
                                    <v-col md="2">
                                        <v-select :rules="[vueHelper.mustRequired]" :items="status" hide-details outlined v-model="threshold.metricStatusId"
                                                  label="Status"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-text-field :disabled="!threshold.isRange" hide-details outlined min="0" type="number"
                                                      v-model.number="threshold.lowerBound" label="Lower Bound"/>
                                    </v-col>
                                    <v-col md="1">
                                        <v-select :disabled="!threshold.isRange" hide-details :items="operators" outlined
                                                  v-model="threshold.lowerBoundOperator"
                                                  label="Lower Bound Operator"/>
                                    </v-col>
                                    <v-col md="1"><p class="display-2 text-center">X</p></v-col>
                                    <v-col md="1">
                                        <v-select :disabled="!threshold.isRange" hide-details :items="operators" outlined
                                                  v-model="threshold.upperBoundOperator"
                                                  label="Upper Bound Operator"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-text-field :disabled="!threshold.isRange" hide-details outlined min="0" type="number"
                                                      v-model.number="threshold.upperBound" label="Upper Bound"/>
                                    </v-col>
                                    <v-col md="2">
                                        <v-text-field hide-details outlined :disabled="threshold.isRange"
                                                      v-model="threshold.value" label="Value"/>
                                        <v-checkbox v-model="threshold.isRange" label="Is Range?" />
                                    </v-col>

                                    <v-col md="1">
                                        <v-btn @click="handleRemoveThreshold(threshold)"><v-icon>remove_circle</v-icon></v-btn>
                                    </v-col>
                                </v-row>
                            </v-col>
                            <v-col>
                                <v-btn v-if="selectedMetric === defaultMetric" color="primary darken-2" @click="handleAddMetric" outlined text>Add Metric</v-btn>
                            </v-col>
                        </v-col>
                    </v-row>
                </v-col>

            </v-row>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {defaultUser, User} from "@/commons/User";
    import {DODS_ENDPOINT, HELPER_ENDPOINT} from "@/helpers/EndPoint";
    import axios from 'axios';
    import _ from "lodash";
    import {handleAxiosError, notify} from "@/helpers/HandleResponse";
    import {AdministrationAcessRoles} from "@/helpers/AuthorizationHelper";
    import {defaultMetric, defaultThreshold, Metric, MetricsGroup, Threshold} from "@/components/DoDs/DoDAdmin";
    import VueHelper from "@/helpers/VueHelper";
    import draggable from "vuedraggable";

    interface Tool {
        tool: string;
        toolOrder: number;
    }

    @Component({
        components: {
            draggable
        }
    })
    export default class DoDAdmin extends Vue {
        metricGroups: MetricsGroup[] = [];
        operators: string[] = [">", "<", ">=", "<="];
        selectedGroup: MetricsGroup | null = null;
        selectedMetric: Metric | null = null;
        defaultMetric = defaultMetric;
        tools: Tool[] = [];
        isNewTool = false;
        isToolManaged = false;
        deletedTool: string | null  = null;
        deletedMetric: string | null = null;        
        // metric: Metric = defaultMetric;
        status = [
            {
                text: "GREEN",
                value: 1
            },
            {
                text: "YELLOW",
                value: 2
            },
            {
                text: "RED",
                value: 3
            }
        ];
        vueHelper = VueHelper;

        async mounted() {
            await this.init();
        }


        async init() {
            try {
                let res = await axios.get(DODS_ENDPOINT + "metrics")
                this.metricGroups = res.data.metricGroups;
                this.tools = this.metricGroups.map(g => ({tool: g.tool, toolOrder: g.toolOrder}));
                this.metricGroups.forEach(g => {
                    g.metrics = _.orderBy(g.metrics, 'order');
                })
                this.handleMetricDragEnd();
            } catch (e) {
                handleAxiosError(e)
            }

        }

        formatPayload() {
            this.metricGroups.forEach(g => {
                g.metrics.forEach(m => {
                    m.thresholds.forEach(t => {
                        // @ts-ignore
                        t.upperBound = parseFloat(t.upperBound);
                        // @ts-ignore
                        t.lowerBound = parseFloat(t.lowerBound);
                        t.metricId = m.id;
                    })
                })
            })
        }

        handleToolDragEnd() {
            this.metricGroups.forEach((g, i) => {
                g.toolOrder = i + 1;
                g.metrics.forEach(m => {
                    m.toolOrder = i + 1;
                })
            })
        }

        handleMetricDragEnd() {
            this.metricGroups.forEach((g, k) => {
                g.metrics.forEach((m, i) => {
                    m.order = i + 1;
                })
            })
        }


        handleSaveMetrics() {
            this.formatPayload();

            axios.put(DODS_ENDPOINT + "metrics", {metricGroups: this.metricGroups}).then(async res => {
                notify("Save metrics successfully", "success");
                await this.init();
                
                this.metricGroups.forEach(g => {
                    for (let i = 0; i< g.metrics.length; i++){
                        if (g.metrics[i].id === this.selectedMetric?.id){
                            g.metrics[i] = this.selectedMetric;
                            return;
                        }
                    }
                })
                
            }).catch(handleAxiosError)
        }

        handleAddMetric() {
            if (this.selectedMetric) this.selectedMetric.order = 10000;
            axios.post(DODS_ENDPOINT + "metrics", {metric: this.selectedMetric}).then(res => {
                notify("Add metrics successfully", "success");
                this.init();
                this.selectedMetric = null;
            }).catch(handleAxiosError);
        }

        handleAddThreshold() {
            let threshold = _.cloneDeep(defaultThreshold);
            this.selectedMetric?.thresholds.push(threshold);
        }

        handleRemoveThreshold(threshold: Threshold) {
            if (this.selectedMetric)
                this.selectedMetric.thresholds = this.selectedMetric?.thresholds.filter(t => t !== threshold);
        }

        handleRenameTool(){
            this.metricGroups.map(g => {
                g.metrics.map(m => m.tool = g.tool);
            })
            this.handleSaveMetrics();
            this.isToolManaged = false;
        }

        handleDeleteTool(){
            if (!this.deletedTool){
                notify("Please type in the tool name to delete", "error");
                return;
            }
            
            if(!this.metricGroups.find(g => g.tool === this.deletedTool)){
                notify("Invalid tool", "error");
                return;
            }
            
            axios.delete(DODS_ENDPOINT + "metrics/tool/" + this.deletedTool).then(res => {
                notify("Tool " + this.deletedTool + " is deleted successfully", "success");
                this.deletedTool = null;
                this.init();
            })
        }
        
        handleDeleteMetric(){
            if (!this.deletedTool || !this.deletedMetric){
                notify("Need to type in both Tool and Metric to delete metric", "error");
                return;
            }
            
            let metrics: Metric[] = [];
            this.metricGroups.forEach(g => {
                metrics.push(...g.metrics)
            });
            
            let metric =metrics.find(m => m.name === this.deletedMetric && m.tool === this.deletedTool); 
            if (!metric){
                notify("Metric does not exist", "error");
                return;
            }

            axios.delete(DODS_ENDPOINT + "metrics/" + metric.id).then(res => {
                notify("Metric " + this.deletedTool + " - " + this.deletedMetric + " is deleted successfully", "success");
                this.deletedTool = null;
                this.deletedMetric = null;
                this.init();
            })
        }

    }

</script>