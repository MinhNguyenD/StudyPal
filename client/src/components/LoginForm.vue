<template>
    <div>
        <h1 class="text-2xl font-bold mt-20 mb-2 text-center">Login</h1>
        <div class="flex items-center justify-center">
            <form class="w-full max-w-lg mt-7" @submit.prevent="submitForm">
                <div class="flex flex-wrap -mx-3 mb-5">
                    <div class="w-full px-3">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="username">
                            Username
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="username" type="text" v-model="username" required />
                        <p class="text-red-500 text-xs italic" v-if="usernameError.length > 0">{{ usernameError }}</p>
                    </div>
                </div>
                <div class="flex flex-wrap -mx-3 mb-5">
                    <div class="w-full px-3">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="password">
                            Password
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="password" type="password" v-model="password" required />
                        <p class="text-red-500 text-xs italic" v-if="passwordError.length > 0">{{ passwordError }}</p>
                    </div>
                </div>
                <div class="text-center">
                    <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
                        Log In
                    </button>
                </div>
            </form>
        </div>
        
        <div class="flex items-center justify-center mt-10">
            <p>Don't have and account? Click <RouterLink class="text-blue-500 hover:text-blue-700 underline" to="/">here</RouterLink> to register</p>
        </div>
    </div>
</template>

<script>
export default {
    name: "LoginForm",
    data() {
        return {
            email: "",
            password: "",
            usernameError: "",
            passwordError: ""
        };
    },
    methods: {
        submitForm() {
            console.log(`Email Address: ${this.email}`);
            console.log(`Password: ${this.password}`);
        }
    }
}    
</script>