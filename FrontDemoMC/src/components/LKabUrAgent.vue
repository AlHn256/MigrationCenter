<template>
 <div>
   <div v-if="isFormVisible" class="DivLKabUrAgent">
    <section class="space-between-width60"><h2><a v-on:click="OpenCloseUserMigrant()">Мигранты</a></h2></section>
    <section class="space-between-width60"><h2><a v-on:click="OpenCloseServices()">Услуги</a></h2></section>
    <section class="space-between-width60"><h2><a v-on:click="$emit('HideAll')">Пакеты</a></h2></section>
    <section class="space-between-width60"><h2><a v-on:click="OpenCloseOrder()">Заявки</a></h2></section>
  </div>
  <div>
    <Services ref="Servicesdetails" @OpenCloseServices="OpenCloseServices"/>
    <UserMigrant ref="UserMigrantdetails" @OpenCloseUserMigrant="OpenCloseUserMigrant"/>
    <Order ref="Orderdetails" @OpenCloseOrder="OpenCloseOrder"/>
  </div>
 </div>
</template>

<script>

import Services from './Services'
import UserMigrant from './UserMigrant'
import Order from './Order'


export default {
name: 'lkaburagent',
data:()=>
  ({
  isFormVisible:false
  }),
  components:
    {
    Services,
    UserMigrant,
    Order,
    
    },
  created()
    {
      // var token= sessionStorage.getItem("accessToken");
      // //console.log('token ',token);
      // this.ChekToken();
    },
  methods:
    {
    OpenCloseUserMigrant()
      {
      this.$refs.UserMigrantdetails.isFormVisible=!this.$refs.UserMigrantdetails.isFormVisible;
      this.isFormVisible=!this.isFormVisible;
      },
    OpenCloseServices()
      {
      this.$refs.Servicesdetails.isFormVisible=!this.$refs.Servicesdetails.isFormVisible;
      this.isFormVisible=!this.isFormVisible;
      },
    OpenCloseOrder()
      {
      this.$refs.Orderdetails.isFormVisible=!this.$refs.Orderdetails.isFormVisible;
      this.isFormVisible=!this.isFormVisible;
      },
    HideAll()
      {
       this.$refs.UserMigrantdetails.isFormVisible=false;
       this.$refs.Servicesdetails.isFormVisible=false;
       this.$refs.Orderdetails.isFormVisible=false;
       this.isFormVisible=false;
      }
    }
  }
</script>

<style scoped>

.DivLKabUrAgent
  {
  width:60%; 
  margin-top: 25px;
  margin-left: 25px;
  }

</style>