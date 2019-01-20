<template>
    <div class="row">
        <product :product="product" v-for="product in productList" :key="product.productId"/>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import product from './product'
export default {
    computed: {
        ...mapGetters(['productList'])
    },
    components: {
        product
    },
    created() {
        this.$store.dispatch('fetchProductList')
    }
}
</script>
