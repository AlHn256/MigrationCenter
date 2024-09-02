<template>
<div id="app">

  <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
  <div class="navbar-collapse collapse show" id="navbarColor01" >
      <ul class="navbar-nav " >
  
      <div v-if="UserMail"  >
        <section class="Mainsspace-between">

          <section>
 
            <div><B><h3><a class="nav-link" @click="OpenLKabFom" >Личный кабинет</a></h3></b></div>
            
            &nbsp;<h2>|</h2>&nbsp;
            
            <div><B><h3><a class="nav-link"  @click="OpenProfile">Профиль</a> </h3></b></div>
            &nbsp;<h2>|</h2>&nbsp;

            <div><B><h3><a class="nav-link"  @click="Logout">Выход</a> </h3></b></div>
             
          </section>

          <section>
            <h5>{{UserMail}}</h5>
          </section>
         
        </section>
        
      </div>

      <div v-else>
        <section class="Mainsflex-end"><h2>Услуги МЦ</h2></section>
      </div>

    </ul>
    </div>
   </nav>

<Login ref="Logindetails" @CloseLoginForm="CloseLoginForm"  @OpenRegistrationForm="OpenRegistrationForm" @LocalhostChange="LocalhostChange"/>
<Registration ref="Registrationdetails" @CloseRegistration="CloseRegistration"/>

<LKabAdmin ref="LKabAdmindetails"/>
<LKabAgent ref="LKabAgentdetails" @ShowAgentOrder="ShowAgentOrder" />
<LKabUrAgent ref="LKabUrAgentdetails" @HideAll="HideAll" />
<LKabBuhgalter ref="LKabBuhgalterdetails" />

<AgentProfile ref="Agentprofileformdetails" @CloseProfile="CloseProfile"/>
<UrAgentProfile ref="UrAgentprofileformdetails" @CloseProfile="CloseProfile" />
<BuhgalterProfile ref="BuhgalterProfileformdetails" @OpenCloseBuhgalterRegistration="CloseProfile" />

<OrderListAgent  
  :SelectedUrAgent="SelectedUrAgent" 
  :OrderList="OrderList"  
  ref="Orderlistagentdetails" 
  @ShowAgentListServices="ShowAgentListServices" 
  @Back="BackToLKabAgentForm"
/>
<ServicesListAgent 
  :OrderList="OrderList" 
  :SelectedOrderId="SelectedOrderId" 
  :SelectedOrderIndex="SelectedOrderIndex"
  :SelectedUrAgent="SelectedUrAgent" 
  :Token="Token" 
  :MigrantServiceList="MigrantServiceList" 
  ref="Serviceslistagentdetails" 
  @Back="BackToOrderListForm"
/>

<Error404 ref="Error404details"/>



</div>
</template>

<script>

import axios from 'axios';
import Login from './components/Login'
import Registration from './components/Registration'
import LKabAdmin from './components/LKabAdmin'
import LKabUrAgent from './components/LKabUrAgent'
import LKabAgent from './components/LKabAgent'
import LKabBuhgalter from './components/Buhgalter/LKabBuhgalter'
import AgentProfile from './components/ProfileAdminAgent'
import UrAgentProfile from './components/ProfileUrAgent'
import BuhgalterProfile from './components/Buhgalter/BuhgalterRegistration'
import OrderListAgent from './components/OrderListAgent'
import ServicesListAgent from './components/ServicesListAgent'
import Error404 from './components/Error404'


