import { User } from "@/models/user";
import { defineStore } from "pinia";
import router from "../router";

interface AuthState {
  user: User | null;
}

export const useUserStore = defineStore("user", {
  state: (): AuthState => ({ user: null }),
  actions: {
    register(user: User) {
      this.user = user;
    },
    login(user: User) {
      this.user = user;
    },
    logout() {
      localStorage.removeItem("token");
      this.user = null;
      router.push("/login");
    },
  },

  getters: {
    getUser: (state): User | null => state.user,
    isAuthenticated: (state): boolean => !!state.user,
  },
});
