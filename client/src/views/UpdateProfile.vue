<template>
    <div class="flex">
        <SideBar />
        <!-- Main content -->
        <div class="flex-1 p-6 pl-8">
            <h2 class="text-3xl font-bold mb-4">Edit Profile</h2>
            <div class="flex items-center">
                <div class="relative">
                    <div class="w-20 h-20 rounded-full flex items-center justify-center">
                        <img :src="ProfilePic" alt="Profile Picture" />
                    </div>
                </div>
                <div class="ml-5">
                    <h2 class="text-lg font-bold">Username</h2>
                    <button class="mt-2 px-3 py-2 bg-blue-500 text-white rounded-full flex items-center space-x-1">
                        <svg class="mb-1"
                            xmlns="http://www.w3.org/2000/svg"
                            width="19"
                            height="19"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            >
                            <path d="M23 19a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h4l2-3h6l2 3h4a2 2 0 0 1 2 2z" />
                            <circle cx="12" cy="13" r="4" />
                        </svg>
                        <span class="pl-2">Edit Photo</span>
                    </button>
                </div>
            </div>
            <form class="w-full max-w-lg mt-10" @submit.prevent="submitForm">
                <div class="flex flex-wrap -mx-3 mb-5">
                    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="firstname">
                            First Name
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="firstName" type="text" v-model="firstName" />
                    </div>
                    <div class="w-full md:w-1/2 px-3">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="lastname">
                            Last Name
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="lastName" type="text" v-model="lastName" />
                    </div>
                </div>
                <div class="flex flex-wrap -mx-3 mb-5">
                    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="university">
                            University
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="university" type="text" v-model="university" />
                    </div>
                    <div class="w-full md:w-1/2 px-3">
                        <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="major">
                            Major
                        </label>
                        <input class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="major" type="text" v-model="major" />
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

                <div class="flex flex-wrap mt-10">
                    <div class="flex items-center me-5">
                        <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full focus:outline-none focus:shadow-outline" type="submit">
                            Save Changes
                        </button>
                    </div>
                    <div class="flex items-center">
                        <button class="hover:bg-red-500 hover:text-white text-red-500 border-2 border-red-500 font-bold py-2 px-4 rounded-full focus:outline-none focus:shadow-outline" type="submit">
                            Delete Profile
                        </button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
import SideBar from '../components/SideBar.vue';
import ProfilePic from '../assets/avatar.png';

export default {
    components: {
        SideBar
    },
    data() {
        return {
            ProfilePic,
            firstName: "",
            lastName: "",
            university: "",
            major: "",
            checkedStudent: "",
            checkedTutor: ""
        }
    }
}
</script>