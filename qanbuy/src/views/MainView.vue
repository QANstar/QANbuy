<template>
    <el-container>
            <el-header class="topNav">
                <TopNav v-on:log="log"/>
            </el-header>
            <el-main class="mainContent">
                <router-view/>
            </el-main>
            <div class="logDialog"> 
                <el-dialog width="40%" title="注册" :visible.sync="signinDialog">
                    <Signin v-on:closeModal="closeModal"/>
                </el-dialog>
                <el-dialog width="40%" title="登录" :visible.sync="loginDialog">
                    <Login v-on:closeModal="closeModal"/>
                </el-dialog>
            </div>
    </el-container>
</template>


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import TopNav from '@/components/TopNav.vue'; 
import Signin from '@/components/Signin.vue';
import Login from '@/components/Login.vue';
import axios from 'axios';

@Component({
  components: {
    TopNav,
    Signin,
    Login
  },
})
export default class Home extends Vue {
  private signinDialog:boolean = false;
  private loginDialog:boolean = false;
  //打开模态框
  public log(data):void{
    if(data == "signin"){
      this.signinDialog = true;
    }else if(data = "login"){
      this.loginDialog = true;
    }
  }

  //关闭模态框
  public closeModal(data):void{
    if(data == "signin"){
      this.signinDialog = false;
    }else if(data = "login"){
      this.loginDialog = false;
    }
  }
}
</script>
