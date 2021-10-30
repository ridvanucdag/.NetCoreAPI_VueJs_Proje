<template>
  <div class="feed">
    <div>
      <h3 class="feed__header">Kişiler</h3>
      <hr />
      <div class="post" v-for="user in users" :key="user" @click="goToProfile">
        <div class="post__avatar">
          <img src="../../public/images/usericon.png" alt="" />
        </div>

        <div class="post__body">
          <div class="post__header">
            <br />
            <div class="post__headerDescription">
              <h3>
                {{ user.name }} {{ user.surName }}
                <span class="post__headerSpecial"
                  ><span class="material-icons post__badge"> verified </span>@{{
                    user.nick
                  }}</span
                >
              </h3>
            </div>
          </div>
        </div>
      </div>
      <button
        v-if="!isUserSearchClicked"
        class="tweetBox__tweetButton"
        style="width: 250px; height: 50px; margin-top: 1%"
        @click="getSearchData(this.word, 'User', this.UserOfset, this.Limit)"
      >
        Daha Fazla Göster
      </button>
    </div>
    <br />
    <div>
      <h3 class="feed__header">Tweetler</h3>
      <hr />
      <div class="post" v-for="tweet in tweets" :key="tweet">
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
        v-if="!isTweetSearchClicked"
        class="tweetBox__tweetButton"
        style="width: 250px; height: 50px; margin-top: 1%"
        @click="getSearchData(this.word, 'Tweet', this.TweetOfset, this.Limit)"
      >
        Daha Fazla Göster
      </button>
    </div>
  </div>
</template>
<script>
import SearchClient from "@/client/Search";
import router from "@/router";
export default {
  data() {
    return {
      users: [],
      tweets: [],
      word: null,
      Ofset: 0,
      Limit: 2,
      TweetOfset: 2,
      UserOfset: 2,
      isTweetSearchClicked: false,
      isUserSearchClicked: false,
      isMoreButtonClicked: false,
    };
  },
  beforeRouteUpdate(to, from, next) {
    this.word = to.params.word;
    this.users = [];
    this.tweets = [];
    this.Ofset = 0;
    this.Limit = 2;
    this.TweetOfset = 2;
    this.UserOfset = 2;
    this.isTweetSearchClicked = false;
    this.isUserSearchClicked = false;
    this.isMoreButtonClicked = false;
    this.getSearchData(this.word, null, this.Ofset, this.Limit);
    next();
  },
  mounted() {
    this.word = this.$route.params.word;
    this.getSearchData(this.word, null, this.Ofset, this.Limit);
  },
  methods: {
    getSearchData(searchWord, type, ofset, limit) {
      SearchClient.Search(searchWord, ofset, limit)
        .then((res) => {
          if (res.data.length != 0) {
            this.isMoreButtonClicked =
              type != null ? !res.data.find((c) => c.type == type) : null;

            res.data.filter((d) => {
              if (
                (d.type == "User" && type == "User") ||
                (d.type == "User" && type == null)
              ) {
                this.users.push(d.user);
              } else if (
                (d.type == "Tweet" && type == "Tweet") ||
                (d.type == "Tweet" && type == null)
              ) {
                this.tweets.push(d.tweet);
              }
              return d;
            });

            if (type == "Tweet") {
              this.TweetOfset += this.Limit;
              this.isMoreButtonClicked == true
                ? ((this.isTweetSearchClicked = true),
                  (this.isMoreButtonClicked = false),
                  alert("Sonuç bulunamadı."))
                : (this.isTweetSearchClicked = false);
            } else if (type == "User") {
              this.UserOfset += this.Limit;
              this.isMoreButtonClicked == true
                ? ((this.isUserSearchClicked = true),
                  (this.isMoreButtonClicked = false),
                  alert("Sonuç bulunamadı."))
                : (this.isUserSearchClicked = false);
            } else {
              this.Ofset += this.Limit;
            }
          } else {
            if (type == "Tweet") {
              this.isTweetSearchClicked = true;
            } else if (type == "User") {
              this.isUserSearchClicked = true;
            } else {
              this.isUserSearchClicked = true;
              this.isTweetSearchClicked = true;
            }
            alert("Sonuç bulunamadı.");
          }
        })
        .catch((er) => {
          //console.log(er);
        });
    },
    goToProfile() {
      router.push("/");
    },
  },
};
</script>
