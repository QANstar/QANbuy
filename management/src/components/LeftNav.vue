<template>
  <div class="leftNav">
    <h3>网上购物平台管理系统</h3>
    <el-menu
      :default-active="$route.path"
      class="el-menu-vertical-demo"
      @select="pathSelect"
    >
      <el-submenu index="1">
        <template slot="title">
          <i class="el-icon-menu"></i>
          <span>商品</span>
        </template>
        <el-menu-item index="/GoodTypeMangement">
          <i class="el-icon-document"></i>
          <span slot="title">商品类型管理</span>
        </el-menu-item>
        <el-menu-item index="/GoodsManage">
          <i class="el-icon-document"></i>
          <span slot="title">商品管理</span>
        </el-menu-item>
      </el-submenu>
      <el-submenu index="2">
        <template slot="title">
          <i class="el-icon-user"></i>
          <span>会员</span>
        </template>
        <el-menu-item index="/UserManage">
          <i class="el-icon-document"></i>
          <span slot="title">查看注册会员</span>
        </el-menu-item>
      </el-submenu>
      <el-submenu index="3">
        <template slot="title">
          <i class="el-icon-s-order"></i>
          <span>订单</span>
          <span></span>
        </template>
        <el-menu-item index="/OrderManage">
          <i class="el-icon-document"></i>
          <span slot="title">订单处理</span>
        </el-menu-item>
      </el-submenu>
      <el-submenu index="5">
        <template slot="title">
          <i class="el-icon-pie-chart"></i>
          <span>报表</span>
          <span></span>
        </template>
        <el-menu-item index="/Charts">
          <i class="el-icon-document"></i>
          <span slot="title">报表</span>
        </el-menu-item>
      </el-submenu>
    </el-menu>
  </div>
</template>

<style scoped>
h3{
    text-align: center;
    color: #409EFF;
}
.leftNav{
    width: 200px;
    position: fixed;
    top: 0;
    bottom: 0;
}

</style>

<script lang="ts">
import axios from "axios";
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class LeftNav extends Vue{
private pathSelect(key, keyPath):void{
  this.$router.push(key);
}
}
</script>