import { createStore } from "vuex";
import VuexPersistance from "vuex-persist";
import Counter from "@/store/modules/Counter";
import Auth from "./modules/Auth";

const vuexLocal = new VuexPersistance({
  storage: window.localStorage,
});

const store = createStore({
  modules: {
    Counter: {
      namespaced: true,
      ...Counter,
    },
    Auth: {
      namespaced: true,
      ...Auth,
    },
  },
  plugins: [vuexLocal.plugin],
});

export default store;
