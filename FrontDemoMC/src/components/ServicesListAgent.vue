<template>
  <div v-if="isFormVisible">
            <section class="space-between-width80">
              <div><B><h2>{{SelectedUrAgent}} </h2></b></div>
            </section>

            <section class="space-between-width80">
              <div><h3>Заявкa № {{SelectedOrderId}} </h3></div>
            </section>

            <section class="space-between-width80">
              <div>Статус</div>
              
              <div><h3>
                <select v-model="selected">
                  <option v-for="(option,index) in options"  :key="index" v-bind:value="option.id">
                  {{ option.text }}
                  </option>
                </select></h3>
              </div>

            </section>

            <div v-if="MigrantServiceList.migrants.length>0" class="divTable unstyledTable" >

              <div class="divTableHeading">
                <div class="divTableRow">
                  <div class="divTableHead">Id</div>
                  <div class="divTableHead">ФИО</div>
                  <div class="divTableHead">Паспорт</div>
                  <div class="divTableHead">Итого</div>
                  <div class="divTableHead">Услуги</div>
                  <div class="divTableHead">Оплачено</div>
                </div>
              </div>

              <div class="divTableBody">
                <div class="divTableRow" v-for="(migrant,index) in MigrantServiceList.migrants" :key="index">
                  <div class="divTableCell">{{migrant.migrantId}}</div>
                  <div class="divTableCell">{{migrant.migrantName}}</div>
                  <div class="divTableCell">{{migrant.passNumber}}\{{migrant.passSerri}}</div>
                  <div class="divTableCell">{{migrant.summ}}</div>

                  <div class="divTableCell">
                    <div class="divTableRow" v-for="(service,index) in MigrantServiceList.migrants[index].servicesList" :key="index">
                      {{service.name}} - {{service.cost}}
                    </div>
                  </div>

                  <div class="divTableCell">
                    <div class="divTableRow" v-for="(service,index) in MigrantServiceList.migrants[index].servicesList" :key="index">
                      <input type="checkbox" v-on:change="ServiceToggle(service)" v-bind:checked="service.isSeleckted">
                    </div>
                  </div>
                  
                </div>
                <div class="divTableRow">
                  <div class="divTableHead"></div>
                  <div class="divTableHead"><B>Всего</B></div>
                  <div class="divTableHead"></div>
                  <div class="divTableHead">{{TotalPrice}}</div>
                  <div class="divTableHead"></div>
                  <div class="divTableHead"></div>
                </div>
                
              </div>
            </div> 

            <section class="space-between-width80">
              <div v-if="errortext"><h2>{{errortext}}</h2></div>
            </section>

            <section class="space-between-width80">
              <button type="button" class="btn" @click="Back" >Назад</button>
              <div v-if="!IsPaid"><button type="button" class="btn" @click="SaveInfo" >Сохранить</button></div> 
            </section>

  </div>
</template>

<script>

import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'orderlistagent',
  data: function()
            {
            return{
                  selected: 0,
                  TotalPrice:0,
                  IsPaid:false,
                  options: 
                    [
                      {id :0, text: 'Новая', Engtext: 'New'},
                      {id :1, text: 'Оказана', Engtext: 'IsDone'},
	                    {id :2, text: 'Оплачена', Engtext: 'IsPaid'}
                    ],
                  isFormVisible: false,
                  errortext:null
                  }
            },
   props:
            {
            Token:String,
            SelectedUrAgent:String,
            SelectedOrderId:Number,
            SelectedOrderIndex:Number,
            MigrantServiceList:
              {
              type: Object,
              default: function()
                {
                return []
                }
              },
            OrderList:
              {
              type: Array,
              default: function()
                {
                return []
                }
              }
            },
  watch:
		{
			isFormVisible: function()
				{
          this.errortext=null;
          this.selected= 0;
          this.TotalPrice=0;
          this.IsPaid=false;
				}
		},
  methods:
    {
    Back()
      {
      this.$emit('Back');
      },
    SaveInfo()
      {
      if(this.IsPaid==true)
        {
          this.errortext="Нельзя сохранить. Заявка уже оплачена!";
        } 
      else
        {
        var OrderToSave=
          {
          cost:this.OrderList[this.SelectedOrderIndex].cost,
          data:this.OrderList[this.SelectedOrderIndex].data,
          id:this.SelectedOrderId,
          name:this.OrderList[this.SelectedOrderIndex].name,
          status:this.options[this.selected].Engtext
          };
      axios.put(App.MainUrl+"Order",OrderToSave,
        {headers: {Authorization: "Bearer " + this.Token}})
      .then(response=>
        {
        console.log('response.data ',response.data);

        if(this.options[this.selected].Engtext=='IsPaid')this.IsPaid=true;
        
        })
        .catch(e=>
          {
          
           if (e.response.status === 400)
           {
             console.log('e.response.status 400 ');
             this.errortext="Нельзя изменить. Заявка уже оплачена!";
             this.IsPaid=true;
           }
           else
            this.errortext=e;
          console.log('this.errortext ', this.errortext);
          })
        }
      },
      ServiceToggle(service)
        {
          service.isSeleckted = !service.isSeleckted;
          this.RecountTotalPrice();
        },
      RecountTotalPrice()
        {
        this.TotalPrice=0;
        for(let i in this.MigrantServiceList.migrants)
          {
            this.MigrantServiceList.migrants[i].summ=0;
            for(let j in this.MigrantServiceList.migrants[i].servicesList)
            {
              if(this.MigrantServiceList.migrants[i].servicesList[j].isSeleckted==true)
              {
                this.MigrantServiceList.migrants[i].summ+=this.MigrantServiceList.migrants[i].servicesList[j].cost;
                this.TotalPrice+=this.MigrantServiceList.migrants[i].servicesList[j].cost;
              }
            }
          }
          // console.log('this.MigrantServiceList' , this.MigrantServiceList);
        }
      }
    }

</script>

<style scoped>

div.unstyledTable {
  font-family: "Times New Roman", Times, serif;
  border: 2px solid #000000;
  width: 80%;
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