import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import AddEditProject from "@/components/Administration/AddEditProject.vue";
import Administration from "@/components/Administration/Administration.vue";
import Projects from "@/components/Administration/Projects.vue";
import ProjectsPhr from "@/components/Administration/ProjectsPhr.vue";
import AddEditProjects from "@/components/PhrProjects/AddEditProjects.vue";
import WeeklyReport from "@/components/PhrWeeklyReports/WeeklyReport.vue";
import DoDAdmin from "@/components/DoDs/DoDAdmin.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'administration',
    component: Projects
  },
  {
    path: '/dmr/administration/projects',
    name: 'DMRProjects',
    component: Projects
  },
  {
    path: '/phr/administration/projects',
    name: 'PHRProjects',
    component: ProjectsPhr
  },
  {
    path: '/dmr/administration/projects/add-edit',
    name: 'DMRAddEditProject',
    component: AddEditProject
  },
  {
    path: '/phr/projects/add-edit',
    name: 'PHRAddEditProject',
    component: AddEditProjects
  },
  {
    path: '/phr/weekly-reports/add-edit',
    name: 'PHRAddEditWeeklyReports',
    component: WeeklyReport
  },
  {
    path: '/phr/administration/dods',
    name: 'PHRDod',
    component: DoDAdmin
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
