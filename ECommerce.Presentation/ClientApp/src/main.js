import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/css/all.css";
import "../styles/site.css";
import $ from "jquery";
import Vue from "vue";
import axios from "axios";

window.axios = axios;
window.Vue = Vue;
window.jQuery = $;
window.$ = $;
window.jquery = $;

import "popper.js";
import "bootstrap";

//Add a namspace
window.MyWebApp = {};

var Routes = {
  Home: {
    init: function() {
      // controller-wide code
    },
    Privacy: function() {
      // Privacy action code
    }
  },
  Account: {
    init: function() {
      // controller-wide code
    },
    Login: function() {
      import("./account/login").then(module => {
        window.MyWebApp.login = module.login;
      });
    },
    Register: function() {
      import("./account/register").then(module => {
        window.MyWebApp.register = module.register;
      });
    }
  }
};

var Router = {
  exec: function(controller, action) {
    action = action === undefined ? "init" : action;

    if (
      controller !== "" &&
      Routes[controller] &&
      typeof Routes[controller][action] === "function"
    ) {
      Routes[controller][action]();
    }
  },

  init: function() {
    let body = document.body;
    let controller = body.getAttribute("data-controller");
    let action = body.getAttribute("data-action");

    Router.exec(controller);
    Router.exec(controller, action);
  }
};

//run this immediately
Router.init();
