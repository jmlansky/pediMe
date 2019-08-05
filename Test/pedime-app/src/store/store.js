import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)
axios.defaults.baseURL= 'https://localhost:44388/api/'


export const store = new Vuex.Store({
    state:{        
        productos:[],
        clientes:[],
        combos:[],
        origenesPedido:[],
        token: localStorage.getItem('access_token') || null,
        username: localStorage.getItem('username') || null,
        nombre: localStorage.getItem('nombre') || null,
    },

    getters: {

        loggedIn(state){
            return state.token != null
        },

        loggedUser(state){
            return {
                username: state.username,
                token: state.token,
                nombre: state.nombre
            }
        },

        getProductos(state){
            return {                
                productos: state.productos
            }
        },

        getCombos(state){
            return {                
                productos: state.combos
            }
        },

        getClientes(state){
            return{
                clientes: state.clientes
            }
        }
    },

    mutations:{

        logout(state){
            state.token = null
            state.username = null,
            state.nombre = null
        },

        setUserData(state, data){
            state.username = data.username,
            state.token = data.token,
            state.nombre = data.nombre
        },

        getProductos(state, productos){
            state.productos = productos
        },

        getCombos(state, combos){
            state.combos = combos
        },

        getClientes(state, clientes){
            state.clientes = clientes
        }
    },

    actions:{
        login(context, credentials){
            return new Promise((resolve, reject) => {
                axios.post('usuarios/login', {
                    nombreUsuario: credentials.nombreUsuario,
                    password: credentials.password
                })
                .then(response => {
                    const token = response.data.token
                    const username = response.data.nombreUsuario
                    const nombre = response.data.nombre

                    localStorage.setItem('access_token', token)
                    localStorage.setItem('username', username)
                    localStorage.setItem('nombre', nombre)

                    context.commit('setUserData', {
                        token: token,
                        username: username,
                        nombre: nombre
                    })

                    resolve(response)
                })
                .catch(error => {
                    if (error.response.status === 401){                        
                        reject('Password o contraseña incorrectos')
                    }
                    else if (error.response.status === 400){
                        reject('Bad Request')
                    }
                    else if (error.response.status === 500){                        
                        reject('Server exception')
                    }
                    else {
                        reject (error.response.message)
                    }
                })
            })
        },

        logout(context){

            axios.defaults.headers.common['Authorization'] = 'Bearer ' + context.state.token
            if (context.getters.loggedIn){
                return new Promise((resolve, reject) => {
                    axios.post('usuarios/logout', {
                        nombreUsuario: context.getters.loggedUser.username,
                        token: context.getters.loggedUser.token
                    })
                    .then(response => {
                        localStorage.removeItem('access_token')
                        context.commit('logout')
                        resolve(response)
                    })
                    .catch(error => { 
                        localStorage.removeItem('access_token')
                        context.commit('logout')                       
                        reject(error)
                    })
                })
            }            
        },

        register(context, data){
            return new Promise((resolve, reject) => {
                axios.post('usuarios/post', {
                    nombreUsuario: data.nombreUsuario,
                    password: data.password,
                    nombre: data.nombre,
                    telefono: data.telefono,
                    domicilio: data.domicilio,
                    empresa: data.empresa
                })
                .then(response => {
                    resolve(response)
                })
                .catch(error => {
                    if (error.response.status === 400){
                        reject (error.response.data)
                    }
                    else if (error.response.status === 404){
                        reject ('Missing parameters.')
                    }
                    else{
                        reject (error.data)
                    }                    
                })
            })
        },

        /* PRODUCTOS */ 
        getProductos(context){
            return new Promise((resolve, reject) => {
                axios.get('productos/get')
                .then(response => {
                    const productos = response.data

                    context.commit('getProductos', { productos: productos })
                    resolve(response)
                })
                .catch(error => { reject (error.response.message) })
            })
        },

        guardarProducto(context, body){
            return new Promise((resolve, reject) => {
                axios.post('productos/post', body, { headers: {'Content-type': 'application/json'}})
                .then((response) => {
                    resolve(response)
                })
                .catch(error => {
                    reject (error.response.message)                    
                })
            })
        },

        eliminarProducto(context, idProducto) {
            return new Promise((resolve, reject) => {
                axios.delete('productos/delete/'+ idProducto)
                .then((response) => { resolve(response) })
                .catch(error => { reject (error.response.message) })
            })            
        },

        activarProducto(context, idProducto) {
            return new Promise((resolve, reject) => {
                axios.put('productos/activar/'+ idProducto)
                .then(response => {resolve(response)})
                .catch(error => {reject(error.response.message) })
            })
        },        
        /* FIN PRODUCTOS */

        /* CLIENTES */
        getClientes(context){
            return new Promise((resolve, reject) => {
                axios.get('clientes/get')
                .then(response => {
                    const clientes = response.data

                    context.commit('getClientes', {
                        clientes: clientes
                    })

                    resolve(response)
                })
                .catch(error => {
                    reject (error.response.message)                    
                })
            })
        },

        guardarCliente(context, body){
            return new Promise((resolve, reject) =>{
                axios.put('clientes/put', body, { headers: { 'Content-type': 'application/json'} })
                .then(response => {resolve(response) })
                .catch(error => {reject(error.response.message) })
            })
        },

        getCliente(context, telefono){
            return new Promise((resolve, reject) => {
                axios.get('clientes/GetCliente/' + telefono)
                .then(response=> resolve(response))
                .catch(error => reject(error))
            })
        },
        /* FIN CLIENTES */


        /* PEDIDOS */
        getDetallesPedido(context){
            return new Promise((resolve, reject) => {
                axios.get('pedidos/GetDetalles')
                .then(response => {resolve(response)})
                .catch(error => reject(error))
            })
        },

        getPedidosDelDia(context){
            return new Promise((resolve, reject) => {
                axios.get('pedidos/GetPedidosDelDia')
                .then(response => resolve(response))
                .catch(error => reject(error))
            })
        },

        guardarPedido(context, body)
        {
            return new Promise((resolve, reject) => {
                axios.post('pedidos/post', body)
                .then(response => {resolve(response)})
                .catch(error => reject(error))
            })
        },
        /* FIN PEDIDOS*/

        getOrigenesDePedido(context){
            return new Promise((resolve, reject) => {
                axios.get('OrigenPedido/Get')
                .then(response => resolve(response))
                .catch(error => reject(error))
            })
        },

        // COMBOS
        getCombos(context){
            return new Promise((resolve, reject) => {
                axios.get('combos/get')
                .then(response => {
                    const combos = response.data

                    context.commit('getCombos', { combos: combos })
                    resolve(response)
                })
                .catch(error => { reject (error.response.message) })
            })
        },

        getCombo(context, idCombo){
            return new Promise((resolve, reject) => {
                axios.get('combos/getCombo?id=' + idCombo)
                .then(response => {                    
                    resolve(response)
                })
                .catch(error => {reject(error.response.message)})
            })
        },

        guardarCombo(context, combo){
            return new Promise((resolve, reject) => {
                axios.post('combos/post', combo)
                .then(response => {resolve(response)})
                .catch(error => reject(error))
            })
        },

        eliminarCombo(context, idCombo){
            return new Promise((resolve, reject) => {
                axios.delete('combos/delete/'+ idCombo)
                .then(response => {resolve (response)})
                .catch(error=> reject(error))
            })
        },
        //FIN COMBOS
    }
})