import { User } from "@/models/user";
import { useUserStore } from "@/store/user";

 export default function authHeader() {
  const userStore = useUserStore();
  let user : User | null = userStore.user;
  let token : string | null = !!user ? user.token : null;
  if (!!user && !!token) {
    return {Authorization: 'Bearer ' + user.token};
  } else {
    return '';
  }
}