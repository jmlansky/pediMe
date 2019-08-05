<template>
  <div class="container ">

    <div class="col-lg-8 b-r">

      <div class="ibox">
        <div class="ibox-content">
          <div class="form-group row">
            <label class="col-lg-2 col-form-label" id="ddOrigenPedido">Pedido en</label>          

            <div class="col-lg-3">
              <select class="chosen-select" v-model="selected">
                  <option v-for="origen in this.origenesDePedido"
                    :key="origen.idOrigen"
                    :value="origen.nombre">
                    {{ origen.nombre }}
                  </option>
              </select>
            </div>

            <div v-if="selected === 'Domicilio'">
              <label class="col-lg-2 col-form-label">Teléfono</label>            
              <div class="col-lg-3">
                <input type="tel" placeholder="Teléfono del cliente" maxlength="13" min="10" class="form-control" name="telefono"                
                v-model='cliente.telefono' @keyup.enter="buscarCliente"/><!-- v-validate="origen.nombre == 'Domicilio' ? 'required' : ''" -->
              </div>
              <div class="col-lg-1">
                <a class="btn btn-primary btn-circle fa fa-search float-right m-t-n-xs" title="Buscar" @click="buscarCliente"/>
              </div>
            </div>
            
            <div v-if="selected === 'Mesa'">
              <label class="col-lg-2 col-form-label">Mesa</label>
              <div class="col-lg-2">
                <input type="text" placeholder="nro de mesa" maxlength="5" min="0" class="form-control" name="mesa"                 
                  v-model='cliente.mesa'/> <!--v-validate="origen.nombre == 'Mesa' ? 'required' : ''"-->
              </div>
            </div>
          </div>

          <div class="form-group row" v-if="selected !== 'Mesa'">
              <label class="col-lg-2 col-form-label">Nombre</label>
              <div class="col-lg-3">
                <input type="tel" placeholder="Nombre del cliente" v-model='cliente.nombre' maxlength="40" class="form-control" />
              </div>
            
            <div v-if="selected === 'Domicilio'">
              <label class="col-lg-2 col-form-label">Dirección</label>
              <div class="col-lg-5">
                <input type="tel" placeholder="Dirección del cliente" maxlength="60" class="form-control" 
                  v-model='cliente.domicilio' />
                  <!-- v-validate="origen.nombre == 'Domicilio' ? 'required' : ''" -->
              </div>
            </div>             

          </div>
        </div>
      </div>

      <div class="ibox">
        <div class="ibox-content">
          <input type='text' class='form-control form-control-sm m-b-xs' id='filter' placeholder='Buscar en la lista de productos'>

          <table class='footable table table-stripped toggle-arrow-tiny' id='productsTable' data-page-size='15' data-filter=#filter>
            <thead>
              <tr>                
                <th>Producto</th>
                <th data-hide="all">Precio</th>
                <th>Cantidad</th>
                <!-- <th>Promoción</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for='detalle in detallesPedido' :key='detalle.IdProducto'>
                <td class='nombre'>{{detalle.nombre}}</td>
                <td class='precio'><strong>${{detalle.precio}}</strong></td>
                <td><input type="text" pattern="\d*" class='cantidad form-control'
                  :readonly='!detalle.seleccionado' min="0" max="999" maxlength="4"
                  v-model='detalle.cantidad'
                  @blur="calcularSubtotal(detalle)">
                </td>
                <!-- <td>
                  <select name="" id="" :disabled='!detalle.seleccionado'>
                    <option value="PROMO1">NINGUNA</option>
                    <option value="PROMO1">PROMO 1</option>
                    <option value="PROMO2">SUPER PROMO 2</option>
                    <option value="PROMO3">ALTA PROMO 3</option>
                  </select>
                </td> -->
                <td><input type="text" class='observaciones' :disabled='!detalle.seleccionado' placeholder="Observaciones"></td>
                <td class='botones'>
                  <button v-if='detalle.seleccionado' class='btn btn-info btn-circle fa fa-check' @click="seleccionarProducto(detalle, false)" title='Seleccionar'></button>
                  <button v-else class='btn btn-warning btn-circle fa fa-times' @click="seleccionarProducto(detalle, true)" title='Eliminar'></button>
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

    <div class="col-lg-4">

      <div class="ibox">
        <div class="ibox-content">
          <table class='footable table table-stripped toggle-arrow-tiny' id='tablaPedidos' data-page-size='15'>
            <thead>
              <tr>
                <th>Fecha</th>
                <th>Domicilio</th>
                <th data-hide="all">Precio</th>
                <th data-hide="all">Observaciones</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for='pedido in pedidosDelDia' :key='pedido.idPedido'>
                <td>{{pedido.fecha}}</td>
                <td>{{pedido.domicilio}}</td>
                <td>${{pedido.total}}</td>
                <td>{{pedido.observaciones}}</td>
                <td>
                  <button class='btn btn-warning btn-circle fa fa-pencil' @click="editarPedido(pedido.idPedido)" title='Editar'></button>
                  <button class='btn btn-success btn-circle fa fa-print' @click="imprimirTicket(pedido)" title='Imprimir Ticket'></button>
                  <button class='btn btn-danger btn-circle fa fa-trash' @click='anularPedido(pedido.idPedido)' title='eliminar'></button>
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

      <div class="ibox pago">
        <div class="ibox-content">
          <div class="col-lg-12">
            <div class="form-group row">
              <label class="col-sm-3 col-form-label">TOTAL</label>
              <div class="col-sm-9"><input type="text" readonly class="form-control" v-model="subtotal"/></div>
            </div>

            <div class="row">
              <label class="col-sm-3 col-form-label">Promo</label>
              <div class="col-sm-9">
                <select class="m-b" style="width:100%">
                  <option value="1">PTGCANT</option>
                  <option selected value="2">DTOCANT</option>
                  <option value="3">PFIJO</option>
                </select>
              </div>
            </div>

            <div class="form-group row">
              <label class="col-lg-2 col-form-label">Pago</label>
              <div class="col-lg-4">
                <input type="text" placeholder="Paga con?" name="montoPago"
                class="form-control" v-model='pago.pago' v-validate="'required|decimal|max:7|min_value:0'" data-vv-as="'Pago'"/>
                <!--  @keypress="soloNumeros" @focus="blur('pago.pago')" @blur="blur('pago.pago')"  -->
              </div>

              <label class="col-sm-2 col-form-label">Vuelto</label>
              <div class="col-sm-4">
                <input type="number" readonly class="form-control" v-model='vuelto'/>
              </div>
            </div>
            <div class="form-group row">
              <label class="col-sm-3 col-form-label">Notas</label>
              <div class="col-sm-9">
                <input type="text" placeholder="Observaciones del pedido" maxlength="55" class="form-control" v-model='pago.notas'/>
              </div>
            </div>
            <div class="form-group row">
              <label class="col-sm-3 col-form-label">Demora</label>
              <div class="col-sm-9">
                <input type="text" placeholder="Min. demora" class="form-control" name="demora"
                v-model='pago.demora' v-validate="'decimal|max:4|min_value:0'" data-vv-as="'Demora'" />                
                <!-- @blur="blur('pago.demora')" @focus="blur('pago.demora')" @keypress="soloNumeros"-->
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12 offset-md-12">
              <button type="button" class="btn btn-primary" @click="validateBeforeSubmit">Guardar Pedido</button>
              <button type="button" class="btn btn-secondary" @click="cancelarPedido">Cancelar</button>
            </div>
          </div>
          
          
          <div v-if="errors.any() || !this.hayAlgunDetalleSeleccionado" class="row server-error" style="margin-top:10px; font-size:90%;">
            <ul>
              <li v-if="errors.has('montoPago')">{{errors.first('montoPago')}}</li>
              <li v-if="errors.has('demora')">{{errors.first('demora')}}</li>
              <li v-if="!this.hayAlgunDetalleSeleccionado">Seleccione algún producto.</li> 
            </ul>
          </div>
          

        </div>

      </div>

    </div>

  </div>
