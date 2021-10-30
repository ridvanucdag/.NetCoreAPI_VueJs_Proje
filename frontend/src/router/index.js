import {
  createRouter,
  createWebHistory
} from "vue-router";
import Posts from "../views/Post.vue";
import Register from "../views/Register.vue";
import Search from "../views/Search";
import Profile from "../views/Profile.vue";
import ResetPassword from "../views/ResetPassword.vue";

import store from "../store";

const routes = [{
    path: "/",
    name: "Home",
    component: Posts,
  },
  {
    path: "/profile/:userName",
    name: "Profile",
    component: Profile,
  },
  {
    path: "/profile",
    name: "Profilim",
    component: Profile,
  },
  {
    path: "/register",
    name: "Register",
    component: Register,
  },
  {
    path: "/posts",
    name: "Post",
    component: Posts,
  },
  {
    path: "/search/:word",
    name: "Search",
    component: Search,
  },
  {
    path: "/resetPassword/:mail",
    name: "ResetPassword",
    component: ResetPassword,
  },
];

const router = createRouter({
  mode: "history",
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  if (!store.getters["Auth/isLoggedIn"]) {
    if (to.name == "ResetPassword") next()
    else next();
  } else if (store.getters["Auth/isLoggedIn"] && to.name == "Register")
    next({
      name: "Post"
    });
  else next();
});
export default router;