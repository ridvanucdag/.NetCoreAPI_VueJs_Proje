<template>
  <div
    class="post profile"
    v-for="user in users"
    :key="user"
    @click="goToProfile(user)"
  >
    <div class="post__avatar">
      <img src="../../public/images/usericon.png" alt="" />
    </div>

    <div class="post__body">
      <div class="post__header">
        <br />
        <div class="post__headerText">
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
    v-if="isUserSearchClicked && go == User"
    class="sidebar__tweet dahafazlayukle"
    @click="getSearchData(word)"
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
      users: [],
      ofset: 0,
      limit: 2,
      searchWord: this.word,
      isUserSearchClicked: false,
    };
  },
  watch: {
    word(value) {
      this.searchWord = value;
      this.ofset = 0;
      this.limit = 2;
      this.users = [];
      this.isUserSearchClicked = false;
      this.getSearchData(this.searchWord);
    },
  },
  methods: {
    getSearchData(searchWord) {
      SearchClient.Search(searchWord, this.ofset, this.limit)
        .then((res) => {
          if (res.data.length != 0) {
            res.data.filter((d) => {
              if (d.type == "User") this.users.push(d.user);
              return d;
            });

            res.data.find((c) => c.type == "User") != null
              ? (this.isUserSearchClicked = true)
              : (this.isUserSearchClicked = false);

            this.ofset += this.limit;
          } else {
            this.isUserSearchClicked = false;
          }
        })
        .catch((er) => {
          //console.log(er);
        });
    },
    goToProfile(user) {
      router.push("/profile/" + user.nick);
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
