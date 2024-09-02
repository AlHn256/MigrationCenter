<template>
  <div v-if="isFormVisible" >
       
            <section class="space-between-width80">
              <div><B><h3>Заявки: {{SelectedUrAgent}} </h3></b></div>
            </section>

            <div v-if="OrderList && OrderList.length>0" class="divTable unstyledTable" >

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
                <div class="divTableRow" v-for="(order,index) in OrderList" :key="index">
                  <div class="divTableCell">{{order.id}}</div>
                    <div class="divTableCell">
                    <!-- <button class="btn" v-on:click="ShowAgentListServices(order.id,index)"> {{order.name}}</button> -->
                      <a v-on:click="ShowAgentListServices(order.id,index)">{{order.name}}</a>
                    </div>
                    <div class="divTableCell">
                      <!-- <button class="btn" v-on:click="ShowAgentListServices(order.id,index)"> {{order.data}}</button> -->
                      <a v-on:click="ShowAgentListServices(order.id,index)">{{order.date}}</a>
                    </div>
                    <div class="divTableCell">
                      <a v-on:click="ShowAgentListServices(order.id,index)">{{order.cost}}</a>
                    </div>
                  <div v-if="order.status==1" class="divTableCell">Новая</div>
                  <div v-else-if="order.status==2" class="divTableCell">Оказана</div>
                  <div v-else class="divTableCell">Оплаченная</div>
                </div>
              </div>

            </div>

             <section class="space-between-width60">
              <button type="button" class="btn" @click="Back" >Назад</button>
            </section>
          </div>
</template>

<script>


export default {
  name: 'orderlistagent',
  data: function()
            {
            return{
                  isFormVisible: false
                  }
            },
   props:
            {
            SelectedUrAgent:String,
            default:'',
            OrderList:
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
    Back()
      {
      this.$emit('Back');
      },
      ShowAgentListServices(id,Index)
      {
        console.log('ShowAgentListServices ',id,' ',Index);
        this.$emit('ShowAgentListServices',id,Index);
      }

    }
  }

</script>

<style scoped>

div.unstyledTable {
  font-family: "Times New Roman", Times, serif;
  border: 2px solid #000000;
  width: 40%;
  height: 50px;
  margin-top: 25px;
  margin-left: 25px;
}
.divTable.unstyledTable .divTableCell, .divTable.unstyledTable .divTableHead {
  border: 3px solid #AAAAAA;
  padding: 3px 3px;
}
.divTable.unstyledTable .divTableHeading {
  background: #DDDDDD;
  background: -moz-linear-gradient(top, #e5e5e5 0%, #e0e0e0 66%, #DDDDDD 100%);
  background: -webkit-linear-gradient(top, #e5e5e5 0%, #e0e0e0 66%, #DDDDDD 100%);
  background: linear-gradient(to bottom, #e5e5e5 0%, #e0e0e0 66%, #DDDDDD 100%);
}
.divTable.unstyledTable .divTableHeading .divTableHead {
  font-size: 16px;
  font-weight: bold;
}
.unstyledTable .tableFootStyle {
  font-size: 15px;
}
/* DivTable.com */
.divTable{ display: table; }
.divTableRow { display: table-row; }
.divTableHeading { display: table-header-group;}
.divTableCell, .divTableHead { display: table-cell;}
.divTableHeading { display: table-header-group;}
.divTableFoot { display: table-footer-group;}
.divTableBody { display: table-row-group;}
</style>