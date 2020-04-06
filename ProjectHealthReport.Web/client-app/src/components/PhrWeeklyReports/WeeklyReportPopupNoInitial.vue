<template>
    <v-row justify="center">
        <v-dialog v-model="noInitialModalShowWatch" persistent max-width="450">
            <v-card>
                <v-card-title class="headline">You cannot add weekly report yet!</v-card-title>
                <v-card-text>You need to specify <strong>Initial Items</strong> and/or <strong>Story
                    Points</strong> first.
                </v-card-text>
                <v-card-actions>
                    <v-spacer/>
                    <v-btn color="primary darken-2" outlined
                           :to="getProjectDetails(report.projectId)" text
                           >Edit this project
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {Report} from "@/components/PhrWeeklyReports/WeeklyReport";

    @Component
    export default class WeeklyReportPopupNoInitial extends Vue {
        @Prop() report!: Report;
        @Prop() noInitialModalShow!: boolean;
        
        get noInitialModalShowWatch(){
            return this.report.backlogItemListReadOnly.length === 0;
        }

        getProjectDetails(projectId: number) {
            return `/Projects/AddEditProject?id=${projectId}`;
        };
        
        // get noInitialModalShowWatcher(){
        //    
        // }
    }

</script>