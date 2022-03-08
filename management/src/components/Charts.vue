<template>
  <div class="charts-content">
    <div>
      <h2>商品销售数量排行</h2>
      <ve-histogram
        :data="numChartData"
        height="400px"
        width="600px"
      ></ve-histogram>
    </div>
    <div>
      <h2>商品排行</h2>
      <ve-histogram
        :data="paymentChartData"
        height="400px"
        width="600px"
      ></ve-histogram>
    </div>
    <div>
      <h2>商品类型数量环图</h2>
      <ve-ring :data="typeNumChartData" height="400px" width="600px"></ve-ring>
    </div>
    <div>
      <h2>商品类型销售数量环图</h2>
      <ve-ring
        :data="typeSaleNumChartData"
        height="400px"
        width="600px"
      ></ve-ring>
    </div>
  </div>
</template>

<style scoped>
.charts-content {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
}
.chart {
  max-width: 50%;
}
h2{
  color:  #409EFF;
}
</style>



<script lang="ts">
import axios from "axios";
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
@Component
export default class Charts extends Vue {
  public mounted(): void {
    this.getTopNumData(6);
    this.getTopPaymentData(6);
    this.getTypeNumData();
    this.getTypeSaleNumData();
  }
  private numChartData: any = {
    columns: ["商品名称", "销售数量"],
    rows: [],
  };
  private paymentChartData: any = {
    columns: ["商品名称", "总销售额"],
    rows: [],
  };
  private typeNumChartData: any = {
    columns: ["类型", "数量"],
    rows: [],
  };
  private typeSaleNumChartData: any = {
    columns: ["类型", "总销售数"],
    rows: [],
  };
  //api获取销量表
  public async getTopNumData(num: number): Promise<void> {
    let res: any = await Api.getTopNumCharts(num);
    if (res.status == 200) {
      console.log(res.data);
      res.data.forEach((element) => {
        let data: any = {
          商品名称: element.name,
          销售数量: element.saleNum,
        };
        this.numChartData.rows.push(data);
      });
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api获取销售表
  public async getTopPaymentData(num: number): Promise<void> {
    let res: any = await Api.getTopPaymentCharts(num);
    if (res.status == 200) {
      console.log(res.data);
      res.data.forEach((element) => {
        let data: any = {
          商品名称: element.name,
          总销售额: element.allPayment,
        };
        this.paymentChartData.rows.push(data);
      });
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api获取类型数量
  public async getTypeNumData(): Promise<void> {
    let res: any = await Api.getTypeNumCharts();
    if (res.status == 200) {
      this.typeNumChartData.rows = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api获取类型销量
  public async getTypeSaleNumData(): Promise<void> {
    let res: any = await Api.getTypeSaleNumCharts();
    if (res.status == 200) {
      this.typeSaleNumChartData.rows = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
}
</script>