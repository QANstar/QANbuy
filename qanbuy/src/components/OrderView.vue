<template>
  <div v-loading="loading" class="orderView centerContent">
      <div><el-tag style="margin:20px 0" size="medium ">{{orderInfo.state}}</el-tag></div>
    <div>
      <el-descriptions direction="vertical" :column="4" border>
        <el-descriptions-item label="商品图片"
          ><img :src="orderInfo.goodPic" alt=""
        /></el-descriptions-item>
        <el-descriptions-item label="商品名称">{{
          orderInfo.goodName
        }}</el-descriptions-item>
        <el-descriptions-item label="商品单价">{{
          orderInfo.goodPrice
        }}</el-descriptions-item>
        <el-descriptions-item label="购买数量">
          {{ orderInfo.num }}
        </el-descriptions-item>
      </el-descriptions>
    </div>
    <div class="orderInfo">
        <div><span>订单编号：</span>{{ orderInfo.id }}</div>
      <div><span>收货地址：</span>{{ orderInfo.harvestAddress }}</div>
      <div><span>发货地址：</span>{{ orderInfo.shipAddress }}</div>
      <div><span>运费：</span>{{ orderInfo.postage }}</div>
    </div>
    <div class="orderInfo">
      <div><span>购买日期：</span>{{ date(orderInfo.payDate) }}</div>
      <div><span>发货日期：</span>{{ date(orderInfo.sendDate) }}</div>
      <div><span>完成日期：</span>{{ date(orderInfo.harvestDate) }}</div>
    </div>
    <el-divider></el-divider>
    <div class="allPayment">实际付款:￥{{ orderInfo.payment }}</div>
  </div>
</template>

<style scoped>
.orderView{
    background-color: #fff;
    padding: 40px;
}
.orderView div{
}
.orderView img {
  max-width: 100px;
}

.orderInfo {
  padding: 20px;
  border: 1px solid #e7e7e7;
  margin: 10px;
}
.orderInfo div {
  margin: 10px;
}
.allPayment {
  text-align: right;
  line-height: 22px;
  font-size: 22px;
  color: #ff66a2;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class orderView extends Vue {
  private loading: boolean = false;
  private orderID: number = Number.parseInt(this.$route.params.orderID);
  private orderInfo: any = {};

  public mounted(): void {
    this.getOrderById(this.orderID);
  }
  //api根据id获取订单
  private async getOrderById(orderId: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.getOrderById(orderId);
    this.loading = false;
    if (res.status == 200) {
      this.orderInfo = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //返回字符串处理时间
  public date(date:string):string{
    if(date==null){
      return null;
    }else{
      return date.substr(0,10);
    }
  }
}
</script>