export default {
  name: 'App',
  //MainUrl: 'http://10.10.60.6:8101/',
  MainUrl: 'https://localhost:44363/',
  data:()=>
  ({
    //MainUrl: 'http://10.10.60.6:8101/',
    MainUrl: 'https://localhost:44363/',
    OrderList:[],
    MigrantServiceList:{},
    UserMail:null,
    UserRole:null,
    SelectedUrAgent:null,
    SelectedUrAgentId:null,
    SelectedOrderId:null,
    SelectedOrderIndex:null,
    Token:null,
    
  }),
  components:
    {
      Login,
      Registration,
      LKabAdmin,
      LKabUrAgent,
      LKabAgent,
      LKabBuhgalter,
      AgentProfile,
      UrAgentProfile,
      BuhgalterProfile,
      OrderListAgent,
      ServicesListAgent,
      Error404,
      
    },
  created()
    {
      //this.CheckLocalhost();
      //console.clear();
    },
  methods:
    {
      CheckLocalhost()
      {

      },
      Logout()
      {
        console.log('Logout');
        sessionStorage.clear();
        this.UserMail=null;
        this.UserRole=null;

        document.location.href = "/";
        this.HideAll();
        this.$refs.Logindetails.isFormVisible=true;
        },
      OpenRegistrationForm()
        {
        this.$refs.Logindetails.isFormVisible=false;
        this.$refs.Registrationdetails.isFormVisible=true;
        },
      CloseRegistration()
        {
        this.$refs.Registrationdetails.isFormVisible=false;
        if(this.ChekToken())this.OpenLKabFom();
        else this.$refs.Logindetails.isFormVisible=true;   
        },
      CloseLoginForm()
        {
        this.$refs.Logindetails.isFormVisible=false;
        if(this.ChekToken())this.OpenLKabFom();
        },
      ChekToken()
        {
        this.Token=sessionStorage.getItem("accessToken");
        if(this.Token==null)
          return false;
        else 
          return true;
        },
      OpenLKabFom()
        {
        if(this.Token==null)
          console.log('Main App GetRole AccessToken null');
        else
          {
          axios.get(this.MainUrl+"GetRole",
            {
            headers: 
              {
              Authorization: "Bearer " + this.Token
              }
            })
          .then(response=>
            {
            this.UserRole = response.data.role;
            this.UserMail = response.data.mail;

            this.HideAll();
            if(this.UserRole=='Admin')
                this.$refs.LKabAdmindetails.isFormVisible=true;
            if(this.UserRole=='Agent')
              this.$refs.LKabAgentdetails.isFormVisible=true;
            if(this.UserRole=='Jurperson')
              this.$refs.LKabUrAgentdetails.isFormVisible=true;
            if(this.UserRole=='Buhgalter')
              this.$refs.LKabBuhgalterdetails.isFormVisible=true;
            })
          .catch(e=>
            {
            this.errortext=e;
            })
          }
        },
      OpenProfile()
        {
        this.HideAll();
        console.log("this.UserRole", this.UserRole);
        if(this.UserRole=='Agent'||this.UserRole=='Admin')
          this.$refs.Agentprofileformdetails.isFormVisible=true;
        if(this.UserRole=='Jurperson')
          this.$refs.UrAgentprofileformdetails.isFormVisible=true;
        if(this.UserRole=='Buhgalter')
          this.$refs.BuhgalterProfileformdetails.isFormVisible=true;
        },
      CloseProfile()
        {
        this.$refs.Agentprofileformdetails.isFormVisible=false;
        this.$refs.UrAgentprofileformdetails.isFormVisible=false;
        this.OpenLKabFom();
        },
      ShowAgentOrder(id,SelectedUrAgent)
        {
        this.SelectedUrAgentId=id;
        this.SelectedUrAgent=SelectedUrAgent;
        this.GetOrderList(this.SelectedUrAgentId);
        },
      GetOrderList(id)
        {
        axios.get(this.MainUrl+"Order/OrdersByUser/"+id,
          {headers: {Authorization: "Bearer " + this.Token}})
        .then(response=>
          {
          this.OrderList=response.data;
          console.log('this.OrderList ',this.OrderList);
          this.$refs.LKabAgentdetails.isFormVisible=false;
          this.$refs.Serviceslistagentdetails.isFormVisible=false;
          this.$refs.Orderlistagentdetails.isFormVisible=!this.$refs.Orderlistagentdetails.isFormVisible;
          })
        .catch(e=>
          {
          this.errortext=e;
          })
        },
      BackToLKabAgentForm()
        {
        this.$refs.LKabAgentdetails.isFormVisible=true;
        this.$refs.Orderlistagentdetails.isFormVisible=!this.$refs.Orderlistagentdetails.isFormVisible;
        },
      ShowAgentListServices(id,index)
        {
        this.SelectedOrderId=id;
        this.SelectedOrderIndex=index;
        this.GetMigrantServiceList(this.SelectedOrderId);
        },
      GetMigrantServiceList(id)
        {
        if(this.Token==null)
          console.log('Main App GetRole AccessToken null');
        else
          {
          console.log('Get MigrantServiceList ',id);

          axios.get(this.MainUrl+"Order/"+id,
            {headers: {Authorization: "Bearer " + this.Token}})
          .then(response =>
            {
            this.MigrantServiceList = response.data;
              
            for(let i in this.MigrantServiceList.migrants)
              {
                this.MigrantServiceList.migrants[i].summ=0;
                for(let j in this.MigrantServiceList.migrants[i].servicesList)
                  this.MigrantServiceList.migrants[i].servicesList[j].isSeleckted=false;
              }

            console.log("this.MigrantServiceList ",this.MigrantServiceList);
            console.log("this.OrderList ",this.OrderList);
            this.$refs.Orderlistagentdetails.isFormVisible=!this.$refs.Orderlistagentdetails.isFormVisible;
            this.$refs.Serviceslistagentdetails.isFormVisible=!this.$refs.Serviceslistagentdetails.isFormVisible;
            })
          .catch(e =>
            {
            this.errortext=e;
            })
          }
        },
      BackToOrderListForm()
        {
        //this.GetMigrantServiceList(this.SelectedOrderId);
        this.GetOrderList(this.SelectedUrAgentId);
        //this.$refs.Orderlistagentdetails.isFormVisible=true;
        },

      LocalhostChange(newLocalHost)
      {
        console.log("App this.MainUrl old ",this.MainUrl);
        this.MainUrl = newLocalHost;
        console.log("App this.MainUrl new ",this.MainUrl);
      },
      OpenError404()
        {
        this.HideAll();  
        this.$refs.Error404details.isFormVisible=true;
        },
      OpenBlog()
        {
        this.HideAll();  
        this.$refs.Error404details.isFormVisible=true;
        },
      HideAll()
        {
        this.$refs.Logindetails.isFormVisible=false;
        this.$refs.Registrationdetails.isFormVisible=false;

        this.$refs.LKabAgentdetails.isFormVisible=false;
        this.$refs.LKabUrAgentdetails.HideAll();
        this.$refs.LKabAdmindetails.HideAll();
        this.$refs.LKabBuhgalterdetails.isFormVisible=false;
        //.isFormVisible=false;
        
        this.$refs.Serviceslistagentdetails.isFormVisible=false;
        this.$refs.Orderlistagentdetails.isFormVisible=false;
        this.$refs.Agentprofileformdetails.isFormVisible=false;
        this.$refs.UrAgentprofileformdetails.isFormVisible=false;
        this.$refs.BuhgalterProfileformdetails.isFormVisible=false;
        this.$refs.Error404details.isFormVisible=false;
        }
      }
    }

</script>

<style>
#app{
    margin: 30;
    font-family: Arial, Helvetica, sans-serif;
    }

section 
{
  display: flex;
}

.space-between-width60
{
  justify-content: space-between;
  margin-top: 25px;
  margin-left: 25px;
  width: 60%;
}

.space-between-width80
{
  justify-content: space-between;
  margin-top: 25px;
  margin-left: 25px;
  width: 80%;
}

.space-between-width90
{
  justify-content: space-between;
  margin-top: 25px;
  margin-left: 25px;
  width: 90%;
}

.space-between-width100
{
  justify-content: space-between;
}

.Mainsflex-center
{
  display: flex;
  align-items: center;
  justify-content: center;
}
.Mainsspace-center 
{
  justify-content: center;
}
.Mainsspace-between 
{
  justify-content: space-between;
}
.Mainsspace-around 
{
  justify-content: space-around;
}

.Mainsflex-start
{
  justify-content: flex-start;
}

.Mainsflex-end
{
  justify-content: flex-end;
} 

</style>