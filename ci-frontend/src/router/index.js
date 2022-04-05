import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import CustomerDetailView from '../views/CustomerDetailView.vue'
import { useStorage } from "vue3-storage"

const routes = [
  {
    path: '/',
    name: 'customer',
    component: HomeView
  },
  {
    path: '/:id',
    name: 'customer-detail',
    component: CustomerDetailView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  }
]

const router = createRouter({
  mode: 'history',
  base: '/cimanagement/',
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  const storage = useStorage()

  if (to.name == 'customer' && storage.getStorageSync('staff-id') == null) {
    next({ name: 'login', replace: true })
  } else {
    next()
  }
})

export default router
