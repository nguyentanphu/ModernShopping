import Vue from 'vue'
import Vuex from 'vuex'

import productModule from './product'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    productModule
  }
})
