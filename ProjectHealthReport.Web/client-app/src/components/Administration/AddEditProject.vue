<template>
    <v-content>
        <v-container>
            <h1>{{title}}</h1>
            <v-form ref="form" v-model="valid">
                <v-container>
                    <v-row>
                        <v-col xl="9" lg="12">
                            <v-row>
                                <v-col md="6">
                                    <v-text-field v-model="project.name" label="Project name *"
                                                  :rules="requiredRule"
                                                  required/>
                                </v-col>
                                <v-col md-3>
                                    <v-text-field v-model="project.code" label="Project code *"
                                                  :rules="requiredRule"
                                                  required/>
                                </v-col>
                                <v-col md="4">
                                    <v-select v-model="project.projectStateTypeId" label="Project state *"
                                              :items="projectStates"
                                              :rules="requiredRule"
                                              required/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="4">
                                    <v-select v-model="project.division" label="Division *"
                                              :items="divisions"
                                              :rules="requiredRule"
                                              required/>
                                </v-col>
                                <v-col md="4">
                                    <v-autocomplete v-model="project.keyAccountManager" label="Key Account Manager *"
                                                    :items="userEmails"
                                                    :rules="requiredRule"
                                                    required/>
                                </v-col>
                                <v-col md="4">
                                    <v-autocomplete v-model="project.deliveryResponsibleName"
                                                    :items="userEmails"
                                                    clearable
                                                    label="Delivery Responsible Name"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md-6>
                                    <v-menu
                                            v-model="menu1"
                                            :close-on-content-click="false"
                                            :nudge-right="40"
                                            transition="scale-transition"
                                            offset-y
                                            min-width="290px"
                                    >
                                        <template v-slot:activator="{ on }">
                                            <v-text-field
                                                    v-model="project.projectStartDate"
                                                    label="Project start date *"
                                                    prepend-icon="event"
                                                    readonly
                                                    :rules="requiredRule"
                                                    required
                                                    v-on="on"
                                            />
                                        </template>
                                        <v-date-picker v-model="project.projectStartDate"
                                                       @input="menu1 = false"/>
                                    </v-menu>
                                </v-col>
                                <v-col md-6>
                                    <v-menu
                                            v-model="menu2"
                                            :close-on-content-click="false"
                                            :nudge-right="40"
                                            transition="scale-transition"
                                            offset-y
                                            min-width="290px"
                                    >
                                        <template v-slot:activator="{ on }">
                                            <v-text-field
                                                    clearable
                                                    v-model="project.projectEndDate"
                                                    label="Project end date"
                                                    prepend-icon="event"
                                                    readonly
                                                    v-on="on"
                                            />
                                        </template>
                                        <v-date-picker v-model="project.projectEndDate"
                                                       @input="menu2 = false"/>
                                    </v-menu>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="4" class="mt-4">
                                    <v-checkbox v-model="project.dmrRequired" :label="'DMR Required ' + '('+ dmrRequiredNote + ')'"/>
                                </v-col>
                                <v-col md="4" class="mt-4">
                                    <v-menu
                                            v-model="menu3"
                                            :close-on-content-click="false"
                                            :nudge-right="40"
                                            transition="scale-transition"
                                            offset-y
                                            min-width="290px"
                                    >
                                        <template v-slot:activator="{ on }">
                                            <v-text-field
                                                    :disabled="!project.dmrRequired"
                                                    v-model="project.dmrRequiredFrom"
                                                    label="Require DMR from *"
                                                    prepend-icon="event"
                                                    readonly
                                                    v-on="on"
                                            />
                                        </template>
                                        <v-date-picker v-model="project.dmrRequiredFrom"
                                                       @input="menu3 = false"/>
                                    </v-menu>
                                </v-col>
                                <v-col md="4" class="mt-4">
                                    <v-menu
                                            v-model="menu4"
                                            :close-on-content-click="false"
                                            :nudge-right="40"
                                            transition="scale-transition"
                                            offset-y
                                            min-width="290px"
                                    >
                                        <template v-slot:activator="{ on }">
                                            <v-text-field
                                                    :disabled="!project.dmrRequired"
                                                    clearable
                                                    v-model="project.dmrRequiredTo"
                                                    label="Require DMR to"
                                                    prepend-icon="event"
                                                    readonly
                                                    v-on="on"
                                            />
                                        </template>
                                        <v-date-picker v-model="project.dmrRequiredTo"
                                                       @input="menu4 = false"/>
                                    </v-menu>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="3" class="mt-4">
                                    <v-checkbox @click="handlePhrRequiredClick" v-model="project.phrRequired" label="PHR Required"/>
                                </v-col>
                                <v-col md="3" class="mt-4">
                                    <v-checkbox :disabled="!project.phrRequired" v-model="project.dodRequired" label="DoD Required"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col class="text-right">
                                    <v-btn @click="handleSaveProject" color="primary">Save Project</v-btn>
                                </v-col>
                            </v-row>
                        </v-col>
                    </v-row>
                </v-container>
            </v-form>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {DIVISION_ENDPOINT, HELPER_ENDPOINT, PROJECTS_ENDPOINT} from "@/helpers/EndPoint";
    import _ from "lodash";
    import axios from "axios"
    import moment from "moment";
    import {handleAxiosError, notify} from "@/helpers/HandleResponse";
    import {defaultProjectValue} from "@/components/Administration/AddEditProject";
    import router from "@/router";
    import VueHelper from "@/helpers/VueHelper";
    
    @Component
    export default class AddEditProject extends Vue {
        title = "Add/Edit Project";
        valid = false;
        editorConfig = null;
        menu1 = false;
        menu2 = false;
        menu3 = false;
        menu4 = false;
        projectStates = [];
        requiredRule = [
            (v: any) => !!v || "Value is required",
        ];
        project = defaultProjectValue;
        divisions = [];
        userEmails = [];
        vueHelper = VueHelper;
        
        get dmrRequiredNote(): string{
            let dmrFromNumber = this.vueHelper.getYearWeek(this.project.dmrRequiredFrom);
            let dmrToNumber = this.vueHelper.getYearWeek(this.project.dmrRequiredTo);
            
            if (dmrToNumber < dmrFromNumber) return `Invalid date`;
            
            let dmrFrom = this.vueHelper.getYearWeekDisplay(this.project.dmrRequiredFrom);
            if (this.project.dmrRequiredTo){
                let dmrTo = this.vueHelper.getYearWeekDisplay(this.project.dmrRequiredTo);
                return `From: ${dmrFrom} To: ${dmrTo}`
            }
            return `From ${dmrFrom}`
        }
        
        mounted() {
            axios.get(HELPER_ENDPOINT + "user-emails").then(res => {
                this.userEmails = res.data;
            }).catch(handleAxiosError);
            axios.get(HELPER_ENDPOINT + "divisions").then(res => {
                this.divisions = res.data;
            }).catch(handleAxiosError);
            axios.get(HELPER_ENDPOINT + "project-states").then(res => {
                this.projectStates = res.data.stateTypes.map((i: any)=> {
                    return {value: i.id, text: i.state}
                })
            }).catch(handleAxiosError);

            this.init();
        };

        init() {
            let queryString = window.location.href.split('?')[1];
            if (queryString) {
                let projectId = parseInt(queryString.split('=')[1]);
                if (projectId < 1) {
                    this.title = "Add Project";
                    return;
                }
                this.title = "Edit Project";
                axios.get(PROJECTS_ENDPOINT + projectId).then(res => {
                    this.project = res.data;
                    this.project.dmrRequiredFrom = moment(this.project.dmrRequiredFrom).format("YYYY-MM-DD");
                    this.project.dmrRequiredTo = this.project.dmrRequiredTo === null ? null : moment(this.project.dmrRequiredTo).format("YYYY-MM-DD");
                    this.project.projectStartDate = moment(this.project.projectStartDate).format("YYYY-MM-DD");
                    this.project.projectEndDate = this.project.projectEndDate === null ? null : moment(this.project.projectEndDate).format("YYYY-MM-DD");
                }).catch(handleAxiosError)
            }
            else{
                this.title = "Add Project";
            }
        };
        handleSaveProject() {
            // @ts-ignore
            if (!this.$refs.form.validate()) return;

            let dmrFromNumber = this.vueHelper.getYearWeek(this.project.dmrRequiredFrom);
            let dmrToNumber = this.vueHelper.getYearWeek(this.project.dmrRequiredTo);

            if (this.project.dmrRequired && dmrToNumber < dmrFromNumber){
                notify(`Invalid DMR Required date`, "error");
                return;  
            } 

            let payload = _.cloneDeep(this.project);

            payload.projectStartDate = payload.projectStartDate ? moment(payload.projectStartDate, "YYYY-MM-DD").format("YYYY-MM-DD") : "";
            payload.projectEndDate = payload.projectEndDate ? moment(payload.projectEndDate, "YYYY-MM-DD").format("YYYY-MM-DD") : undefined;

            payload.dmrRequiredFrom = payload.dmrRequiredFrom ? moment(payload.dmrRequiredFrom, "YYYY-MM-DD").format("YYYY-MM-DD") : "";
            payload.dmrRequiredTo = payload.dmrRequiredTo ? moment(payload.dmrRequiredTo, "YYYY-MM-DD").format("YYYY-MM-DD") : undefined;
            
            if (this.project.id > 0){
                axios.put(PROJECTS_ENDPOINT + "master-data", payload).then(res => {
                    notify(`Project ${this.project.name} is updated`, "success");
                }).catch(handleAxiosError)
            }
            else{
                axios.post(PROJECTS_ENDPOINT, payload).then(res => {
                    notify(`Project ${this.project.name} is created`, "success");
                }).catch(handleAxiosError)
            }
        }

        handlePhrRequiredClick(){
            if (!this.project.phrRequired) this.project.dodRequired = false;
        }
    }

</script>