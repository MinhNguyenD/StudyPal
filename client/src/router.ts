import { createRouter, createWebHistory } from 'vue-router'
import Contact from './components/contact/Contact.vue'
import Homepage from './components/Homepage.vue'
import FAQ from "./views/FAQ.vue"
import Login from "./views/Login.vue"
import Register from "./views/Register.vue"


const routes = [
    { 
      path: '/', component: Homepage, meta: {
        requiresAuth: false
      } 
    },
    { 
      path: '/faq', component: FAQ, meta: {
        requiresAuth: false
      } 
    },
    { 
      path: '/login', component: Login, meta: {
        requiresAuth: false
      } 
    },
    { 
      path: '/register', component: Register, meta: {
        requiresAuth: false
      } 
    },
    { 
      path: '/contact', component: Contact, meta: {
        requiresAuth: false
      }
    }
]


  
const router = createRouter({
    history: createWebHistory(),
    routes,
  });

router.beforeEach(async (to, from) => {
    if (to.meta.requiresAuth) {
      const token = localStorage.getItem('token');
      if (!token) {
        // User is not authenticated, redirect to login
        return '/login';
      }
    } 
  });

export default router;
