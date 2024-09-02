<template>
<div v-if="isFormVisible" class="MainDiv" >
<div class="Migrants" v-show="isModalMigrantsVisible">
  
  <div class="divTable unstyledTable"  >
    <div class="divTableRow">
      <div class="divTableHead"><B><h3>ALL Migrants</h3></b></div>
      <!-- <div class="divTableHead"><button type="button" class="btn" @click="AddMigrant" >+</button></div> -->
    </div>
  </div>

<div class="divTable unstyledTable"  v-if="migrants && migrants.length">
  <div class="divTableHeading">
    <div class="divTableRow">
      <div class="divTableHead">Id</div>
      <div class="divTableHead">FIO</div>
      <div class="divTableHead">Passport N/S</div>
      <div class="divTableHead">Conection Users</div>
      <div class="divTableHead">Edit</div>
      <div class="divTableHead">Is Del</div>
      <div class="divTableHead">Del/Add</div>
    </div>
  </div>

  <div class="divTableBody">
    <div class="divTableRow" v-for="(migrant,index) in migrants" :key="index">
      <div class="divTableCell" >{{migrant.id}}</div>
      <div class="divTableCell">{{migrant.name}}  {{migrant.surname}}</div>
      <div class="divTableCell">{{migrant.passNumber}}/{{migrant.passSerri}}</div>
      <div class="divTableCell">[1,2,23,4,231]</div>
      <div class="divTableCell"><button v-on:click="EditMigrant(index,migrant.id)">Edit</button></div>
      <div class="divTableCell">{{migrant.isDelite}}</div>
      <div class="divTableCell"><button v-on:click="AddDelMigrant(index,migrant.id)">Add/Del</button></div>
      <!-- <div class="divTableCell"><button v-on:click="AdminFullDelMigrant(index,migrant.id)">Совсем-Del</button></div> -->
    </div>
  </div>

</div>

<!-- <modal title="Add Migrant" v-show="isModalAddMigrantVisible" @close="closeModal"></modal> -->
 <section class="space-between-width80">
  <button  type="button"  class="btn" @click="$emit('OpenCloseMigrants')">Назад</button>
 </section>
</div>

<div class="EditMigrant" v-show="isEditMigrantVisible">
    <div class="divTable unstyledTable" >
      <div class="divTableHeading">
      <div class="divTableRow">
        <div class="divTableHead">Id</div>
          <div class="divTableCell">
            {{IdOfEdditMigrant}}
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Фамилия</div>
          <div class="divTableCell">
            <input id="InputSurname" type="text" v-model="migrants[EditIndex].surname" placeholder="Введите фамилию" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Имя</div>
          <div class="divTableCell">
            <input id="InputName" type="text" v-model="migrants[EditIndex].name" placeholder="Введите имя" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Отчество</div>
          <div class="divTableCell">
            <input id="InputPatronymic" type="text" v-model="migrants[EditIndex].patronymic" placeholder="Введите отчество" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Passport Number</div>
          <div class="divTableCell">
            <input id="InputPassNumber" type="text" v-model.number="migrants[EditIndex].passNumber" placeholder="Введите номер паспорта" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Passport Seria</div>
          <div class="divTableCell">
            <input id="InputPassSerri" type="text" v-model.number="migrants[EditIndex].passSerri" placeholder="Введите серию паспорта" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableCell"></div>
        <div class="divTableCell">
          <button  type="button"  class="btn" @click="SaveData"  >SaveData</button>
          <button  type="button"  class="btn" @click="OpenCloseModal"  >Отмена</button>            
        </div>
      </div>

    </div>
  </div>
</div>

<ul v-if="errors && errors.length > 0">
  <li v-for="(error,index) of errors" :key="index">
  {{error.message}}
  </li>
</ul>
</div>
</template>
          
<script>
//import modal from './ModalWindowsAddUserMigrant.vue';
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'Migrants',
  components: 
    {
     // modal,
      App,
    },
  data:()=>({
    Token:'',
    migrants: 
      [{
      name:'',
      migrants:'',
      patronymic:'',
      passNumber:'',
      passSerri:'',
      }],
    Role:'',
    
    isModalMigrantsVisible: true,
    isEditMigrantVisible: false,
    
    //isModalAddMigrantVisible: false,
    isModalEditMigrantVisible: false,

    name:'Рамшан',
    surname:'Джамшутович',
    patronymic:'',
    passnumber:4421,
    passserri:743523,

    IdOfEdditMigrant:0,
    EditIndex:0,
    errors:[],
    isFormVisible:false
  }),
  watch:
    {
    isFormVisible()
      {
      if(this.isFormVisible)
        {
        //console.clear();
        this.Token=sessionStorage.getItem("accessToken");
        this.errors=[];
        this.GetAllMigrantList();
        };
      }
    },

methods:
    {
      // AddMigrant() 
      // {
      //   this.isModalAddMigrantVisible = true;
      // },
      EditMigrant(index,id) 
      {
        console.log('EditMigrant ');

        this.EditIndex=index;
        this.IdOfEdditMigrant=id;

        if(this.isModalMigrantsVisible==true)
        {
          this.isModalMigrantsVisible= false;
          this.isEditMigrantVisible=true;
        }
        else 
        {
          this.isModalMigrantsVisible= true;
          this.isEditMigrantVisible=false;
        }
      },
      SaveData() 
        {
        var data = 
            {
            Id:this.IdOfEdditMigrant,
            Name:this.migrants[this.EditIndex].name,
            Surname:this.migrants[this.EditIndex].surname,
            Patronymic: this.migrants[this.EditIndex].patronymic,
            Passnumber: this.migrants[this.EditIndex].passNumber,
            Passserri: this.migrants[this.EditIndex].passSerri
            }
        axios.put(App.MainUrl+"Migrant",data)
          .then(response=>
            {
            console.log('response.data ',response.data);
            if(this.errorvisible==true)this.errorvisible= false;

            this.GetAllMigrantList();
            this.isModalMigrantsVisible= true;
            this.isEditMigrantVisible=false;
            })
          .catch(e=>
            {
            this.errortext=e;
            this.errorvisible= true;
            })
        },
      OpenCloseModal() 
        {
        if(this.isModalMigrantsVisible==true)
        {
          this.isModalMigrantsVisible= false;
          this.isEditMigrantVisible=true;
        }
        else 
          {
            this.isModalMigrantsVisible= true;
            this.isEditMigrantVisible=false;
          }
        },
      closeModal()
      {
       // this.isModalAddMigrantVisible = false;
        this.isModalEditMigrantVisible = false;
        this.GetAllMigrantList();
      },
      GetAllMigrantList()
        {    
        axios.get(App.MainUrl+"Migrant",
          {
          headers:
            {
            Authorization: "Bearer " + this.Token
            }
          })
        .then(response=>{this.migrants=response.data})
        .catch(e=>{this.errors.push(e)})
        },
    AddDelMigrant(index,id)
      {
      console.log('AddDelMigrant index',index,' id ',id);
      axios.delete(App.MainUrl+"Migrant/AddDelMigrant/" + id,
        {
        headers:
          {
          Authorization: "Bearer " + this.Token
          }
        }) 
      .then(response => 
        {
        console.log('response ',response);
        console.log('response.data ',response.data);
        console.log('response.status ',response.status);
        console.log('response.statusText ',response.statusText);
        console.log('response.headers ',response.headers);
        console.log('response.config ',response.config);
        this. GetAllMigrantList();
        })
      .catch(e=> 
        {
        this.errors.push(e)
        console.log('e ',  e);
        });
      }
    }
}
</script>

<style scope>

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