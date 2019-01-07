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
                            <div class="col-12">
                                <div class="custom-file">
                                    <input
                                        type="file"
                                        class="custom-file-input"
                                        @change="fileSelected"
                                        id="customFile"
                                    >
                                    <label class="custom-file-label" for="customFile">{{ fileName }}</label>
                                </div>
                                <div v-if="uploadPercentage" class="progress mt-1">
                                    <div
                                        class="progress-bar progress-bar-striped progress-bar-animated bg-info"
                                        :style="{'width': uploadPercentage + '%'}"
                                    ></div>
                                </div>
                                <div class="mt-1">
                                    <img
                                        class="image-preview"
                                        v-if="imageSrc"
                                        :src="imageSrc"
                                        alt="image preview"
                                    >
                                </div>
                            </div>
                        </div>

                        <button class="btn btn-primary" type="submit">Submit form</button>
                    </form>
                </div>
            </div>
        </div>

        <div class="col-12 row">
            <div class="col-3">
                <!-- <v-select
                    v-model="selected"
                    :options="options"
                    :filterable="false"
                    @search="onSearch"
                ></v-select>-->
            </div>
            <div class="col-6">
                <!-- <input type="file" class="form-control-file" name="imageUpload" @change="fileSelected"> -->
            </div>
        </div>
    </div>
</template>
<script>
import _ from 'lodash'
import axios from 'axios'
import baseSelect2 from './base/base-select2.vue'

export default {
    data() {
        return {
            selectedSupplier: null,
            selectedCategory: null,
            options: [],
            uploadPercentage: 0,
            imageSrc: undefined,
            fileName: 'Choose image'
        }
    },
    methods: {
        async onSearch(query, loading) {
            loading(true)
            await this.debounceSearch(query, loading)
        },
        async fileSelected(event) {
            const selectedFile = event.target.files[0]
            this.fileName = selectedFile.name
            this.showInputFileImagePreview(selectedFile)

            const formData = new FormData()
            formData.append('imageUpload', selectedFile, selectedFile.name)
            const vm = this
            try {
                const response = await axios.post('/api/images', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    },
                    onUploadProgress(progressEvent) {
                        vm.uploadPercentage = parseInt(
                            Math.round(
                                (progressEvent.loaded * 100) /
                                    progressEvent.total
                            )
                        )
                    }
                })

                this.$notify({
                    group: 'general-message',
                    type: 'success',
                    title: 'Upload completed',
                    text: 'Upload succeeded.'
                })
            } catch (error) {
                this.$notify({
                    group: 'general-message',
                    type: 'error',
                    title: 'Error!',
                    text: 'Error occurred. Please contact your administrator.'
                })
            }
        }
    },
    created() {
        const vm = this

        this.showInputFileImagePreview = function(fileTarget) {
            const reader = new FileReader()

            reader.onload = function() {
                vm.imageSrc = reader.result
            }

            reader.readAsDataURL(fileTarget)
        }
    },
    components: {
        'base-select2': baseSelect2
    }
}
</script>

<style scoped>
.image-preview {
    max-width: 100%;
}
</style>
