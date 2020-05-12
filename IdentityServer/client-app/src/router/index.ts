import Vue from 'vue'
import VueRouter from 'vue-router'
import Administration from "@/components/Administration/Administration.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'administration',
    component: Administration
  },
  {
    path: '/administration',
    name: 'administration',
    component: Administration
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

export default router
