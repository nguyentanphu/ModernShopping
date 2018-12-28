<template>
    <div class="row justify-content-center">
        <div class="col-3">
            <v-select v-model="selected" :options="options" :filterable="false" @search="onSearch"></v-select>
        </div>
        <div class="col-3">{{ uploadPercentage }}</div>
        <div class="col-3">
            <input type="file" name="imageUpload" @change="fileSelected">
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
            uploadPercentage: 0
        };
    },
    methods: {
        async onSearch(query, loading) {
            loading(true);
            await this.debounceSearch(query, loading);
        },
        fileSelected(event) {
            const selectedFile = event.target.files[0];
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
                .then(function(response) {
                    console.log(response.data);
                });
        }
    },
    created() {
        const that = this;
        this.debounceSearch = _.debounce(async (query, loading) => {
            const response = await axios.get(
                "/api/data-source/category-source/" + escape(query)
            );
            that.options = response.data;
            loading(false);
        }, 500);
    }
};
</script>
