<template>
  <div v-if="isOrderFormVisibl" >

            <div class="divSection">
              <section class="space-between">
               <div><B><h3>Заявки </h3></b></div>
               <div><button type="button" class="btn" @click="NewOrder" >+</button></div>
              </section>
            </div>

            <div v-if="orders && orders.length>0" class="divTable unstyledTable" >

              <div class="divTableHeading">
                <div class="divTableRow">
                  <div class="divTableHead">Id</div>
                  <div class="divTableHead">Заявка</div>
                  <div class="divTableHead">Дата</div>
                  <div class="divTableHead">Стоимость</div>
                  <div class="divTableHead">Статус</div>
                </div>
              </div>

              <div class="divTableBody">
                <div class="divTableRow" v-for="(order,index) in orders" :key="index">
                  <div class="divTableCell">{{order.id}}</div>
                  <div class="divTableCell">{{order.name}}</div>
                  <div class="divTableCell">{{order.date}}</div>
                  <div class="divTableCell">{{order.cost}}</div>
                  <div class="divTableCell">{{order.status}}</div>
                </div>
              </div>

            </div>

            <div class="divSection">
              <button type="button" class="btn" @click="Back" >Назад</button>
            </div>

          </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'orderlist',
  data: function()
    {
    return{
      isOrderFormVisibl: true
      }
  },
   props:
            {
            orders:
              {
              type: Array,
              default: function()
                {
                return []
                }
              }
            },
  methods:
  {
  NewOrder()
    {
    this.$emit('NewOrder');
    },
  Back()
    {
    this.$emit('Back');
    }
  }
}

</script>

<style >

</style>