<template>
  <div v-loading="loading">
    <div class="orderDialog">
      <el-dialog width="800px" title="确认订单" :visible.sync="orderDialog">
        <div>
          <el-descriptions direction="vertical" :column="4" border>
            <el-descriptions-item label="商品图片"
              ><img :src="good.goodPic" alt=""
            /></el-descriptions-item>
            <el-descriptions-item label="商品名称">{{
              good.name
            }}</el-descriptions-item>
            <el-descriptions-item label="商品单价">{{
              good.price
            }}</el-descriptions-item>
            <el-descriptions-item label="购买数量">
              {{ buyNum }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
        <div class="orderInfo">
            <div>
              <span>收货地址：</span>{{user.address}}
            </div>
            <div>
              <span>发货地址：</span>{{good.shipAddress}}
            </div>
            <div>
              <span>运费：</span>{{good.postage}}
            </div>
        </div>
        <el-divider></el-divider>
        <div class="allPayment">
          应付:￥{{allPayment}}
        </div>
        <div class="sureBuyBtn">
        <el-button  type="primary" :loading="buyLoading" @click="sureBuyBtn" round>确认</el-button>
        </div>
      </el-dialog>
    </div>
    <div class="topDetail">
      <img :src="good.goodPic" />
      <div class="goodInfo">
        <div class="goodName">{{ good.name }}</div>
        <div class="price">售价：{{ good.price }}</div>
        <div>销量：{{ good.saleNum }}</div>
        <div>库存：{{ good.inventory }}</div>
        <div>邮费：{{ good.postage }}</div>
        <div>发货地址：{{ good.shipAddress }}</div>
        <template>
          <el-input-number
            v-model="buyNum"
            :min="1"
            :max="100"
            label="描述文字"
          ></el-input-number>
        </template>
        <br />
        <el-button
          type="primary"
          @click="addShoppingCartBtn"
          :loading="addShoppingLoading"
          round
          >加入购物车</el-button
        ><br />
        <el-button @click="buyBtn"  type="danger" round>购买</el-button>
      </div>
    </div>
    <div class="bottomDetail">
      <div class="tabs">
        <template>
          <el-tabs v-model="activeName" @tab-click="tabClick">
            <el-tab-pane label="详情" name="description"></el-tab-pane>
            <el-tab-pane label="评论" name="comments"></el-tab-pane>
          </el-tabs>
        </template>
      </div>
      <div class="content">
        <div v-if="activeName == 'description'" v-html="good.description"></div>
        <div v-else>
          <div class="commentInput">
            <div>
              <el-avatar
                class="userHeader"
                fit="cover"
                :src="user.headPic"
              ></el-avatar>
            </div>
            <el-input
              type="textarea"
              :rows="commentInputRow"
              placeholder="请输入内容"
              v-model="commentInput"
            >
            </el-input>
            <el-button
              :loading="commentSendLoading"
              @click="putComment"
              type="primary"
              >发表评论</el-button
            >
          </div>
          <el-divider></el-divider>
          <div v-loading="commentShowLoading" class="commentContent">
            <div v-for="item in comments" :key="item.id">
              <div class="commentItem">
                <div class="comment-headPic">
                  <el-avatar
                    class="userHeader"
                    fit="cover"
                    :src="item.headPic"
                  ></el-avatar>
                </div>
                <div class="comment-right">
                  <div class="comment-name">{{ item.userName }}</div>
                  <div class="comment-comment">{{ item.comment }}</div>
                  <div class="comment-date">
                    {{ item.commentDate.substr(0, 10) }}
                  </div>
                </div>
              </div>
              <el-divider></el-divider>
            </div>
          </div>
        </div>
      </div>
      <div></div>
    </div>
  </div>
</template>

<style scoped>
.topDetail {
  width: 100%;
  min-height: 430px;
  background: #fff;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}

.topDetail img {
  width: 325px;
  border-radius: 10px;
  box-shadow: 0 1px 2px 0 rgb(0 0 0 / 50%), 0 4px 8px 0 rgb(0 0 0 / 40%);
}

.goodInfo {
  padding: 0 70px;
  color: #222;
}

.goodInfo div {
  margin: 10px;
}

.goodInfo button {
  margin: 5px;
  width: 200px;
}

.goodInfo .goodName {
  font-size: 20px;
  font-weight: bold;
}

.goodInfo .price {
  font-size: 18px;
  color: #f25d8e;
  font-weight: bold;
}

.bottomDetail {
  max-width: 1160px;
  margin: 0 auto;
}

.bottomDetail .content {
  background: #fff;
  border-radius: 8px;
  padding: 20px;
}

.commentInput {
  padding: 15px;
  display: flex;
  max-width: 800px;
}

.userHeader {
  width: 54px;
  height: 54px;
  margin-right: 20px;
}

.commentItem {
  display: flex;
  padding: 0 15px;
}
.comment-name {
  font-size: 12px;
  line-height: 14px;
  color: #6d757a;
}
.comment-date {
  font-size: 12px;
  color: #6d757a;
}

.comment-right div {
  margin: 10px;
}
.orderDialog img {
  max-width: 100px;
}

.orderInfo{
  padding: 20px;
  border: 1px solid #e7e7e7;
  margin: 10px;
}
.orderInfo div{
  margin: 10px;
}
.allPayment{
  text-align: right;
  line-height: 22px;
    font-size: 22px;
    color: #ff66a2;
}
.sureBuyBtn{
  width: 100%;
  display: flex;
  justify-content: center;
}
.sureBuyBtn button{
  width: 150px;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class GoodsDetail extends Vue {
  private goodID: number = Number.parseInt(this.$route.params.goodID);
  private good: any = {
    id: 0,
    name: "",
    price: "",
    goodPic: "",
    state: "",
    type: "",
    description: "",
    postage: 0,
    shipAddress: "",
    saleNum: "",
    inventory: 0,
  };
  private user: any = {
    id: 0,
    headPic: "",
    address: "",
  };
  private orderDialog: boolean = false;
  private comments: any = [];
  private activeName: String = "description";
  private commentInput: String = "";
  private loading: boolean = false;
  private commentSendLoading: boolean = false;
  private commentShowLoading: boolean = false;
  private addShoppingLoading: boolean = false;
  private buyLoading:boolean = false;
  private commentInputRow: number = 2;
  private buyNum: number = 1;
  public mounted(): void {
    this.getGoodDetail(this.goodID);
    if(this.$store.state.token!=null){
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
  //api获取商品细节
  private async getGoodDetail(goodID: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.getGoodDetail(goodID);
    this.loading = false;
    if (res.status == 200) {
      this.good = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api获取评论内容
  private async getComments(goodID: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.getComment(goodID);
    this.loading = false;
    if (res.status == 200) {
      this.comments = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api发表评论
  private async putCommentApi(comment: any): Promise<void> {
    this.commentSendLoading = true;
    let res: any = await Api.putComment(comment);
    this.commentSendLoading = false;
    if (res.status == 200) {
      this.$message.success("评论发表成功");
      this.commentInput = "";
    } else {
      this.$message.error("评论发表失败");
      console.log(res);
    }
  }
  //api添加购物车
  private async addShoppingCart(goodAddInfo: any): Promise<void> {
    this.addShoppingLoading = true;
    let res: any = await Api.addShoppingCart(goodAddInfo);
    this.addShoppingLoading = false;
    if (res.status == 200) {
      this.$message.success("添加成功");
    } else {
      this.$message.error("添加失败");
      console.log(res);
    }
  }
  //api购买
  private async buyGood(buyInfo: any): Promise<void> {
    this.buyLoading = true;
    let res: any = await Api.buyGood(buyInfo);
    this.buyLoading = false;
    if (res.status == 200) {
      this.$message.success("购买成功");
      this.orderDialog = false;
    } else {
      this.$message.error(res);
      console.log(res);
    }
  }
  //点击标签
  public tabClick(tab): void {
    if (tab._props.name == "comments") {
      this.getComments(this.goodID);
    }
  }
  //点击发表评论
  public async putComment(): Promise<void> {
    let comment: any = {
      goodID: this.goodID,
      comment: this.commentInput,
    };
    await this.putCommentApi(comment);
    this.getComments(this.goodID);
  }

  //点击购买按钮
  public buyBtn(): void {
    this.orderDialog = true;
  }
  //计算总加钱
  public get allPayment() {
    return this.good.price*this.buyNum+this.good.postage;
  }
  //确认购买按钮
  public sureBuyBtn(){
    let buyInfo = {
      num:this.buyNum,
      goodID:this.goodID
    }
    this.buyGood(buyInfo);
  }
  //加入购物车按钮
  public addShoppingCartBtn(): void {
    this.$confirm("确认加入购物车？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      let addModel: any = {
        goodID: this.goodID,
        num: this.buyNum,
      };
      this.addShoppingCart(addModel);
    });
  }
}
</script>