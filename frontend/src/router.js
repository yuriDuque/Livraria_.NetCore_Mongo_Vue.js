import Vue from 'vue'
import Router from 'vue-router'

import login from './views/login.vue'

//admin
import admin from './views/admin/home.vue'

//user
import user from './views/user/home.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '',
      name: 'login',
      component: login
    },
    {
      path: '/admin',
      name: 'admin',
      component: admin
    },
    {
      path: '/livraria',
      name: 'livraria',
      component: user
    },
  ]
})