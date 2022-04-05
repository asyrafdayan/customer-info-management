<template>
    <div class="mt-2 flex flex-col min-w-full inline-block items-center">
        <div class="overflow-x-auto w-1/2 shadow-md hover:shadow-lg sm:rounded-lg">
            <table class="min-w-full table-auto text-sm text-left text-gray-500 dark:text-gray-400">
                <thead class="text-xs text-white uppercase bg-blue-500 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="px-6 py-3">
                            Id
                        </th>
                        <th scope="col" class="px-6 py-3">
                            Customer Name
                        </th>
                        <th scope="col" class="px-6 py-3">
                            Options
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <template v-if="customerResults.customers.length > 0">
                        <tr class="border-b dark:bg-gray-800 dark:border-gray-700 odd:bg-white even:bg-gray-50 odd:dark:bg-gray-800 even:dark:bg-gray-700"
                        v-for="(customer, index) in customerResults.customers" :key="index">
                            <td class="w-0.5/3 px-6 py-4">
                                {{ customer.id }}
                            </td>
                            <td class="w-1.5/3 px-6 py-4">
                                {{ customer.customerName }}
                            </td>
                            <td class="w-1/3 px-6 py-4">
                                <div class="inline-flex rounded-md shadow-sm" role="group">
                                    <button @click.prevent="viewCustomerProfile(customer.id)" type="button" :class="optionCSS">
                                        Profile
                                    </button>
                                    <button
                                        type="button"
                                        :class="optionCSS"
                                        @click.prevent="openDeleteModal(customer.id)"
                                        :disabled="staffDetails.deleteAccess == false">
                                        Delete
                                    </button>
                                </div>
                            </td>
                        </tr>
                    </template>
                    <template v-else>
                        <tr class="border-b dark:bg-gray-800 dark:border-gray-700 odd:bg-white even:bg-gray-50 odd:dark:bg-gray-800 even:dark:bg-gray-700">
                            <td class="w-3/3 px-6 py-4">
                                No Customers
                            </td>
                        </tr>
                    </template>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Delete Customer Modal -->
    <div
        v-show="isOpen"
        class="absolute inset-0 flex items-center justify-center bg-gray-700 bg-opacity-50"
    >
        <div class="w-1/4 p-6 bg-white rounded-md shadow-xl">
            <div class="flex items-center justify-between">
                <h3 class="text-2xl">Delete Customer ID {{ chosenCustomerId }}</h3>
            </div>
            <div class="mt-4">
                <p class="mb-4 text-sm">
                    This action cannot be undone. Proceed?
                </p>
                <button
                    @click="isOpen = false"
                    class="px-6 py-2 text-blue-800 border border-blue-600 rounded disabled:text-gray-600 disabled:bg-gray-100" :disabled="isSubmitting == true">
                    No
                </button>
                <button
                    @click.prevent="deleteCustomer"
                    class="px-6 py-2 ml-2 text-blue-800 border border-blue-600 rounded disabled:text-gray-600 disabled:bg-gray-100" :disabled="isSubmitting == true">
                    Yes
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
import axios from 'axios'
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            isOpen: false,
            isSubmitting: false,
            chosenCustomerId: 0,
            optionCSS: 'py-2 px-4 text-sm font-medium text-gray-900 bg-white rounded border border-gray-200 hover:bg-gray-100 hover:text-blue-700 disabled:text-gray-400 disabled:bg-gray-100'
        }
    },
    props: ['customerResults'],
    computed: {
        ...mapGetters({
            baseURL: 'getAPIUrl',
            staffDetails: 'getStaffDetails'
        })
    },
    methods: {
        viewCustomerProfile(customerId) {
            this.$router.push({ name: 'customer-detail', params: { id: customerId } })
        },
        openDeleteModal(customerId) {
            this.chosenCustomerId = customerId
            this.isOpen = true
        },
        async deleteCustomer() {
            this.isSubmitting = true
            await axios.delete(this.baseURL + 'DeleteCustomer?id=' + this.chosenCustomerId)
                .then((response) => {
                    this.isOpen = false
                    this.$emit('reload')
                })
                .catch((error) => {
                    console.log(error)
                })
                .then(() => {
                    this.isSubmitting = false
                })
        }
    }
}
</script>
