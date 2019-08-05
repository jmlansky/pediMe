<template>
    <div id="app">
       <ul class="nav" v-if="loggedIn">
           <li><router-link :to="{name: 'pedidos'}">Pedidos</router-link></li>
           <li><router-link :to="{name: 'productos'}">Productos</router-link></li>
           <li><router-link :to="{name: 'clientes'}">Clientes</router-link></li>
           <li><router-link :to="{name: 'promociones'}">Promociones</router-link></li>
           <li><router-link :to="{name: 'balances'}">Balances</router-link></li>
           <li><router-link :to="{name: 'sucursales'}">Sucursales</router-link></li>
           <li><router-link :to="{name: 'empresas'}">Empresas</router-link></li>
           <li><router-link :to="{name: 'usuarios'}">Usuarios</router-link></li>
           <li><router-link :to="{name: 'logout'}">Logout</router-link></li>
        </ul>
        
        <ul class="nav" v-else>
           <li><router-link :to="{name: 'login'}">Login</router-link></li>
           <li><router-link :to="{name: 'register'}">Register</router-link></li>           
       </ul>

        <div class="contenido">
            <router-view></router-view>    
        </div>
        
    </div>
</template>

<script>
export default {   

    computed:{
        loggedIn(){            
            return this.$store.getters.loggedIn
        }
    }
}
</script>

<style>

    @import url('../../assets/lib/footable/css/footable.core.css');
    @import url('../../assets/lib/bootstrap/dist/css/bootstrap.min.css');  
    @import url('../../assets/lib/font-awesome/css/font-awesome.min.css');
    @import url('../../assets/css/modalStyle.css');
    @import url('../../assets/css/style.css'); 
    
    *{
        box-sizing: border-box;
        margin: 0;
        padding: 0;
    }
    
    .flex-center{
        display:flex;
        justify-content: center;
    }

    .nav{
        display: flex;
        list-style: none;
        padding: 1px 0;
        margin: 0;
        justify-content: flex-end;
        background: #F5F8FA;
        border-bottom: 1px solid lightgray;
        margin-bottom: 8px;
        position: fixed;
        z-index: 100;
        width: 100%;
    }

    .contenido {
        padding-top: 55px;
    }

    .nav a{
        color: #636b6f;
        padding: 0 10px;
        font-size: 12px;
        font-weight: 500;
        letter-spacing: .05rem;
        text-decoration: none;
        text-transform: uppercase;
    }

    .server-error{
        margin-bottom: 12px;
        font-size: 16px;
        padding: 10px 16px;
        color: #a94442;
        background: #f3dede;
        border-radius: 4px;
        /*width: 300px;*/
    }
    
</style>