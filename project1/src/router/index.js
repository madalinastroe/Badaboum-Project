import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: function () {
      return import(/* webpackChunkName: "about" */ '../views/About.vue')
    }
  },

  {
    path: '/login',
    name: 'Login',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/Login.vue')
    }}
  
  },

  {
    path: '/show',
    name: 'Show',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/Show.vue')
    }},
  
  },

  {
    path: '/showproducts',
    name: 'ShowProducts',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/ShowProducts.vue')
    }}
  
  },

  {
    path: '/showshops',
    name: 'ShowShops',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/ShowShops.vue')
    }}
  
  },

  {
    path: '/addproduct',
    name: 'AddProduct',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/AddProduct.vue')
    }}
  
  },

  {
    path: '/addshop',
    name: 'AddShop',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/AddShop.vue')
    }}
  
  },

  {
    path: '/showshopproducts',
    name: 'ShowShopProducts',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/ShowShopProducts.vue')
    }}
  
  },

  {
    path: '/addsp',
    name: 'AddSP',
    component: function() {{
      return import(/* webpackChunkName: "login" */ '../views/AddSP.vue')
    }}
  
    
  },



]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
