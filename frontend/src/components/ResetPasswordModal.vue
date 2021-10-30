<template>
  <div v-show="modalActive">
    <div class="modal-mask" v-show="modalActive">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header" style="display: inherit;">
            <i
              class="far fa-times-circle"
              style="float: right;cursor:pointer"
              @click="close"
            ></i>
            <h3>Şifre mi Unuttum?</h3>
          </div>
          <div class="modal-body">
            <form id="tweetForm" @submit.prevent="send" style="padding: 3%;">
              <div class="form-outline mb-4">
                <label class="form-label" for="mail">Mail Adresi</label>

                <input
                  type="email"
                  class="form-control"
                  v-model="mail"
                  id="mail"
                />
                <br />
                <span v-if="v$.mail.$error" class="errorSpan">
                  {{ v$.mail.$errors[0].$message }}
                </span>
              </div>

              <button
                class="btn btn-primary btn-block"
                type="submit"
                style="float: right;"
              >
                Mail Gönder
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AuthClient from "@/client/Auth";
import useVuelidate from "@vuelidate/core";
import { required, helpers, email } from "@vuelidate/validators";
export default {
  props: ["modalActive"],
  data() {
    return {
      mail: null,
    };
  },
  validations: {
    mail: {
      required: helpers.withMessage("Mail giriniz", required),
      email: helpers.withMessage("Mail formatına uygun değildir.", email),
    },
  },
  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close, v$: useVuelidate() };
  },
  methods: {
    send() {
      this.v$.mail.$validate();
      if (!this.v$.mail.$error) {
        AuthClient.sendMail(this.mail)
          .then((response) => {
            this.$toast.success("Mail gönderildi.");
            setTimeout(
              this.$router.go({
                path: "/",
                force: true,
              }),
              150
            );
          })
          .catch((error) => {
            if (error.response.status == 400) {
              this.$toast.warning(error.response.data);
            }
          });
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
