<template>
    <div class="row justify-content-center">
        <div class="col-3">
            <v-select v-model="selected" :options="options" :filterable="false" @search="onSearch"></v-select>
        </div>
        <div class="col-3">{{ uploadPercentage }}</div>
        <div class="col-3">
            <!-- <input type="file" class="form-control-file" name="imageUpload" @change="fileSelected"> -->
            <div class="custom-file">
                <input type="file" class="custom-file-input" @change="fileSelected" id="customFile">
                <label class="custom-file-label" for="customFile">{{ fileName }}</label>
            </div>
        </div>
        <div class="col-3">
            <img class="image-preview" v-if="imageSrc" :src="imageSrc" alt="image preview">
        </div>
    </div>
</template>
<script>
import _ from "lodash";
import axios from "axios";
export default {
    data() {
        return {
            selected: undefined,
            options: [],
            uploadPercentage: 0,
            imageSrc: undefined,
            fileName: 'Choose image'
        };
    },
    methods: {
        async onSearch(query, loading) {
            loading(true)
            await this.debounceSearch(query, loading)
        },
        fileSelected(event) {
            const selectedFile = event.target.files[0]
            this.fileName = selectedFile.name
            this.showInputFileImagePreview(selectedFile)

            const formData = new FormData();
            formData.append("imageUpload", selectedFile, selectedFile.name);
            const vm = this;
            axios
                .post("/api/images", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    },
                    onUploadProgress(progressEvent) {
                        vm.uploadPercentage = parseInt(
                            Math.round(
                                (progressEvent.loaded * 100) /
                                progressEvent.total
                            )
                        );
                    }
                })
                .then(function (response) {
                    console.log(response.data);
                });
        }
    },
    created() {
        const vm = this;
        this.debounceSearch = _.debounce(async (query, loading) => {
            const response = await axios.get(
                "/api/data-source/category-source/" + escape(query)
            );
            vm.options = response.data;
            loading(false);
        }, 500);

        this.showInputFileImagePreview = function (fileTarget) {
            const reader = new FileReader()

            reader.onload = function () {
                vm.imageSrc = reader.result
            }

            reader.readAsDataURL(fileTarget)
        }
    }
};
</script>

<style scoped>
.image-preview {
    max-width: 100%;
}
</style>
