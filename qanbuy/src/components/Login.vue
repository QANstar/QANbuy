<template>
  <div class="logForm">
    <el-form ref="loginForm" label-position="top" label-width="auto" :model="userLogin">
      <el-form-item label="邮箱" prop="email">
        <el-input v-model="userLogin.email"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input show-password v-model="userLogin.password"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button
          :loading="logInLoading"
          @click="logInBtn"
          style="float: right"
          type="primary"
          round
          >登录</el-button
        >
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Emit } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class Login extends Vue {
  private logInLoading:boolean = false;
  private userLogin: any = {
    email: "",
    password: "",
  };

  private rules: any = {
    email: [{ required: true, message: "请输入邮箱", trigger: "blur" }],
    password: [
      { required: true, message: "请输入密码", trigger: "blur" },
      { min: 1, max: 30, message: "长度在 1 到 30 个字符", trigger: "blur" },
    ],
  };

  //登录api
  public async login(userInfo: any): Promise<void> {
    this.logInLoading = true;
    let res: any = await Api.loginAPi(userInfo);
    this.logInLoading = false;
    if (res.status == 200) {
      this.$store.commit("GET_USER", res.data);
      this.$message.success("登录成功");
      this.closeModal("login");
      this.userLogin = {
        email: "",
        password: "",
      };
    } else {
      this.$message.error('登录失败');
    }
  }
  //关闭模态框事件
  @Emit("closeModal")
  public closeModal(data: string): string {
    return data;
  }
  //登录按钮
  public async logInBtn(): Promise<void> {
    (this.$refs["loginForm"] as any).validate((valid) => {
      if (valid) {
        this.login(this.userLogin);
      } else {
        this.$message({
          message: "参数输入不完整",
          type: "error",
        });
      }
    });
  }
}
</script>