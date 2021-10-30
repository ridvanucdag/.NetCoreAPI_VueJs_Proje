<template>
  <LikeModal
    :isLikeClicked="isLikeClicked"
    :data="this.LikesByUser"
    @close="openLikeModal"
  />
  <RemoveModal
    :isRemoveClicked="isRemoveClicked"
    :data="this.deletedId"
    @close="tRemoveModal"
  />
  <!-- feed starts -->
  <div class="feed">
    <div class="feed__header">
      <h2>Ana Sayfa</h2>
    </div>

    <!-- tweetbox starts -->
    <div class="tweetBox">
      <form id="tweetForm" @submit.prevent="addTweet">
        <div class="tweetbox__input">
          <img src="../../public/images/usericon.png" alt="" />
          &nbsp;&nbsp;&nbsp;<textarea
            v-model="tweet.content"
            placeholder="Ne oluyor?"
            rows="4"
            cols="77"
          />
        </div>
        <span v-if="v$.tweet.content.$error" class="errorSpan">
          {{ v$.tweet.content.$errors[0].$message }}
        </span>
        <br />
        <button class="tweetBox__tweetButton" type="submit">Tweet</button>
      </form>
    </div>
    <!-- tweetbox ends -->

    <!-- post starts -->
    <div class="post" v-for="item in list" :key="item.id">
      <div class="post__avatar">
        <img src="../../public/images/usericon.png" alt="" />
      </div>

      <div class="post__body">
        <div class="post__header">
          <div class="post__headerText">
            <h3>
              <a :href="/profile/ + item.user.nick"
                ><button @click="MyProfileUsernamePage" class="Usernamelink">
                  {{ item.user.name }} {{ item.user.surName }}
                </button></a
              >
              <span class="post__headerSpecial"
                ><span class="material-icons post__badge"> verified </span> @{{
                  item.user.nick
                }}
                || {{ moment(item.at).format("LLL") }}</span
              >
              <span
                v-if="checkRemoveControl(item)"
                class="material-icons md-20"
                @click="tRemoveModal(item)"
                >delete_forever
              </span>
            </h3>
          </div>

          <div class="post__headerDescription">
            <p>{{ item.content }}</p>
          </div>
        </div>

        <div class="like">
          <div
            :id="'TW-' + item.id"
            @click="likeClick(item, $event)"
            :class="{ heart: heart, is_animating: checkLike(item, item.like) }"
          ></div>

          <a class="likeCount">
            <div @click="openLikeModal(item.likes)">
              <b> {{ item.likeCount }} </b>
            </div>
          </a>
        </div>
      </div>
    </div>
    <!-- post ends -->
    <button
      v-if="checkTweet"
      @click="gettweet"
      class="sidebar__tweet dahafazlayukle"
    >
      Daha Fazla Yükle...
    </button>
  </div>
</template>

