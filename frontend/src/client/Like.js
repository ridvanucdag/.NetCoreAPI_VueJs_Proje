import axios from "axios"

export default {
    toggleLike(data) {
        return axios.post(`Like/`, data);
    }

}