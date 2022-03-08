<template>
  <div>
    <el-tabs v-model="activeName" @tab-click="tabClick">
      <el-tab-pane
        v-for="item in statusData"
        :key="item.id"
        :label="item.state"
        :name="String(item.id)"
      >
      </el-tab-pane>
    </el-tabs>
    <el-table v-loading="loading" :data="orderData" style="width: 100%">
      <el-table-column align="center" prop="id" label="订单ID">
      </el-table-column>
      <el-table-column align="center" prop="goodPic" label="商品图片">
        <template slot-scope="scope">
          <img style="max-width:100px" :src="scope.row.goodPic" alt="">
        </template>
      </el-table-column>
      <el-table-column align="center" prop="goodID" label="商品ID">
      </el-table-column>
      <el-table-column align="center" prop="goodName" label="商品名称">
      </el-table-column>
       
      <el-table-column align="center" prop="userID" label="购买用户ID">
      </el-table-column>
      <el-table-column align="center" prop="num" label="购买数量">
      </el-table-column>
      <el-table-column align="center" prop="payment" label="实际付款">
      </el-table-column>
      <el-table-column align="center" prop="shipAddress" label="发货地址">
      </el-table-column>
      <el-table-column align="center" prop="harvestAddress" label="收获地址">
      </el-table-column>
      <el-table-column align="center" prop="payDate" label="付款时间">
        <template slot-scope="scope">
          {{date(scope.row.payDate)}}
        </template>
      </el-table-column>
      <el-table-column align="center" prop="sendDate" label="发货时间">
        <template slot-scope="scope">
          {{date(scope.row.sendDate)}}
        </template>
      </el-table-column>
      <el-table-column align="center" prop="harvestDate" label="收货时间">
        <template slot-scope="scope">
          {{date(scope.row.harvestDate)}}
        </template>
      </el-table-column>
      <el-table-column v-if="activeName == '1'" align="center" label="操作">
        <template slot-scope="scope">
            <el-popconfirm title="确定发货？" @confirm="shipButton(scope.row)">
            <el-button
              type="primary"
              size="small"
              slot="reference"
            >发货</el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>
    <div class="pagination">
      <el-pagination layout="prev, pager, next" @current-change="handleCurrentChange" :page-size="size" :current-page="page" :total="totalNum"> </el-pagination>
    </div>
  </div>
</template>

<style scoped>
.addBtn {
  float: right;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class OrderManage extends Vue {
  private orderData: any = [];
  private statusData: any = [];
  private loading: boolean = true;
  private activeName: string = "1";
  private page:number = 1;
  private size:number = 5;
  private totalNum:number = 1;

  public mounted(): void {
    this.getOrderNum(Number.parseInt(this.activeName))
    this.getOrderData(this.page,this.size,Number.parseInt(this.activeName));
    this.getOrderStatus();
  }

  //api获取订单信息
  public async getOrderData(page:number,size:number,stateID:number): Promise<void> {
    this.loading = true;
    let res: any = await Api.getOrderData(page,size,stateID);
    this.loading = false;
    if (res.status == 200) {
      console.log(res.data);
      this.orderData = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api获取订单数量
  private async getOrderNum(stateId: number): Promise<void> {
    let res: any = await Api.getOrderNum(stateId);
    if (res.status == 200) {
      this.totalNum = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api获取状态信息
  public async getOrderStatus(): Promise<void> {
    let res: any = await Api.getOrderStatus();
    if (res.status == 200) {
      this.statusData = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api发货
  public async shipApi(id: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.ship(id);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("发货成功");
    } else {
      this.$message.error("发货失败");
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
  //标签点击
  public async tabClick(tab): Promise<void> {
    this.page=1;
    this.getOrderNum(Number.parseInt(this.activeName))
    this.getOrderData(this.page,this.size,tab._props.name);
  }

  //发货按钮点击
  public async shipButton(row): Promise<void> {
    await this.shipApi(row.id);
    await this.getOrderData(this.page,this.size,Number.parseInt(this.activeName));
  }
  //分页
  public handleCurrentChange(page):void{
    this.page = page;
    this.getOrderData(this.page,this.size,Number.parseInt(this.activeName));
  }
}
</script>