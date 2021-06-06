<template>

    <v-container>

    <v-row>
        <v-col cols="12"
            align="center"
        >
            <h1 class=".font-weight-bold display-3"> Welcome to Badaboum</h1>
        </v-col>
    </v-row>

    <v-row >

    <v-col></v-col>

       
        </v-col>

        <v-col></v-col>
    </v-row>


     <v-row>
      
        <v-col
            align="center"
        >
        <v-btn v-if="!isAuthenticated" 
                color="brown lighten-1"
                elevation="2"
                rounded
                outlined
                @click="$router.push({name: 'Login'})"
            >Login</v-btn>


          <v-btn v-if="isAuthenticated" 
                color="brown lighten-1"
                elevation="2"
                rounded
                outlined
                @click="$store.dispatch('logout');
                $router.push({name: 'Login'})"
            >Logout</v-btn>


        </v-col>
    </v-row>
        
    </v-container>

</template>

<script>

import axios from 'axios'
import lodash from 'lodash'
import {mapGetters} from 'vuex'

export default {
  name: 'Home',

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
         console.log(response);
         this.users=response.data; //am structura pt user
      }).catch((err)=>{
          //this se refera la instanta de view si vreau sa accesez users cu ele
          //tot ce am in data accesez cu this
          //this.loadUsers pentru metode
          console.log('======ERROR=====')
      });
    },

  }
 
}
</script>
