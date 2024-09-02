<template>
  <div v-if="isFormVisible" id="DivRegistration" class="DivRegistration">
    <h1><label>Registration</label></h1>
    <label>Наименование организации</label>
    <input id="InputName" type="text" v-model="name" placeholder="Введите имя организации" class="form-control" /><br>
    <label>ИНН</label>
    <input id="InputInn" type="text" v-model="inn" placeholder="Введите ИНН" class="form-control" /><br>
    <label>КПП</label>
    <input id="InputKpp" type="text" v-model="kpp" placeholder="Введите КПП" class="form-control" /><br>
    <label>Расчетный счет</label>
    <input id="InputRaschSch" type="text" v-model="rasch_sch" placeholder="Введите расчетный счет" class="form-control" /><br>
    <label>Банк расчетного счета</label>
    <input id="InputBankRaschSch" type="text" v-model="bank_rasch_sch" placeholder="Введите банк расчетного счета" class="form-control" /><br>
    <label>Корреспондентский счет</label>
    <input id="InputKorSch" type="text" v-model="kor_sch" placeholder="Введите Корреспондентский счет" class="form-control" /><br>
    <label>Банк корреспондентского счета</label>
    <input id="InputBankKorSch" type="text" v-model="bank_kor_sch" placeholder="Введите банк корреспондентского счета" class="form-control" /><br>
    <label>Телефон</label>
    <input id="InputPhone" type="text" v-model="phone" placeholder="Введите телефон" class="form-control" /><br>
    <label>Юридический адрес</label>
    <input id="InputUrAdress" type="text" v-model="ur_adress" placeholder="Введите юр. адрес" class="form-control" /><br>
    <label>ФИО</label>
    <input type="text" v-model="fio"  placeholder="Введите ФИО" class="form-control" /><br>
    <label>Должность</label>
    <input type="text" v-model="gradation_fio" placeholder="Введите должность" class="form-control" /><br>
    <label>Документ на право подписи</label>
    <input type="text" v-model="main_document" placeholder="Введите документ на право подписи" class="form-control" /><br>
    <label>БИК банка</label>
    <input type="text" v-model="bik_bank" placeholder="Введите БИК банка" class="form-control" /><br>
    <label>Почта</label>
    <input id="InputEmail" type="email" v-model="email" placeholder="Введите почту" class="form-control" /><br>
    <label>Пароль</label>
    <input id="InputPassword" type="password" v-model="password" placeholder="Введите пароль" class="form-control" /><br>

    <div v-if="errortext"><h2>{{errortext}}</h2></div>

    <section class="space-between-width100">
      <button  type="button" class="btn" @click="Registration">Регистрация</button>&nbsp;&nbsp;
      <button  type="button" class="btn" @click="$emit('CloseRegistration')" >Отмена</button>
    </section>
  </div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'Registration',
  data:()=>
    ({
        name:'',
        inn:'',
        kpp:'',
        rasch_sch:'',
        bank_rasch_sch:'',
        kor_sch:'',
        bank_kor_sch:'',
        phone:'',
        ur_adress:'',
        fio:'',
        gradation_fio:'',
        main_document:'',
        bik_bank:'',
        email:'',
        password:'',

        // name:'NewUrAgent',
        // inn:'7432342',
        // kpp:'643432',
        // rasch_sch:'5323423',
        // bank_rasch_sch:'96542',
        // kor_sch:'7653454',
        // bank_kor_sch:'54379234',
        // phone:'85456234',
        // ur_adress:'Ur adress',
        // fio:'Fio',
        // gradation_fio:'Presedent',
        // main_document:'Passport',
        // bik_bank:'5324234',
        // email:'NewUrAgent@mail.ru',
        // password:'NewUrAgent@mail.ru',
        
        isFormVisible:false,
        errortext:null
    }),
    methods:
      {
      Registration()
        {
        if(this.CheckData()==false)
          {
          this.errortext="Не все поля заполнены!!!";
          return;
          }
        else 
          this.errortext=null;
          console.log("Registration");


          axios.post(App.MainUrl+"UrAgentRegistration",
            //           {
            // Email: this.email,
            // Password: this.password
            // })
            {
            Name: this.name,
            INN:this.inn,
            KPP:this.kpp,
            Rasch_Sch:this.rasch_sch,
            Bank_Rasch_Sch:this.bank_rasch_sch,
            Kor_Sch:this.kor_sch,
            Bank_Kor_Sch:this.bank_kor_sch,
            Ur_Adress:this.ur_adress,
            Phone:this.phone,
            Bik_Bank:this.bik_bank,
            FIO:this.fio,
            GradationFIO:this.gradation_fio,
            Main_Document:this.main_document,
            Mail: this.email,
            Password: this.password
            })
          .then(response=>
            {
            sessionStorage.setItem("accessToken", response.data);
            this.$emit('CloseRegistration');
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

          if(this.email.length==0||this.password.length==0)flag=false;
          if(this.name.length==0||this.phone.length==0||this.fio.length==0)flag=false;
          if(this.inn.length==0||this.kpp.length==0||this.rasch_sch.length==0||this.rasch_sch.length==0||this.bank_rasch_sch.length==0||this.bank_kor_sch.length==0||this.bik_bank.length==0)flag=false;
          if(this.ur_adress.length==0||this.main_document.length==0||this.gradation_fio.length==0)flag=false;
          
          return flag;
          } 
      }
    }
</script>

<style scoped>

.DivRegistration
  {
  width:60%; 
  margin-top: 25px;
  margin-left: 25px;
  }

.btn{
  font-size: 20px;
  padding: 8px 16px;
  cursor: pointer;
  border: 2px solid #1e3b10;
  border-radius:6px;
}
</style>
