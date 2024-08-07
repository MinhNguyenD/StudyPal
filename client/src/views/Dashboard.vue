<template>
  <div class="flex">
    <SideBar class="" />
    <div class=" flex w-full flex-1 px-10 pt-10 flex-col overflow-y-hidden gap-6">
      <div class="flex row ">
        <h4 class="font-bold">Welcome Back,</h4>
        <h4 class="text-blue pl-2 font-bold"> {{ username }}!</h4>
      </div>

      <div class="flex rounded-2xl gap-5">
        <div class="bg-zinc-100 p-5 rounded-2xl list-none flex flex-col flex-1  gap-5">
          <h5 class="font-bold">Study Groups</h5>

          <div v-if="Object.keys(filterScheduleByDate(schedules)).length != 0" class="px-2 max-h-56 overflow-y-scroll">
            <div v-for="(schedules, date) in filterScheduleByDate(schedules)" class="mb-4">
              <h6 class="bg-blue-500 text-white rounded-2xl p-2">{{ formatDate(new Date(date)) }}</h6>
              <li v-for="schedule in schedules" class="pt-1 pl-2">{{ formatScheduleItem(schedule.timeFrom,
                schedule.timeTo, schedule.courseId) }}</li>
            </div>
          </div>
          <h6 v-else class="m-auto my-20 text-center">You don't have any Study Group Sessions scheduled for this week.
          </h6>
        </div>
        <div class="bg-zinc-100 p-5 rounded-2xl list-none flex flex-col flex-1  gap-5">
          <h5 class="font-bold">Tutors</h5>

          <div v-if="Object.keys(filterScheduleByDate(tutorSchedules)).length != 0"
            class="px-2 max-h-56 overflow-y-scroll">
            <div v-for="(schedules, date) in filterScheduleByDate(schedules)" class="mb-4">
              <h6 class="bg-blue-500 text-white rounded-2xl p-2">{{ formatDate(new Date(date)) }}</h6>
              <li v-for="schedule in schedules" class="pt-1 pl-2">{{ formatScheduleItem(schedule.timeFrom,
                schedule.timeTo, schedule.courseId) }}</li>
            </div>
          </div>
          <h6 v-else class="m-auto my-20 text-center">You don't have any Tutor Sessions scheduled for this week.</h6>
        </div>
      </div>

      <div class="flex flex-col gap-5 bg-zinc-100 p-5 rounded-2xl">
        <h5 class="font-bold">Courses</h5>
        <div class="flex flex-row gap-5 overflow-x-scroll pb-2">
          <div v-if="Object.keys(courseIds).length != 0" v-for="course in courseIds"
            :style="{ backgroundColor: getCourseColor(course) }"
            class=" min-w-56 h-40 flex flex-col rounded-2xl hover:bg-gray-200">
            <RouterLink :to="'/course/' + course" class="block px-4 p-3 py-2 text-medium">
              <h5 class="">{{ course + ":" }}</h5>
              <h6 class="text-lg">{{ courses.find(val => val.courseCode === course)?.courseName }}</h6>
            </RouterLink>

          </div>
          <h6 v-else class="m-auto my-10 text-center">Schedule Tutor / Group Study Sessions to get started!</h6>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { useUserStore } from "@/store/user";
import SideBar from "../components/SideBar.vue";
import { User } from "@/models/user";
import axios from "axios";
import { ScheudleItem, Course } from "@/models/Schedule";

export default {
  data() {
    return {
      user: null as User | null,
      username: "",
      userId: "",
      schedules: [] as ScheudleItem[],
      tutorSchedules: [] as ScheudleItem[],
      courseIds: [] as string[],
      courses: [] as Course[]
    }
  },
  components: {
    SideBar,
  },
  setup() {
    const userStore = useUserStore();
    return {
      userStore,
    };
  },
  methods: {
    async fetchCourseDetails() {
      try {
        const response = await axios.get("api/course");
        this.courses = response.data;
      } catch (error) {
        console.log(error);
      }
    },
    async fetchAllGroupSessions(id: string) {
      try {
        const response = await axios.get("api/schedule/" + id);
        this.schedules = response.data;
        this.courseIds = this.filterUniqueCourses();
        this.fetchCourseDetails();
      } catch (error) {
        console.log(error);
      }
    },
    filterUniqueCourses() {
      const courseIds = this.schedules.map((course: ScheudleItem) => course.courseId);
      return [...new Set(courseIds)];
    },
    filterScheduleByCourse(id: string) {
      return this.schedules.filter((schedule: ScheudleItem) => schedule.courseId === id);
    },
    filterScheduleByDate(schedules: ScheudleItem[]) {
      const today = new Date();

      const filteredSchedules = schedules
        .filter((schedule: ScheudleItem) => new Date(schedule.timeFrom) >= today)
        .sort((a, b) => a.timeFrom - b.timeFrom);

      const groupedSchedules = filteredSchedules.reduce((groups: { [key: string]: ScheudleItem[] }, schedule: ScheudleItem) => {
        const date = new Date(schedule.timeFrom).toDateString();
        if (!groups[date]) {
          groups[date] = [];
        }
        groups[date].push(schedule);
        return groups;
      }, {});

      return groupedSchedules;
    },
    formatDate(date: Date) {
      const options: Intl.DateTimeFormatOptions = {
        weekday: 'long',
        month: 'long',
        day: 'numeric'
      };
      return date.toLocaleDateString('en-US', options);
    },
    formatTime(time: Date) {
      const date = new Date(time);
      const options: Intl.DateTimeFormatOptions = {
        weekday: 'long',
        month: 'long',
        day: 'numeric'
      };
      const formattedDate = date.toLocaleDateString('en-US', options);
      return formattedDate;
    },
    formatScheduleItem(from: number, to: number, course: string) {
      const fromDate = new Date(from);
      const toDate = new Date(to);

      const val = fromDate.getHours() + ":00 - " + toDate.getHours() + ":00 " + course;
      return val;
    },
    async fetchUser() {
      try {
        const response = await axios.get("update/User/" + this.username);
        this.userId = response.data.id;
        this.fetchAllGroupSessions(this.userId);
      } catch (error) {
        console.log(error);
      }
    },
    getCourseColor(courseCode: string) {
      if (courseCode.includes("CSCI")) {
        return "#F5D8A8";
      } else if (courseCode.includes("MATH")) {
        return "#B1E6FF";
      } else if (courseCode.includes("ASSC")) {
        return "#FFD8E1";
      } else if (courseCode.includes("ENGL")) {
        return "#E6FFB1";
      } else if (courseCode.includes("MGMT")) {
        return "#FFD8A8";
      } else if (courseCode.includes("MKTG")) {
        return "#D8B1FF";
      } else {
        return "#FFFFFF";
      }
    }

  },
  async mounted() {
    this.user = this.userStore.user;
    if (this.user?.username != null) {
      this.username = this.user.username;
      this.fetchUser();
    }
  }
};
</script>

<style scoped>
div::-webkit-scrollbar {
  width: 0.5em;
}

div::-webkit-scrollbar-thumb {
  background-color: gray;
  outline: #333333;
  border-radius: 25px;
}
</style>