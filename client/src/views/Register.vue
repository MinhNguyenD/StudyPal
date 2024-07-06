<template>
    <div class="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div class="sm:mx-auto sm:w-full sm:max-w-sm">
            <!-- <img class="mx-auto h-10 w-auto" src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                alt="StudyPal" /> -->
            <h2 class="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Register new account
            </h2>
        </div>

        <div class="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
            <form id="registerForm" @submit.prevent="handleSubmit" class="space-y-6">

                <div class="flex items-center justify-between">
                    <div>
                        <label for="firstName" class="block text-base font-medium leading-6 text-gray-900">First
                            name</label>
                        <input v-model="firstName" id="firstName" name="firstName" type="text" required=""
                            class="mt-2 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                    <div>
                        <label for="lastName" class="block text-base font-medium leading-6 text-gray-900">Last
                            name</label>
                        <input v-model="lastName" id="lastName" name="lastName" type="text" required=""
                            class="mt-2 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                </div>
                <div>
                    <label for="username" class="block text-base font-medium leading-6 text-gray-900">Username</label>
                    <div class="mt-2">
                        <input v-model="username" id="username" name="username" type="text" required=""
                            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                </div>
                <div>
                    <label for="email" class="block text-base font-medium leading-6 text-gray-900">Email
                        address</label>
                    <div class="mt-2">
                        <input v-model="email" id="email" name="email" type="email" autocomplete="email" required=""
                            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                </div>
                <div>
                    <label for="password" class="block text-base font-medium leading-6 text-gray-900">Password</label>
                    <div class="mt-2">
                        <input v-model="password" id="password" name="password" type="password"
                            autocomplete="current-password" required=""
                            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                </div>
                <div>
                    <label for="confirmPassword" class="block text-base font-medium leading-6 text-gray-900">Comfirm
                        password</label>
                    <div class="mt-2">
                        <input v-model="confirmPassword" id="confirmPassword" name="confirmPassword" type="password"
                            required=""
                            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                    </div>
                </div>
                <div>
                    <label class="block text-base font-medium leading-6 text-gray-900">Roles</label>
                    <div class="flex flex-wrap mt-2">
                        <div class="flex items-center me-10">
                            <input v-model="checkedRoles" value="Student"
                                class="w-5 h-5 border-gray-300 rounded focus:ring-primary" type="checkbox" id="student">
                            <label class="text-sm ms-2 font-base" for="student">Student</label>
                        </div>
                        <div class="flex items-center">
                            <input v-model="checkedRoles" value="Tutor"
                                class="w-5 h-5 border-gray-300 rounded focus:ring-primary" type="checkbox" id="tutor">
                            <label class="text-sm ms-2 font-base" for="tutor">Tutor</label>
                        </div>
                    </div>
                </div>

                <div>
                    <button type="submit"
                        class="flex w-full justify-center rounded-md bg-primary px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Register</button>
                </div>
            </form>

            <p class="mt-10 text-center text-sm text-gray-500">
                Already have an account?
                {{ ' ' }}
                <a href="#" class="font-semibold leading-6 text-primary hover:text-indigo-500">Login to your account
                </a>
            </p>
        </div>
    </div>
</template>

<script>
import { useUserStore } from "@/store/user";
import axios from "axios";

export default {
    name: "Register",
    setup() {
        const userStore = useUserStore();
        return { userStore };
    },
    data() {
        return {
            firstName: "",
            lastName: "",
            username: "",
            email: "",
            password: "",
            confirmPassword: "",
            checkedRoles: []
        };
    },
    methods: {
        async handleSubmit() {
            try {
                const response = await axios.post(
                    "auth/register",
                    {
                        firstname: this.firstName,
                        lastname: this.lastName,
                        username: this.username,
                        email: this.email,
                        roles: this.checkedRoles,
                        password: this.password,
                    }
                )
                this.userStore.register(response.data);
                //TODO: push to login dashboard
                this.$router.push("/dashboard");
            }
            catch (error) {
                alert("Register failed!");
            };
        }
    },
};
</script>

<style></style>