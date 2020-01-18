import VeeValidate, {
  ValidationObserver,
  ValidationProvider,
  localize,
  extend
} from "vee-validate";
import * as rules from "vee-validate/dist/rules";
import zh_TW from "vee-validate/dist/locale/zh_TW.json";

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
          })
          .catch(function(ex) {});
      }
    }
  });
};
