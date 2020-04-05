import Vue from 'vue';
import VueRouter from 'vue-router';
import AddEditProject from "@/components/Administration/AddEditProject.vue";
import Projects from "@/components/Administration/Projects.vue";
Vue.use(VueRouter);
const routes = [
    {
        path: '/',
        name: 'Administration',
        component: Projects
    },
    {
        path: '/administration',
        name: 'Administration',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: Projects
    },
    {
        path: '/administration/projects/add-edit',
        name: 'AddEditProject',
        component: AddEditProject
    }
];
const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
});
export default router;
//# sourceMappingURL=index.js.map