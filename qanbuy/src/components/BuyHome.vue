<template>
  <div class="centerContent">
    <div class="carousel" v-loading="carouselLoading">
      <el-carousel>
        <el-carousel-item v-for="item in topGoods" :key="item.id">
          <div class="carouselItem" @click="enterDetail(item.id)">
            <img style="width: 700px" :src="item.goodPic" :alt="item.name" />
            <div class="carouseInfo">
              <div class="hotFont">热销</div>
              <div class="carouseName">{{ item.name }}</div>
              <div class="carousePrice">￥{{ item.price }}</div>
              <el-button type="danger" round>立即购买</el-button>
            </div>
          </div>
        </el-carousel-item>
      </el-carousel>
    </div>
    <div class="home-content" v-loading="goodLoading"> 
        <div class="goodByType" v-for="(item, i) in typeGoods" :key="i">
          <div class="typeTitle">{{ type[i].type }}</div>
          <div class="goodByTypeItem">
            <div class="typeImg" v-for="img in item" :key="img.id" @click="enterDetail(img.id)">
              <img :src="img.goodPic" :alt="img.name" />
              <div class="imgName">{{ img.name }}</div>
              <div class="imgPrice">￥{{ img.price }}</div>
            </div>
          </div>
      </div>
    </div>

    <div></div>
  </div>
</template>

<style scoped>
.typeTitle {
  font-size: 24px;
  color: #18191c;
  margin-bottom: 20px;
}
.imgName {
  text-align: center;
  font-weight: bold;
}
.goodByTypeItem {
  width: 100%;
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
}
.typeImg {
  width: 200px;
  height: 260px;
  padding: 10px;
  background: #fff;
  border-radius: 10px;
  cursor: pointer;
  box-shadow: 0 2px 4px rgb(0 0 0 / 8%);
  margin: 10px;
}
.typeImg img {
  width: 100%;
  border-radius: 10px;
  box-shadow: 0 2px 4px rgb(0 0 0 / 8%);
}

.typeImg img:hover {
  display: block;
  -webkit-animation: shakeAnmi 0.5s;
}
.imgPrice {
  font-weight: bold;
  text-align: center;
  color: #ff6666;
  margin-top: 10px;
}
.goodByType {
  max-width: 100%;
  margin-top: 20px;
  margin-bottom: 10px;
}
.el-carousel__item {
  background-color: #fff;
}
.el-carousel__item h3 {
  color: #475669;
  font-size: 18px;
  opacity: 0.75;
  line-height: 300px;
  margin: 0;
}

.carousel {
  width: 100%;
  margin: 0 auto;
  margin-bottom: 50px;
  box-shadow: 0 2px 4px rgb(0 0 0 / 8%);
}

.carouselItem {
  cursor: pointer;
  display: flex;
  flex-wrap: wrap;
}

.carouseInfo {
  padding: 20px;
  margin: 0 auto;
  margin-top: 120px;
}
.hotFont {
  color: #ff6666;
  font-weight: bold;
}
.carouseInfo .carouseName {
  font-size: 40px;
  font-weight: bold;
}
.carouseInfo .carousePrice {
  margin-top: 20px;
  margin-bottom: 20px;
  font-size: 30px;
  color: #ff6666;
  font-weight: bold;
}
.home-content {
  display: flex;
  flex-wrap: wrap;
    justify-content: center;
  background-color: #fff;
  padding: 30px;
  border-radius: 20px;
}
.home-content-left {
  width: 80%;
}
.home-content-right {
  width: 20%;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class BuyHome extends Vue {
  private topGoods: any = [];
  private type: any = [];
  private carouselLoading:boolean = false;
  private goodLoading:boolean = false;
  private typeGoods: any = [];

  public mounted(): void {
    this.getTopGoods(6);
    this.showTypeGoods(2);
  }

  //api获取销量排名前几的商品
  private async getTopGoods(num: number): Promise<void> {
    this.goodLoading = true;
    let res: any = await Api.getTopGoods(num);
    this.goodLoading = false;
    if (res.status == 200) {
      this.topGoods = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
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

  //api按类型获取销量排名前几的商品
  private async getTypeGoods(
    page: number,
    size: number,
    type: number
  ): Promise<void> {
    this.carouselLoading = true;
    let res: any = await Api.getTypeGoods(page, size, type);
    this.carouselLoading = false;
    if (res.status == 200) {
      this.typeGoods.push(res.data);
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //显示类型推荐
  private async showTypeGoods(showTypeNum: number): Promise<void> {
    this.typeGoods = [];
    await this.getGoodType();
    for (let i: number = 0; i < showTypeNum; i++) {
      await this.getTypeGoods(1, 5, this.type[i].id);
    }
  }

  //进入商品细节
  public enterDetail(id:number):void{
     this.$router.push("/GoodDetail/goodID=" + id);
  }
}
</script>