<template>
  <div id="app">
    <div class="container feed">
      <FollowModal
        :isFollowingClicked="isFollowingClicked"
        :data="this.followings"
        @close="toggleModalFollowing"
      />

      <FollowModal2
        :isFollowersClicked="isFollowersClicked"
        :data="this.followers"
        @close="toggleModalFollowers"
      />
      <LikeModal
        :isLikeClicked="isLikeClicked"
        :data="this.LikesByUser"
        @close="closeLikeModal"
      />
      <RemoveModal
        :isRemoveClicked="isRemoveClicked"
        :data="this.deletedId"
        @close="tRemoveModal"
      />
      <div class="profile-page tx-13">
        <div class="feed">
          <div class="profile" v-if="profiledata">
            <div class="main-profile">
              <div class="row">
                <div class="col-12 grid-margin">
                  <div class="profile-header">
                    <div class="cover">
                      <div class="gray-shade"></div>
                      <figure class="background-image-top">
                        <div>
                          <img
                            src="../../public/images/bg_1.jpg"
                            class="img-fluid"
                            alt="profile cover"
                          />
                        </div>
                      </figure>
                      <div
                        class="
                          cover-body
                          d-flex
                          justify-content-between
                          align-items-center
                        "
                      >
                        <span class="profile-name">
                          <img
                            class="profile-pic"
                            src="../../public/images/usericon.png"
                            alt="profile"
                          />
                          {{ profiledata.name }}
                          {{ profiledata.surName }}
                          <div class="kullanici-adi-hizalama">
                            @{{ profiledata.nick }}
                          </div>
                        </span>
                        <div class="follow-btn">
                          <button
                            @click="follow"
                            v-if="profiledata.nick != this.user.nick"
                            :text="followResult"
                          >
                            {{ followResult }}
                          </button>
                        </div>
                        <div v-if="isLoggedIn && !this.$route.params.userName">
                          <button
                            type="button"
                            class="btn btn-outline-success"
                            @click="toggleModal"
                          >
                            Profili Güncelle
                          </button>
                          <UpdateProfile
                            @close="toggleModal"
                            :modalActive="modalActive"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="user-bio">
              <p>
                {{ profiledata.bio }}
              </p>
            </div>
            <div class="profilim-icon">
              <div><i class="fas fa-map-marker-alt"></i> İstanbul,Ataşehir</div>
              <div>
                <i class="fas fa-paperclip"></i>&nbsp;
                <a href="">{{ profiledata.mail }}</a>
              </div>
              <div>
                <i class="far fa-calendar-alt"></i>
                <b> &nbsp;{{ moment(profiledata.at).format("LL") }}</b>
                tarihinde katıldı
              </div>
            </div>
            <br />
            <div class="user-follows">
              <div @click="toggleModalFollowing">
                <b>{{ followings.length }}</b> Takipçiler
              </div>

              <div @click="toggleModalFollowers">
                <b>{{ followers.length }}</b> Takip Edilenler
              </div>
              <div>
                <b>{{ tweetCount }}</b> Tweet
              </div>
            </div>
            <br />
            <hr />
            <div class="post" v-for="item in twettt" :key="item.id">
              <div class="post__avatar">
                <img src="../../public/images/usericon.png" alt="" />
              </div>

              <div class="post__body">
                <div class="post__header">
                  <div class="post__headerText">
                    <h3>
                      {{ profiledata.name }} {{ profiledata.surName }}
                      <span class="post__headerSpecial">
                        <span class="material-icons post__badge">
                          verified </span
                        >@{{ profiledata.nick }} ||
                        {{ moment(item.at).format("LLL") }}
                      </span>
                      <span
                        v-if="!this.$route.params.userName"
                        class="material-icons md-20"
                        @click="tRemoveModal(item)"
                        >delete_forever
                        </span>
                    </h3>
                  </div>
                  <div class="post__headerDescription">
                    <p v-if="!this.$route.params.userName">
                      {{ item.content }}
                    </p>
                    <p v-else>{{ item.tweet.content }}</p>
                  </div>
                  <!--<div class="paylasimresim">
                    <img src="../../public/images/bg_1.jpg" alt="" />
                  </div>-->
                </div>

                <div v-if="!this.$route.params.userName" class="like">
                  <div
                    :id="'TW-' + item.id"
                    @click="likeClick(item, $event)"
                    :class="{ heart: heart, is_animating: checkLike(item) }"
                  ></div>

                  <a class="likeCount">
                    <div @click="closeLikeModal(item.likes)">
                      <b> {{ item.likeCount }} </b>
                    </div>
                  </a>
                </div>
                <div v-else class="like">
                  <div
                    :id="'TW-' + item.tweet.id"
                    @click="likeClick(item.tweet, $event)"
                    :class="{
                      heart: heart,
                      is_animating: checkLike(item.tweet, item.like),
                    }"
                  ></div>

                  <a class="likeCount">
                    <div @click="closeLikeModal(item.tweet.likes)">
                      <b> {{ item.tweet.likeCount }} </b>
                    </div>
                  </a>
                </div>
              </div>
            </div>
            <div v-if="!this.$route.params.userName">
              <button
                v-if="checkTweet"
                @click="MyTweet"
                class="sidebar__tweet dahafazlayukle"
              >
                Daha Fazla Yükle...
              </button>
            </div>
            <div v-else>
              <button
                v-if="checkTweet"
                @click="MyProfileUsernameTweet"
                class="sidebar__tweet dahafazlayukle"
              >
                Daha Fazla Yükle...
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import FollowModal from "@/components/FollowModal";
import FollowModal2 from "@/components/FollowModal";
import UpdateProfile from "@/components/UpdateProfile";
import LikeModal from "@/components/LikeModal";
import RemoveModal from "@/components/RemoveModal";
import FollowClient from "@/client/Follow";
import UserClient from "@/client/User";
import { ref } from "vue";
import router from "@/router";
import TweetClient from "@/client/Tweet.js";
import UserProfileClient from "@/client/Profile.js";
import moment from "moment";
import { mapGetters, mapActions, mapMutations } from "vuex";
import LikeClient from "@/client/Like.js";

