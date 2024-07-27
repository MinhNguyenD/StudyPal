import axios from "axios";
// TODO: use authHeader() instead once state management is fixed
const getHeader = () => {
    return { "Authorization": "Bearer " + localStorage.getItem("token") }
}

export const server = axios.create({
    baseURL: "https://csci4177grp-20-1.onrender.com/api",
    headers: getHeader()
});
