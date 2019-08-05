<template>
  <div class="container">
    <div class="wrapper wrapper-content">      

      <div class='row'>
        <div class="col-lg-6">
          <promocion/>
        </div>      
        
        <div class="col-lg-6">
          <combos/>
        </div>
        
      </div>
      

    </div>
  </div>
</template>

<script>

import $ from 'jQuery'
import combos from '../combos/Combos.vue'
import promocion from './Promocion.vue'

export default {
  name: 'promociones',
  components:{
    combos,
    promocion
  }, 
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
