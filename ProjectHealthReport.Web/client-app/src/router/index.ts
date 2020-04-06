import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import AddEditProject from "@/components/Administration/AddEditProject.vue";
import Administration from "@/components/Administration/Administration.vue";
import Projects from "@/components/Administration/Projects.vue";
import ProjectsPhr from "@/components/Administration/ProjectsPhr.vue";

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
    component: Projects
  },
  {
    path: '/administration/projects',
    name: 'Projects',
    component: Projects
  },
  {
    path: '/administration/projects/phr',
    name: 'Projects',
    component: ProjectsPhr
  },
  {
    path: '/administration/projects/add-edit',
    name: 'AddEditProject',
    component: AddEditProject
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
