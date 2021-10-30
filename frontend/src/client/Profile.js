import axios from "axios";
export default {
  MyProfileUser(userName) {
    return axios.get(`profile/` + userName);
  },
  MyProfileUserTweet(userName, ofset, limit) {
    return axios.get(`profile/` + userName + `,` + ofset + `,` + limit);
  },
};
