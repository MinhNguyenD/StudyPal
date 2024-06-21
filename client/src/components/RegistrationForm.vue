<template>
    <h1 class="text-2xl font-bold mt-7 mb-2 text-center">Registration Form</h1>
    <div class="flex items-center justify-center">
        <form class="w-full max-w-lg mt-7" @submit.prevent="submitForm">
            <div class="flex flex-wrap -mx-3 mb-5">
                <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                    <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="firstname">
                        First Name
                    </label>
                    <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="firstName" type="text" v-model="firstName" required />
                </div>
                <div class="w-full md:w-1/2 px-3">
                    <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="lastname">
                        Last Name
                    </label>
                    <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="lastName" type="text" v-model="lastName" required />
                </div>
            </div>
            <div class="flex flex-wrap -mx-3 mb-5">
                <div class="w-full px-3">
                    <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="username">
                        Username
                    </label>
                    <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="username" type="text" v-model="username" required />
                </div>
            </div>
            <div class="flex flex-wrap -mx-3 mb-5">
                <div class="w-full px-3">
                    <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="email">
                        Email Address
                    </label>
                    <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="email" type="text" v-model="email" required />
                    <p class="text-red-500 text-xs italic" v-if="emailError.length > 0">{{ emailError }}</p>
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
            <div class="flex flex-wrap -mx-3 mb-5">
                <div class="w-full px-3">
                    <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="confirmPassword">
                        Confirm Password
                    </label>
                    <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="confirmPassword" type="password" v-model="confirmPassword" required />
                    <p class="text-red-500 text-xs italic" v-if="passwordMatchError.length > 0">{{ passwordMatchError }}</p>
                </div>
            </div>
            <div class="uppercase tracking-wide text-gray-700 text-xs font-bold mb-3">Choose Role:</div>
            <div class="flex flex-wrap mb-5">
                <div class="flex items-center me-10">
                    <input class="w-4 h-4 text-blue-600 bg-gray-200 border-gray-300 rounded focus:ring-blue-500" type="checkbox" id="student" v-model="checkedStudent">
                    <label class="ms-2 text-base font-medium" for="student">Student</label>
                </div>
                <div class="flex items-center">
                    <input class="w-4 h-4 text-blue-600 bg-gray-200 border-gray-300 rounded focus:ring-blue-500" type="checkbox" id="tutor" v-model="checkedTutor">
                    <label class="ms-2 text-base font-medium" for="tutor">Tutor</label>
                </div>
            </div>

            <div class="text-center">
                <button class="bg-mainBlue hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
                    Register
                </button>
            </div>
        </form>
    </div>
</template>

<script>

export default {
    name: "RegistrationForm",
    data() {
        return {
            firstName: "",
            lastName: "",
            username: "",
            email: "",
            emailError: "",
            password: "",
            passwordError: "",
            confirmPassword: "",
            passwordMatchError: "",
            checkedStudent: "",
            checkedTutor: ""
        };
    },
    methods: {
        submitForm() {
            // Regex source: https://emaillistvalidation.com/blog/email-validation-in-javascript-using-regular-expressions-the-ultimate-guide/
            const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            // JavaScript regex test: https://www.w3schools.com/jsref/jsref_regexp_test.asp
            if (!(emailRegex.test(this.email))) {
                this.emailError = "Email is not in correct format. Please enter a valid email.";
            }
            else {
                this.emailError = "";
            }
            const validPassword = this.password && this.password.length >= 8;
            if (!validPassword) {
                this.passwordError = "Password must be at least 8 characters long. Please enter a new password.";
            }
            else {
                this.passwordError = "";
            }
            if (!(this.password == this.confirmPassword)) {
                this.passwordMatchError = "Passwords do not match. Please try again."
            }
            else {
                this.passwordMatchError = "";
            }
            if (!(this.emailError) && !(this.passwordError) && !(this.passwordMatchError)) {
                // router source: https://router.vuejs.org/guide/
                this.$router.push('/login');
            }
        },
    }
}    
</script>
