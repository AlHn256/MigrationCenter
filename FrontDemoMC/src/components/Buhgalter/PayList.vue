<template>
  <transition v-if="isFormVisibl" name="modal-fade">
    <div  class="modal-backdrop">
      <div class="modal" role="dialog" >
        <header class="modal-header" id="modalTitle" >
          <slot name="header">
            <h1>{{SelectedUrAgentName}}</h1>
            <button type="button" class="btn-close" @click="$emit('closePayList')" aria-label="Close modal" >x</button>
          </slot>

          
        </header>
        <section class="modal-body" id="modalDescription">
          <slot name="body">
            <h5>INN\KPP : {{SelectedINN}}\{{SelectedKPP}}</h5><br><br><br>
            <label>Сумма</label>  
            <input id="InputAmaunt" type="number" v-model.number="Amaunt" placeholder="Введите сумму" class="form-control" /><br>
            <label>Обоснование</label>
            <input id="InputPurposeOfPayment" type="text" v-model="PurposeOfPayment" placeholder="Введите обоснование" class="form-control" /><br>
            <div v-if="errortext"><h2>{{errortext}}</h2></div>
          </slot>
        </section>
        <footer class="modal-footer">
          <slot name="footer">
            <button  type="button"  class="btn" @click="$emit('closePayList')"  >Назад</button>
            <button  type="button"  class="btn" @click="Payment"  >Сохранить</button>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
  import axios from 'axios';
  import App from '../../App.vue';

  export default {
    name: 'ModalPayList',
    data:()=>({
        PurposeOfPayment:'FirstPay',
        Amaunt:-10,
        
        isFormVisibl:false,
        errortext:null
    }),
    watch:
		  {
		  isFormVisibl()
    	  {
        if(this.isFormVisibl)
          {
          this.errortext = null;
          this.PurposeOfPayment = '';
          this.Amaunt = null;
          }
        }  
      },
    props:
      {
      SelectedUrAgentName:String,
      default:'',
      SelectedINN:String,
      SelectedKPP:String,
      SelectedId:Number,
      },
    components: 
      {
      App
      },
    methods: 
      {
      Payment()
        {
        console.log('Pay');
        var Token = sessionStorage.getItem("accessToken");
        if(this.Amaunt==null||this.Amaunt==0||Token.length==0)
          {
            if(this.Amaunt==null||this.Amaunt==0)this.errortext='Поле сумма не заполненно!';
            if(Token.length==0)this.errortext='Token не найден!';
          }
        else
          {
          axios.post(App.MainUrl+"Pay",
            {
            recipientId: this.SelectedId,
            amount: this.Amaunt,
            purposeOfPayment: this.PurposeOfPayment
            },
            {
            headers: 
              {
              Authorization: "Bearer " + Token
              }
             })
            .then(response=>
            {
            //console.log('response.data', response.data);
            this.errortext = " Зачисленна сумма "+response.data.amount;
            })
          .catch(e=>
            {
            this.errortext=e;
            if (e.response.status === 400)
              this.errortext='На счету не достаточно средств!';
            })
          }
        }
      // closePayList()
      //   {
      //   console.log('closePayList');
      //   this.$emit('closePayList');
      //   },
      // close()
      //   {

      //   this.isFormVisibl=false;
      //   }
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