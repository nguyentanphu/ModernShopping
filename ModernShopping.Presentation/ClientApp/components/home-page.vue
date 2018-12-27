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
            <div class="card border-info mb-3" v-for="n in 10" :key="n">
                <div class="card-header">
                    Product {{ n }}
                    <span class="badge badge-pill badge-info float-right">$ 199</span>
                </div>
                <div class="card-body text-info">
                    <h5 class="card-title">
                        Info card title
                        <span title="Add to cart" class="float-right">
                            <font-awesome-icon class="click-icon" icon="shopping-cart"/>
                        </span>
                    </h5>
                    <p
                        class="card-text"
                    >Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {};
    }
};
</script>

<style scope>
.click-icon {
    cursor: pointer;
}
</style>
