<template>
  <div
    class="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8"
  >
    <div class="sm:mx-auto sm:w-full sm:max-w-sm">
      <!-- <img class="mx-auto h-10 w-auto" src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                alt="StudyPal" /> -->
      <h2
        class="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900"
      >
        Register new account
      </h2>
    </div>

    <div class="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
      <form id="registerForm" @submit.prevent="handleSubmit" class="space-y-6">
        <div class="flex items-center justify-between">
          <div>
            <label
              for="firstName"
              class="block text-base font-medium leading-6 text-gray-900"
              >First name</label
            >
            <input
              v-model="form.firstName"
              id="firstName"
              name="firstName"
              type="text"
              :class="{
                'ring-gray-300': !errors.firstName,
                'ring-red-600': errors.firstName,
                'focus:ring-red-600': errors.firstName,
              }"
              class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
            />
            <p
              :class="{
                'min-h-6': errors.lastName,
              }"
              class="text-sm text-red-500"
            >
              <span v-if="errors.firstName">
                {{ errors.firstName }}
              </span>
            </p>
          </div>
          <div>
            <label
              for="lastName"
              class="block text-base font-medium leading-6 text-gray-900"
              >Last name</label
            >
            <input
              v-model="form.lastName"
              id="lastName"
              name="lastName"
              type="text"
              :class="{
                'ring-gray-300': !errors.lastName,
                'ring-red-600': errors.lastName,
                'focus:ring-red-600': errors.lastName,
              }"
              class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
            />
            <p
              :class="{
                'min-h-6': errors.firstName,
              }"
              class="text-sm text-red-500"
            >
              <span v-if="errors.lastName">
                {{ errors.lastName }}
              </span>
            </p>
          </div>
        </div>
        <div>
          <label
            for="username"
            class="block text-base font-medium leading-6 text-gray-900"
            >Username</label
          >
          <input
            v-model="form.username"
            id="username"
            name="username"
            type="text"
            :class="{
              'ring-gray-300': !errors.username,
              'ring-red-600': errors.username,
              'focus:ring-red-600': errors.username,
            }"
            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
          />
          <p class="text-sm text-red-500">
            <span v-if="errors.username">
              {{ errors.username }}
            </span>
          </p>
        </div>
        <div>
          <label
            for="email"
            class="block text-base font-medium leading-6 text-gray-900"
            >Email address</label
          >
          <input
            v-model="form.email"
            id="email"
            name="email"
            type="email"
            autocomplete="email"
            :class="{
              'ring-gray-300': !errors.email,
              'ring-red-600': errors.email,
              'focus:ring-red-600': errors.email,
            }"
            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
          />
          <p class="text-sm text-red-500">
            <span v-if="errors.email">
              {{ errors.email }}
            </span>
          </p>
        </div>
        <div>
          <label
            for="password"
            class="block text-base font-medium leading-6 text-gray-900"
            >Password</label
          >
          <input
            v-model="form.password"
            id="password"
            name="password"
            type="password"
            autocomplete="current-password"
            :class="{
              'ring-gray-300': !errors.password,
              'ring-red-600': errors.password,
              'focus:ring-red-600': errors.password,
            }"
            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
          />
          <p class="text-sm text-red-500">
            <span v-if="errors.password">
              {{ errors.password }}
            </span>
          </p>
        </div>
        <div>
          <label
            for="confirmPassword"
            class="block text-base font-medium leading-6 text-gray-900"
            >Comfirm password</label
          >
          <input
            v-model="form.confirmPassword"
            id="confirmPassword"
            name="confirmPassword"
            type="password"
            :class="{
              'ring-gray-300': !errors.confirmPassword,
              'ring-red-600': errors.confirmPassword,
              'focus:ring-red-600': errors.confirmPassword,
            }"
            class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
          />
          <p class="text-sm text-red-500">
            <span v-if="errors.confirmPassword">
              {{ errors.confirmPassword }}
            </span>
          </p>
        </div>
        <div>
          <label class="block text-base font-medium leading-6 text-gray-900"
            >Roles</label
          >
          <div class="flex flex-wrap">
            <div class="flex items-center me-10">
              <input
                v-model="form.checkedRoles"
                value="Student"
                :class="{
                  'border-gray-300': !errors.roles,
                  'border-red-600': errors.roles,
                }"
                class="w-5 h-5 rounded focus:ring-primary"
                type="checkbox"
                id="student"
              />
              <label class="text-sm ms-2 font-base" for="student"
                >Student</label
              >
            </div>
            <div class="flex items-center">
              <input
                v-model="form.checkedRoles"
                value="Tutor"
                :class="{
                  'border-gray-300': !errors.roles,
                  'border-red-600': errors.roles,
                }"
                class="w-5 h-5 border-gray-300 rounded focus:ring-primary"
                type="checkbox"
                id="tutor"
              />
              <label class="text-sm ms-2 font-base" for="tutor">Tutor</label>
            </div>
          </div>
          <p class="text-sm text-red-500">
            <span v-if="errors.roles">
              {{ errors.roles }}
            </span>
          </p>
        </div>

        <div>
          <button
            type="submit"
            class="flex w-full justify-center rounded-md bg-primary px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
          >
            Register
          </button>
        </div>
      </form>

      <p class="mt-10 text-center text-sm text-gray-500">
        Already have an account?
        {{ " " }}
        <RouterLink
          to="/login"
          class="font-semibold leading-6 text-primary hover:text-indigo-500"
          >Login to your account
        </RouterLink>
      </p>
    </div>
  </div>
