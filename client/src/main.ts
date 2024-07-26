import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import { createPinia } from "pinia"

axios.defaults.baseURL="http://localhost:5267/";
const pinia = createPinia();
createApp(App).use(router).use(pinia).mount('#app');
