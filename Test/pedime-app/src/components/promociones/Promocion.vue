<template>
    <div class="col-lg-12">
        <div class='row'>
            <div class='ibox'>
                <div class='ibox-content'>
                    <button v-on:click='showModal = true' class='btn btn-primary'>Nueva Promoción</button>
                </div>
            </div>
        </div>

        <!-- LISTA DE PROMOCIONES -->
        <div class='row'>
          <div class='ibox'>
              <div class='ibox-title'>
                  <h5>Listado de Promociones</h5>
              </div>
              <div class='ibox-content'>
                <input type='text' class='form-control form-control-sm m-b-xs' id='filter' placeholder='Buscar en la lista de promociones'>

                <table class='footable table table-stripped' id='productsTable' data-page-size='10' data-filter=#filter>
                  <thead>
                    <tr>                      
                      <th>Nombre</th>
                      <th data-hide="all">Descripción</th>
                      <th>Aplica en</th>
                      <th data-hide="all">Valor</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for='promo in promociones' :key='promo.idPromocion'>                      
                      <td>{{promo.nombre}}</td>
                      <td>{{promo.descripcion}}</td>
                      <td>{{promo.aplicaEn}}</td>
                      <td><strong>${{promo.valor}}</strong></td>
                      <td class='center'>
                        <button v-if='promo.activo' v-on:click='showModal=true; editarPromocion(promo)' class='btn btn-info btn-circle fa fa-pencil' title='Editar'></button>
                        <a v-if='promo.activo' class='btn btn-danger btn-circle fa fa-trash' v-on:click='eliminarPromocion(promo.idPromocion)' title='Eliminar'></a>
                        <a v-else class='btn btn-success btn-circle fa fa-check' title='Activar' v-on:click='activarPromocion(promo.idPromocion)'></a>
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
        <!-- FIN DE LISTA DE PROMOCIONES --> 

        <!-- MODAL PROMOCIONES -->
        <div v-if='showModal'>        
            <transition name='modal'>
                <div class='modal-mask'>
                    <div class='modal-wrapper'>
                        <div class='modal-dialog' role='document'>               
                            <div class='modal-content'>
                                <div class='modal-body'>
                                    <div class='row'>
                                        <div class='col-lg-12'>
                                            <h3 class='m-t-none m-b'>Nueva Promoción</h3>

                                            <div class='form-group'>
                                                <label>Nombre</label>
                                                <div>
                                                    <input type="text" name="nombre" placeholder='Nombre de la promo' class='form-control' v-model='promocion.nombre' 
                                                        v-validate="'required|max:30'">
                                                </div>
                                                <span>{{errors.first('nombre')}}</span>
                                            </div>
                                            <div class='form-group'>
                                                <label>Descripción</label>
                                                <div>
                                                    <textarea placeholder='Descripción' name="descripcion"
                                                        class='form-control' style='resize:vertical;' 
                                                        v-model='promocion.descripcion' v-validate="'max:200'"></textarea>
                                                </div>                                      
                                                <span>{{errors.first('descripcion')}}</span>
                                            </div>
                                            <div class='form-group'>
                                                <label>Valor</label>
                                                <div>
                                                    <input type="text" name="precio" placeholder='Monto' class='form-control'
                                                        v-model='promocion.valor' v-validate="'required|decimal|min_value:0|max:6'">
                                                    <select>
                                                        <option value="">%</option>
                                                        <option value="">$</option>
                                                    </select>
                                                </div>
                                                <span>{{errors.first('precio')}}</span>
                                            </div>
                                            <div class='form-group'>
                                            <label>Aplica sobre</label>
                                            <div>
                                                <select>
                                                    <option value="">Productos</option>
                                                    <option value="">Envíos</option>
                                                    <option value="">Pedidos</option>
                                                </select>                                            
                                            </div>
                                            <span>{{errors.first('aplicaEn')}}</span>
                                        </div>

                                        </div>
                                    </div>
                                </div>
                                <div class='modal-footer'>
                                    <button type='button' class='btn btn-primary' id='guardarPromocion' v-on:click='validatePromoBeforeSubmit'>Guardar</button>
                                    <button type='button' class='btn btn-secondary' v-on:click='limpiarFormulario(); showModal=false'>Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </transition>
        </div>
        <!-- FIN MODAL -->
    </div>
    
</template>

<script>
export default {
    name:'promocion',

     data(){
    return{
      promociones:[],
      promocion:{
        idPromocion: '',
        nombre:'',
        descripcion:'',
        aplicaEn:'',
        valor:'',
        activo:'',
      },
      showModal: false,      
    }
  },
  
  methods: {
    obtenerPromociones(){},

    validatePromoBeforeSubmit(){
      this.$validator.validateAll().then((result) => {        
        if (result){
          this.guardarPromocion();          
        }
        else{
          this.serverError = result
        }        
      })
    },

    limpiarFormulario(){},
    limpiarFormularioCombos(){},
    guardarPromocion(){},
    activarPromocion(idPromocion) {
            
      this.$store.dispatch('activarPromocion', idPromocion)
      .then(response => {
        if (response.status === 200){ this.obtenerPromociones(); }
        else { alert('Hubo un error al insertar la promoción. ', response.data); }
      })      
      .catch(function (error) { alert('ERROR', error) });  
    },
    editarPromocion(promo){},
    eliminarPromocion(idPromocion){},

  },

  created() {
  }
}
</script>

<style>

</style>