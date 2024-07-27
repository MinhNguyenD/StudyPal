import { createRouter, createWebHistory } from "vue-router";
import Contact from "./components/contact/Contact.vue";
import Homepage from "./components/Homepage.vue";
import FAQ from "./views/FAQ.vue";
import Login from "./views/Login.vue";
import Register from "./views/Register.vue";
import Dashboard from "./views/Dashboard.vue";
import { useUserStore } from "./store/user";
import Course from "./components/course/Course.vue";
import UpdateProfile from "./views/UpdateProfile.vue";
import UpdateSecurity from "./views/UpdateSecurity.vue";
import CourseList from "./views/Courses.vue";
import MainChat from "./components/chats/MainChat.vue";

const routes = [
  {
    path: "/",
    component: Homepage,
  },
  {
    path: "/faq",
    component: FAQ,
  },
  {
    path: "/login",
    component: Login,
  },
  {
    path: "/register",
    component: Register,
  },
  {
    path: "/contact",
    component: Contact,
  },
  {
    path: "/dashboard",
    component: Dashboard,
  },
  {
    path: "/course/:id",
    component: Course,
  },
  {
    path: "/profile",
    component: UpdateProfile,
  },
  {
    path: "/chats",
    component: MainChat,
  },
  {
    path: "/security",
    component: UpdateSecurity,
  },
  {
    path: "/courses",
    component: CourseList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to) => {
  console.log(to);
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = [
    "/login",
    "/faq",
    "/register",
    "/contact",
    "/",
  ]; // temp until redirect is fixed
  const authRequired =
    !publicPages.includes(to.path);
  const auth = useUserStore();

  if (authRequired && !auth.storedUser) {
    return "/login";
  }
});

export default router;
