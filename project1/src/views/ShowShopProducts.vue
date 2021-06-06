<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn
          depressed
          color="brown lighten-1"
          @click="$router.push({ name: 'AddSP' })"
          v-if="isAuthenticated && role == 'admin'"
        >
          <v-icon>mdi-account</v-icon>
          Add New Element
        </v-btn>
      </v-col>

      <v-col
        class="d-flex justify-end"
        v-if="isAuthenticated && role == 'user'"
      >
        <v-card outline>
          <v-navigation-drawer
            v-model="drawer"
            :mini-variant.sync="mini"
            permanent
          >
            <v-list-item class="px-2">
              <v-list-item-avatar>
                <v-img
                  src="https://www.charge.com/wp-content/uploads/2015/12/cart.png"
                ></v-img>
              </v-list-item-avatar>

              <v-list-item-title> {{ this.firstName }} </v-list-item-title>

              <v-btn icon @click.stop="mini = !mini">
                <v-icon>mdi-chevron-left</v-icon>
              </v-btn>
            </v-list-item>

            <v-divider></v-divider>

            <v-list dense>
              <v-row>
                <v-col>
                  <v-list-item v-for="item in orders" :key="item.orderID">
                    <v-list-item-title class="d-flex justify-center md-4">{{
                      item.productName
                    }}</v-list-item-title>

                    <v-list-item-title class="d-flex justify-center md-4"
                      >{{ item.price }} lei</v-list-item-title
                    >

                    <v-btn
                      class="mx-2"
                      fab
                      x-small
                      color="brown lighten-1"
                      @click="deleteOrder(item.orderID)"
                    >
                      <v-icon dark> mdi-delete </v-icon>
                    </v-btn>
                  </v-list-item>
                </v-col>
              </v-row>

              <v-row>
                <v-col class="d-flex justify-center md-4">
                  <v-btn
                    rounded
                    color="brown lighten-1"
                    @click="$router.push({ name: 'Home' })"
                  >
                    ORDER
                  </v-btn>
                </v-col>

                <v-col class="d-flex justify-center md-4">
                  <v-list-item-title>
                    TOTAL: {{ this.totalCost }} lei
                  </v-list-item-title>
                </v-col>
              </v-row>
            </v-list>
          </v-navigation-drawer>
        </v-card>
      </v-col>
    </v-row>

    <v-row></v-row>

    <v-row>
      <v-col cols="8">
        <v-row>
          <v-col
            cols="12"
            lg="4"
            md="10"
            xs="10"
            v-for="product in products"
            :key="product.productID && product.shopID"
          >
            <v-hover v-slot="{ hover }" close-delay="200">
              <v-card
                :elevation="hover ? 16 : 2"
                :class="{ 'on-hover': hover }"
                @click="selectProduct(product)"
              >
                <v-img
                  v-bind:src="`${product.productPicture}`"
                  height="200px"
                ></v-img>

                <v-card-title>
                  {{ product.productName }}
                </v-card-title>

                <v-card-subtitle>
                  Shop: {{ product.shopName }}
                </v-card-subtitle>

                <v-card-subtitle v-if="isAuthenticated && role == 'admin'">
                  Stock:
                  {{ product.stock }}
                </v-card-subtitle>

                <v-card-subtitle v-if="isAuthenticated && role == 'user'">
                  Quantity:
                  {{ product.quantity }}
                </v-card-subtitle>

                <v-card-subtitle>
                  <v-icon>mdi-cash</v-icon>:
                  {{ product.price }}
                </v-card-subtitle>

                <v-card-actions>
                  <v-col>
                    <v-btn
                      class="ma-2"
                      color="orange accent-2"
                      @click="expand = !expand"
                      v-if="isAuthenticated && role == 'admin'"
                    >
                      More
                    </v-btn>

                    <v-expand-transition>
                      <v-card v-show="expand" height="120" width="120">
                        <v-row>
                          <v-col>
                            <v-btn
                              v-show="expand"
                              height="50"
                              width="120"
                              color="orange accent-2"
                              @click="$router.push({ name: 'ShowProducts' })"
                            >
                              Edit Product
                            </v-btn>
                          </v-col>
                        </v-row>

                        <v-row>
                          <v-col>
                            <v-btn
                              v-show="expand"
                              height="50"
                              width="120"
                              color="orange accent-2"
                              @click="$router.push({ name: 'ShowShops' })"
                            >
                              Edit Shop
                            </v-btn>
                          </v-col>
                        </v-row>
                      </v-card>
                    </v-expand-transition>
                  </v-col>

                  <v-spacer></v-spacer>

                  <v-btn
                    v-if="isAuthenticated && role == 'user'"
                    class="mx-2"
                    fab
                    dark
                    small
                    color="brown darken-1"
                    @click="addToCart(product)"
                  >
                    <v-icon dark> mdi-cart-outline </v-icon>
                  </v-btn>

                  <v-btn
                    v-if="isAuthenticated && role == 'admin'"
                    class="mx-2"
                    fab
                    dark
                    x-small
                    color="brown darken-1"
                  >
                    <v-icon dark @click="deleteProduct(product)">
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
        <v-card outlined v-if="selectedProduct != null">
          <v-card-title> Update Element </v-card-title>

          <v-card-text v-if="isAuthenticated && role == 'user'">
            <v-text-field
              v-model="selectedProduct.quantity"
              label="Quantity"
              outlined
            ></v-text-field>
          </v-card-text>

          <v-card-text v-if="isAuthenticated && role == 'admin'">
            <v-text-field
              v-model="selectedProduct.stock"
              label="Stock"
              outlined
            ></v-text-field>
            <v-text-field
              v-model="selectedProduct.price"
              label="Price"
              outlined
            ></v-text-field>
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

    <v-snackbar :color="snackbar.color" v-model="snackbar.show">
      {{ snackbar.message }}
    </v-snackbar>
  </v-container>
