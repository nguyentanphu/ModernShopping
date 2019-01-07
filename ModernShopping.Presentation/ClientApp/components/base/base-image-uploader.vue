<template>
    <div class="col-12">
        <div class="custom-file">
            <input type="file" class="custom-file-input" @change="fileSelected" id="customFile">
            <label class="custom-file-label" for="customFile">{{ fileName }}</label>
        </div>
        <div v-if="uploadPercentage" class="progress mt-1">
            <div
                class="progress-bar progress-bar-striped progress-bar-animated bg-info"
                :style="{'width': uploadPercentage + '%'}"
            ></div>
        </div>
        <div class="mt-1">
            <img class="image-preview" v-if="imageSrc" :src="imageSrc" alt="image preview">
        </div>
    </div>
</template>

<script>
import axios from 'axios'

export default {
    data() {
        return {
            uploadPercentage: 0,
            imageSrc: null,
            fileName: 'Choose image'
        }
    },
    methods: {
        async fileSelected(event) {
            const selectedFile = event.target.files[0]
            this.fileName = selectedFile.name
            this.showInputFileImagePreview(selectedFile)

            const formData = new FormData()
            formData.append('imageUpload', selectedFile, selectedFile.name)
            try {
                const response = await axios.post('/api/images', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    },
                    onUploadProgress: progressEvent => {
                        this.uploadPercentage = Number.parseInt(
                            Math.round(
                                (progressEvent.loaded * 100) /
                                    progressEvent.total
                            )
                        )
                    }
                })

                this.$emit('uploadSuccess', response.data)

                this.$notify({
                    group: 'general-message',
                    type: 'success',
                    title: 'Upload completed',
                    text: 'Upload succeeded.'
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
        }
    },
    created() {
        this.showInputFileImagePreview = fileTarget => {
            const reader = new FileReader()
            reader.onload = e => (this.imageSrc = e.target.result)
            reader.readAsDataURL(fileTarget)
        }
    }
}
</script>

<style scoped>
.image-preview {
    max-width: 100%;
}
</style>


