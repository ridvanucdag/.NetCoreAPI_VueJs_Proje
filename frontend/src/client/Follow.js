import axios from "axios"

export default {
    followed(data) {
        return axios.post(`follow/`, data);
    },
    doesItFollow(data){
        return axios.post(`follow/doesitfollow/`,data);
    },
    followers(data){
        return axios.get(`follow/Followers?nick=${data.nick}`);
    },
    followings(data){
        return axios.get(`follow/Followings?nick=${data.nick}`);
    }
   

}
