import { User } from "@/models/user";
import { defineStore } from "pinia";
import router from "../router";
import axios from "axios";

interface AuthState {
  user: User | null;
  token: string | null;
}

export const useUserStore = defineStore("user", {
  state: (): AuthState => ({ user: null, token: null }),
  actions: {
    async register(
      firstName: string,
      lastName: string,
      username: string,
      email: string,
      checkedRoles: Array<string>,
      password: string
    ) {
      const response = await axios.post("auth/register", {
        firstname: firstName,
        lastname: lastName,
        username: username,
        email: email,
        roles: checkedRoles,
        password: password,
      });
      this.user = response.data;
      this.token = response.data.token;
      localStorage.setItem("token", this.token!);
      localStorage.setItem("user", JSON.stringify(this.user));
    },
    async login(email: string, password: string) {
      const response = await axios.post("auth/login", {
        email: email,
        password: password,
      });
      this.user = response.data;
      console.log(this.user);
      this.token = response.data.token;
      localStorage.setItem("token", this.token!);
      localStorage.setItem("user", JSON.stringify(this.user));
    },
    logout() {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      this.user = null;
      this.token = null;
      router.push("/login");
    },
  },
  getters: {
    storedUser: (state): User | null => {
      if (!state.user) {
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("token");
        if (user && token) {
          state.user = user ? (JSON.parse(user) as User) : null;
          state.token = token;
        }
      }
      return state.user;
    },
    storedToken: (state): string | null => {
      if (!state.token) {
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("token");
        if (user && token) {
          state.user = user ? (JSON.parse(user) as User) : null;
          state.token = token;
        }
      }
      return state.token;
    },
  },
});
