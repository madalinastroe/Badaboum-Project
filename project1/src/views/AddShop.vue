<template>


  <v-container>

    <v-row>
   
      <v-col> 

        <v-card>
            <v-card-title> Add New Shop </v-card-title>
         
            <v-card-text>
              <v-text-field v-model = "shop.name" outlined label = "Name"></v-text-field>        
              
                            
          </v-card-text>

          <v-card-actions>
          
            <v-btn
              outlined
              rounded
              color="brown darken-1"
              @click="createShop()"

            >
            Add shop
            </v-btn>
          </v-card-actions>

        </v-card>


      </v-col>


      <v-col cols="12" lg="6" md="6" xs="12">

      
        <v-card> 

          <v-card-title> 
          {{shop.name}}
          </v-card-title>
         
      <v-btn
        color="orange accent-2"
        text
      >
      DETAILS

      </v-btn>

      <v-spacer></v-spacer>
      
      
      <v-row>
      
        <v-col align="right">
           <v-btn
            class="mx-2"
            fab
            dark
            x-small
            color="brown darken-1"
          >
            <v-icon dark>
                mdi-share
            </v-icon>
          </v-btn>
       
          <v-btn
              class="mx-2"
              fab
              dark
              x-small
              color="brown darken-1"
            >
            <v-icon dark>
                mdi-plus
            </v-icon>
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
import axios from 'axios'
import lodash from 'lodash'



export default {
  name: 'AddShop',



  data()
  {
      return{
        shop: {
          name: '',

        },

        snackbar: {
        show: false,
        message: null,
        color: null

        },

      }
  },

   methods:
  {
    createShop()
    {
      axios.post('https://localhost:44396/api/shops',this.shop).then(()=>
        {
          this.$router.push({name: 'ShowShops'})
        }
      ).catch((err)=>{
          this.snackbar = 
          {
            message: 'Error while adding this shop.',
            color: 'error',
            show: true
          }
          //alert(JSON.stringify(err));
      });
    },

   
   
  }
}
</script>

