import { createWebHistory, createRouter } from 'vue-router'
import Contact from './components/contact/Contact.vue'
import Homepage from './components/Homepage.vue'
import FAQ from "./views/FAQ.vue"
import UpdateProfile from "./views/UpdateProfile.vue"

const routes = [
    { path: '/', component: Homepage },
    { path: '/faq', component: FAQ },
    // { path: '/login', component: Login },
    // { path: '/register', component: Register },
    { path: '/contact', component: Contact },
    { path: '/updateProfile', component: UpdateProfile },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})