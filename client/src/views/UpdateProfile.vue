<template>
  <div class="flex">
    <SideBar />
    <!-- Main content -->
    <div v-if="currentUser" class="flex-1 p-10">
      <h2 class="text-3xl font-bold mb-4">Edit Profile</h2>
      <div class="flex items-center">
        <div class="relative">
          <div class="w-20 h-20 rounded-full flex items-center justify-center">
            <img :src="ProfilePic" alt="Profile Picture" />
          </div>
        </div>
        <div class="ml-5">
          <h2 class="text-lg font-bold">{{ currentUser.username }}</h2>
          <h2 class="text-lg">{{ currentUser.email }}</h2>
          <h2 class="text-sm italic" v-if="roles.length < 2">{{ roles[0] }}</h2>
          <h2 class="text-sm italic" v-if="roles.length == 2">{{ roles[0] }}/{{ roles[1] }}</h2>
        </div>
      </div>
      <form class="w-full max-w-lg mt-10" @submit.prevent="updateProfile">
        <div class="flex flex-wrap -mx-3 mb-5">
          <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="firstname"
            >
              First Name
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white"
              id="firstName"
              type="text"
              v-model="user.firstName"
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="user.firstnameError.length > 0"
            >
              {{ user.firstnameError }}
            </p>
          </div>
          <div class="w-full md:w-1/2 px-3">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="lastname"
            >
              Last Name
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="lastName"
              type="text"
              v-model="user.lastName"
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="user.lastnameError.length > 0"
            >
              {{ user.lastnameError }}
            </p>
          </div>
        </div>
        <div class="flex flex-wrap -mx-3 mb-5">
          <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="university"
            >
              University
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white"
              id="university"
              type="text"
              v-model="user.university"
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="user.universityError.length > 0"
            >
              {{ user.universityError }}
            </p>
          </div>
          <div class="w-full md:w-1/2 px-3">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="major"
            >
              Major
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="major"
              type="text"
              v-model="user.major"
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="user.majorError.length > 0"
            >
              {{ user.majorError }}
            </p>
          </div>
        </div>
        <div
          class="uppercase tracking-wide text-gray-700 text-xs font-bold mb-3"
        >
          Choose Role (Choose both if you want to have both roles):
        </div>
        <div class="flex flex-wrap">
          <div class="flex items-center me-10">
            <input
              class="w-4 h-4 text-blue-600 bg-gray-200 border-gray-300 rounded focus:ring-blue-500"
              type="checkbox"
              id="student"
              value="Student"
              v-model="user.checkedRoles"
            />
            <label class="ms-2 text-base font-medium" for="student"
              >Student</label
            >
          </div>
          <div class="flex items-center">
            <input
              class="w-4 h-4 text-blue-600 bg-gray-200 border-gray-300 rounded focus:ring-blue-500"
              type="checkbox"
              id="tutor"
              value="Tutor"
              v-model="user.checkedRoles"
            />
            <label class="ms-2 text-base font-medium" for="tutor">Tutor</label>
          </div>
        </div>
        <p
          class="text-red-500 text-xs italic"
          v-if="user.rolesError.length > 0"
        >
          {{ user.rolesError }}
        </p>

        <div class="flex flex-wrap mt-10">
          <div class="flex items-center me-5">
            <button
              class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full focus:outline-none focus:shadow-outline"
              type="submit"
            >
              Save Changes
            </button>
          </div>
          <div class="flex items-center">
            <button
              class="hover:bg-red-500 hover:text-white text-red-500 border-2 border-red-500 font-bold py-2 px-4 rounded-full focus:outline-none focus:shadow-outline"
            >
              Delete Profile
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
  <div
    v-if="updateSuccessfully"
    class="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50"
  >
    <div class="bg-white rounded-lg p-6 w-96">
      <div class="flex justify-between items-center border-b pb-3">
        <h3 class="text-lg font-medium text-gray-900">Success</h3>
        <button @click="hideModal" class="text-gray-500 hover:text-gray-700">
          &times;
        </button>
      </div>
      <div class="mt-4">
        <p>Your information has been updated.</p>
      </div>
      <div class="mt-6 text-right">
        <button
          @click="hideModal"
          class="bg-primary text-white py-2 px-4 rounded hover:bg-blue-700"
        >
          OK
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import SideBar from "../components/SideBar.vue";
import ProfilePic from "../assets/avatar.png";
import { useUserStore } from "@/store/user";
import authHeader from "@/services/authHeader";
import axios from "axios";
import { ref } from 'vue';

export default {
  components: {
    SideBar,
  },
  setup() {
    const userStore = useUserStore();
    const currentUser = userStore.storedUser;
    const roles = ref([]);

    const fetchRoles = async () => {
      try {
        const response = await axios.get(`update/user/role/${currentUser.username}`);
        roles.value = response.data;
      }
      catch(error) {
        console.error("Error fetching roles: ", error);
      }
    };

    fetchRoles();

    return {
      currentUser,
      roles
    };
  },
  data() {
    return {
      ProfilePic,
      user: {
        firstName: "",
        lastName: "",
        university: "",
        major: "",
        checkedRoles: [],
        firstnameError: "",
        lastnameError: "",
        universityError: "",
        majorError: "",
        rolesError: ""
      },
      updateSuccessfully: false,
    };
  },
  methods: {
    async updateProfile() {
      if (!this.validateForm()) {
        return;
      }
      if (this.user.firstName == "") {
        this.user.firstName = this.currentUser.firstName;
        console.log(this.user.firstName);
      }
      if (this.user.lastName == "") {
        this.user.lastName = this.currentUser.lastName;
        console.log(this.user.lastName);
      }

      try {
        const update = await axios.put(`update/user/${this.currentUser.username}`, {
          firstname: this.user.firstName,
          lastname: this.user.lastName,
          university: this.user.university,
          major: this.user.major,
          roles: this.user.checkedRoles,
        });
        this.updateSuccessfully = true;
      } catch (error) {
        console.log("Error:", error);
      }
    },
    validateForm() {
      const fieldRegex = /^[A-Za-z ]+$/;
      var formValidated = true;
      if (!(this.user.firstName == "")) {
        if (!fieldRegex.test(this.user.firstName)) {
          this.user.firstnameError = "First name should contain only letters.";
          formValidated = false;
        } else {
          this.user.firstnameError = "";
        }
      }
      if (!(this.user.lastName == "")) {
        if (!fieldRegex.test(this.user.lastName)) {
          this.user.lastnameError = "Last name should contain only letters.";
          formValidated = false;
        } else {
          this.user.lastnameError = "";
        }
      }
      if (!(this.user.university == "")) {
        if (!fieldRegex.test(this.user.university)) {
          this.user.universityError = "University should contain only letters.";
          formValidated = false;
        } else {
          this.user.universityError = "";
        }
      }
      if (!(this.user.major == "")) {
        if (!fieldRegex.test(this.user.major)) {
          this.user.majorError = "Major should contain only letters.";
          formValidated = false;
        } else {
          this.user.majorError = "";
        }
      }

      if (!(this.user.checkedRoles == [])) {
        if (this.user.checkedRoles.length == this.roles.length) {
          if (this.roles.length == 2) {
            this.user.rolesError = "You have been assigned to both roles";
            formValidated = false;
          }
          else {
            if (this.user.checkedRoles[0] == this.roles[0]) {
              this.user.rolesError = "You have been assigned to this role";
              formValidated = false;
            }
          }
        }
        else {
          this.user.rolesError = "";
        }
      }
      return formValidated;
    },
    hideModal() {
      window.location.reload();
      this.updateSuccessfully = false;
    },
  },
};
</script>