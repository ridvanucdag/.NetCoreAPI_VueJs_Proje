import Vue, { createApp } from "vue";
import App from "./App.vue";
import Register from "@/views/Register.vue";
import router from "./router";
import store from "./store";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import VueToast from "vue-toast-notification";
import "vue-toast-notification/dist/theme-default.css";
//import "vue-toast-notification/dist/theme-sugar.css";

import { BACKEND_URL } from "./config/index";
import axios from "axios";


axios.defaults.baseURL = BACKEND_URL;

var app;
if (store.getters["Auth/isLoggedIn"]) {
  const jwtToken = store.getters["Auth/token"];
  axios.defaults.headers.common["Authorization"] = `Bearer ${jwtToken}`;
}
  app = createApp(App);

axios.interceptors.response.use(
  function(response) {
    return response;
  },
  function(error) {
    if (error.response.status === 401) {
      /*this.$toast.warning(
        "Oturumunuz zaman aşımınına uğradı. Giriş yap ekranına yönlendirileceksiniz."
      );*/
      store.commit("Auth/destroyLogin");
      window.location.replace("/register");
    }
    return Promise.reject(error);
  }
);
app
  .use(router)
  .use(store)
  .use(VueToast, {
    position: "bottom-right",
    duration: 4000,
  })
  .mount("#app");
