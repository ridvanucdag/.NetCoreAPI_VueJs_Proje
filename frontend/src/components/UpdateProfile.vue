<template>
  <div v-show="modalActive">
    <div class="modal-mask" v-show="modalActive">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header" style="display: inherit">
            <i
              @click="close"
              class="far fa-times-circle"
              style="float: right; cursor: pointer"
            ></i>
            <h3>Profil Bilgilerinizi Değiştirebilirsiniz</h3>
          </div>

          <slot name="body">
            <div class="tweetBox" v-if="profiledata">
              <form id="tweetForm" @submit.prevent="updateprofile">
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="">Adınız</span>
                  </div>
                  <input
                    type="text"
                    class="form-control"
                    id="name"
                    v-model="v$.profiledata.name.$model"
                  />
                </div>
                <div class="pre-icon os-icon os-icon-user-male-circle"></div>
                <div
                  class="input-errors text-danger"
                  v-for="(error, index) of v$.profiledata.name.$errors"
                  :key="index"
                >
                  <div class="error-msg">{{ error.$message }}</div>
                </div>
                <br />
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="">Soyadınız</span>
                  </div>
                  <input
                    type="text"
                    class="form-control"
                    id="surName"
                    v-model="v$.profiledata.surName.$model"
                  />
                </div>
                <div class="pre-icon os-icon os-icon-user-male-circle"></div>
                <div
                  class="input-errors text-danger"
                  v-for="(error, index) of v$.profiledata.surName.$errors"
                  :key="index"
                >
                  <div class="error-msg">{{ error.$message }}</div>
                </div>
                <br />
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="">Username</span>
                  </div>
                  <input
                    type="text"
                    readonly
                    class="form-control"
                    id="nick"
                    v-model="profiledata.nick"
                  />
                </div>
                <br />
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id=""
                      >Telefon Numaranız</span
                    >
                  </div>
                  <input
                    type="text"
                    class="form-control"
                    id="phone"
                    v-model="v$.profiledata.phone.$model"
                  />
                </div>
                <div class="pre-icon os-icon os-icon-user-male-circle"></div>
                <div
                  class="input-errors text-danger"
                  v-for="(error, index) of v$.profiledata.phone.$errors"
                  :key="index"
                >
                  <div class="error-msg">{{ error.$message }}</div>
                </div>
                <br />
                <div class="input-group-prepend">
                  <span class="input-group-text" id="">Hakkımda</span>
                </div>
                <div class="input-group">
                  <textarea
                    class="form-control"
                    id="bio"
                    rows="3"
                    v-model="v$.profiledata.bio.$model"
                  ></textarea>
                </div>
                <div class="pre-icon os-icon os-icon-user-male-circle"></div>
                <div
                  class="input-errors text-danger"
                  v-for="(error, index) of v$.profiledata.bio.$errors"
                  :key="index"
                >
                  <div class="error-msg">{{ error.$message }}</div>
                </div>
                <br />
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id="">E-mail</span>
                  </div>
                  <input
                    type="text"
                    readonly
                    id="mail"
                    class="form-control"
                    v-model="profiledata.mail"
                  />
                </div>
                <br />
                <div class="input-group">
                  <div class="input-group-prepend">
                    <span class="input-group-text" id=""
                      >Kayıt Tarihi :
                      {{ moment(profiledata.at).format("LLL") }}</span
                    >
                  </div>
                </div>
                <br />
                <button type="submit" class="btn btn-outline-primary" id="btn">
                  Kaydet
                </button>
              </form>
            </div>
          </slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserClient from "@/client/User";
import useVuelidate from "@vuelidate/core";
import { mapGetters } from "vuex";
import moment from "moment";

import { required, minLength, maxLength, numeric } from "@vuelidate/validators";

export function validName(name) {
  let validNamePattern = new RegExp("^[a-zA0-9-Z]+(?:[-'s][a-zA-Z]+)*$");
  if (validNamePattern.test(name)) {
    return true;
  }
  return false;
}

export default {
  props: ["modalActive"],
  data() {
    return {
      profiledata: {
        name: "",
        surName: "",
        phone: "",
        bio: "",
      },
      moment: moment,
      valid: false,
    };
  },

  validations() {
    return {
      profiledata: {
        name: {
          required,
          name_validation: {
            $validator: validName,
            $message: "",
          },
          min: minLength(3),
          max: maxLength(20),
        },
        surName: {
          required,
          name_validation: {
            $validator: validName,
            $message: "",
          },
          min: minLength(3),
          max: maxLength(20),
        },
        phone: {
          required,
          numeric,
          name_validation: {
            $validator: validName,
            $message: "",
          },
          min: minLength(10),
          max: maxLength(10),
        },
        bio: {
          required,
          name_validation: {
            $validator: validName,
            $message: "",
          },
          min: minLength(3),
          max: maxLength(150),
        },
      },
    };
  },

  computed: {
    ...mapGetters({
      user: "Auth/user",
      isLoggedIn: "Auth/isLoggedIn",
    }),
  },

  setup(props, { emit }) {
    const close = () => {
      emit("close");
    };
    return { close, v$: useVuelidate() };
  },

  methods: {
    async MyProfile() {
      const res = await UserClient.Profile();
      this.profiledata = res.data;
    },
    updateprofile() {
      this.valid = false;
      for (const index of this.v$.profiledata.$errors) {
        if (index.$message != "") {
          this.valid = true;
        }
      }

      if (!this.valid) {
        UserClient.UpdateProfile(this.profiledata)
          .then((res) => {
            console.log(res);
            this.$router.go({
              path: "/",
              force: true,
            });
          })
          .catch((er) => {
            console.log(er);
          });
      }
    },
  },
  mounted() {
    this.MyProfile();
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
