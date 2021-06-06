<template>
  <v-container>

  <v-row>
    <v-col>
         <v-btn
          depressed
          color="brown lighten-1"
          @click="$router.push({name: 'AddShop'})"
          v-if = "isAuthenticated && role=='admin'"
        >
            <v-icon >mdi-account</v-icon>
            Add New Shop
      </v-btn>
    </v-col>
  </v-row>


    <v-row>
      <v-col cols="8">
        <v-row>
            <v-col cols="12" lg="4" md="10" xs="10" v-for="shop in shops" :key="shop.shopID">
    

    <v-hover
        v-slot="{ hover }"
        open-delay="200"
      >
        <v-card
          :elevation="hover ? 16 : 2"
          :class="{ 'on-hover': hover }"
         @click="selectShop(shop)"
         
        >
        
 <v-card-title>
     {{shop.name}}
    </v-card-title>


    <v-card-actions>
   
         <v-spacer></v-spacer>

      <v-btn v-if = "isAuthenticated && role=='admin'""
          class="mx-2"
          fab
          dark
          x-small
          color="brown darken-1"
        >
        <v-icon dark @click.stop="deleteShop(shop)">
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
    v-if="selectedShop != null"
  >
  <v-card-title> Update Users </v-card-title>

    <v-card-text>

      <v-text-field v-model = "selectedShop.name" label="Name" outlined ></v-text-field>

  </v-card-text>

    <v-card-actions>

      <v-btn
        outlined
        rounded
        color="brown darken-1"

        @click="updateShop()"
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

import axios from 'axios'
import lodash from 'lodash'
import {mapGetters} from 'vuex'

export default {
  name: 'ShowShops',

  data()
  {
    return{
      shops: [],
      selectedShop : null,
      role:'',
      id:'',
      firstName:'',

    }
  },

  created()
  {
      this.loadShops()
  },

  getters: {
      // ...
      isAuthenticated: (state) => {
        return state.isAuthenticated;
      }
    },

    computed: {
    ...mapGetters([

      'isAuthenticated',
      
    ])
  },


  mounted()
  {
    this.user=this.$store.state.user;
    this.firstName = this.user.firstName;
    this.role = this.user.role;
    this.id = this.user.id;
    console.log("USER ID: "+ this.id);
    console.log("USER NAME : "+ this.firstName);
    console.log("USER ROLE: "+ this.role);
  },
  methods:
  {

    deleteShop(shop)
    {
        console.log("DELETE SHOP");
         axios.delete(`https://localhost:44396/api/shops/${shop.shopID}`).then((response)=>
      {
        //readucem datele de pe server
        this.loadShops();
        this.shop=null;
      }
      );
    },

    updateShop()
    {
      axios.put(`https://localhost:44396/api/shops/${this.selectedShop.shopID}`,this.selectedShop).then((response)=>
      {
        //readucem datele de pe server
        this.loadShops();
      }
      );
    },

    selectShop(shop)
    {
      console.log("SELECT SHOP");

      if(this.role=='admin')
      {
        this.selectedShop = lodash.cloneDeep(shop); 
      }
    },

    loadShops()
    {

      axios.get('https://localhost:44396/api/shops').then((response)=>{

         console.log('=====SUCCESS=====')
         console.log(response);
         this.shops=response.data; //am structura pt user
      }).catch((err)=>{
        
          console.log('======ERROR=====')
      });
    },

   

  }
 
}
</script>