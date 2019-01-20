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
                <div class="col-6" v-for="n in 10" :key="n">
                    <div class="card mb-3">
                        <img
                            class="card-img-top"
                            src="/product-images/default/default.jpg"
                            alt="Card image cap"
                        >
                        <div class="card-body">
                            <h5 class="card-title">
                                Aniseed Syrup as das das das das dasddsdsdsd sd sds
                                <span
                                    class="badge badge-pill badge-info float-right"
                                >$ 199</span>
                            </h5>
                            <p class="card-text">Category sample. Sold by: Supplier sample</p>
                        </div>
                        <div class="card-footer">
                            <small class="text-muted">40 unit(s) x 24 - 12 oz bottles</small>
                            <span title="Add to cart" class="float-right click-icon">
                                <font-awesome-icon icon="shopping-cart"/>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {}
    }
}
</script>

<style scope>
.click-icon {
    cursor: pointer;
}
</style>
