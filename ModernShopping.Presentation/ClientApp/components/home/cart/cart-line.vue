<template>
  <div class="mb-3">
    <div class="text-secondary">
      {{ cartLine.productName }}
      <span class="float-right">
        <span title="Increase quantity" @click="adjustCartLine({product: cartLine, quantity: 1})">
          <font-awesome-icon class="click-icon" icon="chevron-circle-up"/>
        </span>
        <span title="Decrease quantity" @click="adjustCartLine({product: cartLine, quantity: -1})">
          <font-awesome-icon class="click-icon" icon="chevron-circle-down"/>
        </span>
      </span>
    </div>
    <div>
      <span class="text-info">
        $ {{ cartLine.unitPrice }} each
        <font-awesome-icon icon="times"/>
        &nbsp;Quantity: {{ cartLine.quantity }}
      </span>
      <span class="float-right font-weight-bold border border-info">$ {{ lineTotal }}</span>
    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  props: {
    cartLine: {
      type: Object,
      required: true
    }
  },
  computed: {
    lineTotal() {
      return this.cartLine.unitPrice * this.cartLine.quantity
    }
  },
  methods: {
    ...mapActions(['adjustCartLine'])
  }
}
</script>

