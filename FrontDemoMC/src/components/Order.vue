<template>
<div v-if="isFormVisible" class="MainDiv" >

  <div class="Order">
    <orderlist @Back="$emit('OpenCloseOrder')" :orders="orders" ref="Orderdetails" @NewOrder="ShowNewOrder" > </orderlist>
    <NewOrder :NewOrderNumber="NewOrderNumber" :migrants="SelectedMigrantesServices" ref="NewOrderdetails" @Back="ShowNewOrder" @Next="ShowNewOrderForm2" ></NewOrder>
    <NewOrderForm2 :NewOrderNumber="NewOrderNumber" :services="services" :migrants="SelectedMigrantesServices" :TotalSummOfServices="TotalSummOfServices" ref="NewOrderForm2details" @Back="ShowNewOrderForm2" @Next="ShowSaveForm"  ></NewOrderForm2>
    <SaveForm :NewOrderNumber="NewOrderNumber" :migrants="SelectedMigrantesServices" :Token="Token" ref="SaveFormdetails"  @Back="ShowSaveForm"  @ReloadeOrderForm="ReloadeOrderForm"  ></SaveForm>
  </div>

 <div>
  <br><h1>{{errortext}}</h1>
 </div>

</div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';
import Orderlist from './OrderList';
import NewOrder from './NewOrder';
import NewOrderForm2 from './NewOrderForm2';
import SaveForm from './SaveForm';

export default {
  name: 'Order',
  data:()=>( {
    AutoSelect:false,
    //AutoSelect:true,
    orders: [],
    services: [],
    SelectedMigrantesServices: [],
    NewOrderNumber:null,
    TotalSummOfServices:0,
    Token: '',
    OrderName:'Order',
    isFormVisible:false,
    errortext:null
  }),
  components:
    {
      Orderlist,
      NewOrder,
      NewOrderForm2,
      SaveForm,
      App
    },
  watch:
    {
    isFormVisible()
      {
      if(this.isFormVisible)
        {
        console.log('GetServiceList');
        this.Token=sessionStorage.getItem("accessToken");
        this.GetOrdersList();
        this.GetMigrantesServices();
        };
      }
    },
 methods:
  {
    Initialisation()
        {
          this.SelectedMigrantesServices[0].services[1].isSeleckted=true;
          this.SelectedMigrantesServices[0].services[3].isSeleckted=true;
          this.SelectedMigrantesServices[0].services[5].isSeleckted=true;

          for (let i in this.SelectedMigrantesServices)
            this.SelectedMigrantesServices[i].isSeleckted=false;

          this.SelectedMigrantesServices[1].isSeleckted=true;
          this.SelectedMigrantesServices[2].isSeleckted=true;
          this.SelectedMigrantesServices[this.SelectedMigrantesServices.length-1].isSeleckted=true;

          //this.$refs.Orderdetails.isOrderFormVisibl = !this.$refs.Orderdetails.isOrderFormVisibl;
          this.$refs.NewOrderdetails.isNewOrderVisible=!this.$refs.NewOrderdetails.isNewOrderVisible;
          this.$refs.NewOrderForm2details.isOrderForm2Visibl=!this.$refs.NewOrderForm2details.isOrderForm2Visibl;
          this.$refs.SaveFormdetails.isSaveFormVisibl=!this.$refs.SaveFormdetails.isSaveFormVisibl;
        },
  ClearSelectedMigrantesList()
      {
      for (let i in this.SelectedMigrantesServices)
        {
        this.SelectedMigrantesServices[i].isSeleckted=true;
        for (let j in this.SelectedMigrantesServices[i].services)
          this.SelectedMigrantesServices[i].services[j].isSeleckted=false;
        }
      },
    ShowNewOrder()
      {
        this.$refs.Orderdetails.isOrderFormVisibl = !this.$refs.Orderdetails.isOrderFormVisibl;
        this.$refs.NewOrderdetails.isNewOrderVisible=!this.$refs.NewOrderdetails.isNewOrderVisible;
      },
    ShowNewOrderForm2()
      {
        this.$refs.NewOrderdetails.isNewOrderVisible=!this.$refs.NewOrderdetails.isNewOrderVisible;
        this.$refs.NewOrderForm2details.isOrderForm2Visibl=!this.$refs.NewOrderForm2details.isOrderForm2Visibl;
      },
    ShowSaveForm()
      {
        this.$refs.NewOrderForm2details.isOrderForm2Visibl=!this.$refs.NewOrderForm2details.isOrderForm2Visibl;
        this.$refs.SaveFormdetails.isSaveFormVisibl=!this.$refs.SaveFormdetails.isSaveFormVisibl;
      },
    ReloadeOrderForm()
      {
        this.$refs.SaveFormdetails.isSaveFormVisibl=!this.$refs.SaveFormdetails.isSaveFormVisibl;
        this.$refs.Orderdetails.isOrderFormVisibl = !this.$refs.Orderdetails.isOrderFormVisibl;
        
        this.GetOrdersList();
        this.NewOrderNumber=null;
        this.ClearSelectedMigrantesList();
      },
    AddServices:function(identity)
		  {
        let arr=this.services.slice();
        identity.services=arr;
        identity.isSeleckted=true;
		  },
    GetOrdersList()
      {
      console.log('GetOrdersList');
      axios.get(App.MainUrl+"Order/GetOrders",
        {
        headers:
          { 
          Authorization: "Bearer " + this.Token
          }
        })
      .then(response=>
          {
          this.orders=response.data;
          console.log('this.orders ', this.orders);
          if(this.orders.length==0)
            {
            this.errortext=" Заявок нет";
            console.log('this.errortext ', this.errortext);
            }
          else this.errortext=null;
          })
      .catch(e=>
        {
              this.errortext=e;
              console.log('e ', e);
              console.log('this.errortext ', this.errortext);
              console.log("this.errortext "+ this.errortext);
              console.log('this.errortext.text ', this.errortext.text);
              console.log('this.errortext.status ', this.errortext.status);
              console.log('this.errortext.Error ', this.errortext.Error);
              console.log('this.errortext.value ', this.errortext.value);
              console.log('this.errortext.length ', this.errortext.length);
        })
      },
    GetMigrantesServices()
      {
      console.log('GetServiceList');
      axios.get(App.MainUrl+"Service")
      .then(response=>
        {
        this.services=response.data
        this.services.forEach(function (element)
          {
            element.isSeleckted=false;
          });
        for(let i in this.services)
          this.TotalSummOfServices+=this.services[i].cost;
        })
      .catch(e=>
        {
        this.errors.push(e)
        })
      //console.log('GetUserMigrantList');
      axios.get(App.MainUrl+"Migrant/GetUserMigrantList",{headers:{ Authorization: "Bearer " + this.Token}})
      .then(response=>
        {
        this.SelectedMigrantesServices=response.data;
        this.SelectedMigrantesServices.forEach(this.AddServices);

        for(let j in this.SelectedMigrantesServices)
          {
          var test=[];
          for(let i in this.services)
            {
            var service=
              {
              id:this.services[i].id,
              name:this.services[i].name,
              cost:this.services[i].cost,
              isSeleckted:false
              };
            test.push(service);
            }
          this.SelectedMigrantesServices[j].services=test;
          }
        //console.log('this.SelectedMigrantesServices ',this.SelectedMigrantesServices);
        })
      .catch(e=>
        {
        this.errors.push(e);
        })
      }
  }
}
</script>

<style scoped>

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
