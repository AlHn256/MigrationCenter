<template>
 <div v-if="isFormVisible" class="LKabBuhgalter">
  <section class="space-between-width90">
    <h1>Кабинет бухгалтера</h1>
  </section>
  
  <div v-if="UserList.length==0" >
    UserList пуст.
  </div>
  <div v-else>

		<div class="divTable unstyledTable">
		
			<div class="divTableHeading">
				<div class="divTableRow">
				<div class="divTableHead">Id</div>
				<div class="divTableHead">Юр. лицо</div>
				<div class="divTableHead">Счет</div>
				<div class="divTableHead">Edit</div>
				</div>
			</div>
			
			<div class="divTableBody">
				<div class="divTableRow" v-for="(user,index) in UserList" :key="index">
				<div class="divTableCell">{{user.id}}</div>
				<div class="divTableCell">{{user.name}}</div>
				<div class="divTableCell">{{user.account}}</div>
				<div class="divTableCell"><button v-on:click="ShowPayList(index,user.id)">Пополнить счет</button></div>
				</div>
			</div>
		
		</div>

  </div>

  <PayList :SelectedId="SelectedId" :SelectedUrAgentName="SelectedUrAgentName" :SelectedINN="SelectedINN" :SelectedKPP="SelectedKPP" ref="PayListdetails" @closePayList="closePayList" />

 </div>
</template>

<script>

import axios from 'axios';
import App from '../../App.vue';
import PayList from './PayList';


export default {
  name: 'lkabbuhgalter',
  data:()=>
    ({
    UserList:[],
    isFormVisible:false,
    Token:null,
    SelectedUrAgentName:'',
    SelectedINN:'',
    SelectedKPP:'',
    SelectedId:null,
    }),
  components:
    {
    App,
    PayList
    },
  watch:
    {
    isFormVisible()
      {
      if(this.isFormVisible==true)
        {
        this.Token=sessionStorage.getItem("accessToken");
        this.GetListOfJurperson();
        }
      }
    },
  methods:
    {
    GetListOfJurperson()
      {
      console.log("GetListOfJurperson");
      axios.get(App.MainUrl+"GetJurperson",
        {
        headers: 
          {
          Authorization: "Bearer " + this.Token
          }
        })
      .then(response =>
        {
        this.UserList = response.data;
        console.log('this.UserList ',this.UserList);
        })
      .catch(e=>
        {
        console.log('this.errortext ',e);
        })
      },
    ShowPayList(index,id)
      {
      this.SelectedUrAgentName = this.UserList[index].name;
      this.SelectedINN = this.UserList[index].inn;
      this.SelectedKPP = this.UserList[index].kpp;
      this.SelectedId = id;

      console.log('this.SelectedId ',this.SelectedId);
      console.log('this.SelectedKPP ',this.SelectedKPP);
      console.log('this.SelectedUrAgentName ',this.SelectedUrAgentName);

      this.$refs.PayListdetails.isFormVisibl = true;
      },
    closePayList()
      {
      this.GetListOfJurperson();
      this.$refs.PayListdetails.isFormVisibl = false;
      }
    },
  }
</script>

<style scoped>

div.unstyledTable {
  width: 50%;
}

</style>