</template>

<script>

import $ from 'jQuery'
import { truncate } from 'fs';

export default {
  name: 'pedidos',

  data: function(){
    return {
      detallesPedido: [],
      detallesSeleccionados:[
        {
          idProducto:0,
          nombre:'',
          precio:0,
          cantidad:0,
          observaciones: '',
          tipoDetallePedido: 0
        }
      ],
      hayAlgunDetalleSeleccionado: false,
      pedidosDelDia: [],
      selected: 'Domicilio',
      origenesDePedido: [],
      cliente:{
        idCliente: 0,
        nombre:'',
        telefono:'',
        domicilio:'',
        mesa: 0,
        idOrigen: 0
      },
      pago:{
        idPromocion: 0,
        total:0,
        pago:'',
        //vuelto:0,
        notas:'',
        demora:0
      },
      subtotal: 0,
      serverError:'hoolssss'
    }
  },

  computed:{    
    vuelto() {      
      return this.pago.pago - this.subtotal
    }
  
  },

  methods: {    

    validateBeforeSubmit(){       
      if (this.detallesSeleccionados.length > 1){        
        this.hayAlgunDetalleSeleccionado = true
      }      
      
      this.$validator.validateAll().then((result) => {
        if (result && this.hayAlgunDetalleSeleccionado){                    
          this.guardarPedido();          
        }
        else{
          this.serverError = false
        }        
      })
    },
    
    obtenerDetallesPedido() {
      this.$store.dispatch('getDetallesPedido')
      .then((response) => {
        this.detallesPedido = response.data
      })
      .then(()=> {
        $('#productsTable').footable()
      })
      .catch((error) => {alert("error: " + error.message)})
    },

    obtenerOrigenesDePedido(){
      this.$store.dispatch('getOrigenesDePedido')
      .then(response => this.origenesDePedido = response.data)
      .catch(error => alert(error.message))
    },

    seleccionarProducto(detalle, nuevoValor){
      detalle.seleccionado = nuevoValor

      if (nuevoValor == true){
        detalle.cantidad = ''
      }
      else{
        detalle.cantidad = 0
        this.calcularSubtotal(detalle)        
      }
    },

    obtenerPedidosDelDia() {
      //TODO: AGREGAR ID EMPRESA
      this.$store.dispatch('getPedidosDelDia')
      .then(response => {
        this.pedidosDelDia = response.data
      })
      .then(() => {
        $('#tablaPedidos').footable()
        $('#tablaPedidos').trigger('footable_redraw');

        this.cancelarPedido()
      })
      .catch(error => alert(error.message))
    },

    blur(element){
      // if (element === 'pago.pago'){
      //   if (this.pago.pago === 0 || this.pago.pago === "0"){
      //     //this.pago.vuelto = 0
      //     this.pago.pago = ''
      //   }
      //   else if (this.pago.pago === ''){
      //     this.pago.pago = 0
      //     //this.pago.vuelto = 0
      //   }
      // }

      if (element === 'pago.demora'){
        if (this.pago.demora == 0){
          this.pago.demora = ''
        }
        else if (this.pago.demora === ''){
          this.pago.demora = 0
        }
      }
    },

    calcularSubtotal(detalle)
    {
      // Elimina el detalle de la lista de detalles seleccionados y lo descuenta del subtotal
      const index = this.detallesSeleccionados.findIndex(item => item.idProducto == detalle.idProducto)
      if (index >= 0){
        this.subtotal -= (this.detallesSeleccionados[index].cantidad * detalle.precio)
        this.detallesSeleccionados.splice(index, 1)
      }

      // Si hay algo que agregar, lo hace, de lo contrario, decrementó el producto en el paso anterior.
      if (detalle.seleccionado && detalle.cantidad > 0 && detalle.cantidad !== ""){

        const detalleNuevo = {
          "idProducto": detalle.idProducto,
          "precio": detalle.precio,
          "cantidad": detalle.cantidad,
          "nombre": detalle.nombre,
          "observaciones": detalle.observaciones,
          "tipoDetallePedido": detalle.tipoDetallePedido
        }

        this.detallesSeleccionados.push(detalleNuevo)
        this.subtotal += (detalle.precio * detalle.cantidad)
      }

      if (this.detallesSeleccionados.length > 1){
          this.hayAlgunDetalleSeleccionado = true
      }
      else{
        this.hayAlgunDetalleSeleccionado = false
      }
    },

    imprimirTicket(pedido){
      //alert ("TODO: IMPRIMIR TICKET - " + pedido.idPedido )
    },

    // calcularVuelto(){
    //   this.pago.vuelto = this.pago.pago - this.subtotal
    // },

    guardarPedido(){
      //TODO: validar antes de guardar
      //alert("TODO: VALIDAR ANTES DE GUARDAR")

      var pedido = this.crearPedido();
      this.$store.dispatch('guardarPedido', pedido)
      .then(() => {
        this.obtenerPedidosDelDia()
      })
      .then(() => {
        alert("TODO: IMPRIMIR TICKET")
      })
      .catch(error => {alert(error.message)})
    },

    cancelarPedido(){
      this.limpiarCantidades()
      this.limpiarCliente()
      this.limpiarPago()
    },

    anularPedido(idPedido){
      alert("TODO: ANULAR PEDIDO - " + idPedido)
    },

    editarPedido(idPedido){
      alert("TODO: EDITAR PEDIDO - " + idPedido)
    },

    buscarCliente(){
      if (this.cliente.telefono !== '' && this.cliente.telefono != undefined) {
        this.$store.dispatch('getCliente', this.cliente.telefono)
        .then(response => {this.cliente = response.data})
        .catch(error => {alert (error.message)})
      }
    },

    limpiarCantidades(){
      for (var i=0; i< this.detallesPedido.length; i++){
        this.seleccionarProducto(this.detallesPedido[i], false)
      }
    },

    limpiarCliente(){
      this.cliente.nombre = ''
      this.cliente.telefono = ''
      this.cliente.domicilio = ''
      this.cliente.mesa = ''
      this.selected = 'Domicilio'
    },

    limpiarPago(){
      //this.pago.pago = 0,
      //this.pago.vuelto = 0
      this.vuelto = 0
      this.pago.demora = 0
      this.pago.notas = ''
      this.pago.total = 0
    },

    crearPedido(){
      return {
        "cliente":{
          "idCliente": this.cliente.idCliente == "0" ? 0 : this.cliente.idCliente,
          "nombre": this.cliente.nombre == "" ? 0 : this.cliente.nombre,
          "telefono": this.cliente.telefono == "" ? 0 : this.cliente.telefono,
          "mesa": this.cliente.mesa == "" ? 0 : this.cliente.mesa,
          "idOrigen": (this.cliente.idOrigen == "" || this.cliente.idOrigen == "0") 
            ? 0 
            : this.origenesDePedido.find(x=> x.nombre == this.selected).idOrigen,
          "domicilio": this.cliente.domicilio
        },

        "pago": {
          "idPromocion": 0,
          "total": (this.pago.total == "" ||  this.pago.total == "0") ? 0 : this.pago.total,
          "pago": this.pago.pago == "" || this.pago.pago == "0" ? 0 : this.pago.pago,
          "vuelto": this.vuelto, 
          "notas": this.pago.notas,
          "demora": this.pago.demora == "" || this.pago.demora == "0" ? 0 : this.pago.demora,
        },

        "detalles": this.detallesSeleccionados        
      }
    }
  },

  created() {
    this.obtenerOrigenesDePedido();
    this.obtenerDetallesPedido();
    this.obtenerPedidosDelDia();
  }
}
</script>

<style scoped>
  .cantidad{
    width: 65px;
  }

  .nombre{
    width: 200px;
  }

  .precio{
    width: 70px;
  }

  .observaciones{
    width: 100%;
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

  .pago input, label{
    margin: -2px -3px;
  }

  .pago button{
    margin: 8px 3px 0px 6px;
  }

  #tablaPedidos{
    font-size: 85%;
  }

</style>
