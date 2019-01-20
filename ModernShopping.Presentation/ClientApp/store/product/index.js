import axios from 'axios'

const state = {
  productList: []
}

const mutations = {
  UPDATE_PRODUCT_LIST(state, payload) {
    state.productList = payload
  }
}

const actions = {
  async fetchProductList(context) {
    var result = await axios.get('/api/products')
    context.commit('UPDATE_PRODUCT_LIST', result.data)
  }
}

const getters = {
  productList(state) {
    return state.productList
  }
}

const productModule = {
  state,
  mutations,
  actions,
  getters
}

export default productModule
