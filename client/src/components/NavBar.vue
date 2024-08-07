<template>
    <nav class="px-20 flex items-center justify-between p-4 bg-white border-b-2 border-gray-300 sticky top-0">
        <div class="text-2xl font-bold">StudyPal</div>
        <div class="hidden md:flex space-x-7">
            <RouterLink to="/" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">Home
            </RouterLink>
            <RouterLink to="/faq" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">FAQ
            </RouterLink>
            <RouterLink to="/contact" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">
                Contact</RouterLink>
            <RouterLink v-if="userStore.user == null" to="/login"
                class="px-8 py-1 border border-primary text-primary font-semibold rounded-full hover:text-blue-700">
                Login</RouterLink>
            <RouterLink v-if="userStore.user == null" to="/register"
                class="px-7 py-1 bg-primary text-white font-semibold rounded-full hover:bg-blue-700">Register
            </RouterLink>
        </div>
    </nav>
</template>

<script setup lang="ts">
import { useUserStore } from '@/store/user';

const userStore = useUserStore();
</script>