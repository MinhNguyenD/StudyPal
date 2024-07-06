import { useUserStore } from "@/store/user";

 export default function authHeader() {
  const userStore = useUserStore();
  let user = userStore.user;
  let token = userStore.token;
  if (user && token) {
    return {Authorization: 'Bearer ' + user.token};
  } else {
    return '';
  }
}