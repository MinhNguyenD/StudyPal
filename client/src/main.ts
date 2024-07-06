import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import authHeader from './services/authHeader'

axios.defaults.baseURL="http://localhost:5267/";
axios.defaults.headers.common["Authorization"] = authHeader();
createApp(App).use(router).mount('#app')
