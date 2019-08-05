import Vue from 'vue'
import VueRouter from 'vue-router';
import routes from './routes'
import Master from './components/layouts/Master'
import {store} from './store/store'
import VeeValidate from 'vee-validate' 
import Footable from 'footable'

import VuejsDialog from 'vuejs-dialog';
//import VuejsDialogMixin from 'vuejs-dialog/vuejs-dialog-mixin.min.js';

import 'vuejs-dialog/dist/vuejs-dialog.min.css';


//eventBus son eventos que son visibles desde todos los componentes del sistema
window.eventBus = new Vue()

Vue.config.productionTip = false
Vue.use(VueRouter)
Vue.use(VeeValidate)
Vue.use(Footable)

Vue.use(VuejsDialog);

const router = new VueRouter({
  routes,
  mode: 'history'
})


router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresVisitor)) {    
    if (!store.getters.loggedIn) {
      next({
        name: 'login'
      })
    } else {
      next()
    }
  } else {
    next() // make sure to always call next()!
  }
})

new Vue({
  //el: '#app',
  render: h => h(Master), //Master es la pagina que tiene el layout
  router: router,
  store:store,
  components: {Master}
}).$mount('#app')
