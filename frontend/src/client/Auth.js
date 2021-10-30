import axios from "axios";
export default {
  register(data) {
    return axios.post(`authentication/register/`, data);
  },
  login(data) {
    return axios.post(`authentication/login/`, data);
  },
  sendMail(data) {
    return axios.post(`mail/send?mail=` + data);
  },
  resetPassword(data) {
    return axios.post(
      `authentication/resetPassword?mail=` +
        data.mail +
        `&password=` +
        data.password
    );
  },
};
