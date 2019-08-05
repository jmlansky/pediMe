<template>
  <div id="app" class="container">


  </div>
</template>

<script>

import jquery from 'jquery'
import Login from './components/Login.vue'
import footable from 'footable'

export default {
  name: 'app',
  components: {
    Login
  }
}
</script>

<style>

  * {
    box-sizing: border-box;
  }

  .container {
    max-width: 600px;
    margin: 0 auto;
  }

  .logo{
    display: block;
    margin: 20px auto;
    height: 75px;
  }

  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
</style>