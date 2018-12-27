<template>
    <div class="row justify-content-center">
        <div class="col-3">
            <v-select v-model="selected" :options="options" :filterable="false" @search="onSearch"></v-select>
        </div>
        <div class="col-3">{{ options }}</div>
    </div>
</template>
<script>
import _ from 'lodash'
import axios from 'axios'
export default {
    data() {
        return {
            selected: undefined,
            options: []
        }
    },
    methods: {
        async onSearch(query, loading) {
            loading(true)
            await this.debounceSearch(query, loading)
        }
    },
    created() {
        const that = this
        this.debounceSearch = _.debounce(async (query, loading) => {
            const response = await axios.get('/api/data-source/category-source/' + escape(query))
            that.options = response.data
            loading(false)
        }, 500)
    }
};
</script>
