<template>


  <v-container>

              <v-form
            ref="form"
            v-model="valid"
            lazy-validation
            >

    <v-row>
        <v-col>
             <v-row>
                <v-img
                    src="https://cdni.iconscout.com/illustration/premium/thumb/login-here-2161443-1815085.png"
                    height="500px"
                    width="400px"
                >
                </v-img>
             </v-row>

             <v-row
                align="center"
                justify="space-around"
             >
                <v-btn icon href="/About" target="_blank" >
                    You don't have an account? Register 
                    <div class="text-decoration-underline font-weight-bold mx-1"> here! </div>
                </v-btn>
             </v-row>
        </v-col>
        
        <v-col></v-col>

        <v-col cols="8" lg="6" md="8" xs="12" >
            <v-card outlined 
                class="pa-md-6 mx-lg-auto"
                width="500px"
            >

                <v-card-title primary-title class="justify-center"> Login </v-card-title>
                <v-card-text>
    


            <v-text-field
                v-model="email"
                :rules="emailRules"
                label="E-mail"
                required
            ></v-text-field>

            <v-text-field
                v-model="password"
                :rules="passwordRules"
                type="password"
                label="Password"
                required
            ></v-text-field>


                </v-card-text>

                <v-card-actions>
          
          
         <v-row
            align="center"
            justify="space-around"
        >

        <v-btn 
            rounded
            color="deep-orange accent-2"
            dark
            @click="login()"
        >
            Sign in
        </v-btn>     



        </v-row>
        
        
       
          </v-card-actions>

         
        </v-card>

        </v-col>

         <v-snackbar :color="snackbar.color" v-model="snackbar.show">
              {{ snackbar.message }}
          </v-snackbar>

        
    </v-row>

</v-form>

  </v-container>

</template>

<script>
import axios from 'axios'
import lodash from 'lodash'
import {mapGetters} from 'vuex'


export default {
  name: 'Login',


  data()
  {
     return{

       valid: false,

        email:'',
        emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
      ],
        password:'',
        passwordRules: [
        v => !!v || 'Password is required',
      ],
        user: null,

        snackbar: {
        show: false,
        message: null,
        color: null

        },

     }

  },
  

   methods:
  {
      login()
      {

         this.$refs.form.validate();

          if(this.valid)
          {
               this.$http.post('https://localhost:44396/api/users/login', {email: this.email, password: this.password}).then((response) => {

                  this.$store.dispatch('login', response.data);

                  this.$router.push({name: 'Show'})

               }

            ).catch(()=> {
              this.snackbar = 
              {
                message: 'Incorrect username or password',
                color: 'error',
                show: true 
              }
            })
          }

      }
  }
}
</script>
