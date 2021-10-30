<template>
  <div class="sidebar">
    <a href="/"> <i class="fab fa-twitter"></i></a>
    <router-link to="/"
      ><div class="sidebarOption" v-if="isLoggedIn">
        <span class="material-icons"> home </span>
        <h2>Ana Sayfa</h2>
      </div></router-link
    >
    <router-link to="/profile"
      ><div class="sidebarOption" v-if="isLoggedIn">
        <span class="material-icons"> perm_identity </span>
        <h2>Profil</h2>
      </div></router-link
    >
    <div class="sidebarOption" v-if="isLoggedIn" @click="logout">
      <span class="material-icons"> logout </span>
      <h2>Çıkış Yap</h2>
    </div>

    <button class="sidebar__tweet" v-if="isLoggedIn" @click="toggleModal">
      Tweet
    </button>

    <Modal @close="toggleModal" :modalActive="modalActive" />
  </div>
</template>
<script>
import { mapGetters } from "vuex";
import Modal from "@/components/Modal.vue";
import { ref } from "vue";
export default {
  computed: {
    ...mapGetters({
      isLoggedIn: "Auth/isLoggedIn",
    }),
  },
  components: {
    Modal,
  },
  setup() {
    const modalActive = ref(false);
    const toggleModal = () => {
      modalActive.value = !modalActive.value;
    };
    return { modalActive, toggleModal };
  },
  methods: {
    logout() {
      this.$store.dispatch("Auth/logout");
      this.$toast.success("Güle güle :)");
      setTimeout((location.href = "/register"), 60);
    },
  },
};
</script>
<style scoped>
.router-link {
  color: black;
}
</style>
