import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import 'material-design-icons-iconfont/dist/material-design-icons.css';
// @ts-ignore
import CKEditor from 'ckeditor4-vue';


Vue.config.productionTip = false;
Vue.use(CKEditor);

new Vue({
  router,
// @ts-ignore
  vuetify,
  render: h => h(App)
}).$mount('#app');
