<template>
  <div class="flex">
    <SideBar />
    <div class="flex-1 p-6 pl-8">
      <h2 class="text-3xl font-bold mb-7">Security Management</h2>
      <h5 class="font-medium">Change Password</h5>
      <form class="w-full max-w-lg mt-10" @submit.prevent="updatePassword">
        <div class="flex flex-wrap -mx-3 mb-5">
          <div class="w-full px-3">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="currentPassword"
            >
              Current Password
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="currentPassword"
              type="password"
              v-model="currentPassword"
              required
            />
          </div>
        </div>
        <div class="flex flex-wrap -mx-3 mb-5">
          <div class="w-full px-3">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="newPassword"
            >
              New Password
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="newPassword"
              type="password"
              v-model="newPassword"
              required
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="newPasswordError.length > 0"
            >
              {{ newPasswordError }}
            </p>
          </div>
        </div>
        <div class="flex flex-wrap -mx-3 mb-5">
          <div class="w-full px-3">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="confirmPassword"
            >
              Confirm Password
            </label>
            <input
              class="appearance-none block w-full h-10 bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="confirmPassword"
              type="password"
              v-model="confirmPassword"
              required
            />
            <p
              class="text-red-500 text-xs italic"
              v-if="passwordMatchError.length > 0"
            >
              {{ passwordMatchError }}
            </p>
          </div>
        </div>
        <button
          class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full focus:outline-none focus:shadow-outline"
          type="submit"
        >
          Edit
        </button>
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
      </div>
      <div class="mt-4">
        <p>Your password has been updated. Please sign in again</p>
      </div>
      <div class="mt-6 text-right">
        <button
          @click="goToLogin"
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
import { useUserStore } from "@/store/user";
import authHeader from "@/services/authHeader";
import axios from "axios";

export default {
  components: {
    SideBar,
  },
  setup() {
    const userStore = useUserStore();
    return {
      userStore,
    };
  },
  data() {
    return {
      currentPassword: "",
      newPassword: "",
      confirmPassword: "",
      currentPasswordError: "",
      newPasswordError: "",
      passwordMatchError: "",
      updateSuccessfully: false,
    };
  },
  methods: {
    async updatePassword() {
      if (!this.validateForm()) {
        return;
      }
      try {
        const currentUser = this.userStore.storedUser;
        const updatePassword = await axios.put(
          `update/user/password/${currentUser.username}`,
          {
            currentPassword: this.currentPassword,
            newPassword: this.newPassword,
            confirmPassword: this.confirmPassword,
          }
        );
        this.updateSuccessfully = true;
        return updatePassword.data;
      } catch (error) {
        console.log("Error updating password: ", error);
        alert("Change password failed. Please check if you have entered the correct password.");
      }
    },
    validateForm() {
      var formValidated = true;
      const passwordRegex = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W){8,}/;

      if (!passwordRegex.test(this.newPassword)) {
        this.newPasswordError =
          "Password must be at least 8 characters long and include at least one digit, 1 lowercase letter, 1 uppercase letter, and 1 special character";
        formValidated = false;
      } else {
        this.newPasswordError = "";
      }

      if (!(this.confirmPassword == this.newPassword)) {
        this.passwordMatchError = "The confirm password do not match.";
        formValidated = false;
      } else {
        this.passwordMatchError = "";
      }
      return formValidated;
    },
    goToLogin() {
      this.$router.push("/login");
    },
  },
};
</script>
