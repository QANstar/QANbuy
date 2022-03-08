<template>
  <div v-loading="loading">
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
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class BuySearch extends Vue {
  private searchStr:string = "";
  private goods:any = [];
  private page:number = 1;
  private size:number = 9;
  private pageTotal:number = 1;
  private loading:boolean = false;
  public mounted(): void {
    this.searchStr = this.$route.params.searchStr;
    this.searchGoodsNum(this.searchStr);
    this.searchGoods(this.page,this.size,this.searchStr);
  }


  //api获取搜索商品数量
  private async searchGoodsNum(searchStr: string): Promise<void> {
    let res: any = await Api.searchNum(searchStr);
    if (res.status == 200) {
      this.pageTotal = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api搜索商品
  private async searchGoods(
    page: number,
    size: number,
    searchStr: string
  ): Promise<void> {
    this.loading = true;
    let res: any = await Api.searchGoods(page, size, searchStr);
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
  //分页
  public handleCurrentChange(page:number):void{
    this.page = page;
    this.searchGoods(this.page,this.size,this.searchStr);
  }

  @Watch("searchStr", { immediate: false })
  private onSearchChange(searchStr: string) {
      console.log(searchStr)
  }
}
</script>