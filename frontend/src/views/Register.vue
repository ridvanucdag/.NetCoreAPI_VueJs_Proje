<template>
  <ResetPasswordModal @close="toggleModal" :modalActive="modalActive" />

  <div id="ortala">
    <article>
      <div class="container" id="container">
        <div class="form-container sign-up-container">
          <form id="registerForm" @submit.prevent="register">
            <h2>TwitterApp'a Kaydol</h2>
            <div class="social-container">
              <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
              <a href="#" class="social"
                ><i class="fab fa-google-plus-g"></i
              ></a>
              <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
            </div>

            <input
              type="text"
              v-model="state.registerRequest.name"
              name="adiniz"
              placeholder="Adınız"
            />

            <span v-if="v$.registerRequest.name.$error" class="errorSpan">
              {{ v$.registerRequest.name.$errors[0].$message }}
            </span>

            <input
              type="text"
              v-model="state.registerRequest.surname"
              name="soyadiniz"
              placeholder="Soyadınız"
            />

            <span v-if="v$.registerRequest.surname.$error" class="errorSpan">
              {{ v$.registerRequest.surname.$errors[0].$message }}
            </span>

            <input
              type="text"
              v-model="state.registerRequest.nick"
              placeholder="Kullanıcı Adınız"
            />

            <span v-if="v$.registerRequest.nick.$error" class="errorSpan">
              {{ v$.registerRequest.nick.$errors[0].$message }}
            </span>

            <input
              type="email"
              v-model="state.registerRequest.mail"
              placeholder="Email adresiniz"
            />

            <span v-if="v$.registerRequest.mail.$error" class="errorSpan">
              {{ v$.registerRequest.mail.$errors[0].$message }}
            </span>

            <input
              type="text"
              v-model="state.registerRequest.phone"
              @keypress="onlyNumbers"
              placeholder="Telefon Numaranız"
            />
            <span v-if="v$.registerRequest.phone.$error" class="errorSpan">
              {{ v$.registerRequest.phone.$errors[0].$message }}
            </span>

            <textarea
              v-model="state.registerRequest.bio"
              placeholder="Hakkınızda bilgi giriniz"
              rows="3"
            ></textarea>
            <span v-if="v$.registerRequest.bio.$error" class="errorSpan">
              {{ v$.registerRequest.bio.$errors[0].$message }}
            </span>

            <input
              type="password"
              v-model="state.registerRequest.password"
              placeholder="Parola"
            />

            <span v-if="v$.registerRequest.password.$error" class="errorSpan">
              {{ v$.registerRequest.password.$errors[0].$message }}
            </span>

            <input
              v-model="state.passwordConfig"
              type="password"
              placeholder="Parola Tekrar"
            />

            <span v-if="v$.passwordConfig.$error" class="errorSpan">
              {{ v$.passwordConfig.$errors[0].$message }}
            </span>

            <button type="submit" id="signUpBtn">Kaydol</button>
          </form>
        </div>
        <div class="form-container sign-in-container">
          <form id="loginForm" @submit.prevent="login">
            <h1>Giriş Yapınız</h1>
            <div class="social-container">
              <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
              <a href="#" class="social"
                ><i class="fab fa-google-plus-g"></i
              ></a>
              <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
            </div>
            <input
              type="email"
              v-model="state.loginRequest.mail"
              placeholder="Email adresiniz"
            />

            <span v-if="v$.loginRequest.mail.$error" class="errorSpan">
              {{ v$.loginRequest.mail.$errors[0].$message }}
            </span>

            <input
              type="password"
              v-model="state.loginRequest.password"
              placeholder="Parolanız"
            />

            <span v-if="v$.loginRequest.password.$error" class="errorSpan">
              {{ v$.loginRequest.password.$errors[0].$message }}
            </span>

            <a @click="toggleModal">Şifremi Unuttum?</a>
            <button type="submit">Giriş Yapın</button>
          </form>
        </div>
        <div class="overlay-container">
          <div class="overlay">
            <div class="overlay-panel overlay-left">
              <h1>TwitterAPP'a Hoşgeldiniz</h1>
              <p>
                Bizimle bağlantıda kalmak için lütfen kişisel bilgilerinizle
                giriş yapın
              </p>
              <button class="ghost" id="signIn" @click="girisyap">
                Giriş Yapın
              </button>
            </div>
            <div class="overlay-panel overlay-right">
              <h1>Hoşgeldiniz</h1>
              <p>Kişisel bilgilerinizi girin ve bizimle yolculuğa başlayın</p>
              <button class="ghost" id="signUp" @click="kayitol">Kaydol</button>
            </div>
          </div>
        </div>
      </div>
    </article>
  </div>
</template>

