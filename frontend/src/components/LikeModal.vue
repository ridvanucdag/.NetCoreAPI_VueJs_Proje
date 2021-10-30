<template>
  <div v-show="isLikeClicked && data.length != 0" @click="close">
    <div class="modal-mask" v-show="isLikeClicked && data.length != 0">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <i class="far fa-times-circle" style="float: right"></i>
            <h3>Beğenenler</h3>
            <br />
          </div>
          <hr />
          <div class="modal-body" style="overflow-y: scroll; max-height: 400px">
            <div class="post" v-for="like in data" :key="like">
              <div class="post__avatar">
                <img src="../../public/images/usericon.png" alt="" />
              </div>

              <div class="post__body">
                <div class="post__header">
                  <br />
                  <div class="post__headerDescription">
                    <a :href="/profile/ + like.user.nick">
                      <h3 v-if="isLikeClicked">
                        {{ like.user.name }} {{ like.user.surName }}
                        <span class="post__headerSpecial"
                          ><span class="material-icons post__badge">
                            verified </span
                          >@{{ like.user.nick }}</span
                        >
                      </h3>
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TweetClient from "@/client/Tweet";
export default {
  props: ["modalActive", "data", "isLikeClicked"],

  data() {
    return {};
  },
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close };
  },
};
</script>
<style scoped>
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
  width: 600px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  font-family: Helvetica, Arial, sans-serif;
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
</style>
