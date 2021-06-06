<template>
  <v-container>
    <v-row>
      <v-col> 

        <v-card>
            <v-card-title> Add New Element </v-card-title>
         
            <v-card-text>

                <v-col cols="12">
                    <v-autocomplete
                        v-model="shop.shopName"
                        :items="shopNames"
                        dense
                        filled
                        label="Shops"
                    ></v-autocomplete>
                </v-col>

                  <v-col cols="12">
                    <v-autocomplete
                        v-model="shop.productName"
                        :items="productNames"
                        dense
                        filled
                        label="Products"
                    ></v-autocomplete>
                </v-col>

                <v-text-field v-model = "shop.stock" outlined label = "Stock"></v-text-field>        
                <v-text-field v-model = "shop.quantity" outlined label = "Quantity"></v-text-field>        
                <v-text-field v-model = "shop.price" outlined label = "Price"></v-text-field>    
    
                            
          </v-card-text>

          <v-card-actions>
          
          <v-row>
            <v-col>

            <v-btn
              outlined
              rounded
              color="brown darken-1"
              @click="createSP()"

            >
            Add element
            </v-btn>



            </v-col>

            <v-col 
                  class="d-flex justify-end"

            >
              <v-btn
              outlined
              rounded
              color="brown darken-1"
              @click="seeProductPicture()"

            >

            See photo

            </v-btn>
            </v-col>

            </v-row>

          </v-card-actions>

        </v-card>


      </v-col>


      <v-col>

      
        <v-card> 

          <v-img
            v-bind:src="`${shop.productPicture}`"

            height="200px"
            ></v-img>

          <v-card-subtitle> 
          Shop Name: {{shop.shopName}}
          </v-card-subtitle>

          <v-card-subtitle> 
          Product Name: {{shop.productName}}
          </v-card-subtitle>

          <v-card-subtitle> 
          Stock: {{shop.stock}}
          </v-card-subtitle>

          <v-card-subtitle> 
          Quantity: {{shop.quantity}}
          </v-card-subtitle>

          <v-card-subtitle> 
          Price: {{shop.price}}
          </v-card-subtitle>  

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
  name: 'AddSP',



  data()
  {
      return{
        shop: {
            shopID:null,
            productID:null,
          shopName: '',
          productName:'',
          productPicture:'',
          quantity:'',
          stock:'',
          price:''
        },

        shopNames:[],
        productNames:[],

        value:null,
        value1:'',
        picture:'',

        snackbar: {
        show: false,
        message: null,
        color: null

        },

      }
  },

  created()
  {
      this.getShopNames();
      this.getProductNames();
  },

   methods:
  {

    getShopNames()
    {
        axios.get('https://localhost:44396/api/shops').then((response)=>{

         console.log('=====SUCCESS=====')
         return response.data.filter(shop=>
         {
            this.shopNames.push(shop.name);
         });

      }).catch((err)=>{
        
          console.log('======ERROR=====')
      });
    },

    getProductNames()
    {
        axios.get('https://localhost:44396/api/products').then((response)=>{

         console.log('=====SUCCESS=====')
         return response.data.filter(product=>
         {
            this.productNames.push(product.name);
         });

      }).catch((err)=>{
        
          console.log('======ERROR=====')
      });
    },
    

    seeProductPicture()
    {
        if(this.shop.shopName!='')
        {
            axios.post('https://localhost:44396/api/shops/getid' + '?shopName=' + this.shop.shopName).then((response)=>
            {
                this.shop.shopID = response.data.shopID;
                return this.shop.shopID;

            }).catch((error) => {

            console.log('======ERROR=====')
            //alert(JSON.stringify(error));
            });
        }

       if(this.shop.productName!='')
       {
           axios.post('https://localhost:44396/api/products/getpicture' + '?productName=' + this.shop.productName).then((response)=>
            {
                this.shop.productID = response.data.productID;
                this.shop.productPicture = response.data.picture;

            return this.shop.productPicture;

            }).catch((error) => {

            console.log('======ERROR=====')
            //alert(JSON.stringify(error));
            });
       }
     
    },

    createSP()
    {
        console.log(JSON.stringify(this.shop));
        axios.post('https://localhost:44396/api/shopproducts',this.shop).then(()=>
        {
            
            this.$router.push({name: 'ShowShopProducts'})
        }
        ).catch((err)=>{
            this.snackbar = 
            {
                message: 'Duplicate element',
                color: 'error',
                show: true
            }
            //alert(JSON.stringify(err));
        });
    },
   


  }
}
</script>

