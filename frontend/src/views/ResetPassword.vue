<template>
  <div v-show="state.modalResetActive">
    <div class="modal-mask" v-show="state.modalResetActive">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header" style="display: inherit;">
            <i
              class="far fa-times-circle"
              style="float: right;cursor:pointer"
              @click="close()"
            ></i>
            <h3>Şifre Yenileme</h3>
          </div>
          <div class="modal-body">
            <form
              id="passwordForm"
              @submit.prevent="reset"
              style="padding: 1%;"
            >
              <div class="form-outline mb-4">
                <label class="form-label" for="password">Parola</label>
                <input
                  type="password"
                  class="form-control"
                  v-model="state.passwordControl.password"
                  id="password"
                />
                <span
                  v-if="v$.passwordControl.passwordConfig.$error"
                  class="errorSpan"
                >
                  {{ v$.passwordControl.passwordConfig.$errors[0].$message }}
                </span>
              </div>
              <div class="form-outline mb-4">
                <label class="form-label" for="passwordConfig"
                  >Parola Tekrarı</label
                >
                <input
                  type="password"
                  class="form-control"
                  v-model="state.passwordControl.passwordConfig"
                  id="passwordConfig"
                />
                <span
                  v-if="v$.passwordControl.passwordConfig.$error"
                  class="errorSpan"
                >
                  {{ v$.passwordControl.passwordConfig.$errors[0].$message }}
                </span>
              </div>
              <button
                type="submit"
                class="btn btn-primary btn-block"
                style="float: right;"
              >
                Şifreyi Yenile
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
import { required, helpers, minLength, sameAs } from "@vuelidate/validators";
import { reactive, computed } from "vue";

export default {
  setup() {
    const state = reactive({
      passwordControl: {
        password: null,
        passwordConfig: null,
      },
      mail: null,
      modalResetActive: null,
    });
    const rules = computed(() => {
      return {
        passwordControl: {
          password: {
            required: helpers.withMessage("Şifrenizi giriniz.", required),
            minLength: helpers.withMessage(
              "Şifre uzunluğu en az 4 karakter olmalıdır.",
              minLength(4)
            ),
          },
          passwordConfig: {
            required: helpers.withMessage("Şifrenizi giriniz", required),
            minLength: helpers.withMessage(
              "Şifre uzunluğu en az 4 karakter olmalıdır.",
              minLength(4)
            ),
            sameAs: helpers.withMessage(
              "Şifreler birbirleriyle uyuşmamaktadır.",
              sameAs(state.passwordControl.password)
            ),
          },
        },
      };
    });

    const v$ = useVuelidate(rules, state);

    return { state, v$ };
  },
  watch: {
    "$route.params.mail": {
      handler: function(mail) {
        if (mail != null) {
          this.state.mail = mail;
          this.state.modalResetActive = true;
        } else {
          this.state.modalResetActive = false;
        }
      },
      deep: true,
      immediate: true,
    },
  },
  methods: {
    reset() {
      this.v$.passwordControl.$validate();
      if (!this.v$.passwordControl.$error) {
        AuthClient.resetPassword({
          mail: this.state.mail,
          password: this.state.passwordControl.password,
        })
          .then((response) => {
            this.$toast.success("Şifreniz değişitirilmiştir.");
            setTimeout(() => {
              window.location.href = "/register";
            }, 150);
          })
          .catch((error) => {
            if (error.response.status == 400) {
              this.$toast.warning(error.response.data);
            }
          });
      }
    },
    close() {
      this.$toast.warning("Şifreniz değiştirilmedi.");

      setTimeout((window.location.href = "/register"), 150);
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
