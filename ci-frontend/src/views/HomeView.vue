<template>
  <div v-if="isLoadingData == false" class="flex flex-col justify-center animate__animated animate__fadeIn">

    <!-- Alert messages -->
    <Alert
      v-if="showErrorModal"
      :alertMsg="errorModalMsg"
    />

    <button @click.prevent="toggleAddCustomerModal"
      class="mt-10 ml-[480px] bg-blue-600 text-white px-2 py-2 rounded-xl max-w-[200px] disabled:text-gray-400 disabled:bg-gray-100"
      :disabled="staffDetails.createAccess == false">Add New Customer</button>

    <!-- Loading indicator -->
    <svg v-if="isFetchingCustomers == true" role="status" class="mr-2 mt-2 w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600 content-center" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"/>
        <path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"/>
    </svg>

    <!-- Customer displays -->
    <CustomerTable
      v-else
      :customerResults="customerResults"
      @reload="getAllCustomers" />

    <AddCustomerModal
      :isOpen="showAddCustomerModal"
      @toggle-modal="toggleAddCustomerModal"
      @reload="getAllCustomers"
    />

  </div>
</template>

<script>
import Alert from "@/components/Alert.vue"
import AddCustomerModal from "@/components/AddCustomerModalComponent.vue"
import CustomerTable from "@/components/CustomersTableComponent.vue"
import { mapGetters, mapMutations } from 'vuex'
import axios from 'axios'
import { useStorage } from "vue3-storage"

export default {
  data() {
    return {
      isLoadingData: false,
      isFetchingCustomers: false,
      errorModalMsg: '',
      showErrorModal: false,
      customerResults: null,
      showAddCustomerModal: false
    }
  },
  computed: {
    ...mapGetters({
      baseUrl: 'getAPIUrl',
      staffDetails: 'getStaffDetails'
    })
  },
  created() {
    this.getStaffInformation()
    this.getAllCustomers()
  },
  methods: {
    ...mapMutations(['setStaffDetails']),
    // Fetch staff information
    async getStaffInformation() {
      this.isLoadingData = true
      const storage = useStorage()
      await axios.get(this.baseUrl + 'GetStaffInfo?id=' + storage.getStorageSync("staff-id"))
        .then((response) => {
          this.setStaffDetails(response.data)
        })
        .catch((error) => {
          console.log(error)
        })
        .then(() => {
          this.isLoadingData = false
        })
    },
    // Fetch Users list
    async getAllCustomers() {
      this.isFetchingCustomers = true
      this.showErrorModal = false
      await axios.get(this.baseUrl + 'GetCustomersList')
      .then((response) => {
        this.customerResults = response.data
      })
      .catch((error) => {
        this.errorModalMsg = error.response.data.statusMessage
        this.showErrorModal = true
      })
      .then(() => {
        this.isFetchingCustomers = false
      })
    },
    toggleAddCustomerModal() {
      this.showAddCustomerModal = !this.showAddCustomerModal
    }
  },
  components: {
    Alert,
    AddCustomerModal,
    CustomerTable
  }
}
</script>
