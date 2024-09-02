<template>
<div v-if="isOrderForm2Visibl" >

          <div class="divSection">
            <div class="divTableHead"><B><h3>Создание новой заявки</h3></b></div>
          </div>
          <div class="divSection">
            <B><h3>Всего: {{TotalSummOfServices}}</h3></b>
          </div>


      <div class="divSection">
       <section class="space-between">
      <div><h5><a class="btn" @click="PreviousMigrant">Предыдущий</a></h5></div>

      <div>
        <h2><select v-model="SelectedMigrant" class="selector">
          <option  v-for="(migrant,index) in migrants" :key="index" v-bind:value="index" v-if="migrant.isSeleckted">
          {{ migrant.name }}
          </option>
        </select></h2>
      </div>

      <div><a class="btn" @click="NextMigrant">Следующий</a></div>
       </section>
      </div>

          <div class="divTable unstyledTable">

            <div class="divTableHeading">
              <div class="divTableRow">
                <div class="divTableHead">Id</div>
                <div class="divTableCell"><input type="checkbox" v-on:change="toggleAll()" v-model="isCheckedAll"></div>
                <div class="divTableHead">Service</div>
                <div class="divTableCell">Cost</div>
              </div>
            </div>

            <div class="divTableBody">
              <div class="divTableRow" v-for="(service,index) in migrants[SelectedMigrant].services" :key="index">
                <div class="divTableCell">{{service.id}}</div>
                <div class="divTableCell"><input type="checkbox" v-on:change="ServiceToggle(service)" v-bind:checked="service.isSeleckted" ></div>

                <div class="divTableCell">
                      <Div v-if="service.isSeleckted">
                        {{service.name}}
                      </div>
                      <div v-else class="spandel"> 
                         {{service.name}}
                      </div>
                </div>


                <div class="divTableCell">
                      <Div v-if="service.isSeleckted">
                        {{service.cost}}
                      </div>
                      <div v-else class="spandel"> 
                        {{service.cost}}
                      </div>
                </div>
              </div>

              <div class="divTableRow">
                <div class="divTableCell"></div>
                <div class="divTableCell"></div>
                <div class="divTableCell"><h3>Итого: </h3></div>
                <div class="divTableCellFixedWidth20ps"><h3>{{TotalPrice}}</h3></div>
              </div>

            </div>

          </div>

          <div v-if="error">
            <h1>Error: {{error}} </h1>
          </div>

            <div class="divSection">
              <section class="space-between">
                <button type="button" class="btn" @click="Back" >Назад</button>
                <button type="button" class="btn" @click="Next" >Далее</button>
              </section>
            </div>



        </div>
</template>

<script>


export default {
  name: 'NewOrder',
   props:
            {
            NewOrderNumber:Number,
            services:
              {
              type: Array,
              default: function()
                {
                return []
                }
              },
            migrants:
              {
              type: Array,
              default: function()
                {
                return []
                }
              },
            TotalSummOfServices:Number
            },
  
 data: function()
            {
            return{
                  SelectedMigrant:0,
                  isOrderForm2Visibl: false,
                  isCheckedAll: false,
                  error:"",
                  TotalPrice:0,
                  massToSave:[],
                  selected: 9
                  }
            },
            watch:
		        {
			      isOrderForm2Visibl: function()
        		  {
              this.Reloade();
              },
            SelectedMigrant: function()
              {
              this.CheckAllToggle();
              this.RecountTotalPrice();
              }  
            },
            methods:
            {
            Reloade()
              {
              if(this.migrants[this.SelectedMigrant].isSeleckted==false)
                this.NextMigrant();
              this.$forceUpdate();
              },
            ServiceToggle(service)
              {
              service.isSeleckted = !service.isSeleckted;
              this.RecountTotalPrice();
              this.CheckAllToggle();
              },
            RecountTotalPrice()
              {
              this.TotalPrice=0;
              for (let i in this.migrants[this.SelectedMigrant].services)
                {
                if(this.migrants[this.SelectedMigrant].services[i].isSeleckted)
                  this.TotalPrice+=this.migrants[this.SelectedMigrant].services[i].cost;
                }
              },
            CheckMigrantIsSelect()
            {
              this.error=null;
              if(this.migrants)
              {
                for (let i in this.migrants)
                  {
                    if(this.migrants[i].isSeleckted==true)
                      return true;
                  }
              }
              this.error=" Ни один из мигрантов не выбран!";
              return false;
            },

            CheckServicesIsSelect()
            {
              this.error=null;
              if(this.migrants)
              {
                for (let i in this.migrants)
                  {
                    if(this.migrants[i].isSeleckted==true)
                    {
                      for (let j in this.migrants[i].services)
                        {
                          if( this.migrants[i].services[j].isSeleckted==true)
                            return true;
                        }
                    }
                  }
              }
              this.error=" Ни один из сервисов не выбран!";
              return false;
            },
            NextMigrant()
              {
                if(this.CheckMigrantIsSelect())
                {
                do
                  {
                    this.SelectedMigrant++;
                    if(this.SelectedMigrant>this.migrants.length-1)this.SelectedMigrant=0;
                  }while(this.migrants[this.SelectedMigrant].isSeleckted!=true)
                }
              },
            PreviousMigrant()
              {
              if(this.CheckMigrantIsSelect())
                {
                do
                  {
                    this.SelectedMigrant--;
                    if(this.SelectedMigrant<0)this.SelectedMigrant=this.migrants.length-1;
                  }while(this.migrants[this.SelectedMigrant].isSeleckted!=true)
                }
              },
            DeliteMigrantWithoutService()
              {
                for (let i in this.migrants)
                  {
                    if(this.migrants[i].isSeleckted==true)
                    {
                      var flag=false;

                      for (let j in this.migrants[i].services)
                        {
                          if( this.migrants[i].services[j].isSeleckted==true)
                          {
                            flag=true;
                            break;
                          }
                        }
                      if(flag==false)this.migrants[i].isSeleckted=flag;
                    }
                  }
              },
            Next()
              {
                if(this.CheckServicesIsSelect())
                {
                  this.DeliteMigrantWithoutService();
                  this.$emit('Next');
                }
              },
            Back()
              {
                this.$emit('Back');
              },
            CheckAllToggle()
              {
              this.isCheckedAll=true;
              for (let j in this.migrants[this.SelectedMigrant].services)
                {
                if(this.migrants[this.SelectedMigrant].services[j].isSeleckted==false)
                  {
                  this.isCheckedAll=false;
                  break;
                  }
                }
              },
            toggleAll()
              {
              for (let j in this.migrants[this.SelectedMigrant].services)
                this.migrants[this.SelectedMigrant].services[j].isSeleckted=this.isCheckedAll;
              this.RecountTotalPrice();
              }
            }
}
</script>

<style>
</style>