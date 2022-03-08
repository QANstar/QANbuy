<template>
  <div class="logForm">
    <el-form
      label-position="top"
      label-width="auto"
      :model="userSignin"
      :rules="rules"
      ref="userSignUp"
    >
      <el-form-item label="用户名" prop="userName">
        <el-input v-model="userSignin.userName"></el-input>
      </el-form-item>
      <el-form-item label="邮箱" prop="email">
        <el-input v-model="userSignin.email"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input show-password v-model="userSignin.password"></el-input>
      </el-form-item>
      <el-form-item label="确认密码" prop="surePassword">
        <el-input show-password v-model="userSignin.surePassword"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button
          :loading="signUpLoading"
          @click="signUpBtn"
          style="float: right"
          type="primary"
          round
          >注册</el-button
        >
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Emit } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class Signin extends Vue {
  private signUpLoading: boolean = false;
  private userSignin: any = {
    userName: "",
    email: "",
    password: "",
    surePassword: "",
  };
  private rules: any = {
    userName: [
      { required: true, message: "请输入用户名", trigger: "blur" },
      { min: 1, max: 20, message: "长度在 1 到 20 个字符", trigger: "blur" },
    ],
    email: [{ required: true, message: "请输入邮箱", trigger: "blur" }],
    password: [
      { required: true, message: "请输入密码", trigger: "blur" },
      { min: 1, max: 30, message: "长度在 1 到 30 个字符", trigger: "blur" },
    ],
    surePassword: [
      { required: true, message: "请输入确认密码", trigger: "blur" },
      { min: 1, max: 30, message: "长度在 1 到 30 个字符", trigger: "blur" },
    ],
  };

  //注册api
  public async signUp(userInfo: any): Promise<void> {
    this.signUpLoading = true;
    let res: any = await Api.signUpApi(userInfo);
    this.signUpLoading = false;
    if (res.status == 200) {
      this.$message.success("注册成功");
      this.closeModal("signin");
      this.userSignin = {
        userName: "",
        email: "",
        password: "",
        surePassword: "",
      };
    } else {
      this.$message.error(res);
    }
  }
  //关闭模态框事件
  @Emit("closeModal")
  public closeModal(data:string):string{return data}
  //注册按钮
  public async signUpBtn(): Promise<void> {
    (this.$refs["userSignUp"] as any).validate((valid) => {
      if (valid) {
        if (this.userSignin.password != this.userSignin.surePassword) {
          this.$message({
            message: "两次输入密码不相等",
            type: "error",
          });
        } else {
          this.signUp(this.userSignin);
        }
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