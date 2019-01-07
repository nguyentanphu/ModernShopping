<template>
    <div class="row justify-content-center">
        <div class="col-8">
            <div class="card">
                <div class="card-header bg-info">Product #1</div>
                <div class="card-body">
                    <form class="needs-validation" novalidate>
                        <div class="form-row mb-3">
                            <div class="col-6">
                                <label for="product-name">Product name</label>
                                <input
                                    type="text"
                                    class="form-control form-control-lg"
                                    id="product-name"
                                    placeholder="Sample name"
                                    required
                                >
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                            <div class="col-6">
                                <label for="quantity-per-unit">Quality per unit</label>
                                <input
                                    type="text"
                                    class="form-control form-control-lg"
                                    id="quantity-per-unit"
                                    placeholder="e.g. 10 boxes x 20 bags"
                                    required
                                >
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                        </div>

                        <div class="form-row mb-3">
                            <div class="col-6">
                                <label for="product-name">Category</label>
                                <base-select2
                                    url="/api/data-source/category-source/"
                                    :min-input-length="2"
                                    v-model="selectedCategory"
                                />
                                {{ selectedCategory }}
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                            <div class="col-6">
                                <label for="quantity-per-unit">Supplier</label>
                                <base-select2
                                    url="/api/data-source/supplier-source/"
                                    :min-input-length="2"
                                    v-model="selectedSupplier"
                                />
                                {{ selectedSupplier }}
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                        </div>

                        <div class="form-row mb-3">
                            <div class="col-4">
                                <label for="unit-price">Unit price</label>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">$</span>
                                    </div>
                                    <input type="number" id="unit-price" class="form-control">
                                </div>
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                            <div class="col-4">
                                <label for="quantity-per-unit">Units in stock</label>
                                <input type="number" id="units-in-stocks" class="form-control">
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                            <div class="col-4">
                                <label for="quantity-per-unit">Units on order</label>
                                <input type="number" id="units-on-order" class="form-control">
                                <div class="valid-feedback">Looks good!</div>
                            </div>
                        </div>
                        <div class="form-row mb-3">
                            <base-image-uploader @uploadSuccess="uploadSuccessHandler"/>
                            {{ imageId }}
                        </div>

                        <button class="btn btn-primary" type="submit">Submit form</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import baseSelect2 from './base/base-select2.vue'
import baseImageUploader from './base/base-image-uploader.vue'

export default {
    data() {
        return {
            selectedSupplier: null,
            selectedCategory: null,
            imageId: null
        }
    },
    methods: {
        uploadSuccessHandler(eventData) {
            this.imageId = eventData.imageId
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
