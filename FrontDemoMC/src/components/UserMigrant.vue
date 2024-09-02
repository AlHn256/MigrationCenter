<template>
<div v-if="isFormVisible" class="MainDiv" >
  
<div class="Migrants" v-show="isModalMigrantsVisible">


  <section class="space-between-width60">
    <div class="divTableHead"><B><h3>Мигранты</h3></b></div>
    <div class="divTableHead"><button type="button" class="btn" @click="AddMigrant" >+</button></div>
  </section>

  <div class="divTable unstyledTable"  v-if="migrants.length>0">
    <div class="divTableHeading">
      <div class="divTableRow">
        <div class="divTableHead">Id</div>
        <div class="divTableHead">FIO</div>
        <div class="divTableHead">Passport N/S</div>
        <div class="divTableHead">Edit</div>
        <div class="divTableHead">Del</div>
      </div>
    </div>

    <div class="divTableBody">
      <div class="divTableRow" v-for="(migrant,index) in migrants" :key="index">
        <div class="divTableCell">{{migrant.id}}</div>
        <div class="divTableCell">{{migrant.name}}  {{migrant.surname}}</div>
        <div class="divTableCell">{{migrant.passNumber}}/{{migrant.passSerri}}</div>
        <div class="divTableCell"><button v-on:click="EditMigrant(index,migrant.id)">Edit</button></div>
        <div class="divTableCell"><button v-on:click="DeleteMigrant(index,migrant.id)">Del</button></div>
      </div>
    </div>
  </div>
  <div>

  <section  class="space-between-width60">
    <button  type="button" class="btn" @click="$emit('OpenCloseUserMigrant')" >Назад</button>
  </section>

  </div>
  <modal title="Add Migrant" v-show="isModalAddMigrantVisible" @close="closeModal"></modal>
</div>

<div class="EditMigrant" v-if="isEditMigrantVisible && migrants.length>0">

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
            <input id="InputPassNumber" type="text" v-model="migrants[EditIndex].passNumber" placeholder="Введите номер паспорта" class="form-control" />
          </div>
      </div>

      <div class="divTableRow">
        <div class="divTableHead">Passport Seria</div>
          <div class="divTableCell">
            <input id="InputPassSerri" type="text" v-model="migrants[EditIndex].passSerri" placeholder="Введите серию паспорта" class="form-control" />
          </div>
      </div>
    </div>
  </div>

  <section class="space-between-width60">
    <button  type="button"  class="btn" @click="SaveData"  >Сохранить</button>
    <button  type="button"  class="btn" @click="close"  >Отмена</button>
  </section>
  </div>


 <div>
  <section class="space-between-width60">
    <h1>{{errors}}</h1>
  </section>
 </div>

</div>
</template>

<script>
import modal from './ModalWindowsAddUserMigrant.vue';
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'Migrants',

  data:()=>({
    Token:null,
    migrants: 
      [{
      name:'',
      migrants:'',
      patronymic:'',
      passNumber:'',
      passSerri:'',
      }],

    isModalMigrantsVisible: true,
    isEditMigrantVisible: false,
    
    isModalAddMigrantVisible: false,
    //isModalEditMigrantVisible: false,

    name:'',
    surname:'',
    patronymic:'',
    passnumber:null,
    passserri:null,

    IdOfEdditMigrant:0,
    EditIndex:0,

    errors:null,
    isFormVisible:false
  }),
    components: 
    {
      modal,
      App
    },
watch:
  {
  isFormVisible()
    {
      if(this.isFormVisible)
      {
        this.Token=sessionStorage.getItem("accessToken");
        this.GetUserMigrantList();
      }
    }
  },
methods:
    {
    AddMigrant() 
      {
      this.isModalAddMigrantVisible = true;
      },
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
        axios.put(App.MainUrl+"Migrant",data,
          {
          headers:
            {
            Authorization: "Bearer " + this.Token
            }
          })
          .then(response=>
            {
            console.log('response.data ' ,response.data);
            if(this.errors!=null)this.errors=null;
            
            this.GetUserMigrantList();
            this.isModalMigrantsVisible= true;
            this.isEditMigrantVisible=false;
            })
          .catch(e=>
            {this.errors=e;})
        },
      close() 
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
      closeModal(){
        this.isModalAddMigrantVisible = false;
        //this.isModalEditMigrantVisible = false;
        this.GetUserMigrantList();
      },
      GetUserMigrantList()
      {   
        console.log('GetUserMigrantList');
        axios.get(App.MainUrl+"Migrant/GetUserMigrantList",
          {
          headers:
            {
            Authorization: "Bearer " + this.Token
            }
          })
        .then(response =>
          {
          console.log('GetUserMigrantList response.data ', response.data);
          this.migrants = response.data;
          if(this.migrants.length == 0)
            this.errors="Список мигрантов пуст";
          else 
            this.errors = null;
          })
        .catch(e=>
          {
          this.errors=e;
          })
      },
    DeleteMigrant(index,id)
      {
      console.log('AddDelMigrant index',index,' id ',id);
      console.log('this.Token ',this.Token);
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

        //this.migrants.splice(index, 1);
        this.GetUserMigrantList();
        console.log("Migrants " +response.data+ " Del");
        })
      .catch(e=> 
        {
        this.errors.push(e)
        this.GetUserMigrantList();
        console.log('e ',  e);
        });
      }

    // DeleteUserMigrant(index,id)
    //   {
    //     console.log('DeleteMigrant index',index,' id ',id);
    //     //{headers:{ Authorization: "Bearer " + this.Token}}
    //     axios.delete(App.MainUrl+"Migrant/AddDelMigrant/" + id) 
    //     .then(response=> 
    //       {
    //       console.log("response.data " +response.data);
    //       if(response.data!="-1")
    //       {
    //         console.log("this.migrants.length " +this.migrants.length);
    //         this.migrants.splice(index, 1);
    //         console.log("Migrants " +response.data+ " Del");
    //       }
    //       else 
    //       {
    //         console.log("response.data " +response.data);
    //       }
    //       })
    //     .catch(e=>{this.errors.push(e)})
    //   }
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
  width: 60%;
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

.divTable{ display: table; }
.divTableRow { display: table-row; }
.divTableHeading { display: table-header-group;}
.divTableCell, .divTableHead { display: table-cell;}
.divTableHeading { display: table-header-group;}
.divTableFoot { display: table-footer-group;}
.divTableBody { display: table-row-group;}
</style>