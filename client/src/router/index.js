import { createWebHistory, createRouter } from 'vue-router'

import RegistrationView from '../views/RegistrationView.vue'
import LoginView from '../views/LoginView.vue'
import LandingPageView from '../views/LandingPageView.vue'
import FAQView from '../views/FAQView.vue'
import ContactView from '../views/ContactView.vue'

const routes = [
  { path: '/', name: 'Home', component: LandingPageView },
  { path: '/register', name: 'Register', component: RegistrationView },
  { path: '/login', name: 'Login', component: LoginView },
  {path: '/FAQ', name: 'FAQ', component: FAQView},
  {path: '/contact', name: 'contact', component: ContactView}

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router;