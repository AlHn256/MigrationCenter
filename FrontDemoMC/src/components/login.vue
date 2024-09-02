<template>
  <div v-if="isFormVisible" id ="loginForm"  class="DivLogin">
        <h2>Вход на сайт </h2>
        <label>Email (login)</label><br>
        <input type="login" id="Login" v-model="email" placeholder="Введите Email (login)"  v-on:keyup.13="Login" @input="SetLogin"/> <br>
        <label>Пароль</label><br>
        <input type="password" id="password" v-model="password" placeholder="Введите пароль"  v-on:keyup.13="Login" @input="SetPass"/><br>
        
        <br>
        <input class="btn" @click="Login"  type=submit value="Вход">
        <input class="btn" @click="$emit('OpenRegistrationForm')" type=submit value="Регистрация">

        <br><br><div v-if="errortext"><h2>{{errortext}}</h2></div>
    </div>
</template>

<script>
import axios from 'axios';
import App from '../App.vue';

export default {
  name: 'login',
  data:()=>
  ({
    // email:'',
    // password:'',

    email:"test@mail.ru",
    password:"test3",
    //email:"admin",
    //password:"admin",
    //email:"email@email.com",
    //password:"email@email.com",

     //email:"GlavBuh@mail.com",
     //password:"GlavBuh@mail.com",
    // email:"Buhgalter@mail.com",
    // password:"Buhgalter@mail.com",
    Token:null,
    Role:null,
    isFormVisible:true,

    errortext: null,
  }),
  components:
  {
    App
  },
    created()
    {
      this.CheckLocalhost();
      //console.clear();
    },
  methods:
      {
      CheckLocalhost()
        {
          
        },
      SetLogin: function(event)
        {
          this.login = event.target.value;
        },
      SetPass: function(event)
        {
          this.password = event.target.value;
        },
      GetString()
        {
          var url = App.MainUrl+"getstring";
          console.log('Login ',this.email);
          console.log('Url ',url);
          axios.get(url)
          .then(response=>
            {
            console.log('response.data  ',response.data);
            this.errortext=response.data;
            })
          .catch(e =>
            {
            console.log('error ',e);
            this.errortext=e;
            })
// 
        },
      Login()
        {
          // localhost
          // http://localhost:8101/
          // http://localhost:8090/
          // https://localhost:44363/
          // test@mail.ru
          if(this.email== 'localhost')
          {
            App.MainUrl=this.password;
            this.$emit('LocalhostChange',this.password);
          }
          else
          {
          console.log('Login ',this.email);
          console.log('Url ',App.MainUrl+"login");
          var data = 
            {
            email: this.email,
            password: this.password
            }
          axios.post(App.MainUrl+"login",data)
          .then(response=>
            {
            this.errortext=null;
            sessionStorage.setItem("accessToken", response.data);
            this.$emit('CloseLoginForm');
            })
          .catch(e =>
            {
            this.errortext=e;
            if (e.response != undefined||e.response != null)
              {
              if (e.response.status === 401 || e.response.status === 400)
                this.errortext = e.response.data;
              }
            })
          }
        },
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

.DivLogin
{
    width:60%; 
    margin-top: 25px;
    margin-left: 25px;
}
</style>