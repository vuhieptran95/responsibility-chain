<template>
    <div>
<!--        <v-navigation-drawer-->
<!--                v-model="drawer"-->
<!--                app-->
<!--        >-->
<!--            <v-list dense>-->
<!--                <v-list-item :key="i" v-for="(link, i) in links" :to="link.route">-->
<!--                    <v-list-item-action>-->
<!--                        <v-icon>{{link.icon}}</v-icon>-->
<!--                    </v-list-item-action>-->
<!--                    <v-list-item-content>-->
<!--                        <v-list-item-title>{{link.title}}</v-list-item-title>-->
<!--                    </v-list-item-content>-->
<!--                </v-list-item>-->
<!--            </v-list>-->
<!--        </v-navigation-drawer>-->
        <v-app-bar
                
                dense
                app
                dark
        >
            <v-app-bar-nav-icon @click.stop="drawer = !drawer"/>
            <v-toolbar-title>
                <v-btn text href="/">Identity Server Administration</v-btn>
            </v-toolbar-title>
        </v-app-bar>
    </div>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'
    import {defaultUser, User} from "@/commons/User";

    @Component
    export default class AppBar extends Vue {
        user: User = defaultUser;
        links = [
            {
                icon: "home", title: "Projects", route: "/administration"
            }
        ];
        drawer = null;

        mounted() {
            // axios.get(HELPER_ENDPOINT + "current-user").then(res => this.user = res.data).catch(handleAxiosError)
        }
    }

</script>