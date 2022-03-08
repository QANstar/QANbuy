<template>
  <div>
    <div class="centerContent" v-loading="loading">
      <el-table
        v-loading="loading"
        :data="shoppingCar"
        @selection-change="handleSelectionChange"
        style="width: 100%"
      >
        <el-table-column type="selection" width="55"> </el-table-column>
        <el-table-column align="center" prop="goodPic" label="商品图片">
          <template slot-scope="scope">
            <img class="goodTableImg" :src="scope.row.goodPic" />
          </template>
        </el-table-column>
        <el-table-column align="center" prop="name" label="商品名称">
        </el-table-column>
        <el-table-column
          align="center"
          prop="price"
          label="商品价格"
        ></el-table-column>
        <el-table-column align="center" prop="num" label="数量">
          <template slot-scope="scope">
            <el-input-number
              v-model="scope.row.num"
              @change="handleChange(scope.row)"
              :min="1"
              :max="100"
              label="描述文字"
            ></el-input-number>
          </template>
        </el-table-column>
        <el-table-column align="center" label="操作">
          <template slot-scope="scope">
            <el-popconfirm
              title="确定删除？"
              @confirm="deleteRow(scope.row.goodID)"
            >
              <el-button
                type="danger"
                icon="el-icon-delete"
                circle
                size="small"
                slot="reference"
              ></el-button>
            </el-popconfirm>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <div class="bottomBuy">
      <div class="bottomContent">
        <div>
          合计：<span>￥{{ allPayment }}</span>
        </div>
        <el-button type="danger" @click="buyBtn" round>结算</el-button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.centerContent {
  padding: 20px;
  border-radius: 10px;
  background: #fff;
  margin-bottom: 70px;
}
.goodTableImg {
  max-width: 120px;
}
.bottomBuy {
  position: fixed;
  bottom: 0;
  left: 25%;
  height: 70px;
  width: 50%;
  border-radius: 10px;
  background-color: #fff;
  font-size: 22px;
  box-shadow: -6px -6px 12px #e7e8e9, 6px 6px 12px #ffffff;
  z-index: 100;
}
.bottomBuy .bottomContent {
  position: relative;
  display: flex;
  height: 100%;
  align-items: center;
  justify-content: space-around;
  color: #222;
}
.bottomBuy span {
  color: #ff66a2;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class UserCenter extends Vue {
  private shoppingCar: any = [];
  private loading: boolean = false;
  private allPayment: number = 0;
  private buyInfo: any = [];

  public mounted(): void {
    this.getShoppingCart();
  }

  //api获取购物车
  private async getShoppingCart(): Promise<void> {
    this.loading = true;
    let res: any = await Api.getShoppingCart();
    this.loading = false;
    if (res.status == 200) {
      this.shoppingCar = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api修改购物车数量
  private async editShoppingCartNum(goodAddInfo: any): Promise<void> {
    let res: any = await Api.editShoppingCartNum(goodAddInfo);
    if (res.status == 200) {
    } else {
      this.$message.error("修改数量失败");
      console.log(res);
    }
  }

  //api删除购物车
  private async deleteCartGood(goodID: number): Promise<void> {
    let res: any = await Api.deleteCartGood(goodID);
    if (res.status == 200) {
      this.getShoppingCart();
      this.$message.success("删除成功");
    } else {
      this.$message.error("删除失败");
      console.log(res);
    }
  }
  //api批量购买
  private async buyGoods(buyInfo: any): Promise<void> {
    let res: any = await Api.buyGoods(buyInfo);
    if (res.status == 200) {
      this.getShoppingCart();
      this.$message.success("购买成功");
    } else {
      this.$message.error(res);
      console.log(res);
    }
  }

  //删除按钮
  public deleteRow(goodId: number): void {
    this.deleteCartGood(goodId);
  }

  //数量改变
  public handleChange(row: any): void {
    let addModel: any = {
      goodID: row.goodID,
      num: row.num,
    };
    this.editShoppingCartNum(addModel);
  }
  //多选
  public handleSelectionChange(val): void {
    this.allPayment = 0;
    this.buyInfo = [];
    val.forEach((element) => {
      this.allPayment += element.price * element.num + element.postage;
      this.buyInfo.push(element.goodID);
    });
  }
  //购买按钮
  public buyBtn(): void {
    this.$confirm("确定购买所选物品?", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      this.buyGoods(this.buyInfo);
    });
  }
}
</script>