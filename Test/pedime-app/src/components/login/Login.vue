<template>

  <div>
    <div class="container flex-center">

      <form method="post" @submit.prevent="validateBeforeSubmit" name="login">
        <div v-if="serverError" class="server-error">
          {{serverError}}
        </div>

        <div class="form-group">
          <div class="input-group">
            <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
            <div class="col-lg-12">
              <input type="text" name="username" placeholder="Usuario" 
                class="form-control-sm" maxlength="30" v-model="usuario"                 
                autocomplete="usuario" v-validate="'required'">                
                    <span>{{errors.first('username')}}</span>
            </div>
          </div>
        </div>
        <div class="form-group">
          <div class="input-group">
            <div class="input-group-addon"><i class="fa fa-asterisk"></i></div>
            <div class="col-lg-12">
              <input type="password" name="password" placeholder="Password" 
                class="form-control-sm" maxlength="30"  v-model="password" autocomplete="current-password"
                v-validate="'required|min:3'">                               
                <span>{{errors.first('password')}}</span>
            </div>
          </div>
        </div>
        <div class="form-group">
            <button type="submit" class="btn btn-success btn-md">Iniciar Sesión</button>
            <button type="submit" class="btn btn-white btn-md float-right">
              <router-link class='link text-light float-right' :to="{name: 'register'}">Crear Cuenta</router-link>
            </button>
        </div>
      </form>
  
    </div>
  </div>

  
</template>

<script>

import sha256 from 'sha256'

export default {
  name: 'Login',

  data: function(){
    return {
      usuario:'',
      password:'',
      serverError: ''
    }
  },

  methods:{
    validateBeforeSubmit(){
      this.$validator.validateAll().then((result) => {        
        if (result){
          this.login();          
        }
        else{
          this.serverError = result
        }        
      })
    },

    login(){      
      // debugger;
      this.$store.dispatch('login', {
          nombreUsuario: this.usuario,
          password:  sha256(this.password)
      })
      .then(()=> {
          this.$router.push({ name: 'pedidos'})
      })
      .catch(error => {
          this.serverError = error;
          this.password = ''
      })
    },

  }
}

</script>


<style lang="scss" scoped>
  button, input{
    margin: 0 5px;
  }

  .container{
    padding-top: 90px;
  }
  
 </style>

