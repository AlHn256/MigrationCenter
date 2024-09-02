<template>
  <div v-if="isFormVisible" >
    
    <section class="space-between-width60">
        <h1><label>Профиль агента</label></h1>
    </section>

    <div  class="DivAgentRegistration">
        <label>Фамилия</label>
        <input id="InputName" type="text" v-model="surname" placeholder="Введите фамилию" class="form-control" /><br>
        <label>Имя</label>
        <input id="InputInn" type="text" v-model="name" placeholder="Введите имя" class="form-control" /><br>
        <label>Отчество</label>
        <input id="InputKpp" type="text" v-model="patronymic" placeholder="Введите отчество" class="form-control" /><br>
        <label>Телефон</label>
        <input id="InputPhone" type="text" v-model="phone" placeholder="Введите телефон" class="form-control" /><br>
        <label>Почта</label>
        <input id="InputEmail" type="email" v-model="email" placeholder="Введите почту" class="form-control" /><br>
        <label>Пароль</label>
        <input id="InputPassword" type="password" v-model="password" placeholder="Введите пароль" class="form-control" /><br>
    </div>

    <section class="space-between-width60">
    <div v-if="errortext"><h2>{{errortext}}</h2></div>
    </section>

    <section class="space-between-width60">
        <button  type="button" class="btn" @click="$emit('CloseProfile')">Отмена</button>
        <button  type="button" class="btn" @click="Registration">Сохранить</button>
    </section>

 
  </div>
</template>

<script>

import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'AgentRegistration',

    data: function()
      {
        return{

        // name:'',
        // surname:'',
        // patronymic:'',
        // phone:'',
        // fio:'',
        // email:'',
        // password:'',

        name:'Бендер',
        surname:'Остап',
        patronymic:'',
        phone:'12912830198102',
        fio:'',
        email:'email3@email.com',
        password:'email2@email.com',
        
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

          this.name=this.surname+" "+ this.name+" "+this.patronymic;
          var data = 
            {
            Name: this.name,            
            Phone:this.phone,
            Mail: this.email,
            Password: this.password
            }
           console.log("Registration");
		       console.log('Pass ',data.Password);
           console.log('data.Mail ',data.Mail);
           console.log('data.FIO ',data.FIO);

          axios.post(App.MainUrl+"AgentRegistration",data,
            {
            headers:
              { 
              'Accept': 'application/json',
              'Content-Type': 'application/json'
              }
            })
          .then(response=>
            {
            console.log('Token ' ,sessionStorage.getItem("accessToken"));
            this.errortext ="Агент "+ data.FIO+" сохранен.";
            })
          .catch(e=>
            {
            this.errortext=e;

            if (e.response.status === 400)
              {
              console.log('e.response.status 400 ');
              this.errortext="Пользователь с такой почтой " + this.email + " уже зарегистрирован!!!!";
              }
            })
        },
        CheckData()
        {
          var flag=true;
          this.fio=this.surname+" "+ this.name+" "+this.patronymic;
          if(this.fio.length==0||this.email.length==0||this.password.length==0)flag=false;
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
