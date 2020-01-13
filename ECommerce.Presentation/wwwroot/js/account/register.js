Vue.component("validation", VeeValidate.ValidationProvider);
Vue.component("validation-observer", VeeValidate.ValidationObserver);

// install localize
VeeValidate.localize({
  zh_TW: {
    names: {
      email: "電子郵件",
      password: "密碼",
      confirmPassword: "密碼再確認"
    },
    messages: zh_TW.messages
  }
});
// use current lang
VeeValidate.localize("zh_TW");

var registerVue = new Vue({
  el: "#registerVue",
  data: {
    email: "",
    password: "",
    confirmPassword: ""
  },
  methods: {
    onSubmit: function () {
      var self = this;
      var data = {
        Username: self.email,
        Email: self.email,
        Password: self.password,
        ConfirmPassword: self.confirmPassword
      };

      axios.post("/api/Account/Register", data)
        .then(function (response) {
          console.log(response);
          vueModal.show({
            titleText: "訊息",
            bodyText: response.data.message,
            cancelBtnText: false
          }).then(function (result) {
            if (result && response.data.isSuccess) {
              location.href = response.data.redirectUrl;
            }
          });
        }).catch(function (error) {
          console.log(error.response);
        });
    }
  }
});