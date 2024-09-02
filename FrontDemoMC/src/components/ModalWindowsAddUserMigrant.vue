<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal" role="dialog" >
        <header class="modal-header" id="modalTitle" >
          <slot name="header">
            <h1>Add User Migrant</h1>
            <button type="button" class="btn-close" @click="close" aria-label="Close modal" >x</button>
          </slot>
        </header>
        <section class="modal-body" id="modalDescription">
          <slot name="body">
              
            <label>Имя</label>
            <input id="InputName" type="text" v-model="name" placeholder="Введите имя" class="form-control" /><br>
            <label>Фамилия</label>
            <input id="InputSurname" type="text" v-model="surname" placeholder="Введите фамилию" class="form-control" /><br>
            <label>Отчество</label>
            <input id="InputPatronymic" type="text" v-model="patronymic" placeholder="Введите отчество" class="form-control" /><br>
            <label>Паспорт серия</label>  
            <input id="InputPassSerri" type="text" v-model="passserri" placeholder="Введите номер паспорта" class="form-control" /><br>
            <label>Паспорт номер</label>
            <input id="InputPassNumber" type="text" v-model="passnumber" placeholder="Введите серию паспорта" class="form-control" /><br>
            <div v-show="errorvisible"><h2>{{errortext}}</h2></div>
          </slot>
        </section>
        <footer class="modal-footer">
          <slot name="footer">
            <button  type="button"  class="btn" @click="AddUserMigrant"  >Ок</button>
            <button  type="button"  class="btn" @click="close"  >Отмена</button>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
  import axios from 'axios';
  import App from '../App.vue';

  export default {
    name: 'modal',
    data:()=>({
        name:'',
        surname:'',
        patronymic:'',
        passnumber:null,
        passserri:null,
        
        errorvisible:false,
        errortext:'Error'
  }),
    components: 
    {
      App
    },
    methods: 
     {
      close() 
      {
        this.$emit('close');
      },
      AddUserMigrant() 
        {
        console.log("AddUserMigrant");
        var Token= sessionStorage.getItem("accessToken");
        //console.log("this.Token"+Token);
        axios.post(App.MainUrl+"Migrant",
          {
          Name:this.name,
          Surname:this.surname,
          Patronymic: this.patronymic,
          Passnumber: this.passnumber,
          Passserri: this.passserri
          },
          {
          headers: 
            {
            Authorization: "Bearer " + Token
            }
          })
        .then(response=>
          {
          console.log('response.data ' ,response.data);
          if(this.errorvisible==true)this.errorvisible= false;
          this.$emit('close');
          })
        .catch(e=>
          {
          this.errortext=e;
          this.errorvisible= true;
          })
        }
     }
  };
</script>

<style scoped>

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

#modal-backdrop {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #e7370a;
  margin-top: 60px;
}

.modal-body
{
  width:50%; 
  margin-left: 25px;
  flex-direction: column;
}

.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: #ffffff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
}

.modal-header,
.modal-footer {
  padding: 15px;
  display: flex;
  width:50%;
}

.modal-header {
  border-bottom: 1px solid #eeeeee;
  color: #4aae9b;
  justify-content: space-between;
}

.modal-footer {
  border-top: 1px solid #eeeeee;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.modal-fade-enter,
.modal-fade-leave-active {
  opacity: 0;
}

.modal-fade-enter-active,
.modal-fade-leave-active {
  transition: opacity 0.5s ease;
}

</style>