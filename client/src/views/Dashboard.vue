<!-- TESTING PURPOSE -->
<template>
    <p>Welcome {{ userStore.getUser.username }}</p>
    <button @click="handleClick">Click</button>
</template>
<script>
import authHeader from '@/services/authHeader';
import { useUserStore } from '@/store/user';
import axios from 'axios';

export default {
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