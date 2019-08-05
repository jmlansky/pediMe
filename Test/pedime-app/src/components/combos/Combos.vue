<template>
    <div class="col-lg-12">
        <div class='row'>
            <div class='ibox'>
            <div class='ibox-content'>
                <button v-on:click='redrawModalCombo(0)' class='btn btn-primary'>Nuevo Combo</button>
            </div>
            </div>
        </div>

        <!-- LISTA DE COMBOS -->
        <div class='row'>
            <div class='ibox'>
                <div class="ibox-title" >                
                    <h5>Listado de Combos</h5>                    
                </div>

                <div class='ibox-content'>
                    <input type='text' class='form-control m-b-xs' id='filterCombos' placeholder='Buscar en la lista de combos'>

                    <table class='footable table table-stripped' id="tablaCombos" data-page-size='5' data-filter=#filterCombos>
                    <thead>
                        <tr>
                            <th>Nombre</th>
                            <th>Valor</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for='combo in combos' :key='combo.id'>
                            <td>{{combo.nombre}}</td>
                            <td><strong>${{combo.valor}}</strong></td>
                            <td class='center'>
                                <button v-if='combo.activo' v-on:click='redrawModalCombo(combo.id)' class='btn btn-info btn-circle fa fa-pencil' title='Editar'></button>
                                <a v-if='combo.activo' class='btn btn-danger btn-circle fa fa-trash' v-on:click='eliminarCombo(combo.id)' title='Eliminar'></a>
                                <a v-else class='btn btn-success btn-circle fa fa-check' title='Activar' v-on:click='activarCombo(combo.id)'></a>
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
        <!-- FIN DE LISTA DE COMBOS -->

        <!-- MODAL COMBOS -->
        <div v-if='showModalCombos'>        
            <transition name='modal'>
            <div class='modal-mask '>
                <div class='modal-wrapper'>
                    <div class='modal-dialog modal-lg' role='document'>
                        <div class='modal-content'>
                        
                            <!-- MODAL BODY -->
                            <div class='modal-body'>
                                <div class='row'>
                                    <div class='col-lg-12'>
                                        <h3 class='m-t-none m-b'>Nuevo Combo</h3>
                                        
                                        <div class='form-group'>
                                            <label class="col-lg-2 col-form-label">Descripción</label>
                                            <div class="col-lg-10">
                                                <input type="text" placeholder="Descripción del combo" v-model="combo.nombre" name="descripcion" class='form-control' v-validate="'required|max:100'">                            
                                            </div>                                      
                                            <span>{{errors.first('descripcion')}}</span>
                                        </div>
                                        
                                        <div class='row'>
                                            <div class='col-lg-10'>
                                                <div class='ibox-content'>
                                                    <input type='text' class='form-control form-control-sm m-b-xs' id='filterProductos' placeholder='Buscar en la lista de productos'>

                                                    <table class='footable table table-stripped small' id="tablaProductos" data-page-size='7' data-filter=#filterProductos>
                                                        <thead>
                                                            <tr>
                                                                <th><small>Nombre</small></th>
                                                                <th><small>Cantidad</small></th>
                                                                <th><small>Precio</small></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for='detalle in combo.detalles' :key='detalle.idProducto'> 
                                                                <td class='nombre'>{{detalle.producto}}</td>

                                                                <td><input type="text" class='cantidad form-control'
                                                                    :readonly='!detalle.seleccionado' name="cantidad" v-validate="'decimal|min_value:0|max:8'"
                                                                    v-model='detalle.cantidad'
                                                                    @blur="calcularSubtotal(detalle)">
                                                                </td>
                                                                
                                                                <td class='precio'><strong>${{detalle.valor}}</strong></td> 
                                                                <td class='botones'>
                                                                <button v-if='detalle.seleccionado' class='btn btn-danger btn-circle fa fa-times' @click="seleccionarProducto(detalle, false)" title='Seleccionar'></button>
                                                                <button v-else class='btn btn-info btn-circle fa fa-check' @click="seleccionarProducto(detalle, true)" title='Eliminar'></button>
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

                                            <div class="col-lg-2">
                                                <div class="row form-group">
                                                    <label>Precio</label> 
                                                    <input type="text" name="precio" placeholder='$0.00' class='form-control' readonly v-model='combo.valorTeorico'>
                                                </div>
                                                <div class="row form-group">
                                                    <label>Precio del Combo</label>
                                                    <input type="text" name="precioCombo" placeholder='Monto' class='form-control'
                                                        v-model='combo.valor' v-validate="'required|decimal|min_value:0|max:6'">
                                                    <span>{{errors.first('precioCombo')}}</span>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <!-- MODAL FOOTER -->
                            <div class='modal-footer'>
                                <button type='button' class='btn btn-primary' v-on:click='validateComboBeforeSubmit'>Guardar</button>
                                <button type='button' class='btn btn-secondary' v-on:click='limpiarFormularioCombos(); showModalCombos=false'>Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </transition>
        </div>
        <!-- FIN MODAL COMBOS -->
    </div>
    
    