</template>

<script>
import axios from "axios";
import lodash from "lodash";
import { mapGetters } from "vuex";

export default {
  name: "ShowShopProducts",

  data() {
    return {
      products: [],
      selectedProduct: null,
      role: "",
      id: "",
      firstName: "",
      expand: false,

      order: {
        shopID: null,
        shopName: "",
        productID: null,
        productName: "",
        userID: null,
        userName: "",
        status: "",
        price: null,
      },
      orders: [],
      drawer: true,
      mini: true,
      totalCost: 0,
      shopID: null,
      productID: null,
      userID: null,

      snackbar: {
        show: false,
        message: null,
        color: null,
      },

      items: [
        { title: "Home", icon: "mdi-home-city" },
        { title: "My Account", icon: "mdi-account" },
        { title: "Users", icon: "mdi-account-group-outline" },
      ],
    };
  },

  created() {
    this.loadProducts();
    this.loadOrders();
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
    deleteOrder(order) {
      axios
        .delete(`https://localhost:44396/api/orders/${order}`)
        .then((response) => {
          this.order = null;
          this.loadOrders();
        });
    },

    loadOrders() {
      console.log("ID: " + this.user.id);
      axios
        .get(`https://localhost:44396/api/orders/user/${this.user.id}`)
        .then((response) => {
          this.orders = [];
          return response.data.filter((order) => {
            this.totalCost = this.totalCost + order.price;
            console.log(this.totalCost);
            this.orders.push(order);
          });
        })
        .catch((err) => {
          console.log("======ERROR=====");
        });
    },

    addToCart() {
      this.purchase = this.selectedProduct;

      if (this.purchase.quantity != 0) {
        this.order.shopID = this.purchase.shopID;
        this.order.shopName = this.purchase.shopName;
        this.order.productID = this.purchase.productID;
        this.order.productName = this.purchase.productName;
        this.order.userID = this.id;
        this.order.userName = this.firstName;
        this.order.status = "processing";
        this.order.price = this.purchase.price * this.purchase.quantity;

        axios
          .post("https://localhost:44396/api/orders", this.order)
          .then(() => {
            this.loadOrders();
          })
          .catch((err) => {
            this.snackbar = {
              message: "Error while adding this order",
              color: "error",
              show: true,
            };
            //alert(JSON.stringify(err));
          });

        this.order = null;
      } else {
        this.snackbar = {
          message: "Select Quantity for this product",
          color: "error",
          show: true,
        };
      }
    },

    updateProduct() {
      axios
        .put(
          `https://localhost:44396/api/shopproducts/updateElement`,
          this.selectedProduct
        )
        .then((response) => {
          //readucem datele de pe server
          console.log(this.selectedProduct.picture);
          this.loadProducts();
        });
    },

    selectProduct(product) {
      this.selectedProduct = lodash.cloneDeep(product); //creeaza clona a userului si nu se reflecta modificarile
    },

    loadProducts() {
      //metodele returneaza un promise ca se va rezolva requestul acesta la un moment dat
      //ce e in then, se executa dupa ce endpointul din get raspunde
      //la http serverul nu raspunde niciodata neintrebat

      axios
        .get("https://localhost:44396/api/shopproducts")
        .then((response) => {
          this.products = response.data; //am structura pt user
        })
        .catch((err) => {
          //this se refera la instanta de view si vreau sa accesez users cu ele
          //tot ce am in data accesez cu this
          //this.loadUsers pentru metode
          console.log("======ERROR=====");
        });
    },

    deleteProduct(product) {
      console.log("DELETE SHOP PRODUCT");
      if (this.role == "admin") {
        this.selectedProduct = lodash.cloneDeep(product); //creeaza clona a userului si nu se reflecta modificarile
        console.log("DELETING: " + JSON.stringify(this.selectedProduct));

        axios
          .delete(
            "https://localhost:44396/api/shopproducts/deletesp?" +
              "shopID=" +
              this.selectedProduct.shopID +
              "&" +
              "productID=" +
              this.selectedProduct.productID
          )
          .then((response) => {
            //readucem datele de pe server
            this.loadProducts();
            this.product = null;
          });
      }
    },
  },
};
</script>