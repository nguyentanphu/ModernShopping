<template>
  <div>
    <h1 class="text-center">
      <font-awesome-icon icon="shopping-cart"/>
    </h1>
    <hr>
    <cart-line v-for="line in cartLines" :key="line.productId" :cartLine="line"/>

    <div class="font-weight-bold text-info">
      Total quantity: {{ totalQuantity }}
      <span
        v-if="!isCartEmpty"
        class="float-right click-icon"
        @click="removeCart"
      >
        <font-awesome-icon icon="trash"/>&nbsp;Remove all
      </span>
    </div>
    <div class="mt-3 mb-3">
      <button
        :disabled="isCartEmpty"
        class="btn btn-lg btn-block btn-success"
      >Check out! ($ {{ totalCartPrice }})</button>
    </div>
  </div>
</template>

<script>
import cartLine from './cart-line'
import { mapGetters, mapActions } from 'vuex'

export default {
  components: {
    'cart-line': cartLine
  },
  computed: {
    ...mapGetters(['cartLines']),
    isCartEmpty() {
      return !this.cartLines || this.cartLines.length === 0
    },
    totalQuantity() {
      if (this.cartLines.length === 0)
        return 0

      return this.cartLines.reduce((acc, currentLine) => acc + currentLine.quantity, 0)
    },
    totalCartPrice() {
      if (this.cartLines.length === 0)
        return 0

      return this.cartLines.reduce((acc, currentLine) => acc + currentLine.quantity * currentLine.unitPrice, 0)
    }
  },
  methods: {
    ...mapActions(['removeCart'])
  },
  created() {
    this.$store.dispatch('fetchCart')
  }

}
</script>

