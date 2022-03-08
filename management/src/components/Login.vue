<template>
  <div class="LogBox">
    <el-form class="LogForm" ref="adminLogin" :model="admin">
      <el-form-item>
        <h1>QANbuy后台管理系统</h1>
      </el-form-item>
      <el-form-item>
        <el-input placeholder="管理员ID" v-model="admin.MID"></el-input>
      </el-form-item>
      <el-form-item>
        <el-input placeholder="管理员密码" v-model="admin.MPassword"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button
          :loading="btnLoading"
          @click="loginF()"
          style="width: 100%"
          type="primary"
          >登录</el-button
        >
      </el-form-item>
    </el-form>
  </div>
</template>

<style scoped>
.LogBox {
  width: 100%;
  height: 100%;
  position: fixed;
  display: flex;
  align-items: center;
  justify-content: center;
}
.LogBox form {
  min-width: 300px;
  background-color: rgba(255, 255, 255, 0.1);
  padding: 45px 30px;
  box-shadow: 0 20px 40px rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(8px);
  border-radius: 8px;
  border: 1px solid rgba(255, 255, 255, 0.2);
  border-right: 1px solid rgba(255, 255, 255, 0.1);
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

@media (max-device-width: 750px) {
  .LogBox form {
    padding-top: 50vh;
    height: 100%;
    width: 100% !important;
  }
}

.LogBox h1 {
  text-align: center;
  color: #fff;
}
</style>



<script lang="ts">
import axios from "axios";
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class Login extends Vue {
  private admin: any = {
    MID: "",
    MPassword: "",
  };
  private apiHost: string = "https://localhost:44398";
  private btnLoading: boolean = false;
  //登录
  public async loginF(): Promise<void> {
    this.btnLoading = true;
    let res: any = await Api.login(this.admin);
    this.btnLoading = false;
    if (res.status == 200) {
      this.$store.commit("GET_USER", res.data);
      this.$router.push("/");
    } else {
      this.admin.MPassword = "";
      this.$message.error("登录失败,账号或密码错误");
    }
  }
}
</script>