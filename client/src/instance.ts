import axios from "axios";
// TODO: use authHeader() instead once state management is fixed
const getHeader = () => {
    return { "Authorization": "Bearer " + localStorage.getItem("token") }
}
export const server = axios.create({
    baseURL: "http://localhost:5267/api/",
    headers: getHeader()
});