</template>

<script>
import Switches from 'vue-switches';
import $ from 'jQuery'

export default {
    name:'combos',
    data(){
        return{
            combos:[],            
            combo:{
                nombre:'',
                valor:'',
                valorTeorico:'',
                detalles:[],              
            },            
            showModalCombos: false,
            soloActivos: true,
        }
    },

    components: {
        Switches
    },

    methods:{
        obtenerCombos(){
            this.$store.dispatch('getCombos')
            .then((response) => {
                this.combos = response.data
            })
            .then(()=> {
                $('#tablaCombos').footable()        
            })
            .catch((error) => {alert("error: " + error.message)})
        },        

        redrawModalCombo(idCombo){      
            this.showModalCombos= true;            
            this.editarCombo(idCombo);            
        },

        limpiarFormularioCombos(){},

        validateComboBeforeSubmit(){
            this.$validator.validateAll().then((result) => {        
                if (result){
                    this.guardarCombo();          
                }
                else{
                    this.serverError = result
                }        
            })
        },

        editarCombo(idCombo){
            this.$store.dispatch('getCombo', idCombo)
            .then((response) => {
                this.combo = response.data
            })
            .then(()=> {
                $('#tablaProductos').footable()                
            })
            .catch((error) => {alert("error: " + error)})
        },

        eliminarCombo(idCombo){
            this.$dialog.confirm('Desea eliminar el producto?', {okText:'Si', cancelText:'No'})
                .then(dialog => {
                    this.$store.dispatch('eliminarCombo', idCombo)
                .then(response => {
                    if (response.status === 200){
                        this.obtenerCombos();
                    }
                    else
                    {
                        alert('Hubo un error al eliminar el combo. ', response.data);
                    }
                })      
                .catch((error) => { alert('ERROR', error) });
            })              
        },

        activarCombo(idCombo){},

        guardarCombo(){
            this.$store.dispatch('guardarCombo', this.combo)
            .then((response) => {
                this.obtenerCombos()
                this.showModalCombos = false
            })
            .then(()=> {
                $('#tablaProductos').footable()                
            })
            .catch((error) => {alert("error: " + error)})
        },

        seleccionarProducto(producto, nuevoValor){
            producto.seleccionado = nuevoValor

            if (nuevoValor == true){
                producto.cantidad = ''
            }
            else{
                producto.cantidad = 0
                this.calcularSubtotal(producto)
            }
        },
        
        calcularSubtotal(detalle){
            this.combo.valorTeorico = 0
            for (var i = 0; i < this.combo.detalles.length; i++){
                if (this.combo.detalles[i].seleccionado){
                    var item = this.combo.detalles[i]
                    this.combo.valorTeorico += item.cantidad * item.valor
                }                
            }            
        },
    },

    created(){
        this.obtenerCombos()
    }
}
</script>

<style scoped>
  .cantidad{
    width: 65px;
  }

  .btn-circle{
    width: 20px;
    height: 20px;
    padding: 3px 0;
    border-radius: 10px;
    text-align: center;
    font-size: 10px;
    line-height: 1.25;
  }

  .botones{
    width: 28px;
  }

</style>
