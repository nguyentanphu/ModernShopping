<template>
  <div class="row">
    <div class="col-4">
      <cart/>
    </div>
    <div class="col-8">
      <product-list/>
    </div>
  </div>
</template>

<script>
import productList from './product/product-list'
import cart from './cart/cart'
export default {
  components: {
    'product-list': productList,
    'cart': cart
  }
}
</script>

<style scope>
.click-icon {
  cursor: pointer;
}
</style>
