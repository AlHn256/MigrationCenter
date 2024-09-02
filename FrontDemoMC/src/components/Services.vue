<template>
 <div v-if="isFormVisible" class="services">

  <section class="space-between-width60">
    <h1>Услуги</h1>
  </section>

  <div class="divTable unstyledTable"  v-if="services && services.length">

  <div class="divTableHeading">
    <div class="divTableRow">
      <div class="divTableHead">Id</div>
      <div class="divTableHead">Service</div>
      <div class="divTableHead">Cost</div>
      <div class="divTableHead">Edit</div>
      <div class="divTableHead">Del</div>
    </div>
  </div>

  <div class="divTableBody">
    <div class="divTableRow" v-for="(service,index) in services" :key="index">
      <div class="divTableCell">{{service.id}}</div>
      <div class="divTableCell">{{service.name}}</div>
      <div class="divTableCell">{{service.cost}}</div>
      <div class="divTableCell"><button v-on:click="EditService(index,service.id)">Edit</button></div>
      <div class="divTableCell"><button v-on:click="DeleteService(index,service.id)">Del</button></div>
    </div>
  </div>

</div>

  <section  class="space-between-width60">
    <button  type="button" class="btn" @click="$emit('OpenCloseServices')" >Назад</button>
  </section>


  <ul v-if="errors && errors.length">
    <li v-for="(error,index) of errors" :key="index">
    {{error.message}}
    </li>
  </ul>

 </div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'services',
  data:()=>( {
    services: [],

    isFormVisible:false,
    errors:[]
  }),
    components:
    {
      App
    },
  watch:
    {
    isFormVisible()
      {
      if(this.isFormVisible)
        {
        console.log('GetServiceList');
        axios.get(App.MainUrl+"Service")
        .then(response=> 
          {
            this.services = response.data
            console.log('response.data ',response.data);
          })
        .catch(e=>{this.errors.push(e)})
        };
      }
    },
  methods:
  {
  DeleteService(index,id)
    {
    var url=App.MainUrl+"Service/"+id;
    console.log('DelService url',url);
    console.log("Index "+index+" Id "+ id);
        axios.delete(url)
        .then(response=>
          {
          this.services.splice(index, 1);
          console.log("Service: " +response.data);
          })
        .catch(e=>{this.errors.push(e)})
      },
      EditService(index,id)
      {

        var data =
            {
            Id:id,
            Name:"New Service",
            Cost:999999,
            }

        var url=App.MainUrl+"Service";

        console.log("EditService(index,id) " +index+","+id );
        console.log('PutService url',url);

        axios.post(url,data)
        .then(response=>
          {
          //this.services.splice(index, 1);
          console.log('response.data', response.data);
          })
        .catch(e=>
          {
          this.errors.push(e)
          })
      }
  }
}

</script>

<style scoped>

.btn{
  font-size: 20px;
  padding: 8px 16px;
  cursor: pointer;
  border: 2px solid #1e3b10;
  border-radius:6px;
}

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