<script>
import AuthClient from "@/client/Auth";
import { render } from "@vue/runtime-dom";
import { mapGetters } from "vuex";
import useVuelidate from "@vuelidate/core";
import ResetPasswordModal from "@/components/ResetPasswordModal";
import ResetPassword from "@/views/ResetPassword";
import { ref } from "vue";

import {
  required,
  email,
  minLength,
  maxLength,
  sameAs,
  helpers,
} from "@vuelidate/validators";
import { reactive, computed } from "vue";

export function validName(name) {
  let validNamePattern = new RegExp("^[a-zA0-9-Z]+(?:[-'\s][a-zA-Z]+)*$");
  if (validNamePattern.test(name)) {
    return true;
  }
  return false;
}

export default {
  setup() {
    const state = reactive({
      registerRequest: {
        name: null,
        surname: null,
        nick: null,
        phone: "",
        bio: null,
        mail: null,
        password: null,
      },
      passwordConfig: null,
      loginRequest: {
        mail: null,
        password: null,
      },
    });

    const rules = computed(() => {
      return {
        registerRequest: {
          name: {
            required: helpers.withMessage("Adınızı giriniz", required),
            minLength: helpers.withMessage(
              "Adınızın uzunluğu en az 2 karakter olmalıdır.",
              minLength(2)
            ),
          },
          surname: {
            required: helpers.withMessage("Soyadınız giriniz", required),
            minLength: helpers.withMessage(
              "Soyadınızın uzunluğu en az 2 karakter olmalıdır.",
              minLength(2)
            ),
          },
          nick: {
            required: helpers.withMessage(
              "Kullanıcı adınızı giriniz",
              required
            ),
            minLength: helpers.withMessage(
              "Kullanıcı adınızın uzunluğu en az 2 karakter olmalıdır.",
              minLength(2)
            ),
            name_validation: {
              $validator: validName,
              $message: "Kullanıcı adınız  türkçe karakter kullanılmamalıdır.",
            },
          },
          phone: {
            minLength: helpers.withMessage(
              "Telefon numaranız en az 10 hane olmalıdır.",
              minLength(10)
            ),
            maxLength: helpers.withMessage(
              "Telefon numaranız en fazla 10 hane olmalıdır.",
              maxLength(10)
            ),
          },
          bio: {
            maxLength: helpers.withMessage(
              "Bio nuz en fazla 150 hane olmalıdır.",
              maxLength(150)
            ),
          },
          mail: {
            required: helpers.withMessage("Mailinizi giriniz.", required),
            email: helpers.withMessage("Mail formatına uygun değildir.", email),
          },
          password: {
            required: helpers.withMessage("Şifrenizi giriniz.", required),
            minLength: helpers.withMessage(
              "Şifre uzunluğu en az 4 karakter olmalıdır.",
              minLength(4)
            ),
          },
        },

        passwordConfig: {
          required: helpers.withMessage("Şifrenizi giriniz", required),
          sameAs: helpers.withMessage(
            "Şifreler birbirleriyle uyuşmamaktadır.",
            sameAs(state.registerRequest.password)
          ),
          minLength: helpers.withMessage(
            "Şifre uzunluğu en az 4 karakter olmalıdır.",
            minLength(4)
          ),
        },

        loginRequest: {
          mail: {
            required: helpers.withMessage("Mail giriniz", required),
            email: helpers.withMessage("Mail formatına uygun değildir.", email),
          },
          password: {
            required: helpers.withMessage("Şifre giriniz", required),
            minLength: helpers.withMessage(
              "Şifre uzunluğu en az 4 karakter olmalıdır.",
              minLength(4)
            ),
          },
        },
      };
    });

    const v$ = useVuelidate(rules, state);

    const modalActive = ref(false);
    const toggleModal = () => {
      modalActive.value = !modalActive.value;
    };

    return {
      state,
      v$,
      modalActive,
      toggleModal,
    };
  },
  components: {
    ResetPasswordModal,
    ResetPassword,
  },
  computed: {
    ...mapGetters(["Auth"["user"]]),
  },
  methods: {
    girisyap() {
      addEventListener("click", () => {
        container.classList.remove("right-panel-active");
      });
    },

    kayitol() {
      addEventListener("click", () => {
        container.classList.add("right-panel-active");
      });
    },
    register() {
      this.v$.registerRequest.$validate();
      this.v$.passwordConfig.$validate();
      if (!this.v$.registerRequest.$error && !this.v$.passwordConfig.$error) {
        document.getElementById("signUpBtn").disabled = true;
        AuthClient.register(this.state.registerRequest)
          .then((res) => {
            document.getElementById("registerForm").reset();
            document.getElementById("loginForm").submit();
            this.$toast.success("Kayıt işlemi başarılı.");
          })
          .catch((error) => {
            document.getElementById("signUpBtn").disabled = false;

            if (error.response.status == 400) {
              this.$toast.warning(error.response.data);
            }
          });
      }
    },
    login() {
      this.v$.loginRequest.$validate();
      if (!this.v$.loginRequest.$error) {
        this.$store
          .dispatch("Auth/login", this.state.loginRequest)
          .catch((error) => {
            if (error.response.status == 400) {
              this.$toast.warning(error.response.data);
            }
          });
      }
    },
    onlyNumbers(event) {
      let keyCode = event.keyCode ? event.keyCode : event.which;
      if (keyCode < 48 || keyCode > 57) {
        // 46 is dot
        event.preventDefault();
      }
    },
  },
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css?family=Montserrat:400,800");

* {
  box-sizing: border-box;
}

body {
  background: #f6f5f7;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  font-family: "Montserrat", sans-serif;
  height: 100vh;
  margin: -20px 0 50px;
}

#ortala {
  margin: auto;
  padding: 0;
}

