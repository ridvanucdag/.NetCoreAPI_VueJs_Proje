<template>
  <div
    class="post profile"
    v-for="tweet in tweets"
    :key="tweet"
    @click="goToProfile(tweet)"
  >
    <div class="post__avatar">
      <img src="../../public/images/usericon.png" alt="" />
    </div>

    <div class="post__body">
      <div class="post__header">
        <div class="post__headerText">
          <h3>
            {{ tweet.user.name }} {{ tweet.user.surname }}
            <span class="post__headerSpecial"
              ><span class="material-icons post__badge"> verified </span>@{{
                tweet.user.nick
              }}</span
            >
          </h3>
        </div>
        <div class="post__headerDescription">
          <p>{{ tweet.content }}</p>
        </div>
      </div>

      <div class="post__footer">
        <span class="material-icons"> favorite_border </span>
      </div>
    </div>
  </div>

  <button
    v-if="isTweetSearchClicked && go == User"
    class="sidebar__tweet dahafazlayukle"
    @click="getSearchData(this.word)"
  >
    Daha Fazla Göster
  </button>
</template>

<script>
import SearchClient from "@/client/Search";
import router from "@/router";
export default {
  props: ["word", "go"],
  data() {
    return {
      tweets: [],
      ofset: 0,
      limit: 2,
      searchWord: this.word,
      isTweetSearchClicked: true,
    };
  },
  watch: {
    word(value) {
      this.searchWord = value;
      this.tweets = [];
      this.ofset = 0;
      this.limit = 2;
      this.isTweetSearchClicked = false;
      this.getSearchData(this.searchWord);
    },
  },
  methods: {
    getSearchData(searchWord) {
      if (searchWord != null) {
        SearchClient.Search(searchWord, this.ofset, this.limit)
          .then((res) => {
            if (res.data.length != 0) {
              res.data.filter((d) => {
                if (d.type == "Tweet") this.tweets.push(d.tweet);
                return d;
              });

              res.data.find((c) => c.type == "Tweet") != null
                ? (this.isTweetSearchClicked = true)
                : (this.isTweetSearchClicked = false);

              this.ofset += this.limit;
            } else {
              this.isTweetSearchClicked = false;
            }
          })
          .catch((er) => {
            //console.log(er);
          });
      }
    },
    goToProfile(tweet) {
      router.push("/profile/" + tweet.user.nick);
    },
  },
};
</script>
<style scoped>
.profile {
  cursor: pointer;
}
.more {
  margin: 2% 0% 0% 2%;
  width: 250px;
}
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
