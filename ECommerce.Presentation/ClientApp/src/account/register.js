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

export const register = function() {
  // install localize
  localize({
    zh_TW: {
      names: {
        email: "電子郵件",
        password: "密碼",
        confirmPassword: "密碼再確認"
      }
    }
  });
  // use current lang
  localize("zh_TW", zh_TW);

  new Vue({
    el: "#registerVue",
    data: {
      email: "",
      password: "",
      confirmPassword: ""
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
          ConfirmPassword: self.confirmPassword
        };
        console.log(data);
        axios
          .post("/api/Account/Register", data)
          .then(function(response) {
            console.log(response);
            var result = response.data;
            if (result.isSuccess) {
              modal
                .show({
                  title: "Message",
                  body: "Register Successed"
                })
                .then(function() {
                  location.href = result.redirectUrl;
                });
            } else {
              if (response.data.message) {
                modal.show({
                  title: "Message",
                  body: response.data.message
                });
              } else {
                modal.show({
                  title: "Message",
                  body: "Error"
                });
              }
            }
          })
          .catch(function(ex) {
            modal.show({
              title: "Error",
              body: "Error Occurred"
            });
          });
      }
    }
  });
};
