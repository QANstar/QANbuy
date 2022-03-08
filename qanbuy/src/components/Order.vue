<template>
  <div class="centerContent">
    <div class="tabs">
      <template>
        <el-tabs v-model="activeName" @tab-click="tabClick">
          <el-tab-pane
            :key="item.id"
            v-for="item in orderState"
            :label="item.state"
            :name="item.id.toString()"
          >
          </el-tab-pane>
        </el-tabs>
      </template>
    </div>
    <div v-loading="loading" class="orderList">
      <div v-for="item in orderList" :key="item.id" class="orderItem">
        <div class="orderTitle">
          <div>购买时间：{{ item.payDate.substr(0, 10) }}</div>
          <div>订单号：{{ item.id }}</div>
        </div>
        <div class="orderContent">
          <div>
            <img :src="item.goodPic" alt="" />
          </div>
          <div class="goodName">{{ item.goodName }}</div>
          <div>购买数量:{{ item.num }}</div>
          <div>实际付款:￥{{ item.payment }}</div>
          <el-button plain @click="showOrderBtn(item.id)">查看订单</el-button>
          <el-button
            v-if="activeName == '2'"
            :loading="suerBtnLoading"
            @click="sureGetBtn(item.id)"
            type="warning"
            plain
            >确认收货</el-button
          >
        </div>
      </div>
      <div class="pagination">
        <el-pagination
          layout="prev, pager, next"
          @current-change="handleCurrentChange"
          :page-size="size"
          :current-page="page"
          :total="pageTotal"
        >
        </el-pagination>
      </div>
      <div style="height: 50px"></div>
    </div>
  </div>
</template>

<style scoped>
.orderItem {
  border-radius: 5px;
  margin-top: 20px;
  border: 1px solid #e5e9ef;
}
.orderList {
  padding: 20px 60px;
  border-radius: 20px;
  background-color: #fff;
}
.orderTitle {
  display: flex;
  height: 42px;
  align-items: center;
  background-color: #f4f5f7;
  color: #99a2aa;
}
.orderTitle div {
  margin-left: 40px;
  font-size: 12px;
}
.orderContent {
  padding: 20px;
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  justify-content: space-around;
}
.orderContent div {
  margin: 0 20px;
  font-size: 12px;
  color: #222;
}
.orderContent img {
  width: 100px;
  border-radius: 5px;
}
.orderContent .goodName {
  line-height: 20px;
  font-size: 18px;
  color: #222;
}
.pagination {
  width: 100%;
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class Order extends Vue {
  private activeName: string = "1";
  private orderState: any = [];
  private orderList: any = [];
  private page: number = 1;
  private size: number = 4;
  private loading: boolean = false;
  private pageTotal: number = 1;
  private suerBtnLoading: boolean = false;

  public mounted(): void {
    this.getOrderState();
    this.getOrderByState(
      this.page,
      this.size,
      Number.parseInt(this.activeName)
    );
    this.getOrderNum(Number.parseInt(this.activeName));
  }

  //api获取订单状态
  private async getOrderState(): Promise<void> {
    let res: any = await Api.getOrderState();
    if (res.status == 200) {
      this.orderState = res.data;
    } else {
      console.log(res);
    }
  }
  //api获取订单数量
  private async getOrderNum(stateId: number): Promise<void> {
    let res: any = await Api.getOrderNum(stateId);
    if (res.status == 200) {
      this.pageTotal = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api确认收获
  private async sureGet(orderId: number): Promise<void> {
    this.suerBtnLoading = true;
    let res: any = await Api.suerGet(orderId);
    this.suerBtnLoading = false;
    if (res.status == 200) {
      this.$message.success("确认成功");
      this.getOrderByState(
      this.page,
      this.size,
      Number.parseInt(this.activeName)
    );
    } else {
      this.$message.error("确认失败");
      console.log(res);
    }
  }
  //api获取订单
  private async getOrderByState(
    page: number,
    size: number,
    state: number
  ): Promise<void> {
    this.loading = true;
    let res: any = await Api.getOrderByState(page, size, state);
    this.loading = false;
    if (res.status == 200) {
      this.orderList = res.data;
    } else {
      console.log(res);
    }
  }
  //确认收获
  public sureGetBtn(orderId: number): void {
    this.sureGet(orderId);
  }
  //查看订单标签
  public showOrderBtn(orderId:number):void{
      this.$router.push("/OrderView/orderID="+orderId)
  }
  //点击标签
  public tabClick(tab): void {
    this.getOrderByState(
      this.page,
      this.size,
      Number.parseInt(tab._props.name)
    );
    this.getOrderNum(Number.parseInt(tab._props.name));
    this.page = 1;
  }
  //分页
  public handleCurrentChange(page: number): void {
    this.page = page;
    this.getOrderByState(
      this.page,
      this.size,
      Number.parseInt(this.activeName)
    );
  }
}
</script>