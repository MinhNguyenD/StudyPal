<!-- TESTING PURPOSE -->
<template>
    <div class="flex">
        <SideBar />
        <div class="flex-1 p-6 pl-8">
            <p>Welcome {{ userStore.getUser.username }}</p>
            <button @click="handleClick">Click</button>
        </div>
    </div>
</template>
<script>
import authHeader from '@/services/authHeader';
import { useUserStore } from '@/store/user';
import axios from 'axios';
import SideBar from '../components/SideBar.vue';

export default {
    components: {
        SideBar
    },
    setup() {
        const userStore = useUserStore();
        return {
            userStore
        }
    },
    methods: {
        async handleClick() {
            var res = await axios.get("test", { headers: authHeader() });
            alert(res.data);
        }
    }

}
</script>