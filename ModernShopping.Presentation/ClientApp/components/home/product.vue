<template>
    <div class="col-6">
        <div class="card mb-3">
            <img class="card-img-top" :src="productImageUrl" alt="product.productName">
            <div class="card-body">
                <h5 class="card-title">
                    {{ product.productName }}
                    <span
                        class="badge badge-pill badge-info float-right"
                    >$ {{ product.unitPrice }}</span>
                </h5>
                <p class="card-text">Category: {{ product.categoryName }}</p>
                <p class="card-text">Sold by: {{ product.supplierName }}</p>
            </div>
            <div class="card-footer">
                <small class="text-muted">{{ product.quantityPerUnit }}</small>
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
            required: true,
            type: Object
        }
    },
    computed: {
        productImageUrl() {
            return this.product.imageDtos[0] ? this.product.imageDtos[0].imageUrl : '/product-images/default/default.jpg';
        }
    }
}
</script>
