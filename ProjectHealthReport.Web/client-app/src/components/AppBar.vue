<template>
    <v-app-bar
            app
            color="white"
    >
        <v-toolbar-title><v-btn text href="/">Delivery Manager Report</v-btn></v-toolbar-title>
        <v-btn text to="/Administration">Administration</v-btn>
        <v-spacer></v-spacer>
        <v-btn text>Hello, {{user.username}}</v-btn>
    </v-app-bar>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {defaultUser, User} from "@/commons/User";
    import axios from "axios";
    import {HELPER_ENDPOINT} from "@/helpers/EndPoint";
    import {handleAxiosError} from "@/helpers/HandleResponse";
    
    
    @Component
    export default class AppBar extends Vue {
        user: User = defaultUser;
        
        mounted(){
            axios.get(HELPER_ENDPOINT + "current-user").then(res => this.user = res.data).catch(handleAxiosError)
        }
    }

</script>