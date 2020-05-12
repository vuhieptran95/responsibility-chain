<template>
    <v-content>
        <v-container class="mt-10">
            <v-data-table
                    :headers="headers"
                    :items="items"
                    :hide-default-footer="true"
                    class="elevation-1">
                <template v-slot:item.policies="{item}">
                    <v-dialog :key="i" v-for="(p, i) in item.policies" width="700">
                        <template v-slot:activator="{ on }">
                            <v-chip @click="handlePolicyClick(p.id)" v-on="on" class="mr-2 mb-1 mt-1" >{{p.name}}</v-chip>
                        </template>
                        <v-card>
                            <v-card-title
                                    class="headline grey lighten-2"
                                    primary-title
                            >
                                {{p.name}}
                            </v-card-title>
                            <v-chip :key="i" v-for="(s, i) in scopes" class="mr-2 mb-1 mt-1" >{{s}}</v-chip>
                            <v-card-text>
                                
                            </v-card-text>

                            <v-divider></v-divider>
                        </v-card>
                    </v-dialog>
                </template>
                <template v-slot:item.scopes="{item}">
                    <v-chip class="mr-2 mb-1 mt-1" :key="i" v-for="(s, i) in item.scopes">{{s}}</v-chip>
                </template>
            </v-data-table>
        </v-container>
    </v-content>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator';
    import axios from "axios";
    import {ADMINISTRATION_ENDPOINT} from "@/helpers/EndPoint";
    import {User} from "@/components/Administration/Administration";
    
    @Component
    export default class Administration extends Vue {
        headers = [
            {
                text: "Roles",
                value: "role",
                width: "10%",
            },
            {
                text: "Users",
                value: "username",
                width: "5%",
            },
            {
                text: "Policies",
                value: "policies",
                width: "20%",
            },
            {
                text: "Scopes",
                value: "scopes",
                width: "65%",
            }
        ];
        
        items: User[] = [];
        
        scopes: string[] = [];
        
        mounted(){
            axios.get(ADMINISTRATION_ENDPOINT + "users").then(res => {
                this.items = res.data.users;
            })
        }

        handlePolicyClick(id: string){
            axios.get(ADMINISTRATION_ENDPOINT + `policies/${id}/scopes`).then(res => {
                this.scopes = res.data.scopes;
            })
        }
    }

</script>