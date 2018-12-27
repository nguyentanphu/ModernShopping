import Vue from 'vue'
import router from './router/index'
import store from './store'
import App from 'components/app-root'

import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'animate.css'
import './icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

// Registration of global components
Vue.component('font-awesome-icon', FontAwesomeIcon)

new Vue({
  el: '#app',
  store,
  router,
  ...App
})
