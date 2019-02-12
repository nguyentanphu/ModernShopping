<template>
    <form :class="{'was-validated': isSubmited}" novalidate @submit.prevent="createProducts">
        <div class="row justify-content-center">
            <div class="col-8 mb-3">
                <button
                    type="button"
                    class="btn btn-secondary"
                    v-if="productsToCreate.length === 0"
                    @click="addProductToCreate"
                >
                    <font-awesome-icon icon="plus"/>&nbsp;Add more product
                </button>
            </div>

            <transition-group
                tag="div"
                name="list"
                enter-active-class="animated slideInLeft"
                leave-active-class="animated slideOutRight"
                class="col-8"
            >
                <div v-for="(product,index) in productsToCreate" :key="index">
                    <div class="card mb-3">
                        <div class="card-header bg-info">
                            Product #{{ index + 1 }}
                            <button
                                type="button"
                                class="btn btn-secondary"
                                @click="removeProductToCreate(index)"
                            >
                                <font-awesome-icon icon="minus"/>
                            </button>
                            <button
                                type="button"
                                class="btn btn-light float-right"
                                v-if="index === productsToCreate.length - 1"
                                @click="addProductToCreate"
                            >
                                <font-awesome-icon icon="plus"/>
                            </button>
                        </div>
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
                                    <label for="quantity-per-unit">Quantity per unit</label>
                                    <input
                                        type="text"
                                        class="form-control form-control-lg"
                                        id="quantity-per-unit"
                                        placeholder="e.g. 10 boxes x 20 bags"
                                        required
                                        v-model.trim="product.quantityPerUnit"
                                    >
                                    <div class="valid-feedback">Looks good!</div>
                                    <div class="invalid-feedback">Please provide quantity per unit.</div>
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
            </transition-group>
            <div class="col-8">
                <button class="btn btn-primary" type="submit">Submit form</button>
            </div>
        </div>
    </form>
</template>
<script>
import axios from 'axios'
import baseSelect2 from '../base/base-select2'
import baseImageUploader from '../base/base-image-uploader'

export default {
    data() {
        return {
            isSubmited: false,
            productsToCreate: []
        }
    },
    methods: {
        getEmptyProduct() {
            return {
                productName: null,
                quantityPerUnit: null,
                category: null,
                supplier: null,
                unitPrice: null,
                unitsInStock: null,
                unitsOnOrder: null,
                imageId: null
            }
        },
        async createProducts() {
            this.isSubmited = true

            for (const product of this.productsToCreate) {
                if (!this.isValidProduct(product)) return
            }

            try {
                const response = await axios.post(
                    '/api/products-collections',
                    this.productsToCreate
                )

                this.productsToCreate = []
                this.isSubmited = false

                this.$notify({
                    group: 'general-message',
                    type: 'success',
                    title: 'Create product(s) succeeded!',
                    text: 'Your new product(s) have been saved!'
                })
            } catch (error) {
                console.log(error)
                this.$notify({
                    group: 'general-message',
                    type: 'error',
                    title: 'Error!',
                    text: 'Error occurred. Please contact your administrator.'
                })
            }
        },
        addProductToCreate() {
            this.productsToCreate.push({
                productName: null,
                quantityPerUnit: null,
                category: null,
                supplier: null,
                unitPrice: null,
                unitsInStock: null,
                unitsOnOrder: null,
                imageId: null
            })
            this.scrollToBottom()
        },
        removeProductToCreate(index) {
            this.productsToCreate.splice(index, 1)
        },
        uploadSuccessHandler(index, eventData) {
            this.productsToCreate[index].imageId = eventData.imageId
        }
    },
    created() {
        this.scrollToBottom = () => {
            var scrollingElement = document.scrollingElement || document.body

            const productHeight = 730
            let scrollDistance = 0
            const scrollInterval = setInterval(() => {
                scrollDistance += 5
                scrollingElement.scrollTop += 5
                if (scrollDistance >= productHeight)
                    clearInterval(scrollInterval)
            }, 10)
        }
        this.isValidProduct = product => {
            for (const key in product) {
                if (!product[key]) return false

                return true
            }
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
