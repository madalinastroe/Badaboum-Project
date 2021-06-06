<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card>
          <v-card-title> Add New Product </v-card-title>

          <v-card-text>
            <v-text-field
              v-model="product.name"
              outlined
              label="Name"
            ></v-text-field>
            <v-text-field
              v-model="product.description"
              outlined
              label="Description"
            ></v-text-field>
            <v-text-field
              v-model="product.picture"
              outlined
              label="Picture"
            ></v-text-field>
          </v-card-text>

          <v-card-actions>
            <v-btn
              outlined
              rounded
              color="brown darken-1"
              @click="createProduct()"
            >
              Add product
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>

      <v-col cols="12" lg="6" md="6" xs="12">
        <v-card>
          <v-img v-bind:src="`${product.picture}`" height="200px"></v-img>

          <v-card-title>
            {{ product.name }}
          </v-card-title>

          <v-card-subtitle>
            {{ product.description }}
          </v-card-subtitle>

          <v-btn color="orange accent-2" text> DETAILS </v-btn>

          <v-spacer></v-spacer>

          <v-row>
            <v-col align="right">
              <v-btn class="mx-2" fab dark x-small color="brown darken-1">
                <v-icon dark> mdi-share </v-icon>
              </v-btn>

              <v-btn class="mx-2" fab dark x-small color="brown darken-1">
                <v-icon dark> mdi-plus </v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </v-card>
      </v-col>

      <v-snackbar :color="snackbar.color" v-model="snackbar.show">
        {{ snackbar.message }}
      </v-snackbar>
    </v-row>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
  </v-container>
</template>

<script>
import axios from "axios";
import lodash from "lodash";
import { mapGetters } from "vuex";

import * as signalR from "@microsoft/signalr";

export default {
  name: "AddProduct",

  data() {
    return {
      product: {
        name: "",
        description: "",
        picture: "",
      },
      role: "",
      id: "",
      firstName: "",

      snackbar: {
        show: false,
        message: null,
        color: null,
      },

      message: "New product added",
      connection:null,
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
  },

  getters: {
    // ...
    isAuthenticated: (state) => {
      return state.isAuthenticated;
    },
  },

  computed: {
    ...mapGetters(["isAuthenticated", "user"]),
  },

  mounted() {
    this.user = this.$store.state.user;
    this.firstName = this.user.firstName;
    this.role = this.user.role;
    this.id = this.user.id;
    console.log("USER ID: " + this.id);
    console.log("USER NAME : " + this.firstName);
    console.log("USER ROLE: " + this.role);
  },

  methods: {
    createProduct() {
      axios
        .post("https://localhost:44396/api/products", this.product)
        .then(() => {

            this.connection.invoke("SendMessage", this.firstName, this.message).catch(err => console.log(err));
          this.$router.push({ name: "ShowProducts" });
        })
        .catch((err) => {
          this.snackbar = {
            message: "Error while adding this product.",
            color: "error",
            show: true,
          };
          //alert(JSON.stringify(err));
        });
    },
  },
};
</script>