export default {
  data() {
    return {
      UserProfile: null,
      followdata: {
        followedID: null,
      },
      followings: [],
      followers: [],
      LikesByUser: [],
      userName: null,
      tweet: {
        content: null,
      },
      like: {
        userID: null,
        tweetID: null,
      },
      profiledata: null,
      moment: moment,
      twettt: [],
      ofset: 0,
      limit: 3,
      checkTweet: true,
      followResult: null,
      tweetCount: null,
      heart: true,
      is_animating: false,
      deletedId: null,
      isRemoveClicked: false,
    };
  },
  components: {
    FollowModal,
    FollowModal2,
    UpdateProfile,
    LikeModal,
    RemoveModal,
  },
  computed: {
    ...mapGetters({
      user: "Auth/user",
      isLoggedIn: "Auth/isLoggedIn",
    }),
  },

  setup() {
    const isFollowersClicked = ref(false);
    const isLikeClicked = ref(false);

    const isFollowingClicked = ref(false);
    const toggleModalFollowing = () => {
      isFollowingClicked.value = !isFollowingClicked.value;
    };

    const toggleModalFollowers = () => {
      isFollowersClicked.value = !isFollowersClicked.value;
    };

    const modalActive = ref(false);
    const toggleModal = () => {
      modalActive.value = !modalActive.value;
    };

    return {
      isFollowingClicked,
      isFollowersClicked,
      isLikeClicked,
      toggleModalFollowing,
      toggleModalFollowers,
      modalActive,
      toggleModal,
    };
  },
  watch: {
    profiledata(value) {
      this.isFollow();
      this.count();
      this.loadFollowers(value.nick);
      this.loadFollowing(value.nick);
    },

    "$route.params.userName": {
      handler: function (userName) {
        this.ofset = 0;
        this.twettt = [];
        if (!userName) {
          this.MyTweet();
          this.MyProfile();
        } else {
          this.MyProfileUsername();
          this.MyProfileUsernameTweet();
        }
      },
      deep: true,
      immediate: true,
    },
  },
  methods: {
    moreModal() {
      this.isMoreClicked = !this.isMoreClicked;
    },
    tRemoveModal(tweet) {
      this.isRemoveClicked = !this.isRemoveClicked;
      if (this.isRemoveClicked) this.deletedId = tweet.id;
    },
    closeLikeModal(like) {
      this.LikesByUser = like;
      if (like === undefined) this.isLikeClicked = false;
      else if (like.length != 0) this.isLikeClicked = !this.isLikeClicked;
    },
    checkLike(tweet, like) {
      if (!this.$route.params.userName)
        var findLike = tweet.likes.find((x) => x.userID === this.like.userID);
      else var findLike = like.find((x) => x.userID === this.like.userID);

      if (findLike != [] && findLike != undefined && findLike != null)
        return true;
      else return false;
    },
    likeClick(tweet, event) {
      this.like.tweetID = tweet.id;
      LikeClient.toggleLike(this.like).then((response) => {
        tweet.likes = response.data;
        tweet.likeCount = response.data.length;
        this.LikesByUser = tweet.likes;
      });

      var element = document.getElementById("TW-" + tweet.id);
      if (element.className === "heart")
        element.className = "heart is_animating";
      else element.className = "heart";
    },
    async isFollow() {
      this.followdata.followedID = this.profiledata.id;
      this.followdata.userID = this.user.id;
      const followResponse = await FollowClient.doesItFollow(this.followdata);
      this.followResult =
        followResponse.data == true ? "Takip Ediliyor" : "Takip Et";
    },
    async count() {
      const response = await TweetClient.count(this.profiledata.nick);
      this.tweetCount = response.data;
    },
    async follow() {
      this.followdata.followedID = this.profiledata.id;
      const followResponse = await FollowClient.followed(this.followdata);
      this.followResult =
        followResponse.data == true ? "Takip Ediliyor" : "Takip Et";

      this.loadFollowers(this.profiledata.nick);
      this.loadFollowing(this.profiledata.nick);
    },
    async loadFollowers(value) {
      const response = await FollowClient.followers({
        nick: value,
      });
      this.followers = response.data;
    },
    async loadFollowing(value) {
      const response = await FollowClient.followings({
        nick: value,
      });
      this.followings = response.data;
    },
    MyTweet() {
      TweetClient.getMyTweet(this.ofset, this.limit).then((res) => {
        if (res.data.length != 0) {
          this.twettt.push(...res.data);
          this.checkTweet = true;
          this.ofset += 3;
        } else this.checkTweet = false;
      });
    },

    async MyProfile() {
      const res = await UserClient.Profile();
      this.profiledata = res.data;
    },
    async MyProfileUsername() {
      this.userName = this.$route.params.userName;
      const res = await UserProfileClient.MyProfileUser(this.userName);
      this.profiledata = res.data.user;
    },
    async MyProfileUsernameTweet() {
      this.userName = this.$route.params.userName;
      const res = await UserProfileClient.MyProfileUserTweet(
        this.userName,
        this.ofset,
        this.limit
      );
      if (res.data.length != 0) {
        this.twettt.push(...res.data);
        this.checkTweet = true;
        this.ofset += 3;
      } else {
        this.checkTweet = false;
      }
    },
  },
  mounted() {
    this.like.userID = this.$store.getters["Auth/user"].id;
  },
};
</script>

