import axios from "axios"

export default {
    add(data) {
        return axios.post(`tweet/`, data);
    },
    getTweet(ofset, limit) {
        return axios.get(`tweet/myfollowed/` + ofset + `,` + limit);
    },
    getMyTweet(ofset, limit) {
        return axios.get(`tweet/myUserTweet/` + ofset + `,` + limit);
    },
    MyProfileUser(userName) {
        return axios.get(`profile/` + userName)
    },
    count(nick) {
        return axios.get(`tweet/count?nick=` + nick);
    },
    deleteTweet(id) {
        axios.delete(`tweet/` + id);
        return true;
    }
}