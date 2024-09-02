<template>
<div>
  <div v-if="isFormVisible" class="DivLKabAdmin">
    <!-- <section class="space-between-width90"><h2><a href="#/Error404">Пользователи</a></h2></section> -->
    <section class="space-between-width90"><h2><a v-on:click="OpenCloseMigrants()">Мигранты</a></h2></section>
    <!-- <section class="space-between-width90"><h2><a href="#/Error404">Сбросить пароль</a></h2></section> -->
    <section class="space-between-width90"><h3><a v-on:click="OpenCloseBuhgalterRegistration()">Регистрация бухгалтера</a></h3></section>
  </div>
  
  <AllMigrants ref="AllMigrantsdetails" @OpenCloseMigrants="OpenCloseMigrants"/>

  <BuhgalterRegistration ref="BuhgalterRegistrationdetails" @OpenCloseBuhgalterRegistration="OpenCloseBuhgalterRegistration"/>

</div>
</template>

<script>
import BuhgalterRegistration from './Buhgalter/BuhgalterRegistration'
import AllMigrants from './AllMigrants'

export default {
  name: 'lkabadmin',
   data:()=>
    ({
    isFormVisible:false,
    }),
  components:
    {
      BuhgalterRegistration,
      AllMigrants
    },
    methods:
    {
    OpenCloseMigrants()
      {
      this.$refs.AllMigrantsdetails.isFormVisible=!this.$refs.AllMigrantsdetails.isFormVisible;
      this.isFormVisible=!this.isFormVisible;
      },
    OpenCloseBuhgalterRegistration()
      {
      this.$refs.BuhgalterRegistrationdetails.isFormVisible=!this.$refs.BuhgalterRegistrationdetails.isFormVisible;
      this.isFormVisible=!this.isFormVisible;
      },
    HideAll()
      {
      this.$refs.BuhgalterRegistrationdetails.isFormVisible=false;
      this.$refs.AllMigrantsdetails.isFormVisible=false;
      this.isFormVisible=false;
      }
    }
  }

</script>

<style scoped>
.DivLKabAdmin
  {
  margin-top: 25px;
  margin-left: 25px;
  }
</style>