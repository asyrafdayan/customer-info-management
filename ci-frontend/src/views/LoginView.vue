<template>
    <div class="flex items-center justify-center min-h-screen">
      <div class="container mx-auto">
        <div
          class="bg-white rounded-sm shadow-xl sm:max-w-md sm:w-full sm:mx-auto sm:overflow-hidden pb-10 mb-20 animate__animated animate__fadeIn"
        >
            <!-- Logo -->
            <div class="mt-4 -mb-10">
                <img
                class="mx-auto h-40 w-auto object-contain sm:object-scale-down"
                src="@/assets/logo.png"
                alt="Logo"
                />
            </div>
            <div class="px-4 py-4 sm:px-10">
                <div class="relative mt-6">
                    <div class="absolute inset-0 flex items-center">
                        <div class="w-full border-t border-gray-300"></div>
                    </div>
                    <div class="relative flex justify-center text-sm leading-5">
                        <span class="px-2 text-gray-500 bg-white">
                            Sign in to your account
                        </span>
                    </div>
                </div>

                <!-- Form -->
                <form class="space-y-6 mt-5" action="#">
                    <!-- Alert messages -->
                    <Alert
                        v-if="showModal"
                        :alertMsg="alertMsg"
                    />

                    <!-- Username -->
                    <div>
                        <label
                            for="email"
                            class="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">
                            Username
                        </label>
                        <input
                            type="text"
                            name="Username"
                            id="username"
                            class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-md focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white"
                            placeholder="Username"
                            v-model.trim="state.username"
                        />
                    </div>

                    <!-- Password -->
                    <div>
                        <label
                            for="password"
                            class="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">
                            Password
                        </label>
                        <input
                            type="password"
                            name="password"
                            id="password"
                            placeholder="••••••••"
                            class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-md focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white"
                            v-model="state.password"
                        />
                    </div>

                    <!-- Submit Button -->
                    <button
                        :disabled="isSubmitted"
                        type="submit"
                        class="w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-semibold rounded-md text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
                        @click.prevent="login"
                    >
                        Sign In
                        <svg
                        v-if="isSubmitted"
                        role="status"
                        class="inline mr-3 w-4 h-4 text-white animate-spin"
                        viewBox="0 0 100 101"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                        >
                        <path
                            d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                            fill="#E5E7EB"
                        />
                        <path
                            d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                            fill="currentColor"
                        />
                        </svg>
                    </button>
                </form>
            </div>
        </div>
        </div>
    </div>
</template>

<script>
import Alert from "@/components/Alert.vue"
import router from "@/router/index.js"
import axios from 'axios'
import { mapGetters, mapMutations } from "vuex"
import { useStorage } from "vue3-storage"

export default {
    data() {
        return {
            showModal: false,
            alertMsg: '',
            isSubmitted: false,
            state: {
                username: '',
                password: ''
            }
        }
    },
    computed: {
        // Setup VUEX getters
        ...mapGetters({
            baseUrl: 'getAPIUrl'
        })
    },
    components: {
        Alert
    },
    methods: {

        // Setup VUEX mutations
        ...mapMutations(['setStaffDetails']),

        // Validates the field
        // Displays alert message for errors
        validate() {
            if (this.state.username == '' || this.state.password.trim() == '') {
                this.alertMsg = 'Incomplete field'
                this.showModal = true
                return false
            }
            this.showModal = false
            return true
        },

        async login() {
            if (this.validate() == true) {
                const storage = useStorage()
                this.isSubmitted = true

                await axios.post(this.baseUrl + 'Login', {
                    username: this.state.username,
                    password: this.state.password
                })
                .then((response) => {
                    storage.setStorageSync("staff-id", response.data.id)
                    router.replace({ name: 'customer' })
                })
                .catch((error) => {
                    this.alertMsg = error.response.data.statusMessage
                    this.showModal = true
                })
                .then(() => {
                    this.isSubmitted = false
                })
            }
        }
    }
}
</script>
