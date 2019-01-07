<template>
    <v-select
        :value="value"
        :options="options"
        :filterable="false"
        @search="searchHandler"
        @input="inputHandler"
    >
        <template slot="no-options">
            <span v-if="showMinInput">Input length should be greater than {{ minInputLength }}</span>
            <span v-else>Sorry, no options available!</span>
        </template>
    </v-select>
</template>

<script>
import _ from 'lodash'
import axios from 'axios'
import vSelect from 'vue-select'

export default {
    data() {
        return {
            options: [],
            showMinInput: true
        }
    },
    props: {
        value: {
            required: false,
            default: null
        },
        url: {
            required: true,
            type: String
        },
        minInputLength: {
            required: true,
            type: Number
        }
    },
    methods: {
        async searchHandler(query, loading) {
            if (query.length < this.minInputLength) {
                return
            }

            this.showMinInput = false
            loading(true)
            await this.debounceSearch(query, loading)
        },
        inputHandler(val) {
            this.$emit('input', val)
        }
    },
    components: {
        'v-select': vSelect
    },
    created() {
        this.debounceSearch = _.debounce(async (query, loading) => {
            try {
                const response = await axios.get(this.url + escape(query))
                this.options = response.data
                loading(false)
            } catch (error) {
                this.$notify({
                    group: 'general-message',
                    type: 'error',
                    title: 'Error!',
                    text: 'Error occurred. Please contact your administrator.'
                })
            }
        }, 500)
    }
}
</script>

