<template>
<div v-if="isNewOrderVisible" >

          <div class="divSection">
            <div class="divTableHead"><B><h3>Создание новой заявки</h3></b></div>
          </div>

              <div v-if="migrants && migrants.length>0" class="divTable unstyledTable">
                <div class="divTableHeading">
                  <div class="divTableRow">
                    <div class="divTableHead">Id</div>
                    <div class="divTableCell"><input type="checkbox" v-on:change="toggleAll()" v-model="isCheckedAll"></div>
                    <div class="divTableHead">FIO</div>
                    <div class="divTableHead">Passport N / S</div>
                  </div>
                </div>

                <div class="divTableBody">
                  <div class="divTableRow" v-for="(migrant,index) in migrants" :key="index">
                    <div class="divTableCell">{{migrant.id}}</div>
                    <div class="divTableCell"><input type="checkbox" v-on:change="MigrantToggle(migrant)" v-bind:checked="migrant.isSeleckted"></div>

                    <div class="divTableCell">
                      <Div v-if="migrant.isSeleckted">
                        {{migrant.name}}  {{migrant.surname}}
                      </div>
                      <div v-else class="spandel"> 
                        {{migrant.name}}  {{migrant.surname}}
                      </div>
                    </div>

                    <div class="divTableCell">
                      <Div v-if="migrant.isSeleckted">
                        {{migrant.passNumber}} / {{migrant.passSerri}}
                      </div>
                      <div v-else class="spandel"> 
                        {{migrant.passNumber}} / {{migrant.passSerri}}
                      </div>
                    </div>

                  </div>
                </div>

              </div>

            <div class="divSection">
              <section class="space-between">
                <button type="button" class="btn" @click="Back" >Назад</button>
                <button type="button" class="btn" @click="Next" >Далее</button>
              </section>
            </div>

            </div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default {
name: 'neworder',
data: function()
            {
            return{
                  isNewOrderVisible: false,
                  isCheckedAll: true
                  }
            },
components:
    {
      App,
    },
props:
            {
            NewOrderNumber:Number,
            migrants:
              {
              type: Array,
              default: function()
                {
                return []
                }
              },
            },
methods:
    {
    Back()
              {
                this.$emit('Back');
             
              },
              Next()
              {
                this.$emit('Next');
              },
              toggleAll()
              {
                for (let i in this.migrants)
                  this.migrants[i].isSeleckted=this.isCheckedAll;
              },
              MigrantToggle(migrant)
              {
                migrant.isSeleckted = !migrant.isSeleckted;
                this.CheckAllToggle();
              },
              CheckAllToggle()
              {
              this.isCheckedAll=true;
              for (let i in this.migrants)
                {
                if(this.migrants[i].isSeleckted==false)
                  {
                  this.isCheckedAll=false;
                  break;
                  }
                }
              }
    }
}
</script>

<style>
</style>