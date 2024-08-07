<template>
    <div class="flex">
        <SideBar />
        <div class="flex-1 p-10">
            <div class="px-4 flex items-center justify-between bg-white">
                <h2 class="text-3xl font-bold mb-4">Course List</h2>
                <button class="px-4 py-1 bg-primary text-white font-semibold rounded-full hover:bg-blue-700" @click="addCourse">Add Course</button>
            </div>
            <ul class="divide-y divide-gray-200">
                <li v-for="course in courses" :key="course.id" class="py-4">
                    <RouterLink
                        :to="'/course/' + course.courseCode.replaceAll(whitespace, replaced)"
                        class="block hover:bg-gray-100 px-4 py-2 rounded-md transition-colors duration-200 text-medium">
                        {{ course.courseCode }}: {{ course.courseName }}
                    </RouterLink>
                </li>
            </ul>
        </div>
    </div>
  <div
    v-if="isVisible"
    class="fixed inset-0 flex items-center justify-center z-50 bg-gray-900 bg-opacity-50"
  >
    <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-lg">
      <div class="flex justify-between items-center mb-4">
        <h2 class="text-2xl font-bold">Add New Course</h2>
        <button @click="closeModal" class="text-gray-500 hover:text-gray-700">
          &times;
        </button>
      </div>
      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label for="code" class="block text-gray-700">Course Code</label>
          <input
            type="text"
            id="code"
            v-model="courseCode"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:border-blue-500"
            required
          />
          <p
            class="mt-1 text-red-500 text-xs italic"
            v-if="courseCodeError.length > 0"
          >
            {{ courseCodeError }}
          </p>
        </div>
        <div class="mb-4">
          <label for="name" class="block text-gray-700">Course Name</label>
          <input
            type="text"
            id="name"
            v-model="courseName"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:border-blue-500"
            required
          />
        </div>
        <div class="flex justify-end">
          <button
            type="button"
            @click="closeModal"
            class="mr-2 px-4 py-2 bg-gray-500 text-white rounded-lg hover:bg-gray-600"
          >
            Cancel
          </button>
          <button
            type="submit"
            class="px-4 py-2 bg-primary text-white rounded-lg hover:bg-blue-700"
          >
            Submit
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
// TODO: this script should have used typesript <script lang="ts">
import SideBar from "../components/SideBar.vue"
import axios from 'axios'
import { useUserStore } from '@/store/user';
import { RouterLink } from "vue-router";
import { server } from '@/instance';
// because i cant put quotes in the html tag
const whitespace = " ";
const replaced = "";

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
      courses: [],
      isVisible: false,
      courseCode: "",
      courseName: "",
      courseCodeError: "",
    };
  },
  created() {
    this.getCourses();
  },
  methods: {
    async getCourses() {
      const currentUser = this.userStore.storedUser;

            try {
                const response = await server.get(`/api/course`);

                this.courses = response.data;
                console.log(this.courses);
            }
            catch (error) {
                console.log("Error fetching courses: ", error);
            }
        },
        addCourse() {
            this.isVisible = true;
        },
        async handleSubmit() {
            const currentUser = this.userStore.getUser;
            if (!this.validateForm()) {
                return;
            }
            try {
                const update = await server.post(`/api/course`,
                    {
                        code: this.courseCode,
                        name: this.courseName
                    }
                );
                alert("Add Course Successfully");
                this.getCourses();
                this.isVisible = false;
            }
            catch (error) {
                console.log("Error updating courses: ", error);
            }
        },
        validateForm() {
            const formRegex = /^[A-Za-z0-9 ]+$/;
            var formValidated = true;
            if(!(formRegex.test(this.courseCode))) {
                this.courseCodeError = "Code can only contain letters and numbers";
                formValidated = false;
            }
            else {
                this.courseCodeError = "";
            }
            if (!(this.course == null) && (this.courses.length > 0)) {
                this.courses.forEach(course => {
                    if (this.courseCode == course.courseCode) {
                        this.courseCodeError = "Course existed!";
                        formValidated = false;
                    }
                });
            }
            return formValidated;
        },
        closeModal() {
            this.isVisible = false;
        }
    }
}
</script>
