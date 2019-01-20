<template>
    <div class="col-6">
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
</template>

<script>
export default {
    props: {
        product: {
            required: true
        }
    }
}
</script>
