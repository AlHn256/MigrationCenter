<template>
  <div v-if="isFormVisible" >
    
    <section class="space-between-width60">
      <h1><label>Регистрация бухгалтера</label></h1>
    </section>

    <div  class="DivAgentRegistration">
        <label>Ф.И.О.</label>
        <input id="InputName" type="text" v-model="Name" placeholder="Введите ФИО" class="form-control" /><br>
        <label>Номер лицензии</label>
        <input id="InputLicens" type="text" v-model="LicensNumber" placeholder="Введите номер лицензии" class="form-control" /><br>
        <label>Время действия лицензии</label>
        <input id="InputStartDateLicens" type="date" v-model="StartDateLicens" class="form-control" /><br>
        <input id="InputEndDateLicens" type="date" v-model="EndDateLicens" class="form-control" /><br>
        <label>Квалификация</label>
        <input id="InputQualification" type="text" v-model="Qualification" placeholder="Введите квалификация" class="form-control" />
        <label>Телефон</label>
        <input id="InputPhone" type="text" v-model="Phone" placeholder="Введите телефон" class="form-control" />
        <label>Почта</label>
        <input id="InputEmail" type="email" v-model="Mail" placeholder="Введите почту" class="form-control" /><br>
        <label>Пароль</label>
        <input id="InputPassword" type="password" v-model="Password" placeholder="Введите пароль" class="form-control" /><br>
    </div>

    <section class="space-between-width60">
      <div v-if="errortext"><h2>{{errortext}}</h2></div>
    </section>

    <section class="space-between-width60">
        <button  type="button" class="btn" @click="$emit('OpenCloseBuhgalterRegistration')">Отмена</button>
        <button  type="button" class="btn" @click="Registration">Сохранить</button>
    </section>

 
  </div>
</template>

<script>

import axios from 'axios';
import App from '../../App.vue';

export default {
  name: 'AgentRegistration',

    data: function()
      {
        return{
        
        // Name:'',
        // LicensNumber:'',
        // StartDateLicens:'',
        // EndDateLicens:'',
        // Qualification:0,
        // Phone:'',
        // Mail:'',
        // password:'',

        Name:'GlavBuh',
        LicensNumber:'623412312',
        StartDateLicens:'2015-11-04',
        EndDateLicens:'2025-11-04',
        Qualification:4,
        Phone:'12912830198102',
        Mail:'GlavBuh@mail.com',
        Password:'GlavBuh@mail.com',
        
        isFormVisible:false,
        errortext:null
        }
      },
    components:
    {
        App
    },
    methods:
    {
    Registration()
      {
      if(!this.CheckData())return false;
      var data = 
          {
          Name:this.Name,
          LicensNumber:this.LicensNumber,
          StartDateLicens:this.StartDateLicens,
          EndDateLicens:this.EndDateLicens,
          Qualification:this.Qualification,
          Phone:this.Phone,
          Mail:this.Mail,
          Password:this.Password,
          }
        console.log("Registration data", data);

        axios.post(App.MainUrl+"BuhgalterRegistration",data)
        .then(response=>
            {
            this.errortext =data.Name+" зарегистрирован.";
            })
          .catch(e=>
            {
            this.errortext=e;
            if (e.response.status === 400)
              {
              //console.log('e.response.status 400 ');
              this.errortext = e.response.data;
              }
            })
        },
        CheckData()
        {
          var flag=true;
          if(this.Mail.length==0||this.Password.length==0)flag=false;
          if(flag==false)this.errortext="Не все поля заполнены!!!";
          return flag;
        }
    }
 }
</script>

<style scoped>

.DivAgentRegistration
{
    width:60%; 
    margin-top: 25px;
    margin-left: 25px;
}

.btn {
  color: rgb(14, 12, 12);
  font-size: 20px;
  background: #4aae68;
  border: 2px solid #1e3b10;
  border-radius:6px;
  padding: 8px 16px;
  cursor: pointer;
}

.btn-close {
  border: none;
  font-size: 20px;
  padding: 5px;
  cursor: pointer;
  font-weight: bold;
  color: #0aec28;
  background: transparent;
} 
</style>
