Vue.component("validation", VeeValidate.ValidationProvider);

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

Vue.use(VeeValidate);

const i18n = new VueI18n({
  locale: "zh_TW",
  zh_TW: {
    validations: zh_TW
  }
});

var registerVue = new Vue({
  el: "#registerVue",
  data: {
    email: "",
    password: "",
    confirmPassword: ""
  },
  methods: {
    validateForm: function() {},
    submit: function() {}
  }
});
