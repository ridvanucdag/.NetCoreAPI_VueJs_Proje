<template>
  <div class="feed">
    <nav>
      <div class="nav nav-tabs" id="nav-tab" role="tablist">
        <button
          :class="buttonAll"
          @click="gotoAll()"
          id="nav-all-tab"
          data-bs-toggle="tab"
          data-bs-target="#nav-home"
          type="button"
          role="tab"
          aria-controls="nav-home"
          aria-selected="true"
        >
          Tüm Arama
        </button>
        <button
          :class="buttonUsers"
          @click="gotoUser()"
          id="nav-users-tab"
          data-bs-toggle="tab"
          data-bs-target="#nav-users"
          type="button"
          role="tab"
          aria-controls="nav-users"
          aria-selected="false"
        >
          Kişiler
        </button>
        <button
          :class="buttonTweets"
          @click="gotoTweet()"
          id="nav-tweets-tab"
          data-bs-toggle="tab"
          data-bs-target="#nav-tweets"
          type="button"
          role="tab"
          aria-controls="nav-tweets"
          aria-selected="false"
        >
          Tweetler
        </button>
      </div>
    </nav>
    <div class="tab-content" id="nav-tabContent">
      <div
        :class="tabPanelAll"
        id="nav-home"
        role="tabpanel"
        aria-labelledby="nav-all-tab"
      >
        <SearchUserModal :word="word" go="User" />
        <button
          class="sidebar__tweet dahafazlayukle"
          @click="gotoUser()"
          v-if="word != null"
        >
          Daha Fazla Kullanıcı Göster
        </button>
        <hr v-if="word != null" />
        <SearchTweetModal :word="word" go="Tweet" />
        <button
          class="sidebar__tweet dahafazlayukle"
          @click="gotoTweet()"
          v-if="word != null"
        >
          Daha Fazla Tweet Göster
        </button>
        <hr v-if="word != null" />
      </div>
      <div
        :class="tabPanelUsers"
        id="nav-users"
        role="tabpanel"
        aria-labelledby="nav-users-tab"
      >
        <SearchUserModal :word="word" />
      </div>
      <div
        :class="tabPanelTweets"
        id="nav-tweets"
        role="tabpanel"
        aria-labelledby="nav-tweets-tab"
      >
        <SearchTweetModal :word="word" />
      </div>
    </div>
  </div>
</template>
<script>
import SearchUserModal from "@/components/SearchUserModal";
import SearchTweetModal from "@/components/SearchTweetModal";
import { ref } from "vue";

export default {
  components: {
    SearchUserModal,
    SearchTweetModal,
  },
  data() {
    return {
      word: null,
      tabPanelUsers: "tab-pane fade",
      tabPanelTweets: "tab-pane fade",
      tabPanelAll: "tab-pane fade active show",
      buttonAll: "nav-link active",
      buttonUsers: "nav-link",
      buttonTweets: "nav-link",
    };
  },
  beforeRouteUpdate(to, from, next) {
    this.word = to.params.word;
    this.tabPanelUsers = "tab-pane fade";
    this.tabPanelTweets = "tab-pane fade";
    this.tabPanelAll = "tab-pane fade active show";
    this.buttonAll = "nav-link active";
    this.buttonUsers = "nav-link";
    this.buttonTweets = "nav-link";
    if (this.word != null) {
      next();
    }
  },
  mounted() {
    this.word = this.$route.params.word;
  },
  methods: {
    gotoUser() {
      this.tabPanelUsers = "tab-pane fade active show";
      this.tabPanelAll = this.tabPanelTweets = "tab-pane fade";
      this.buttonUsers = "nav-link active";
      this.buttonAll = this.buttonTweets = "nav-link";
    },
    gotoTweet() {
      this.tabPanelTweets = "tab-pane fade active show";
      this.tabPanelAll = this.tabPanelUsers = "tab-pane fade";
      this.buttonTweets = "nav-link active";
      this.buttonAll = this.buttonUsers = "nav-link";
    },
    gotoAll() {
      this.tabPanelUsers = this.tabPanelTweets = "tab-pane fade";
      this.tabPanelAll = "tab-pane fade active show";
      this.buttonUsers = this.buttonTweets = "nav-link";
      this.buttonAll = "nav-link active";
    },
  },
};
</script>
<style>
.dahafazlayukle {
  background-color: #fff;
  border: none;
  color: black;
  font-weight: bold;
  text-decoration: none;
  display: inline-block;
  font-size: 15px;
  cursor: pointer;
  transition-duration: 0.4s;
}
.dahafazlayukle:hover {
  text-decoration: underline;
}
</style>