<style scoped>
.profile-page .profile-header {
  box-shadow: 0 0 10px 0 rgba(183, 192, 206, 0.2);
  border: 1px solid #f2f4f9;
}
.profile-page .profile-header .cover {
  position: relative;
  border-radius: 0.25rem 0.25rem 0 0;
}
.profile-page .profile-header .cover figure {
  margin-bottom: 0;
}
@media (max-width: 767px) {
  .profile-page .profile-header .cover figure {
    height: 110px;
    overflow: hidden;
  }
}
@media (min-width: 2400px) {
  .profile-page .profile-header .cover figure {
    height: 450px;
    overflow: hidden;
  }
}
.profile-page .profile-header .cover figure img {
  border-radius: 0.25rem 0.25rem 0 0;
  width: 720px;
}
@media (max-width: 767px) {
  .profile-page .profile-header .cover figure img {
    -webkit-transform: scale(2);
    transform: scale(2);
    margin-top: 15px;
  }
}
@media (min-width: 2400px) {
  .profile-page .profile-header .cover figure img {
    margin-top: -55px;
  }
}
.likeCount {
  color: #0d6efd;
  text-decoration: underline;
  cursor: pointer;
}
.background-image-top {
  height: 220px;
}
.background-image-top div img {
  height: 220px;
}
.kullanici-adi-hizalama {
  margin-left: 110px;
  margin-top: -40px;
  font-weight: normal;
  font-size: 17px;
}
.profile-page .profile-header .cover .gray-shade {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1;
  background: linear-gradient(rgba(255, 255, 255, 0.1), #fff 99%);
}
.profile-page .profile-header .cover .cover-body {
  position: absolute;
  bottom: -20px;
  left: 0;
  z-index: 2;
  width: 100%;
  padding: 0 20px;
}
.profile-page .profile-header .cover .cover-body .profile-pic {
  border-radius: 50%;
  width: 100px;
}
@media (max-width: 767px) {
  .profile-page .profile-header .cover .cover-body .profile-pic {
    width: 70px;
  }
}
.profile-page .profile-header .cover .cover-body .profile-name {
  font-size: 20px;
  font-weight: 600;
  margin-left: 17px;
}
.profile-page .profile-header .header-links {
  padding: 15px;
  display: -webkit-flex;
  display: flex;
  -webkit-justify-content: center;
  justify-content: center;
  background: #fff;
  border-radius: 0 0 0.25rem 0.25rem;
}
.profile-page .profile-header .header-links ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}
.profile-page .profile-header .header-links ul li a {
  color: #000;
  -webkit-transition: all 0.2s ease;
  transition: all 0.2s ease;
}
.profile-page .profile-header .header-links ul li:hover,
.profile-page .profile-header .header-links ul li.active {
  color: #727cf5;
}
.profile-page .profile-header .header-links ul li:hover a,
.profile-page .profile-header .header-links ul li.active a {
  color: #727cf5;
}
.profile-page .profile-body .left-wrapper .social-links a {
  width: 30px;
  height: 30px;
}
.profile-page .profile-body .right-wrapper .latest-photos > .row {
  margin-right: 0;
  margin-left: 0;
}
.profile-page .profile-body .right-wrapper .latest-photos > .row > div {
  padding-left: 3px;
  padding-right: 3px;
}
.profile-page .profile-body .right-wrapper .latest-photos > .row > div figure {
  -webkit-transition: all 0.3s ease-in-out;
  transition: all 0.3s ease-in-out;
  margin-bottom: 6px;
}
.profile-page
  .profile-body
  .right-wrapper
  .latest-photos
  > .row
  > div
  figure:hover {
  -webkit-transform: scale(1.06);
  transform: scale(1.06);
}
.profile-page
  .profile-body
  .right-wrapper
  .latest-photos
  > .row
  > div
  figure
  img {
  border-radius: 0.25rem;
}
.rtl .profile-page .profile-header .cover .cover-body .profile-name {
  margin-left: 0;
  margin-right: 17px;
}
.img-xs {
  width: 37px;
  height: 37px;
}
img {
  vertical-align: middle;
  border-style: none;
}
.container {
  height: 100%;
  width: 100%;
  padding-right: 0;
  padding-left: 0;
  justify-content: center;
  align-items: center;
}
.feed {
  flex: 0.5;
  border-right: 1px solid var(--twitter-background);
  min-width: fit-content;
  overflow-y: scroll;
  overflow-x: scroll;
}
.feed::-webkit-scrollbar {
  display: none;
}
.feed {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
.main-profile {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
}
.follow-btn button {
  font-size: 1rem;
  border: 3px solid #1da1f2;
  border-radius: 25px;
  background-color: rgba(29, 161, 242, 0);
  padding: 8px 20px;
  margin: 5px;
  font-weight: bold;
  float: right;
  color: #1da1f2;
  cursor: pointer;
  outline: none;
  transition: all 0.2s ease-in-out;
}
.follow-btn button:hover {
  background-color: red;
  color: #fff;
  border: 3px solid #ffffff;
  border-radius: 25px;
}
.user-bio {
  font-family: Georgia, "Times New Roman", Times, serif;
  font-size: 15px;
  color: #0f1419;
  margin-top: 35px;
  margin-bottom: 15px;
}
.user-bio a {
  color: #1da1f2;
  text-decoration: none;
}
.user-bio a:hover {
  text-decoration: underline;
}
.user-follows {
  display: flex;
  color: #5b7083;
}
.user-follows div {
  margin-right: 10px;
  cursor: pointer;
}
.user-follows b {
  color: black;
}
.profilim-icon {
  display: flex;
  color: #5b7083;
}
.profilim-icon div {
  margin-right: 10px;
}
.profilim-icon b {
  color: black;
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
.like {
  padding: fixed;
  float: right;
}
.like a {
  margin-left: 36px;
  margin-top: -34px;
  float: left;
  cursor: pointer;
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
