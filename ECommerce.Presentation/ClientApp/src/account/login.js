import VeeValidate, {
  ValidationObserver,
  ValidationProvider,
  localize,
  extend
} from "vee-validate";
import * as rules from "vee-validate/dist/rules";
import zh_TW from "vee-validate/dist/locale/zh_TW.json";
import { modal } from "../shared/modal.js";

Object.keys(rules).forEach(rule => {
  extend(rule, rules[rule]);
});

export const login = function() {
  localize({
    zh_TW: {
      names: {
        email: "電子郵件",
        password: "密碼",
        rememberMe: "記住我"
      }
    }
  });

  localize("zh_TW", zh_TW);
  new Vue({
    el: "#loginVue",
    data: {
      email: "",
      password: "",
      rememberMe: false,
      loading: false
    },
    components: {
      validationobserver: ValidationObserver,
      validationprovider: ValidationProvider
    },
    methods: {
      onSubmit: function() {
        var self = this;
        var data = {
          Username: self.email,
          Email: self.email,
          Password: self.password,
          RememberMe: self.rememberMe
        };
        self.loading = true;
        axios
          .post("/Api/Account/Login", data)
          .then(function(response) {
            console.log(response.data);
            if (response.data.isSuccessed) {
              modal
                .show({
                  title: "Message",
                  body: "Login Successed"
                })
                .then(function() {
                  location.href = response.data.redirectUrl;
                  self.loading = false;
                });
            } else {
              modal.show({
                title: "Message",
                body: "Email or Password is not correct"
              });
            }
            self.loading = false;
          })
          .catch(function(errors) {
            console.log(errors);
            self.loading = false;
          });
      }
    }
  });
};
