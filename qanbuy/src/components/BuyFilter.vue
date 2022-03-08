<template>
  <div v-loading="loading">
    <div class="sizerTop">
      <div class="sizerContent">
        <span class="sizerTitle">类型：</span>
        <span class="sizerItem" @click="choSizer(0)" :class="{active:sizerChecked == 0}">全部类型</span>
        <span v-for="item in type" :key="item.id" class="sizerItem" @click="choSizer(item.id)" :class="{active:sizerChecked == item.id}">{{item.type}}</span>
      </div>
    </div>
    <div class="goodList">
      <div v-for="item in goods" :key="item.id" class="goodItem" @click="enterDetail(item.id)">
        <img :src="item.goodPic" :alt="item.name" />
        <div class="goodIntr">
          <div class="goodName">{{item.name}}</div>
          <div class="goodNum">销量：{{item.saleNum}}</div>
          <div class="goodPrice">￥{{item.price}}</div>
        </div>
      </div>
    </div>
    <div class="pagination">
      <el-pagination layout="prev, pager, next" @current-change="handleCurrentChange" :page-size="size" :current-page="page" :total="pageTotal"> </el-pagination>
    </div>
  </div>
</template>

<style scoped>
.sizerTop {
  width: 100%;
  background-color: #fff;
  min-height: 70px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.sizerContent {
  max-width: 1000px;
}

.sizerContent span {
  margin-right: 50px;
}

.sizerTitle {
  color: #99a2aa;
}
.goodList {
  padding: 20px;
  max-width: 1360px;
  margin: 0 auto;
  display: flex;
    flex-wrap: wrap;
  justify-content: space-around;
}
.goodItem {
  background-color: #fff;
  width: 330px;
  height: 150px;
  padding: 10px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgb(0 0 0 / 8%);
  display: flex;
  cursor: pointer;
  margin-bottom: 35px;
}
.goodItem img {
  height: 100%;
}
.goodIntr {
  padding: 10px;
}
.goodName {
  font-size: 24px;
  font-weight: bold;
}
.goodPrice {
  margin-top: 20px;
  font-size: 25px;
  font-weight: bold;
  color: #f25d8e;
}
.goodNum {
  margin-top: 15px;
  color: #999;
}
.sizerItem{
  cursor: pointer;
}
.active{
  color: #00aeec;
}
.pagination{
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
export default class BuySearch extends Vue {
  private type: any = [];
  private sizerChecked:number = 0;
  private goods:any = [];
  private page:number = 1;
  private size:number = 9;
  private pageTotal:number = 1;
  private loading:boolean = false;
  public mounted(): void {
    this.getGoodType();
    this.getNumByType(this.sizerChecked);
    this.getTypeGoods(this.page,this.size,0);
  }

  //api获取商品类型
  private async getGoodType(): Promise<void> {
    let res: any = await Api.getGoodType();
    if (res.status == 200) {
      this.type = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api获取商品数量
  private async getNumByType(typeId: number): Promise<void> {
    let res: any = await Api.getNumByType(typeId);
    if (res.status == 200) {
      this.pageTotal = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api按类型获取销量排名前几的商品
  private async getTypeGoods(
    page: number,
    size: number,
    type: number
  ): Promise<void> {
    this.loading = true;
    let res: any = await Api.getTypeGoods(page, size, type);
    this.loading = false;
    if (res.status == 200) {
      this.goods=res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
//进入商品细节
  public enterDetail(id:number):void{
     this.$router.push("/GoodDetail/goodID=" + id);
  }
  //筛选点击
  public choSizer(id:number):void{
    this.page = 1;
    this.sizerChecked = id;
    this.getNumByType(this.sizerChecked);
    this.getTypeGoods(this.page,this.size,id);
  }
  //分页
  public handleCurrentChange(page:number):void{
    this.page = page;
    this.getTypeGoods(this.page,this.size,this.sizerChecked);
  }
  }
</script>