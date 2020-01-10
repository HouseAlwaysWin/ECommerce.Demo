Vue.component("validation", VeeValidate.ValidationProvider);
Vue.component("validation-observer", VeeValidate.ValidationObserver);

// install localize
VeeValidate.localize({
  zh_TW: {
    names: {
      email: "電子郵件",
      password: "密碼",
      rememberMe: "記住我"
    },
    messages: zh_TW.messages
  }
});
// use current lang
VeeValidate.localize("zh_TW");


var loginVue = new Vue({
  el: "#loginVue",
  data: {
    email: "",
    password: "",
    rememberMe: false
  },
  methods: {
    validateForm: function () {},
    onSubmit: function () {
      var self = this;
      var data = {
        Username: self.email,
        Email: self.email,
        Password: self.password,
        RememberMe: self.rememberMe
      }
      axios.post('/Api/Account/Login', data)
        .then(function (response) {
          console.log(response);
        }).catch(function (errors) {
          console.log(errors);
        });

    }
  }
});