﻿<template>
    <v-content>
        <v-container>
            <h1>{{title}}</h1>
            <v-form ref="form" v-model="valid">
                    <v-row>
                        <v-col xl="7" lg="9">
                            <v-row>
                                <v-col md="6">
                                    <v-text-field v-model="project.name" label="Project name" readonly filled required :rules="requiredRule"/>
                                </v-col>
                                <v-col md-3>
                                    <v-text-field v-model="project.code" label="Project code" readonly filled required :rules="requiredRule"/>
                                </v-col>
                                <v-col md="4">
                                    <v-text-field v-model="project.projectState" label="Project state" readonly filled required :rules="requiredRule"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="4">
                                    <v-text-field v-model="project.division" label="Division" readonly filled required :rules="requiredRule"/>
                                </v-col>
                                <v-col md="4">
                                    <v-text-field v-model="project.keyAccountManager" label="Key Account Manager"
                                                  readonly filled required :rules="requiredRule"/>
                                </v-col>
                                <v-col md="4">
                                    <v-text-field v-model="project.deliveryResponsibleName"
                                                  readonly filled
                                                    :rules="requiredRule"
                                                    label="Delivery Responsible Name *"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="6">
                                    <v-text-field type="number" v-model.number="project.backlogItem.itemsAdded"
                                                  label="Initial items *" required :rules="requiredRule"/>
                                </v-col>
                                <v-col md="6">
                                    <v-text-field type="number" clearable v-model.number="project.backlogItem.storyPointsAdded"
                                                  label="Initial story points"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="12">
                                    <v-autocomplete :items="nitecans" v-model="usersAsPIC" multiple clearable at
                                                    label="Add users with PIC privilege">
                                        <template v-slot:selection="{ item }">
                                            <v-chip close @click:close="handleRemovePic(item)">
                                                <span>{{ item }}</span>
                                            </v-chip>
                                        </template>
                                    </v-autocomplete>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="12">
                                    <v-text-field v-model="project.platformVersion" label="Platform/Version"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="12">
                                    <v-text-field clearable v-model="project.jiraLink" label="Jira link"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md="12">
                                    <v-text-field clearable v-model="project.sourceCodeLink" label="Source code link"/>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col md-6>
                                    <v-text-field
                                            v-model="project.projectStartDate"
                                            label="Project start date *"
                                            prepend-icon="event"
                                            readonly filled
                                            :rules="requiredRule"
                                            required
                                    />
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
                                <v-col md="12">
                                    <p class="v-label theme--light">Project note</p>
                                    <ckeditor v-model="project.note" :config="editorConfig"></ckeditor>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col class="text-right">
                                    <v-btn @click="handleSaveProject" color="primary">Save Project</v-btn>
                                </v-col>
                            </v-row>
                        </v-col>
                    </v-row>
            </v-form>
        </v-container>
    </v-content>
</template>

<script lang="ts">

    import {Component, Vue} from "vue-property-decorator";
    import {defaultProject, Project, ProjectAccess} from "@/components/PhrProjects/AddEditProjects";
    import VueHelper from "@/helpers/VueHelper";
    import {User} from "@/commons/User";
    import {HELPER_ENDPOINT, PROJECTS_ENDPOINT} from "@/helpers/EndPoint";
    import axios from "axios";
    import {handleAxiosError, notify} from "@/helpers/HandleResponse";
    import moment from "moment";
    import _ from "lodash";
    import {RolePic} from "@/helpers/AuthorizationHelper";
    import router from "@/router";
    import {NavigationGuard, Route} from "vue-router";
    import {RouteHelper} from "@/helpers/RouteHelper";
    
    @Component
    export default class AddEditProjects extends Vue {
        title = "Edit Project";
        valid = false;
        editorConfig = {
            defaultLanguage: 'en',
            language: 'en'
        };
        menu2 = false;
        usersAsPIC: string[] = [];
        requiredRule = [
            (v: any) => {
                if (v === 0) return true;
                return !!v || "Value is required";
            }
        ];
        project: Project = defaultProject;
        divisions = [];
        nitecans: User[] = [];
        vueHelper = VueHelper;

        mounted() {
            axios.get(HELPER_ENDPOINT + "user-emails").then(res => {
                this.nitecans = res.data;
            }).catch(handleAxiosError);
            
            this.init();
        }
        
        init(){
            let queryString = window.location.href.split('?')[1];
            if (queryString){
                let projectId = parseInt(queryString.split('=')[1]);
                axios.get(PROJECTS_ENDPOINT + projectId).then(res => {
                    this.project = res.data;
                    this.project.projectStartDate = moment(this.project.projectStartDate).format("YYYY-MM-DD");
                    this.project.backlogItem = this.project.backlogItem ?? defaultProject.backlogItem;
                    this.project.projectEndDate = this.project.projectEndDate === null ? null : moment(this.project.projectEndDate).format("YYYY-MM-DD");
                
                    this.usersAsPIC = this.project.projectAccesses.map(i => i.email);
                }).catch(handleAxiosError)
            }
        }
        
        handleRemovePic(name: string){
            this.usersAsPIC = this.usersAsPIC.filter(u => u !== name);
        }
        
        handleSaveProject(){
            // @ts-ignore
            if (!this.$refs.form.validate()) return;

            let payload = _.cloneDeep(this.project);
            payload.projectEndDate = payload.projectEndDate ? moment(payload.projectEndDate, "YYYY-MM-DD").format("YYYY-MM-DD") : null;
            payload.projectAccesses = payload.projectAccesses.filter(i => i.role !== RolePic);
            
            let projectAccessPics = this.usersAsPIC.map(i => {
                let pa: ProjectAccess = {role: RolePic, email: i, id: 0, projectId: this.project.id};
                return pa;
            });
            
            payload.projectAccesses.push(...projectAccessPics);
            
            axios.put(PROJECTS_ENDPOINT + "non-master-data", payload).then(res => {
                notify(`Project ${this.project.name} is updated`, "success");
                this.init();
            }).catch(handleAxiosError)
        }
    }

</script>