import axios from 'axios'

const state = {
  cartLines: [],
  checkout: false
}

const mutations = {
  UPDATE_CART(state, payload) {
    state.cartLines = payload
  },
  ADJUST_PRODUCT_TO_CART(state, payload) {
    const existingLine = state.cartLines.find(l => l.productId === payload.productId)
    if (existingLine) {
      existingLine.quantity = payload.quantity
      existingLine.unitPrice = payload.unitPrice

      state.cartLines = state.cartLines.filter(l => l.quantity !== 0)
    } else {
      state.cartLines.push(payload)
    }
  }
}

const actions = {
  async fetchCart(context) {
    var result = await axios.get('/api/cart')
    context.commit('UPDATE_CART', result.data.cartLines)
  },
  async adjustCartLine(context, {
    product,
    quantity
  }) {
    const cartLine = {
      productId: product.productId,
      unitPrice: product.unitPrice,
      productName: product.productName,
      quantity: quantity
    }

    const result = await axios.post('/api/cart/cart-line', cartLine)
    context.commit('ADJUST_PRODUCT_TO_CART', result.data)
  },
  async removeCart(context) {
    await axios.delete('/api/cart')
    context.commit('UPDATE_CART', [])
  }
}

const getters = {
  cartLines(state) {
    return state.cartLines
  }
}

const cartModule = {
  state,
  mutations,
  actions,
  getters
}

export default cartModule