<script>
import LikeModal from "@/components/LikeModal";
import RemoveModal from "@/components/RemoveModal";
import TweetClient from "@/client/Tweet.js";
import LikeClient from "@/client/Like.js";
import useVuelidate from "@vuelidate/core";
import { maxLength, helpers } from "@vuelidate/validators";
import UserProfileClient from "@/client/Profile.js";
import moment from "moment";
import { ref } from "vue";
export default {
  data() {
    return {
      tweet: {
        content: "",
      },
      like: {
        userID: null,
        tweetID: null,
      },
      moment: moment,
      userName: null,
      list: [],
      LikesByUser: [],
      ofset: 0,
      limit: 3,
      checkTweet: true,
      heart: true,
      is_animating: false,
      deletedId: null,
      isRemoveClicked: false,
    };
  },
  components: {
    LikeModal,
    RemoveModal,
  },
  setup() {
    const isLikeClicked = ref(false);

    return { v$: useVuelidate(), isLikeClicked };
  },

  validations: {
    tweet: {
      content: {
        maxLength: helpers.withMessage(
          "Tweetinizin uzunluğu en fazla 200 karakter olmalıdır.",
          maxLength(200)
        ),
      },
    },
  },
  methods: {
    tRemoveModal(tweet) {
      this.isRemoveClicked = !this.isRemoveClicked;
      if (this.isRemoveClicked) this.deletedId = tweet.id;
    },
    checkRemoveControl(tweet) {
      if (tweet.user.id === this.like.userID) return true;
      else return false;
    },
    tweetRemove(tweet) {
      if (tweet != undefined && tweet != []) {
        TweetClient.deleteTweet(tweet.id);
        this.$toast.warning("Tweetiniz Silindi.");
        this.$router.go({
          path: "/",
          force: true,
        });
      }
    },
    openLikeModal(like) {
      this.LikesByUser = like;
      if (like === undefined) this.isLikeClicked = false;
      else if (like.length != 0) this.isLikeClicked = !this.isLikeClicked;
    },
    checkLike(tweet, like) {
      var findLike = tweet.likes.find((x) => x.userID === this.like.userID);

      if (findLike != [] && findLike != undefined && findLike != null)
        return true;
      else return false;
    },
    likeClick(tweet, event) {
      this.like.tweetID = tweet.id;
      LikeClient.toggleLike(this.like).then((response) => {
        tweet.likes = response.data;
        tweet.likeCount = response.data.length;
      });

      var element = document.getElementById("TW-" + tweet.id);
      if (element.className === "heart")
        element.className = "heart is_animating";
      else element.className = "heart";
    },
    gettweet() {
      TweetClient.getTweet(this.ofset, this.limit)
        .then((response) => {
          if (response.data.length != 0) {
            this.list.push(...response.data);
            this.checkTweet = true;
            this.ofset += 3;
          } else this.checkTweet = false;
        })
        .catch((e) => {
          //console.log(e);
        });
    },

    MyProfileUsernamePage() {
      this.$router.push("/Profile");
    },
    addTweet() {
      this.v$.tweet.$validate();
      if (!!this.tweet.content && !this.v$.tweet.$error) {
        TweetClient.add(this.tweet)
          .then((response) => {
            this.$toast.success("Tweet eklendi");
            this.list = [];
            this.ofset = 0;
            this.limit = 3;
            this.checkTweet = true;
            this.tweet.content = null;
            this.gettweet();
          })
          .catch((error) => {
            //console.log(error);
          });
      } else {
        this.$toast.warning("Tweet için açıklama giriniz.");
      }
    },
  },
  mounted() {
    this.gettweet();
    this.like.userID = this.$store.getters["Auth/user"].id;
  },
  computed: {
    compClasses: function () {
      return {
        heart: this.heart,
        is_animating: this.is_animating,
      };
    },
  },
};
</script>
<style scoped>
.heart {
  cursor: pointer;
  height: 50px;
  width: 50px;
  background-image: url("https://abs.twimg.com/a/1446542199/img/t1/web_heart_animation.png");
  background-position: left;
  background-repeat: no-repeat;
  background-size: 2900%;
}
.heart:hover {
  background-position: right;
}
.is_animating {
  animation: heart-burst 0.8s steps(28) 1;
  background-position: right;
}
@keyframes heart-burst {
  from {
    background-position: left;
  }
  to {
    background-position: right;
  }
}
.likeCount {
  color: #0d6efd;
  text-decoration: underline;
  cursor: pointer;
}

.like {
  padding: fixed;
  float: right;
}
.like a {
  margin-left: 36px;
  margin-top: -34px;
  float: left;
}
.errorSpan {
  color: red;
}
.dahafazlayukle,
.Usernamelink {
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
.Usernamelink:hover {
  text-decoration: underline;
  box-shadow: 0 1px 0 0 rgba(0, 0, 0, 0.19);
}
textarea {
  border: none;
}
textarea:hover {
  border: none;
}
textarea:focus {
  outline: 0;
}
.material-icons.md-20 {
  font-size: 20px;
  cursor: pointer;
  float: right;
  padding-right: 5px;
  color: slategrey;
}
.material-icons.md-20:hover {
  font-size: 25px;
  color: #c93f4a;
}
</style>
