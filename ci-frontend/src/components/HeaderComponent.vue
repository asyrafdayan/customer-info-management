<template>
    <div class="max-w py-5 bg-stone-50 px-20 top-0 flex flex-col justify-center items-center">
        <p class="text-3xl font-bold">Customer Information Management</p>
        <p v-if="staffDetails != null" class="text-sm">Hello, {{ staffDetails.staffName }}
            <span @click="logout" class="underline cursor-pointer text-blue-300 hover:text-blue-600">
                Logout
            </span>
        </p>
    </div>
</template>

<script>
import { useStorage } from 'vue3-storage'
import { mapGetters, mapMutations } from 'vuex'
export default {
    computed: {
        ...mapGetters({
            staffDetails: 'getStaffDetails'
        })
    },
    methods: {
        ...mapMutations(['setStaffDetails']),
        logout() {
            const storage = useStorage()
            this.setStaffDetails(null)
            storage.clearStorageSync()
            this.$router.replace({ name: 'login' })
        }
    }
}
</script>
