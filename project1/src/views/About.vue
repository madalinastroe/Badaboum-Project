<template>


  <v-container>
    <v-row>
   
      <v-col> 

        <v-card>
            <v-card-title> Registration Form </v-card-title>
         
            <v-card-text>
              <v-text-field v-model = "user.firstName" outlined label = "First Name"></v-text-field>
              <v-text-field v-model = "user.lastName" outlined label = "Last Name"></v-text-field>
              <v-text-field v-model = "user.email" outlined label = "E-mail"></v-text-field>
              <v-text-field v-model = "user.password" type="password" outlined label = "Password"></v-text-field>
              <v-text-field v-model = "user.address" outlined label = "Address"></v-text-field>
              <v-text-field v-model = "user.avatar" outlined label = "Avatar"></v-text-field>

              
                            
          </v-card-text>

          <v-card-actions>
          
            <v-btn
              outlined
              rounded
              color="brown darken-1"
              @click="createUser()"

            >
            
            Register
            </v-btn>
          </v-card-actions>

        </v-card>


      </v-col>


      <v-col cols="12" lg="6" md="6" xs="12">

      
        <v-card> 
            <v-img
          v-bind:src="`${user.avatar}`"

          height="200px"
        ></v-img>

          <v-card-title> 
          {{user.firstName}}
          </v-card-title>

          <v-card-subtitle>
             {{user.lastName}}
          </v-card-subtitle>

          <v-card-subtitle>
            {{user.email}}
          </v-card-subtitle>

          <v-card-subtitle>
            {{user.password}}
          </v-card-subtitle>

          <v-card-subtitle>
            {{user.address}}
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
  name: 'About',



  data()
  {
      return{
        user: {
          firstName: '',
          lastName: '',
          email: '',
          address: '',
          avatar:'',
          role:''
        },

        snackbar: {
        show: false,
        message: null,
        color: null

        },

        image: null,
        imageUrl: '',
      }
  },

   methods:
  {
    createUser()
    {
      this.user.role="user";
      axios.post('https://localhost:44396/api/users',this.user).then(()=>
        {
          this.$router.push({name: 'Home'})
        }
      ).catch((err)=>{
          this.snackbar = 
          {
            message: 'Another account is using this e-mail',
            color: 'error',
            show: true
          }
          //alert(JSON.stringify(err));
      });
    },

   
   
  }
}
</script>

