<template>
  <div class="container">
    <div class="wrapper wrapper-content animated fadeIn" id="app">

      <div class="row">
        <div class="col-lg-12">
          <div class="ibox">
            <div class="ibox-title">
              <h5>Listado de Clientes</h5>
            </div>
            <div class="ibox-content">
              <input type="text" class="form-control form-control-sm m-b-xs" id="filter" placeholder="Buscar en la lista de clientes">

              <table class="footable table table-stripped" data-page-size="7" data-filter=#filter>
                <thead>
                  <tr>
                    <th>Teléfono</th>
                    <th>Dirección</th>
                    <th data-hide="phone,tablet">Nombre</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="cliente in clientes" :key="cliente.idCliente">
                    <td>{{cliente.telefono}}</td>
                    <td>{{cliente.domicilio}}</td>
                    <td>{{cliente.nombre}}</td>                    
                    <td class="center">
                      <button @click="showModal=true; editarCliente(cliente)" class="btn btn-info btn-circle fa fa-pencil" title="Editar"></button>
                      <!-- <button @click="limpiarFormulario(); editarCliente(cliente); showStaticModal();" class="btn btn-info btn-circle fa fa-pencil" title="Editar"></button> -->
                    </td>
                  </tr>
                </tbody>
                <tfoot>
                  <tr>
                    <td colspan="4">
                      <ul class="pagination float-right"></ul>
                    </td>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>
        </div>
      </div>

      <div v-if="showModal">
        <transition name="modal">
          <div class="modal-mask">
            <div class="modal-wrapper">

              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-body row">
                    
                      <div class="col-lg-12">

                        <div class="row form-group">
                          <div class="col col-md-3"><label for="nombre" class=" form-control-label">Nombre</label></div>
                          <div class="col-12 col-md-9">
                            <input type="text" name="nombre" placeholder="Nombre" 
                              class="form-control" v-model="cli.nombre" maxlength="30" 
                              v-validate="'required'">
                          </div>
                          <span>{{errors.first('nombre')}}</span>
                        </div>

                        <div class="row form-group">
                          <div class="col col-md-3"><label for="domicilio" class=" form-control-label">Domicilio</label></div>
                          <div class="col-12 col-md-9">
                            <input type="domicilio" id="domicilio" name="domicilio" placeholder="Domicilio" 
                              class="form-control" v-model="cli.domicilio" maxlength="50" v-validate="'required'">
                          </div>
                          <span>{{errors.first('domicilio')}}</span>
                        </div>

                        <div class="row form-group">
                          <div class="col col-md-3"><label for="telefono" class=" form-control-label">Telefono</label></div>
                          <div class="col-12 col-md-9">
                            <input type="tel" name="telefono" placeholder="Telefono" class="form-control" 
                              v-model="cli.telefono" v-validate="'required|min:10|max:13'">
                          </div>
                          <span>{{errors.first('telefono')}}</span>
                        </div>

                      </div>
                    </div>

                    <div class="modal-footer">
                      <button type="button" class="btn btn-primary" id="guardarCliente" v-on:click="validateBeforeSubmit">Guardar</button>
                      <button type="button" class="btn btn-secondary" v-on:click="limpiarFormulario(); showModal=false">Cerrar</button>
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

import $ from 'jQuery'
//import footable from 'footable'

export default {
  name: 'clientes',
  data: function(){
    return {
      showModal:false,     
      clientes: [],
      cli: {
        idCliente: 0,
        nombre: '',
        domicilio: '',
        telefono: ''
      }
    }                
  },  

  methods: {
    validateBeforeSubmit(){      
      this.$validator.validateAll().then((result) => {        
        if (result){
          this.guardarCliente();          
        }
        else{
          this.serverError = result
        }        
      })
    },    

    obtenerClientes() {
      this.$store.dispatch('getClientes')
      .then((response) => { this.clientes = response.data })
      .then(()=> { $('.footable').footable() })
      .catch((error) => {alert("error: " + error.message)})
    },

    editarCliente(clienteSeleccionado) {                    
      this.cli.idCliente = clienteSeleccionado.idCliente;
      this.cli.nombre = clienteSeleccionado.nombre;
      this.cli.domicilio = clienteSeleccionado.domicilio;
      this.cli.telefono = clienteSeleccionado.telefono;                   
    },

    guardarCliente() {
      var body = this.crearCliente();
      this.$store.dispatch('guardarCliente', body)
      .then(response => {
        if (response.status === 200){
          this.obtenerClientes();
          this.showModal = false;
        }
        else {
          alert('Hubo un error al actualizar el cliente. ', response.data);
        }
      })               
      .catch(error => { alert('Hubo un error: ', error); });
    },

    limpiarFormulario() {
      this.cli.idCliente = 0;
      this.cli.nombre = '';
      this.cli.domicilio = '';
      this.cli.telefono = '';
    },

    crearCliente() {
      return {
        'idCliente': this.cli.idCliente,
        'nombre': this.cli.nombre,
        'domicilio': this.cli.domicilio,
        'telefono': this.cli.telefono
      };
    },

    showStaticModal () {
      this.$refs.staticModal.open();
    }
  },

  created: function () { 
    this.obtenerClientes(); 
  }
}
</script>

<style scoped>
    @import url('../../assets/css/modalStyle.css');
</style>
