import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import axios from "axios";
import { createPinia } from "pinia";

axios.defaults.baseURL = "https://csci4177grp-20.onrender.com/";
const pinia = createPinia();
createApp(App).use(router).use(pinia).mount("#app");
