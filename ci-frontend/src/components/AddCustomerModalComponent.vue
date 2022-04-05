<template>
    <div
        v-if="isOpen"
        class="absolute inset-0 flex items-center justify-center bg-gray-700 bg-opacity-50"
    >
        <div class="w-1/2 p-6 bg-white rounded-md shadow-xl">
            <div class="flex items-center justify-between">
                <h3 class="text-2xl">Add New Customer</h3>
            </div>
            <div class="mt-4 pt-4">
                <Alert
                    v-if="showAlert"
                    :alertMsg="alertMsg"
                />
                <form class="mb-5">
                    <div class="grid grid-cols-2 gap-5">
                        <div class="mb-3">
                            <label for="customer_name" :class="labelCSS">Customer Name</label>
                            <input type="text" id="customer_name" v-model="state.customerName" :class="inputCSS" placeholder="Full name" required>
                        </div>
                        <div class="mb-3">
                            <label for="dob" :class="labelCSS">Date of Birth</label>
                            <input type="date" id="dob" v-model="state.dateOfBirth" :class="inputCSS" required>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="street" :class="labelCSS">Street Address</label>
                        <input type="email" id="street" v-model="state.customerStreet" :class="inputCSS" placeholder="Street Address">
                    </div>
                    <div class="grid grid-cols-2 gap-5">
                        <div class="mb-3">
                            <label for="pos_code" :class="labelCSS">Postal Code</label>
                            <input type="text" id="pos_code" v-model="state.customerPosCode" :class="inputCSS" placeholder="Pos Code">
                        </div>
                        <div class="mb-3">
                            <label for="city" :class="labelCSS">City</label>
                            <input type="text" id="city" v-model="state.customerCity" :class="inputCSS" placeholder="City">
                        </div>
                    </div>
                    <div class="grid grid-cols-2 gap-5">
                        <div class="mb-3">
                            <label for="state" :class="labelCSS">State</label>
                            <input type="text" id="state" v-model="state.customerState" :class="inputCSS" placeholder="State">
                        </div>
                        <div class="mb-3">
                            <label for="country" :class="labelCSS">Country</label>
                            <input type="text" id="country" v-model="state.customerCountry" :class="inputCSS" placeholder="Country">
                        </div>
                    </div>
                </form>
                <button
                    @click="toggleModal"
                    class="px-6 py-2 text-blue-800 border border-blue-600 rounded-3xl hover:text-white hover:bg-blue-600 disabled:bg-gray-100 hover:disabled:text-blue-800 hover:disabled:bg-gray-100"
                    :disabled="isSubmitting == true"
                >
                    Cancel
                </button>
                <button
                    type="submit"
                    @click.prevent="addCustomer"
                    class="px-6 py-2 ml-2 text-blue-800 border border-blue-600 rounded-3xl hover:text-white hover:bg-blue-600 disabled:bg-gray-100 hover:disabled:text-blue-800 hover:disabled:bg-gray-100"
                    :disabled="isSubmitting == true">
                    Submit
                    <svg
                        v-if="isSubmitting"
                        role="status"
                        class="inline mr-3 w-4 h-4 text-white animate-spin"
                        viewBox="0 0 100 101"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="#0000FF"
                        />
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="#0000FF"
                        />
                    </svg>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import Alert from '@/components/Alert.vue'
import axios from 'axios'
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            showAlert: false,
            alertMsg: '',
            isSubmitting: false,
            state: {
                customerName: '',
                dateOfBirth: '',
                customerStreet: '',
                customerPosCode: '',
                customerCity: '',
                customerState: '',
                customerCountry: ''
            },
            labelCSS: 'block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300',
            inputCSS: 'bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 focus:shadow-md hover:shadow-md block w-full p-2.5'
        }
    },
    computed: {
        ...mapGetters({
            baseURL: 'getAPIUrl'
        })
    },
    props: ['isOpen'],
    methods: {
        toggleModal() {
            this.$emit('toggle-modal')
        },
        async addCustomer() {
            this.isSubmitting = true
            this.showAlert = false
            if (this.state.customerName.trim() == '' || this.state.dateOfBirth == '') {
                this.alertMsg = 'Full name and date of birth are required'
                this.showAlert = true
                this.isSubmitting = false
            } else {
                await axios.post(this.baseURL + 'RegisterCustomer', this.state)
                    .then((response) => {
                        this.$emit('reload')
                        this.toggleModal()
                    })
                    .catch((error) => {
                        if (error.response.status == 507) {
                            this.alertMsg = 'Due to database limitations, the maximum number of customers have been limited to 10 people.'
                            this.showAlert = true
                        } else {
                            this.alertMsg = 'Server Error. Please try again.'
                            this.showAlert = true
                        }
                    })
                    .then(() => {
                        this.isSubmitting = false
                    })
            }
        }
    },
    components: {
        Alert
    }
}
</script>