h1 {
  font-weight: bold;
  margin: 0;
}

h2 {
  text-align: center;
}

p {
  font-size: 14px;
  font-weight: 100;
  line-height: 20px;
  letter-spacing: 0.5px;
  margin: 20px 0 30px;
}

span {
  font-size: 12px;
}

.errorSpan {
  color: red;
}

a {
  color: #333;
  font-size: 14px;
  text-decoration: none;
  margin: 15px 0;
}

button {
  border-radius: 20px;
  border: 1px solid #50b7f5;
  background-color: #50b7f5;
  color: #ffffff;
  font-size: 12px;
  font-weight: bold;
  padding: 12px 45px;
  letter-spacing: 1px;
  text-transform: uppercase;
  transition: transform 80ms ease-in;
}

button:active {
  transform: scale(0.95);
}

button:focus {
  outline: none;
}

button.ghost {
  background-color: transparent;
  border-color: #ffffff;
}

form {
  background-color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 50px;
  height: 100%;
  text-align: center;
}

input {
  background-color: #eee;
  border: none;
  height: 35px;
  padding: 12px 15px;
  margin: 8px 0;
  width: 100%;
}
textarea {
  background-color: #eee;
  border: none;
  width: 100%;
}

.container {
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
  position: relative;
  overflow: hidden;
  width: 900px;
  max-width: 100%;
  min-height: 680px;
}

.form-container {
  position: absolute;
  top: 0;
  height: 100%;
  transition: all 0.6s ease-in-out;
}

.sign-in-container {
  left: 0;
  width: 50%;
  z-index: 2;
}

.container.right-panel-active .sign-in-container {
  transform: translateX(100%);
}

.sign-up-container {
  left: 0;
  width: 50%;
  opacity: 0;
  z-index: 1;
}

.container.right-panel-active .sign-up-container {
  transform: translateX(100%);
  opacity: 1;
  z-index: 5;
  animation: show 0.6s;
}

@keyframes show {
  0%,
  49.99% {
    opacity: 0;
    z-index: 1;
  }

  50%,
  100% {
    opacity: 1;
    z-index: 5;
  }
}

.overlay-container {
  position: absolute;
  top: 0;
  left: 50%;
  width: 50%;
  height: 100%;
  overflow: hidden;
  transition: transform 0.6s ease-in-out;
  z-index: 100;
}

.container.right-panel-active .overlay-container {
  transform: translateX(-100%);
}

.overlay {
  background: linear-gradient(
    to right,
    rgba(3, 154, 167),
    rgb(6, 230, 151),
    rgba(3, 154, 167)
  );
  background-repeat: no-repeat;
  background-size: cover;
  background-position: 0 0;
  color: #ffffff;
  position: relative;
  left: -100%;
  height: 100%;
  width: 200%;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay {
  transform: translateX(50%);
}

.overlay-panel {
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 40px;
  text-align: center;
  top: 0;
  height: 100%;
  width: 50%;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.overlay-left {
  transform: translateX(-20%);
}

.container.right-panel-active .overlay-left {
  transform: translateX(0);
}

.overlay-right {
  right: 0;
  transform: translateX(0);
}

.container.right-panel-active .overlay-right {
  transform: translateX(20%);
}

.social-container {
  margin: 20px 0;
}

.social-container a {
  border: 1px solid #dddddd;
  border-radius: 50%;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  margin: 0 5px;
  height: 40px;
  width: 40px;
}

footer {
  background-color: #222;
  color: #fff;
  font-size: 14px;
  bottom: 0;
  position: fixed;
  left: 0;
  right: 0;
  text-align: center;
  z-index: 999;
}

footer p {
  margin: 10px 0;
}

footer i {
  color: red;
}

footer a {
  color: #3c97bf;
  text-decoration: none;
}
</style>
