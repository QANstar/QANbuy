<template>
  <div>
    <el-table v-loading="loading" :data="userData" style="width: 100%">
        <el-table-column align="center" prop="headPic" label="头像">
          <template slot-scope="scope">
          <img class="userImg" :src="scope.row.headPic" />
        </template>
      </el-table-column>
      <el-table-column align="center" prop="id" label="用户ID">
      </el-table-column>
      <el-table-column align="center" prop="userName" label="用户名称">
      </el-table-column>
      <el-table-column align="center" prop="email" label="用户邮箱">
      </el-table-column>
      <el-table-column align="center" prop="address" label="用户地址">
      </el-table-column>
    </el-table>
  </div>
</template>

<style scoped>
.addBtn {
  float: right;
}
.userImg{
  max-width: 100px;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class UserManage extends Vue {
  private userData: any = [];
  private loading: boolean = true;

  public mounted(): void {
    this.getUserData();
  }

  //api获取用户信息
  public async getUserData(): Promise<void> {
    this.loading = true;
    let res: any = await Api.getUserData();
    this.loading = false;
    if (res.status == 200) {
      this.userData = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
}
</script>