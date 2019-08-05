<template>
  <div class="container register flex-center">
    <div class="wrapper wrapper-content">

      <div class='row'>
        <div class='col-lg-12'>
          <div class='ibox'>

              <form method="post" @submit.prevent="validateBeforeSubmit" name="register">

                <div v-if="serverError" class="server-error">{{ serverError}}</div>

                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-address-card"></i></div>
                    <input type="text" name="nombre" placeholder="Nombre" 
                      class="form-control-sm" autocomplete="nombre" v-model="nombre" v-validate="'required'">                               
                    <span>{{errors.first('nombre')}}</span>
                  </div>
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-at"></i></div>
                    <input type="text" name="email" placeholder="Correo Electrónico" 
                      class="form-control-sm" autocomplete="email" v-model="email" v-validate="'required|email'">
                    <span>{{errors.first('email')}}</span> 
                  </div>
                 
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-address-card"></i></div>
                    <input type="text" id="empresa" name="empresa" placeholder="Empresa" 
                      class="form-control-sm" autocomplete="empresa" v-model="empresa"
                      v-validate="'required'">
                    <span>{{errors.first('empresa')}}</span>
                  </div>
                  
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-address-card"></i></div>
                    <input type="text" id="domicilio" name="domicilio" placeholder="Domicilio" 
                      class="form-control-sm" autocomplete="domicilio" v-model="domicilio"
                      v-validate="'required'">
                    <span>{{errors.first('domicilio')}}</span>
                  </div>                  
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                    <input type="text" name="telefono" placeholder="Teléfono" 
                      class="form-control-sm" autocomplete="telefono" v-model="telefono" v-validate="'required'">
                    <span>{{errors.first('telefono')}}</span>
                  </div>
                  
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                    <input type="text" name="usuario" placeholder="Usuario" 
                      class="form-control-sm" autocomplete="usuario" v-model="usuario"
                      v-validate="'required|min:6'">
                    <span>{{errors.first('usuario')}}</span>
                  </div>
                  
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-asterisk"></i></div>
                    <input type="password" name="password" placeholder="Password" 
                      class="form-control-sm" autocomplete="password" v-model="password" v-validate="'required|min:6'">       
                    <span>{{errors.first('password')}}</span>
                  </div>
                  
                </div>
                <div class="form-group">
                  <div class="input-group">
                    <div class="input-group-addon"><i class="fa fa-asterisk"></i></div>
                    <input type="password" name="passwordCppy" placeholder="Reingresar Password" 
                      class="form-control-sm" autocomplete="reingresar-password" 
                      v-model="passwordReingresado" v-validate="'required|min:6'">
                    <span>{{errors.first('passwordCppy')}}</span>
                  </div>
                  
                </div>        
                <div class="d-flex flex-column flex-lg-row align-items-center justify-content-between down-container flex-center">
                  <button class="btn btn-primary" type="submit">
                    Registrarse
                  </button>

                  <button type="submit" class="btn btn-white btn-md float-right">
                    <router-link class='link text-light float-right' :to="{name: 'login'}">Ya está registrado?</router-link>
                  </button>
                </div>
              </form>

          </div>
        </div>
      </div>      

    </div>     

  </div>
</template>

<script>
import sha256 from 'sha256'

  export default {
    name: 'Register',

    data: function(){
      return {
        usuario:'',
        password:'',
        passwordReingresado:'',
        domicilio:'',
        telefono:'',
        empresa: '',
        nombre:'',
        email: '',
        serverError:''
      }
    },

    methods:{
      validateBeforeSubmit(){
      this.$validator.validateAll().then((result) => {        
        if (result){
          this.register();          
        }
        else{
          this.serverError = result
        }        
      })
    },

      register(){
        if (this.password !== this.passwordReingresado) {
          alert('Las contraseñas deben coincidir')          
        }
        
        this.$store.dispatch('register', {
            nombreUsuario: this.usuario,
            password:  sha256(this.password),          
            nombre: this.nombre,
            domicilio: this.domicilio,
            telefono: this.telefono,
            empresa: this.empresa,
            email: this.email
        })
        .then(()=> {
            this.$router.push({ name: 'login'})
        })
        .catch(error => {
            //alert(error)
            this.serverError = error;
        })
      },
    }
  }
  
</script>

<style lang="scss">
  .register {

    h2 {
      text-align: center;
    }
    width: 21.375rem;

    .down-container {
      margin-top: 2.6875rem;
    }

    button, input{
      margin: 0 5px;      
    }

    input.form-control-sm{
      width: 300px;
    }    
  }

</style>