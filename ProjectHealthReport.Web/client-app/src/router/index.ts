import Vue from 'vue'
import VueRouter from 'vue-router'
import AddEditProject from "@/components/Administration/AddEditProject.vue";
import Projects from "@/components/Administration/Projects.vue";
import ProjectsPhr from "@/components/Administration/ProjectsPhr.vue";
import AddEditProjects from "@/components/PhrProjects/AddEditProjects.vue";
import WeeklyReport from "@/components/PhrWeeklyReports/WeeklyReport.vue";
import DoDAdmin from "@/components/DoDs/DoDAdmin.vue";
import DivisionIndex from "@/components/Divisions/DivisionIndex.vue";
import ProjectIndex from "@/components/PhrProjects/ProjectIndex.vue";
import DivisionReport from "@/components/Divisions/DivisionReport.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'administration',
    component: Projects
  },
  {
    path: '/divisions',
    name: 'DivisionIndex',
    component: DivisionIndex
  },
  {
    path: '/divisions/report',
    name: 'DivisionReport',
    component: DivisionReport
  },
  {
    path: '/projects',
    name: 'ProjectIndex',
    component: ProjectIndex
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
