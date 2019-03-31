import Vue from 'vue'
import router from './router/index'
import store from './store'
import App from 'components/app-root'

import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'animate.css'
import './icons'
import {
  FontAwesomeIcon
} from '@fortawesome/vue-fontawesome'
import Notifications from 'vue-notification'
import axios from 'axios'

Vue.use(Notifications)

// Registration of global components
Vue.component('font-awesome-icon', FontAwesomeIcon)

const v = new Vue({
  el: '#app',
  store,
  router,
  ...App
})

axios.interceptors.request.use((config) => {
    const user = v.$root.user
    if (user) {
      const authToken = user.access_token
      if (authToken) {
        config.headers.Authorization = `Bearer ${authToken}`
      }
    }
    return config
  },
  (err) => {
    // What do you want to do when a call fails?
    console.log(err)
  })
