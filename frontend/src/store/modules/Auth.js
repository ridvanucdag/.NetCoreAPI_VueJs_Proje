import AuthClient from "@/client/Auth";
import router from "@/router/index";
import store from "@/store";

export default {
  state: () => ({
    token: null,
    user: null,
  }),
  mutations: {
    initLogin(state, { token, user }) {
      state.user = user;
      state.token = token;
    },
    destroyLogin(state) {
      state.user = null;
      state.token = null;
    },
  },
  getters: {
    token(state) {
      return state.token;
    },
    user(state) {
      return state.user;
    },
    isLoggedIn(state) {
      return !!state.token;
    },
  },
  actions: {
    async login({ state, commit }, data) {
      await AuthClient.login(data).then((response) => {
        commit("initLogin", response.data);
        location.href = "posts";
      });
    },
    logout({ commit }) {
      commit("destroyLogin");
    },
  },
};
