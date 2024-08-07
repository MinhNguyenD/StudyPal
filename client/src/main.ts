import { createApp, watch } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import axios from "axios";
import { createPinia } from "pinia";
import { useUserStore } from "./store/user";

axios.defaults.baseURL = "https://csci4177grp-20.onrender.com/";
const pinia = createPinia();
const app = createApp(App);
app.use(router).use(pinia);
const userStore = useUserStore();
watch(
    () => userStore.storedToken,
    (newToken) => {
        if (newToken) {
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + newToken;
        } else {
            delete axios.defaults.headers.common['Authorization'];
        }
    },
    { immediate: true }
);
app.mount("#app");