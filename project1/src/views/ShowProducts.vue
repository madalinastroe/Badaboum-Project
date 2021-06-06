<template>
  <v-container>

<v-row>
    <v-col>
        <v-btn
        depressed
        color="brown lighten-1"
        @click="$router.push({name: 'AddProduct'})"
        v-if = "isAuthenticated && role=='admin'"
      >
          <v-icon >mdi-account</v-icon>
          Add New Product
      </v-btn>
    </v-col>
</v-row>


<v-row></v-row>


    <v-row>
      <v-col cols="8">
        <v-row>
            <v-col cols="12" lg="4" md="10" xs="10" v-for="product in products" :key="product.productID">
   
 <v-hover
        v-slot="{ hover }"
        close-delay="200"
      >
        <v-card
          :elevation="hover ? 16 : 2"
          :class="{ 'on-hover': hover }"
        
          @click="selectProduct(product)"
        >
          
 <v-img
      v-bind:src="`${product.picture}`"
      height="200px"
    ></v-img>


    <v-card-title>
    
     {{product.name}}
    </v-card-title>

    <v-card-subtitle>
       {{product.description}}
     </v-card-subtitle>

    <v-card-actions>
    
      <v-spacer></v-spacer>


    <v-btn v-if = "isAuthenticated && role=='admin'""
          class="mx-2"
          fab
          dark
          x-small
          color="brown darken-1"
        >
        <v-icon dark @click.stop="deleteProduct(product)">
            mdi-delete
         </v-icon>
    </v-btn>
      
    </v-card-actions>

   

        </v-card>
      </v-hover>

            </v-col>
          </v-row>
      </v-col>

  <v-col cols="4" lg="4" ms="6" xs="12">

     <v-card
    outlined
    v-if="selectedProduct != null"
  >
  <v-card-title> Update Products </v-card-title>

    <v-card-text>

      <v-text-field v-model = "selectedProduct.name" label="Name" outlined ></v-text-field>
      <v-text-field v-model = "selectedProduct.description" label="Description" outlined ></v-text-field>
      <v-text-field v-model = "selectedProduct.picture" label="Picture" outlined ></v-text-field>


  </v-card-text>

    <v-card-actions>

      <v-btn
        outlined
        rounded
        color="brown darken-1"

        @click="updateProduct()"
      >
        Save Changes
      </v-btn>
    </v-card-actions>
  </v-card>
  </v-col>
    </v-row>

  </v-container>

</template>

<script>
import axios from "axios";
import lodash from "lodash";
import { mapGetters } from "vuex";

export default {
  name: "ShowProducts",

  data() {
    return {
      products: [],
      selectedProduct: null,
      role: "",
      firstName: "",
      id: "",
      cartItems: [],

      
    };
  },

  created() {
    this.loadProducts();
  },

  getters: {
    // ...
    isAuthenticated: (state) => {
      return state.isAuthenticated;
    },
  },

  computed: {
    ...mapGetters(["isAuthenticated"]),
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
    deleteProduct(product) {
      console.log("DELETE PRODUCT");
      axios
        .delete(`https://localhost:44396/api/products/${product.productID}`)
        .then((response) => {
          //readucem datele de pe server
          this.loadProducts();
          this.product = null;
        });
    },

    updateProduct() {
      axios
        .put(
          `https://localhost:44396/api/products/${this.selectedProduct.productID}`,
          this.selectedProduct
        )
        .then((response) => {
          //readucem datele de pe server
          console.log(this.selectedProduct.picture);
          this.loadProducts();
        });
    },

    selectProduct(product) {
      console.log("SELECT PRODUCT");
      if (this.role == "admin") {
        this.selectedProduct = lodash.cloneDeep(product); //creeaza clona a userului si nu se reflecta modificarile
      }
    },

    addToCart(product) {
      if (this.role == "user") {
        this.selectedProduct = lodash.cloneDeep(product); //creeaza clona a userului si nu se reflecta modificarile
      }

      this.cartItems.push(this.selectedProduct);
      console.log(this.cartItems);
    },

    loadProducts() {
      //metodele returneaza un promise ca se va rezolva requestul acesta la un moment dat
      //ce e in then, se executa dupa ce endpointul din get raspunde
      //la http serverul nu raspunde niciodata neintrebat

      axios
        .get("https://localhost:44396/api/products")
        .then((response) => {
          console.log("=====SUCCESS=====");
          //console.log(response);

          this.products = response.data; //am structura pt user
          this.snackbar = {
            message: "New product added.",
            color: "blue",
            show: true,
          };
        })
        .catch((err) => {
          //this se refera la instanta de view si vreau sa accesez users cu ele
          //tot ce am in data accesez cu this
          //this.loadUsers pentru metode
          console.log("======ERROR=====");
        });
    },
  },
};
</script>