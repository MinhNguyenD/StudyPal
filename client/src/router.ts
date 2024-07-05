import { createMemoryHistory, createRouter } from 'vue-router'
import Contact from './components/contact/Contact.vue'
import Homepage from './components/Homepage.vue'
import FAQ from "./views/FAQ.vue"
import Login from "./views/Login.vue"
import Register from "./views/Register.vue"


const routes = [
    { path: '/', component: Homepage },
    { path: '/faq', component: FAQ },
    { path: '/login', component: Login },
    { path: '/register', component: Register },
    { path: '/contact', component: Contact },
]

export const router = createRouter({
    history: createMemoryHistory(),
    routes,
})