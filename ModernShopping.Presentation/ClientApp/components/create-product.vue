<template>
    <form :class="{'was-validated': isSubmited}" novalidate @submit.prevent="createProducts">
        <div class="row justify-content-center">
            <div class="col-8" v-for="(product,index) in productsToCreate" :key="index">
                <div class="card mb-3">
                    <div class="card-header bg-info">Product #{{ index + 1 }}</div>
                    <div class="card-body">
                        <div class="form-row mb-3">
                            <div class="col-6">
                                <label for="product-name">Product name</label>
                                <input
                                    type="text"
                                    class="form-control form-control-lg"
                                    id="product-name"
                                    placeholder="Sample name"
                                    required
                                    v-model.trim="product.productName"
                                >
                                <div class="valid-feedback">Looks good!</div>
                                <div class="invalid-feedback">Please provide product name.</div>
                            </div>
                            <div class="col-6">
                                <label for="quantity-per-unit">Quanlity per unit</label>
                                <input
                                    type="text"
                                    class="form-control form-control-lg"
                                    id="quantity-per-unit"
                                    placeholder="e.g. 10 boxes x 20 bags"
                                    required
                                    v-model.trim="product.quanlityPerUnit"
                                >
                                <div class="valid-feedback">Looks good!</div>
                                <div class="invalid-feedback">Please provide quanlity per unit.</div>
                            </div>
                        </div>

                        <div class="form-row mb-3">
                            <div class="col-6">
                                <label for="product-name">Category</label>
                                <base-select2
                                    url="/api/data-source/category-source/"
                                    :min-input-length="2"
                                    v-model="product.category"
                                    :required="true"
                                />
                                <div class="valid-feedback">Looks good!</div>
                                <div class="invalid-feedback">Please select a category.</div>
                            </div>
                            <div class="col-6">
                                <label for="quantity-per-unit">Supplier</label>
                                <base-select2
                                    url="/api/data-source/supplier-source/"
                                    :min-input-length="2"
                                    v-model="product.supplier"
                                    :required="true"
                                />
                                <div class="valid-feedback">Looks good!</div>
                                <div class="invalid-feedback">Please select a supplier.</div>
                            </div>
                        </div>

                        <div class="form-row mb-3">
                            <div class="col-4">
                                <label for="unit-price">Unit price</label>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">$</span>
                                    </div>
                                    <input
                                        type="number"
                                        id="unit-price"
                                        class="form-control"
                                        required
                                        v-model="product.unitPrice"
                                    >
                                    <div class="valid-feedback">Looks good!</div>
                                    <div class="invalid-feedback">Please provide unit price.</div>
                                </div>
                            </div>
                            <div class="col-4">
                                <label for="quantity-per-unit">Units in stock</label>
                                <input
                                    type="number"
                                    id="units-in-stocks"
                                    class="form-control"
                                    required
                                    v-model="product.unitsInStock"
                                >
                                <div class="valid-feedback">Looks good!</div>
                                <div class="invalid-feedback">Please provide unit in stock.</div>
                            </div>
                            <div class="col-4">
                                <label for="quantity-per-unit">Units on order</label>
                                <input
                                    type="number"
                                    id="units-on-order"
                                    class="form-control"
                                    v-model="product.unitsOnOrder"
                                >
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                        </div>
                        <div class="form-row mb-3">
                            <base-image-uploader
                                @uploadSuccess="uploadSuccessHandler(index, $event)"
                            />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <button class="btn btn-primary" type="submit">Submit form</button>
            </div>
        </div>
    </form>
</template>
<script>
import baseSelect2 from './base/base-select2.vue'
import baseImageUploader from './base/base-image-uploader.vue'

export default {
    data() {
        return {
            isSubmited: false,
            productsToCreate: [
                {
                    productName: null,
                    quanlityPerUnit: null,
                    category: null,
                    supplier: null,
                    unitPrice: null,
                    unitsInStock: null,
                    unitsOnOrder: null,
                    imageId: null
                },
                {
                    productName: null,
                    quanlityPerUnit: null,
                    category: null,
                    supplier: null,
                    unitPrice: null,
                    unitsInStock: null,
                    unitsOnOrder: null,
                    imageId: null
                }
            ]
        }
    },
    methods: {
        createProducts() {
            this.isSubmited = true
            console.log(this)
        },
        uploadSuccessHandler(index, eventData) {
            this.productsToCreate[index].imageId = eventData.imageId
        }
    },
    components: {
        'base-select2': baseSelect2,
        'base-image-uploader': baseImageUploader
    }
}
</script>

<style scoped>
</style>
