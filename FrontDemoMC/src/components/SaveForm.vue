<template>
<div v-if="isSaveFormVisibl">

          <div class="divSection">
            <div class="divTableHead"><B><h3>Создание новой заявки</h3></b></div>
          </div>

            <div>
              <br><B><h3>&nbsp;&nbsp;&nbsp;&nbsp;Всего:{{TotalPrice}}</h3></b>
            </div>

            <div v-if="migrants && migrants.length>0" class="divTable unstyledTable">
              
                <div class="divTableHeading">
                  <div class="divTableRow">
                    <div class="divTableHead">Id</div>
                    <div class="divTableHead">FIO</div>
                    <div class="divTableHead">Passport N / S</div>
                    <div class="divTableHead">Services</div>
                  </div>
                </div>

                <div class="divTableBody">
                    <!-- <div class="divTableRow" v-for="(migrant,index) in migrants" :key="index" v-if="migrant.isSeleckted"> -->
                  <div class="divTableRow" v-for="(migrant,index) in migrants" :key="index" >

                    <div v-if="migrant.isSeleckted" class="divTableCell">{{migrant.id}}</div>
                    <div v-if="migrant.isSeleckted" class="divTableCell">{{migrant.name}}  {{migrant.surname}}</div>
                    <div v-if="migrant.isSeleckted" class="divTableCell">{{migrant.passNumber}} / {{migrant.passSerri}}</div>
                    <div v-if="migrant.isSeleckted" class="divTableCell">
                        <div v-for="(service,index) in migrant.services" :key="index">
		                      <p v-if="service.isSeleckted">{{service.name}} - {{service.cost}}</p>
	                      </div>
                    </div>
                  </div>
                </div>
              </div>

           <div v-if="error">
              <h1>Error: {{error}} </h1>
            </div>


            <div class="divSection">
              <section class="space-between">
                      <button type="button" class="btn" @click="Back" >Назад</button>
                      <button type="button" class="btn" @click="Save" >Сохранить</button>
              </section>
            </div>

          </div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'saveform',
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
          Token:String
          },

    data: function()
          {
          return{
                isSaveFormVisibl: false,
                TotalPrice:0,
                error:null
                }
          },
    watch:
		        {
                isSaveFormVisibl: function()
            		{
                    this.Reloade();
                    }
                },
    components:
                {
                    App
                },
    methods:
          {
          RecountTotalPrice()
            {
               this.TotalPrice=0;
               for (let i in this.migrants)
               {
                 if(this.migrants[i].isSeleckted)
                 {
                   for (let j in this.migrants[i].services)
                    {
                      if(this.migrants[i].services[j].isSeleckted)
                        this.TotalPrice+=this.migrants[i].services[j].cost;
                    }
                 }
               }
            },
          Back()
            {
              this.$emit('Back');
            },
          CheckServicesIsSelect()
            {
              this.error=null;
              if(this.migrants)
              {
                for (let i in this.migrants)
                  {
                    if(this.migrants[i].isSeleckted==true)
                    {
                      for (let j in this.migrants[i].services)
                        {
                          if( this.migrants[i].services[j].isSeleckted==true)
                            return true;
                        }
                    }
                  }
              }
              this.error=" Ни один из сервисов не выбран!";
              return false;
            },
          Save()
            {
            if(this.CheckServicesIsSelect())
              {
  						if(this.Token==null)console.log('Token null');
							else
	  						{
                var NewOrderArr=
                {
                  name: "Заявка № ",
                  migrants:[]
                };

                for (let i in this.migrants)
                  {
                  if(this.migrants[i].isSeleckted==true)
                    {
                    var ServiceArreyId=[];
                    for (let j in this.migrants[i].services)
                      if(this.migrants[i].services[j].isSeleckted)
                        ServiceArreyId.push(this.migrants[i].services[j].id);

                    var migrant=
                      {
                      migrantid:this.migrants[i].id,
                      servicesid:ServiceArreyId
                      };
                    NewOrderArr.migrants.push(migrant);
                    }
                  }
                console.log('NewOrderArr ',NewOrderArr);
                axios.post(App.MainUrl+"Order/AddOrder",NewOrderArr,
                  {
                  headers:
                    { 
                    Authorization: "Bearer " + this.Token
                    }
                  })
                .then(response=>
                  {
                  //console.log('response.data ' ,response.data);
                  this.$emit('ReloadeOrderForm');
                  })
                .catch(e=>
                  {
					        this.error=e;
					        console.log('errortext ',this.error);
					        })
        }
      }
    },
  Reloade()
    {
    this.RecountTotalPrice();
    this.$forceUpdate();
    }
  }
}
</script>

<style >


.spandel {
  color: rgba(0, 0, 0, 0.3);
}

.divSection
{
  font-family: "Times New Roman", Times, serif;
  width: 65%;
  height: 50px;
  margin-top: 25px;
  margin-left: 25px;
}

div.unstyledTable {
  font-family: "Times New Roman", Times, serif;
  border: 4px solid #000000;
  width: 65%;
  height: 50px;
  margin-top: 25px;
  margin-left: 25px;
}

.divTable.unstyledTable .divTableCell, .divTable.unstyledTable .divTableHead {
  border: 3px solid #AAAAAA;
  padding: 3px 3px;
}

.divTable.unstyledTable .divTableCellFixedWidth20ps, .divTable.unstyledTable .divTableHeadFixedWidth20ps {
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
.divTableCell, .divTableHead 
{
  display: table-cell;
}

.divTableCellFixedWidth20ps, .divTableHeadWidth20ps
{
  display: table-cell;
  width:20%;
}

.divTableHeading { display: table-header-group;}
.divTableFoot { display: table-footer-group;}
.divTableBody { display: table-row-group;}



.selector {
  width: 200px;
  font-size: 23px;
  border: 2px solid #1e3b10;
  border-radius:6px;
  padding: 8px 16px;
  cursor: pointer;
  align-items: center;
}

section 
{
  display: flex;
}

.flex-end
{
  display: flex;
  align-items: center;
  justify-content: flex-end;
  width: 40%;
}

.flex-start
{
  display: flex;
  align-items: center;
  justify-content: flex-start;
  width: 40%;
}
.flex-center
{
  display: flex;
  align-items: center;
  justify-content: center;
}
.center 
{
  justify-content: center;
}
.space-between 
{
  justify-content: space-between;
}
.space-around 
{
  justify-content: space-around;
}
</style>
