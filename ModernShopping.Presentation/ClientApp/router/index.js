import Vue from 'vue'
import VueRouter from 'vue-router'
import {
  routes
} from './routes'

Vue.use(VueRouter)

let router = new VueRouter({
  mode: 'history',
  routes
})

router.beforeEach(async (to, from, next) => {
  const app = router.app.$data || {
    isAuthenticated: false
  }
  if (app.isAuthenticated) {
    next()
  } else if (to.matched.some(r => r.meta.requireAuth)) {
    // authentication require
    await router.app.authenticate(to.path)
    console.log('authenticating a protected url:' + to.path)
  } else {
    // no authentication require
    next()
  }
})

export default router
