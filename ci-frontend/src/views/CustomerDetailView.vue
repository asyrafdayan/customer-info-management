<template>
    <div class="animate__animated animate__fadeIn">

        <div class="mt-10 flex flex-col container mx-auto px-96">
            <button type="button" @click.prevent="goBack" class="w-[100px] text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2">Back</button>

            <svg v-if="isLoading == true" role="status" class="mr-2 w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600 content-center" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/>
                <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/>
            </svg>

            <div v-else class="border rounded-lg shadow-sm bg-stone-50 p-10 hover:shadow-2xl">
                <template v-if="customerInfo != null">
                    <div class="grid grid-cols-2 gap-4">
                        <div :class="fieldCss">
                            <label :class="labelCss">Customer Name</label>
                            <input type="text" v-model="customerInfo.customerName" :class="inputCss" disabled/>
                        </div>
                        <div :class="fieldCss">
                            <label :class="labelCss">DoB</label>
                            <input type="text" v-model="customerInfo.dateOfBirth" :class="inputCss" disabled/>
                        </div>
                    </div>
                    <div :class="fieldCss">
                        <label :class="labelCss">Street Address</label>
                        <input type="text" v-model="customerInfo.customerStreet" :class="inputCss" disabled/>
                    </div>
                    <div class="grid grid-cols-2 gap-4">
                        <div :class="fieldCss">
                            <label :class="labelCss">Postal Code</label>
                            <input type="text" v-model="customerInfo.customerPosCode" :class="inputCss" disabled/>
                        </div>
                        <div :class="fieldCss">
                            <label :class="labelCss">City</label>
                            <input type="text" v-model="customerInfo.customerCity" :class="inputCss" disabled/>
                        </div>
                    </div>
                    <div class="grid grid-cols-2 gap-4">
                        <div :class="fieldCss">
                            <label :class="labelCss">Customer State</label>
                            <input type="text" v-model="customerInfo.customerState" :class="inputCss" disabled/>
                        </div>
                        <div :class="fieldCss">
                            <label :class="labelCss">Customer Country</label>
                            <input type="text" v-model="customerInfo.customerCountry" :class="inputCss" disabled/>
                        </div>
                    </div>
                </template>
                <template v-else>
                    No content
                </template>
            </div>
        </div>

    </div>
</template>

<script>
import axios from 'axios'
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            isLoading: false,
            customerId: this.$route.params.id,
            customerInfo: null,

            fieldCss: 'relative mb-1 w-full group',
            labelCss: 'block mb-1 text-sm text-gray-900 dark:text-gray-300',
            inputCss: 'bg-transparent text-gray-900 text-sm font-bold rounded-md block w-full px-2 py-2'
        }
    },
    computed: {
        ...mapGetters({
            baseUrl: 'getAPIUrl'
        })
    },
    created() {
        this.getCustomerDetails()
    },
    methods: {
        // Returns to the previous screen
        goBack() {
            this.$router.go(-1)
        },

        // Fetches customer details
        async getCustomerDetails() {
            this.isLoading = true
            await axios.get(this.baseUrl + 'GetCustomerInformation?id=' + this.customerId)
            .then((response) => {
                this.customerInfo = response.data
            })
            .catch((error) => {
                console.log(error)
            })
            .then(() => {
                this.isLoading = false
            })
        }
    }
}
</script>
