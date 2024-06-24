<template>
    <nav class="mx-10 flex items-center justify-between p-4 bg-white">
        <div class="text-2xl font-bold">StudyPal</div>
        <div class="hidden md:flex space-x-7">
            <RouterLink to="#" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">Home</RouterLink>
            <RouterLink to="#" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">FAQ</RouterLink>
            <RouterLink to="#" class="pt-1 font-semibold hover:underline hover:text-primary active:text-primary">Contact</RouterLink>
            <RouterLink to="#" class="px-8 py-1 border border-primary text-primary font-semibold rounded-full hover:text-blue-700">Login</RouterLink>
            <RouterLink to="#" class="px-7 py-1 bg-primary text-white font-semibold rounded-full hover:bg-blue-700">Register</RouterLink>
        </div>
    </nav>
</template>

<script setup lang='ts'>
</script>