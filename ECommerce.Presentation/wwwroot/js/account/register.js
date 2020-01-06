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
    validateForm: function() {},
    onSubmit: function() {
      alert("submit");
      var self = this;
      var data = {
        Username: self.email,
        Email: self.email,
        Password: self.password,
        ConfirmPassword: self.confirmPassword,
        ExternalLogins: [],
        ReturnUrl: "/"
      };
      console.log(data);
      axios.post("/Account/Register", data).then(function(response) {
        console.log(response);
      });
    }
  }
});