</template>

<script>
import { useUserStore } from "@/store/user";
import axios from "axios";
import { reactive } from "vue";

export default {
  name: "Register",
  setup() {
    const userStore = useUserStore();
    const form = reactive({
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
      checkedRoles: [],
    });
    const errors = reactive({
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
      roles: "",
    });
    return { userStore, form, errors };
  },
  methods: {
    async handleSubmit() {
      if (!this.validateForm()) {
        return;
      }
      try {
        await this.userStore.register(
          this.form.firstName,
          this.form.lastName,
          this.form.username,
          this.form.email,
          this.form.checkedRoles,
          this.form.password
        );

        this.createUserProfile();
        this.$router.push("/dashboard");
      } catch (error) {
        if (error.response && error.response.data) {
          for (const e of error.response.data) {
            if (e.code === "DuplicateUserName") {
              this.errors.username = e.description;
            }
            if (e.code === "DuplicateEmail") {
              this.errors.email = e.description;
            }
          }
        }
      }
    },
    async createUserProfile() {
      try {
        await axios.post("api/UserProfile/add", {
          firstname: this.form.firstName, 
          lastname: this.form.lastName,
          username: this.form.username,
          email: this.form.email
        });
      } catch (error) {
        console.log(error);
      }
    },

    validateForm() {
      this.errors.firstName = "";
      this.errors.lastName = "";
      this.errors.username = "";
      this.errors.email = "";
      this.errors.password = "";
      this.errors.confirmPassword = "";
      this.errors.roles = "";

      const firstNameRegEx = /^[a-zA-Z\s]+$/;
      const lastNameRegEx = /^[a-zA-Z-\']+$/;
      const emailRegEx = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$/;
      const passwordRegEx = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W){8,}/;
      let validated = true;
      if (!this.form.firstName) {
        this.errors.firstName = "First name is required";
        validated = false;
      } else if (!firstNameRegEx.test(this.form.firstName)) {
        this.errors.firstName = "First name is invalid";
        validated = false;
      }

      if (!this.form.lastName) {
        this.errors.lastName = "Last name is required";
        validated = false;
      } else if (!lastNameRegEx.test(this.form.lastName)) {
        this.errors.lastName = "Last name is invalid";
        validated = false;
      }

      if (!this.form.username) {
        this.errors.username = "Username is required";
        validated = false;
      }

      if (!this.form.email) {
        this.errors.email = "Email is required";
        validated = false;
      } else if (!emailRegEx.test(this.form.email)) {
        this.errors.email = "Email is invalid";
        validated = false;
      }

      if (!this.form.password) {
        this.errors.password = "Password is required";
        validated = false;
      } else if (!passwordRegEx.test(this.form.password)) {
        validated = false;
        this.errors.password = `Password must be at least 8 characters long and include at least 1 digit, 1 lowercase letter, 1 uppercase letter, and 1 special character`;
      }

      if (
        !this.form.confirmPassword ||
        this.form.confirmPassword !== this.form.password
      ) {
        this.errors.confirmPassword = "The confirm password does not match";
        validated = false;
      }

      if (this.form.checkedRoles.length == 0) {
        this.errors.roles = "At least one role is required";
        validated = false;
      }

      return validated;
    },
  },
};
</script>

<style></style>
