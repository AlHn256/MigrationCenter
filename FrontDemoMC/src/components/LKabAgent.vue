<template>
 <div v-if="isFormVisible" class="LKabAgent">
  <section class="space-between-width60">
    <h1>Кабинет агента</h1>
  </section>
  <section class="space-between-width60">
    <div v-if="UserList.length==0" >
      UserList пуст.
    </div>
  <div v-else>
    <div v-for="(user,index) in UserList" :key="index">
      <div>
      <!-- <button class="btn" v-on:click="ShowAgentOrder(user.id,user.name)"> {{user.name}}</button> -->
      <h3><a v-on:click="ShowAgentOrder(user.id,user.name)">{{user.name}}</a></h3>
      </div>
    </div>
  </div>

  </section>
 </div>
</template>

<script>

import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'lkabagent',
  data:()=>
    ({
    UserList:[],
    isFormVisible:false,
    Token:null
    }),
  components:
    {
    App
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
      .then(response=>
        {
        this.UserList=response.data;
        })
      .catch(e=>
        {
        //this.errortext=e;
        console.log('this.errortext ',e);
        })
      },
    ShowAgentOrder(id,SelectedUrAgent)
      {
      //console.log('ShowAgentOrder ' ,id,' ',SelectedUrAgent);
      this.$emit('ShowAgentOrder',id,SelectedUrAgent);
      },
    BackLink()
      {
      console.log("BackLinksdasd");
      document.location.href = "#/LKabUrAgent";
      }
    },
  }
</script>

<style scoped>

</style>