<template>
  <div class="container">

    <div class='wrapper wrapper-content' >
      <div class='row'>
        <div class='col-lg-12'>
          <div class='ibox'>
            <div class='ibox-content'>
              <button v-on:click='showModal = true' class='btn btn-primary'>Nuevo Producto</button>
            </div>
          </div>
        </div>     
      </div>

      <div class='row'>
        <div class='col-lg-12'>
          <div class='ibox'>
              <div class="ibox-title" style="padding-bottom:45px;">
                <div class='col-md-10'>                
                  <h5>Listado de Productos</h5>
                </div>
                
                <div class="col-md-2">                  
                  <switches v-model="soloActivos" class="vue-switcher vue-switcher vue-switcher-theme--default vue-switcher-color--green" 
                    text-enabled="Mostrar Solo Activos" text-disabled="Mostrar Todos">
                  </switches>
                </div>
              </div>
              
              <div class='ibox-content'>
                <input type='text' class='form-control form-control-sm m-b-xs' id='filter' placeholder='Buscar en la lista de productos'>

                <table class='footable table table-stripped' id='productsTable' data-page-size='10' data-filter=#filter>
                  <thead>
                    <tr>                      
                      <th>Nombre</th>
                      <th>Descripción</th>
                      <th>Precio</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for='producto in obtenerProductosFiltrados()' :key='producto.idProducto'>                      
                      <td class='nombre'>{{producto.Nombre}}</td>
                      <td class='descripcion'>{{producto.Descripcion}}</td>
                      <td class='precio'><strong>${{producto.Precio}}</strong></td>
                      <td class='center'>
                        <button v-if='producto.Activo' v-on:click='showModal=true; editarProducto(producto)' class='btn btn-info btn-circle fa fa-pencil' title='Editar'></button>
                        <a v-if='producto.Activo' class='btn btn-danger btn-circle fa fa-trash' v-on:click='eliminarProducto(producto.IdProducto)' title='Eliminar'></a>
                        <a v-else class='btn btn-success btn-circle fa fa-check' title='Activar' v-on:click='activarProducto(producto.IdProducto)'></a>
                      </td>
                    </tr>
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colspan='4'>
                        <ul class='pagination float-right'></ul>
                      </td>
                    </tr>
                  </tfoot>
                </table>
              </div>
          </div>
        </div>
      </div>
      
      <div v-if='showModal'>
        <transition name='modal'>
          <div class='modal-mask'>
            <div class='modal-wrapper'>
              <div class='modal-dialog' role='document'>
                <div class='modal-content'>
                  <div class='modal-body'>
                    <div class='row'>
                      <div class='col-lg-12'>
                        <h3 class='m-t-none m-b'>Nuevo Producto</h3>

                        <div class='form-group'>
                            <label>Nombre</label>
                            <div>
                              <input type="text" name="nombre" placeholder='Nombre del producto' class='form-control' v-model='prod.nombre' 
                                v-validate="'required|max:30'">
                            </div>
                            <span>{{errors.first('nombre')}}</span>
                        </div>
                        <div class='form-group'>
                            <label>Descripción</label>
                            <div>
                              <textarea placeholder='Descripción' name="descripcion"
                                class='form-control' style='resize:vertical;' 
                                v-model='prod.descripcion' v-validate="'max:200'"></textarea>
                            </div>                                      
                            <span>{{errors.first('descripcion')}}</span>
                        </div>
                        <div class='form-group'>
                            <label>Precio</label>
                            <div>
                              <input type="text" name="precio" placeholder='Precio' class='form-control'
                                v-model='prod.precio' v-validate="'required|decimal|min_value:0|max:6'">
                            </div>
                            <span>{{errors.first('precio')}}</span>
                        </div>

                      </div>
                    </div>
                  </div>
                  <div class='modal-footer'>
                    <button type='button' class='btn btn-primary' id='guardarProducto' v-on:click='validateBeforeSubmit'>Guardar</button>
                    <button type='button' class='btn btn-secondary' v-on:click='limpiarFormulario(); showModal=false'>Cerrar</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </transition>
      </div>
    </div>

  </div>
</template>

<script>

import Switches from 'vue-switches';
import $ from 'jQuery'

export default {
  name: 'productos',

  components: {
    Switches
  },

  data(){
    return {
      showModal: false,
      productos: [],      
      prod: {
          idProducto: 0,
          nombre: '',
          descripcion: '',
          precio: ''
      },
      soloActivos: true,
    }
  },
  
  methods: {
    validateBeforeSubmit(){      
      this.$validator.validateAll().then((result) => {        
        if (result){
          this.guardarProducto();          
        }
        else{
          this.serverError = result
        }        
      })
    },

    guardarProducto() {
      var body = this.crearProducto();
      this.$store.dispatch('guardarProducto', body)
      .then((response)=>{
        if (response.status === 200){
          this.obtenerProductos();
          this.showModal = false;
          this.limpiarFormulario();
        }
        else{
          alert('Hubo un error al insertar el producto. ', response.message);
        }
      })
      .catch((error) => { alert('Hubo un error: ', error); });
    },
    
    obtenerProductos() {
      this.$store.dispatch('getProductos')
      .then((response) => {
        this.productos = response.data
      })
      .then(()=> {
        $('.footable').footable()
      })
      .catch((error) => {alert("error: " + error.message)})
    },

    obtenerProductosFiltrados(){
      
      if(this.soloActivos){
        const lista = this.productos.filter(prod => prod.Activo == true)
        return lista       
      }      

      return this.productos;
    },

    eliminarProducto(idProducto) {
      this.$dialog.confirm('Desea eliminar el producto?', {okText:'Si', cancelText:'No'})
      .then(dialog => {
        this.$store.dispatch('eliminarProducto', idProducto)
          .then(response => {
            if (response.status === 200){
              this.obtenerProductos();
            }
            else
            {
              alert('Hubo un error al eliminar el producto. ', response.data);
            }
          })      
          .catch((error) => { alert('ERROR', error) }
        );  
      })
      .catch(() => {
        console.log('Clicked on cancel');
      });
    },

    editarProducto(productoSeleccionado) {                    
      this.prod.idProducto = productoSeleccionado.IdProducto;
      this.prod.nombre = productoSeleccionado.Nombre;
      this.prod.descripcion = productoSeleccionado.Descripcion;
      this.prod.precio = productoSeleccionado.Precio;                    
    },

    limpiarFormulario() {
      this.prod.idProducto = 0;
      this.prod.nombre = '';
      this.prod.descripcion = '';
      this.prod.precio = '';
    },

    activarProducto(idProducto) {
            
      this.$store.dispatch('activarProducto', idProducto)
      .then(response => {
        if (response.status === 200){ this.obtenerProductos(); }
        else { alert('Hubo un error al insertar el producto. ', response.data); }
      })      
      .catch(function (error) { alert('ERROR', error) });  
    },

    crearProducto() {
      return {
          'idProducto': this.prod.idProducto,
          'nombre': this.prod.nombre,
          'descripcion': this.prod.descripcion,
          'precio': this.prod.precio == '' ? 0 : this.prod.precio,
          'activo': true
      };
    }
  },

  created() {
    this.obtenerProductos();
  }
}
</script>
