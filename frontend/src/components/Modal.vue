<template>
  <div v-show="modalActive">
    <div class="modal-mask" v-show="modalActive">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header" style="display: inherit;">
            <i
              @click="close"
              class="far fa-times-circle"
              style="float: right;cursor:pointer"
            ></i>
            <h3>Tweet Paylaş</h3>
          </div>
          <div class="modal-body">
            <slot name="body">
              <div class="tweetBox">
                <form id="tweetForm" @submit.prevent="addTweet">
                  <div class="tweetbox__input">
                    <img src="../../public/images/usericon.png" alt="" />
                    <input
                      type="text"
                      v-model="tweet.content"
                      placeholder="Ne oluyor?"
                    />
                  </div>
                  <span v-if="v$.tweet.content.$error" class="errorSpan">
                    {{ v$.tweet.content.$errors[0].$message }}
                  </span>
                  <br />
                  <button class="tweetBox__tweetButton" type="submit">
                    Tweet
                  </button>
                </form>
              </div>
            </slot>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TweetClient from "@/client/Tweet";
import useVuelidate from "@vuelidate/core";
import { maxLength, helpers } from "@vuelidate/validators";
export default {
  props: ["modalActive"],
  data() {
    return {
      tweet: {
        content: null,
      },
    };
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
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close, v$: useVuelidate() };
  },
  methods: {
    addTweet() {
      this.v$.tweet.$validate();
      if (!!this.tweet.content && !this.v$.tweet.$error) {
        TweetClient.add(this.tweet)
          .then((response) => {
            this.$toast.success("Tweet eklendi");
            this.$router.go({
              path: "/",
              force: true,
            });
          })
          .catch((error) => {
            //console.log(error);
          });
      } else {
        this.$toast.warning("Tweet için açıklama giriniz.");
      }
    },
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
.errorSpan {
  color: red;
}
</style>
