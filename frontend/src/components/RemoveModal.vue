<template>
  <div v-show="isRemoveClicked" @click="close">
    <div class="modal-mask" v-show="isRemoveClicked">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="content">
            <h1 class="content">Tweet silinsin mi?</h1>
            <p class="content">
              Bu işlem geri alınamaz ve Tweet, profilinden, seni takip eden tüm
              hesapların zaman akışından ve Twitter arama sonuçlarından
              kaldırılacak.
            </p>
            <div class="pos">
              <p>
                <button class="buton-sil" @click="tweetRemove(data)">
                  Sil
                </button>
              </p>

              <p>
                <button class="buton-iptal" @click="isRemoveClicked = false">
                  İptal
                </button>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import TweetClient from "@/client/Tweet.js";
export default {
  props: ["data", "isRemoveClicked"],
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close };
  },
  methods: {
    tweetRemove(id) {
      if (id != undefined && id > 0) {
        TweetClient.deleteTweet(id);
        this.$router.go({
          path: "/",
          force: true,
        });
        this.$toast.warning("Tweetiniz Silindi.");
      }
    },
  },
};
</script>
<style scoped>
.content {
  padding: 1px 3px 15px 10px;
  float: left;
  margin: auto;
  margin-bottom: 5px;
}
p.content {
  font-family: TwitterChirp, -apple-system, BlinkMacSystemFont, "Segoe UI",
    Roboto, Helvetica, Arial, sans-serif;
  font-size: 15px;
  font-weight: 400;
  text-align: left;
  line-height: 20px;
  width: 100%;
  color: rgb(83, 100, 113);
}
h1.content {
  margin-top: 20px;
  margin-bottom: 1px;
  text-align: left;
  font-size: 20px;
  font-weight: 700;
  color: rgb(15, 20, 25);
  font-family: TwitterChirp, -apple-system, BlinkMacSystemFont, "Segoe UI",
    Roboto, Helvetica, Arial, sans-serif;
}
.pos {
  float: left;
}
.buton-sil {
  padding: 2px 110px 2px 110px;
  margin-left: 7px;
  float: left;
  color: white;
  font: inherit;
  background: rgb(244, 33, 46);
  border-radius: 9999px;
  border-color: rgba(0, 0, 0, 0);
  height: 45px;
}
.buton-sil:hover {
  background: rgb(220, 30, 41);
}
.buton-iptal {
  height: 45px;
  margin-top: 5px;
  margin-left: 7px;
  padding: 2px 100px 2px 105px;
  float: left;
  font: inherit;
  color: rgba(0, 0, 0, 1);
  background-color: rgba(255, 255, 255, 1);
  border-radius: 9999px;
  border-color: rgb(207, 217, 222);
}
.buton-iptal:hover {
  background-color: rgba(15, 20, 25, 0.1);
}
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 312px;
  height: 300px;
  padding: 10px 5px 10px 10px;
  background-color: rgba(255, 255, 255, 1);
  border-radius: 16px;
  box-sizing: border-box;
  z-index: 0;
  margin: auto;
}

.modal-header h3 {
  margin-top: 0;
  color: var(--twitter-color);
}

.modal-body {
  margin: 20px 0;
}

.modal-default-button {
  display: block;
  margin-top: 1rem;
}

.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.5s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}
.errorSpan {
  color: red;
}
</style>