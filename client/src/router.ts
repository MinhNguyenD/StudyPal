import { createMemoryHistory, createRouter } from 'vue-router'
import Contact from './components/contact/Contact.vue'

const routes = [
    // { path: '/', component: Home },
    // { path: '/faq', component: FAQ },
    // { path: '/login', component: Login },
    // { path: '/register', component: Register },
    { path: '/contact', component: Contact },
]

export const router = createRouter({
    history: createMemoryHistory(),
    routes,
})