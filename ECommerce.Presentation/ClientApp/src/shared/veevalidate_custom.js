import VeeValidate, {
  ValidationObserver,
  ValidationProvider,
  localize,
  extend
} from "vee-validate";

export const custom_rule = function() {
  extend("", {
    params: ["url", "arg"],
    validate: (value, { url, arg }) => {},
    message: ""
  });
};
