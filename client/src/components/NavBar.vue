<template>
    <nav class="flex items-center justify-between p-4 bg-white">
        <div class="text-2xl font-bold">StudyPal</div>
        <div class="hidden md:flex space-x-5">
            <RouterLink to="/" class="pt-2 font-bold hover:underline hover:text-mainBlue active:text-mainBlue">Home</RouterLink>
            <RouterLink to="/FAQ" class="pt-2 font-bold hover:underline hover:text-mainBlue active:text-mainBlue">FAQ</RouterLink>
            <RouterLink to="/contact" class="pt-2 font-bold hover:underline hover:text-mainBlue active:text-mainBlue">Contact</RouterLink>
            <RouterLink to="/login" class="px-7 py-1 border border-mainBlue text-mainBlue font-bold rounded-full hover:text-blue-700">Login</RouterLink>
            <RouterLink to="/register" class="px-7 py-1 bg-mainBlue text-white font-bold rounded-full hover:bg-blue-700">Register</RouterLink>
        </div>
    </nav>
</template>

<script>
export default {
    name: "NavBar"
}
</script>