<template>
  <Div v-if="isFormVisible">
    <section class="space-between-width60">
      <h1><label>Профиль Юр. лица</label></h1>
    </section>

   <div  class="DivUrAgentRegistration">
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
            <label>БИК банка</label>
            <input type="text" v-model="bik_bank" placeholder="Введите БИК банка" class="form-control" /><br>
            <label>Почта</label>
            <input id="InputEmail" type="email" v-model="email" placeholder="Введите почту" class="form-control" /><br>
            <label>Пароль</label>
            <input id="InputPassword" type="password" v-model="password" placeholder="Введите пароль" class="form-control" /><br>
    </div>

    <section class="space-between-width60">
        <button  type="button" class="btn" @click="close">Отмена</button>
        <button  type="button" class="btn" @click="Registration">Сохранить</button>
    </section>


</div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default 
  {
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
    
    errortext:null,
    isFormVisible:false
    }),
  methods:
    {
    Registration()
        {
          var data = 
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
            }
           console.log("Registration");

		      // console.log('Pass ',data.Password);
          // console.log('data.Mail ',data.Mail);
          // console.log('data.FIO ',data.FIO);
          axios.post(App.MainUrl+"UrAgentRegistration",data,
            {
            headers:
              { 
              'Accept': 'application/json',
              'Content-Type': 'application/json'
              }
            })
          .then(response=>
            {
            sessionStorage.setItem("accessToken", response.data);
            console.log('Token ' ,sessionStorage.getItem("accessToken"));
            this.$emit('CloseProfile');
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
    close()
      {
      this.$emit('CloseProfile');
      }
    }
  }
</script>

<style scoped>

.DivUrAgentRegistration
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
