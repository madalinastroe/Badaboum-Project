<template>
  <div>
    <v-app>
      <v-app-bar
        app
        color="brown darken-1
"
        dark
      >
        <div class="d-flex align-center" style="width: 8rem">
          <h1 @click="$router.push({ name: 'Home' })" style="cursor: pointer">
            Badaboum
          </h1>
        </div>

        <v-spacer></v-spacer>

        <v-spacer></v-spacer>

        <v-btn @click="$router.push({ name: 'Home' })" icon>
          <v-icon>mdi-home</v-icon>
        </v-btn>

        <v-btn
          v-if="isAuthenticated"
          @click="
            $store.dispatch('logout');
            $router.push({ name: 'Login' });
          "
          icon
        >
          <v-icon>mdi-logout</v-icon>
        </v-btn>

    <v-snackbar :color="snackbar.color" v-model="snackbar.show">
        {{ snackbar.message }}
    </v-snackbar>
  
      </v-app-bar>

      <v-main>
        <router-view></router-view>
      </v-main>
    </v-app>

    <v-row v-if="isAuthenticated">
      <v-bottom-navigation color="teal" grow>
        <v-btn @click="$router.push({ name: 'Show' })">
          <span>Users</span>

          <v-icon>{{ icons.mdiAccount }}</v-icon>
        </v-btn>

        <v-btn @click="$router.push({ name: 'ShowProducts' })">
          <span>Products</span>

          <v-icon>mdi-gift</v-icon>
        </v-btn>

        <v-btn @click="$router.push({ name: 'ShowShops' })">
          <span>Shops</span>

          <v-icon>mdi-map-marker</v-icon>
        </v-btn>

        <v-btn @click="$router.push({ name: 'ShowShopProducts' })">
          <span>Shop Products</span>

          <v-icon>mdi-cart-outline</v-icon>
        </v-btn>
      </v-bottom-navigation>
    </v-row>
  </div>
</template>

<script>
import HelloWorld from "./components/HelloWorld";
import { mapGetters } from "vuex";

import * as signalR from "@microsoft/signalr";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

import {
  mdiAccount,
  mdiPencil,
  mdiShareVariant,
  mdiDelete,
  mdiGift,
  mdiCartOutline,
  mdiCash,
} from "@mdi/js";

export default {
  name: "App",

  components: {
    HelloWorld,
  },

  data() {
    return {
      icons: {
        mdiAccount,
        mdiPencil,
        mdiShareVariant,
        mdiDelete,
        mdiCash,
      },

      snackbar: {
        show: false,
        message: null,
        color: null,
      },

      message: "",
      connection: null,
      user:''
    };
  },

  created() {
     this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44396/socket", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();


    this.connection
      .start()
      .then(() => console.log("Connection started"))
      .catch(function (e) {
        console.log("Cannot start connection");
        console.log(e.message);
      });

      this.connection.on("ReceiveMessage", (user, message) => {
          console.log("MESSAGE: " + message);

//          alert(message);
           this.snackbar = {
            message: "New product added",
            color: "blue",
            show: true,
          };
      });
  },


  computed: {
    ...mapGetters(["isAuthenticated"]),
  },


}
</script>

