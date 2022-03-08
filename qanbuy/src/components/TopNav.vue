<template>
  <div class="navContent">
    <ul class="navItem">
      <li>
        <router-link to="/"><span class="logoTitle">QANbuy</span></router-link>
      </li>
      <li>
        <router-link to="/"><span>首页</span></router-link>
      </li>

      <li>
        <router-link to="/Filter"><span>过滤</span></router-link>
      </li>
    </ul>
    <div>
      <el-input
        placeholder="请输入内容"
        v-model="searchStr"
        class="input-with-select"
      >
        <el-button
          @click="searchButton"
          slot="append"
          icon="el-icon-search"
        ></el-button>
      </el-input>
    </div>
    <div class="avatar">
      <template v-if="this.$store.state.token">
        <el-dropdown @command="menu">
          <el-avatar fit="cover" :src="user.headPic"></el-avatar>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item command="user">个人中心</el-dropdown-item>
            <el-dropdown-item command="shoppingCar">购物车</el-dropdown-item>
            <el-dropdown-item command="order">订单</el-dropdown-item>
            <el-dropdown-item command="logout">注销</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </template>
      <template v-else>
        <el-dropdown @command="log">
          <el-avatar icon="el-icon-user-solid"></el-avatar>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item command="login">登录</el-dropdown-item>
            <el-dropdown-item command="signin">注册</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </template>
    </div>
  </div>
</template>

<style scoped>
.logoTitle {
  font-size: 20px;
  color: #00aeec;
}

.navItem li:hover span {
  display: block;
  -webkit-animation: shakeAnmi 0.5s;
}
</style>

<script lang="ts">
import { Component, Watch, Prop, Vue, Emit } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class TopNav extends Vue {
  private searchStr: string = "";
  private user: any = {
    id: 0,
    userName: "",
    headPic: "",
    address: "",
    email: "",
  };

  public mounted(): void {
    if(this.$store.state.token!=null)
    {
    this.getUserInfo();

    }
  }
  //api获取用户信息
  private async getUserInfo(): Promise<void> {
    let res: any = await Api.getUserInfo();
    if (res.status == 200) {
      this.user = res.data;
    } else {
      console.log(res);
    }
  }

  public searchButton(): void {
    this.$router.push("/Search/searchStr=" + this.searchStr);
  }

  @Emit("log")
  public log(command): string {
    if (command == "signin") {
      return "signin";
    }
  }

  public menu(command): void {
    if (command == "logout") {
      this.logout();
    } else if (command == "user") {
      this.$router.push("/UserCenter");
    } else if (command == "shoppingCar") {
      this.$router.push("/ShoppingCar");
    } else if (command == "order") {
      this.$router.push("/Order");
    }
  }

  public logout(): void {
    this.$confirm("确认退出登录?", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      this.$store.commit("LOGOUT");
      this.$message({
        type: "success",
        message: "退出成功!",
      });
    });
  }

  public get userToken() {
    return this.$store.state.token;
  }
  @Watch("userToken", { immediate: false })
  private onUserChange(searchStr: string) {
    if (this.$store.state.token != null) {
      this.getUserInfo();
    }
  }
}
</script>