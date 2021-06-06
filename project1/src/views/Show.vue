<template>
<v-container class="grey lighten-5">

<v-row>
    <v-col>
        <v-btn
          depressed
          color="brown lighten-1"
          @click="$router.push({name: 'About'})"
          v-if = "isAuthenticated && role=='admin'"
        >
          Add New User
            <v-icon >mdi-account</v-icon>
      </v-btn>
    </v-col>
</v-row>

<v-row></v-row>
    <v-row>

      <v-col cols="8">
          <v-row>
            <v-col cols="12" lg="4" md="10" xs="10" v-for="user in users" :key="user.id">

    
    <v-hover
        v-slot="{ hover }"
        open-delay="200"
      >
        <v-card
          :elevation="hover ? 16 : 2"
          :class="{ 'on-hover': hover }"
          class="mx-auto"
          @click="selectUser(user)"
        >
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

    <div v-if=" isAuthenticated && role=='admin'">
     <v-card-subtitle>
        <p class="font-weight-medium">E-mail: </p> {{user.email}}
     </v-card-subtitle>

     <v-card-subtitle>
        <p class="font-weight-medium"> Address: </p> {{user.address}}
     </v-card-subtitle>

     <v-card-subtitle>
      <p class="font-weight-medium">Password: </p> {{user.password}}
     </v-card-subtitle>

    </div>

    <v-card-actions>
     

      <v-spacer></v-spacer>

    <v-btn v-if=" isAuthenticated && role=='admin'"
          class="mx-2"
          fab
          dark
          x-small
          color="brown darken-1"
        >
        <v-icon  dark @click.stop="deleteUser(user)">
            mdi-delete
         </v-icon>
    </v-btn>

    </v-card-actions>

        </v-card>
      </v-hover>              
    <v-card  >
 
   
   
    </v-card>
            </v-col>
          </v-row>
      </v-col>

  <v-col cols="4" lg="4" ms="6" xs="12">

     <v-card
    outlined
    v-if="selectedUser != null  && isAuthenticated && role=='admin'"
  >
  <v-card-title> Update Users </v-card-title>

    <v-card-text >

      <v-text-field v-model = "selectedUser.firstName" label="First Name" outlined ></v-text-field>
      <v-text-field v-model = "selectedUser.lastName" label="LastName" outlined ></v-text-field>
      <v-text-field v-model = "selectedUser.address" label="Address" outlined ></v-text-field>
      <v-text-field v-model = "selectedUser.email" label="E-mail" outlined ></v-text-field>
      <v-text-field v-model = "selectedUser.password" label="Password" outlined ></v-text-field>
      <v-text-field v-model = "selectedUser.avatar" label="Avatar" outlined ></v-text-field>

  </v-card-text>

    <v-card-actions>

      <v-btn
        outlined
        rounded
        color="brown darken-1"

        @click="updateUser()"
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
  name: 'Show',

  data()
  {
    //returneaza un obiect (toate proprietatile pe care le folosim in template)
    return{
      users: [],
      selectedUser : null,
      role:'',
      firstName:'',
      id:'',

    }
  },


  created()
  {
      this.loadUsers()

       axios.get('https://localhost:44396/api/users').then((response)=>{     

        //this.names=response.data.map(x=>x.firstName);

      }).catch((err)=>{
         
          console.log('======ERROR=====')
      });
      
  },

  getters: {
      // ...
      isAuthenticated: (state) => {
        return state.isAuthenticated;
      },

    
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

    deleteUser(user)
    {
        console.log("DELETE USER");
         axios.delete(`https://localhost:44396/api/users/${user.id}`).then((response)=>
      {
        //readucem datele de pe server
        this.loadUsers();
        this.user=null;
      }
      );
    },

    updateUser()
    {
      axios.put(`https://localhost:44396/api/users/${this.selectedUser.id}`,this.selectedUser).then((response)=>
      {
        //readucem datele de pe server
        this.loadUsers();
      }
      );
    },

    selectUser(user)
    {
        console.log("SELECT USER");
        this.selectedUser = lodash.cloneDeep(user); //creeaza clona a userului si nu se reflecta modificarile
    },

    loadUsers()
    {

        //metodele returneaza un promise ca se va rezolva requestul acesta la un moment dat
      //ce e in then, se executa dupa ce endpointul din get raspunde
      //la http serverul nu raspunde niciodata neintrebat

      axios.get('https://localhost:44396/api/users').then((response)=>{     

        console.log('=====SUCCESS=====')
        this.users=response.data;

      }).catch((err)=>{
         
          console.log('======ERROR=====')
      });
    }   

  }
 
}
</script>
