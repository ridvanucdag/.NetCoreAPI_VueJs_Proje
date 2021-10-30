<template>
  <link
    rel="stylesheet"
    href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
  />
  <sidebar v-if="isLoggedIn"></sidebar>
  <router-view />
  <div class="widgets" v-if="isLoggedIn">
    <div class="widgets__input">
      <span class="material-icons widgets__searchIcon"> search </span>
      <input
        v-model.lazy="word"
        type="text"
        size="30"
        placeholder="Search Twitter"
        @keyup.enter="goToSearch"
      />
    </div>
  </div>
</template>

<script>
import "@/assets/global.css";
import sidebar from "@/components/Sidebar.vue";
import { mapGetters, mapActions, mapMutations } from "vuex";

export default {
  components: {
    sidebar,
  },
  computed: {
    ...mapGetters({
      aa: "Counter/count",
      isLoggedIn: "Auth/isLoggedIn",
    }),
  },
  data() {
    return {
      word: null,
    };
  },
  methods: {
    ...mapActions({
      addOne: "Counter/addOne",
    }),
    ...mapMutations({
      increment: "Counter/increment",
    }),
    goToSearch() {
      if (!!this.word) {
        this.$router.push({ name: "Search", params: { word: this.word } });
      }
    },
  },
  mounted() {
    //Direkt Erişim
    this.$store.dispatch("Counter/addOne");
    this.$store.commit("Counter/increment");

    //Maping örneği
    this.addOne();
    this.increment();
  },
};
</script>
<style scoped></style>
