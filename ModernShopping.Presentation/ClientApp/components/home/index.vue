<template>
    <div class="row">
        <div class="col-4">
            <h1 class="text-center">
                <font-awesome-icon icon="shopping-cart"/>
            </h1>
            <hr>
            <div class="mb-3" v-for="n in 20" :key="n">
                <div class="text-secondary">
                    The fullstack hoodie
                    <span class="float-right">
                        <span title="Increase quantity">
                            <font-awesome-icon class="click-icon" icon="chevron-circle-up"/>
                        </span>
                        <span title="Decrease quantity">
                            <font-awesome-icon class="click-icon" icon="chevron-circle-down"/>
                        </span>
                    </span>
                </div>
                <div>
                    <span class="text-info">$ 199 each
                        <font-awesome-icon icon="times"/>&nbsp;Quantity: 2
                    </span>
                    <span class="float-right font-weight-bold border border-info">$ 398</span>
                </div>
            </div>

            <div class="font-weight-bold text-info">
                Total quantity: 40
                <span class="float-right click-icon">
                    <font-awesome-icon icon="trash"/>&nbsp;Remove all
                </span>
            </div>
            <div class="mt-3 mb-3">
                <button class="btn btn-lg btn-block btn-success">Check out! ($2000)</button>
            </div>
        </div>
        <div class="col-8">
            <div class="row">
                <product :product="product" v-for="product in products" :key="product.productId"/>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import product from './product'
export default {
    data() {
        return {
            products: null
        }
    },
    components: {
        'product': product
    },
    async created() {
        const result = await axios.get('/api/products')
        this.products = result.data
    }
}
</script>

<style scope>
.click-icon {
    cursor: pointer;
}
</style>
