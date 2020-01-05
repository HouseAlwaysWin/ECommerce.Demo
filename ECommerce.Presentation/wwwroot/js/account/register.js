Vue.component("validation", VeeValidate.ValidationProvider);

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
