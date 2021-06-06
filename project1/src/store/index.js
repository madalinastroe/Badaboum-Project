import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'


Vue.use(Vuex)

export default new Vuex.Store({
  
  plugins: [createPersistedState()],

  state: {
    user:null,
    isAuthenticated: false,
  },

  getters: {
    // ...
    isAuthenticated: (state) => {
      return state.isAuthenticated;
    },

    user: (state) => {
      return state.user;
    }

  },

  mutations: {

    setUser(state,user) {
      state.user= user;
    },

    setIsAuthenticated(state, loggedIn) {
      state.isAuthenticated = loggedIn;
    },

  },
  actions: {

    login(context,user)
    {
      context.commit('setUser',user);
      context.commit('setIsAuthenticated',true);
    },

    logout(context,user)
    {
      context.commit('setUser',null);
      context.commit('setIsAuthenticated',false);
    },



  },
  modules: {


  }
})
