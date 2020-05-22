import Vue from 'vue'
import Router from 'vue-router'

import login from './views/login.vue'

//admin
import admin from './views/admin/index.vue'
import listaLivros from './views/admin/livro/listaLivros.vue'
import listaUsuarios from './views/user/usuario/listaUsuarios.vue'

//user
import user from './views/user/index.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '',
      name: 'login',
      component: login,
    },
    {
      path: '/admin',
      name: 'admin',
      component: admin,
      children:[
        {
          path: '/livros',
          name: 'lista-livros',
          component: listaLivros,
        },
        {
          path: '/usuarios',
          name: 'lista-usuarios',
          component: listaUsuarios,
        },
      ]
    },
    {
      path: '/usuario',
      name: 'usuario',
      component: user
    },
  ]
})