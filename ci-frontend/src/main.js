import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Vue3Storage, { StorageType } from "vue3-storage"

// Tailwind
import './index.css'
import 'flowbite'

const storageOptions = {
    namespace: "ci_",
    storage: StorageType.Session
}

createApp(App).use(store).use(router).use(Vue3Storage, storageOptions).mount('#app')
