<template>
  <div>
    <el-dialog :title="dialogTitle" :visible.sync="dialogVisible">
      <el-form :model="goodType">
        <el-form-item label="商品类型" label-width="auto">
          <el-input v-model="goodType.type" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="sureButon()">确 定</el-button>
      </div>
    </el-dialog>
    <el-button
      class="addBtn"
      icon="el-icon-plus"
      type="primary"
      circle
      @click="openAdd()"
    ></el-button>
    <el-table v-loading="loading" :data="typeData" style="width: 100%">
      <el-table-column align="center" prop="id" label="类型ID">
      </el-table-column>
      <el-table-column align="center" prop="type" label="类型名称">
      </el-table-column>
      <el-table-column align="center" label="操作">
        <template slot-scope="scope">
          <el-button
            type="primary"
            icon="el-icon-edit"
            @click="editRow(scope.row)"
            size="small"
            circle
          ></el-button>
          <el-popconfirm title="确定删除？" @confirm="deleteRow(scope.row)">
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
export default class GoodTypeMangement extends Vue {
  private titleStr: any = {
    add: "添加类型",
    edit: "编辑类型",
  };
  private typeData: any = [];
  private loading: boolean = true;
  private dialogVisible: boolean = false;
  private dialogTitle: string = this.titleStr.edit;
  private goodType: any = {
    ID: 0,
    type: "",
  };

  public mounted(): void {
    this.getGoodType();
  }

  //api获取商品类型
  private async getGoodType(): Promise<void> {
    this.loading = true;
    let res: any = await Api.getGoodType();
    this.loading = false;
    if (res.status == 200) {
      this.typeData = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api添加商品类型
  private async addGoodType():Promise<void>{
    this.loading = true;
    let res: any = await Api.addGoodType(this.goodType.type);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("添加成功");
    } else {
      this.$message.error("添加失败");
      console.log(res);
    }
  }

  //api编辑商品类型
  private async editGoodType():Promise<void>{
    this.loading = true;
    let res: any = await Api.editGoodType(this.goodType);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("编辑成功");
    } else {
      this.$message.error("编辑失败");
      console.log(res);
    }
  }

  //api编辑商品类型
  private async deleteGoodType(id:number):Promise<void>{
    this.loading = true;
    let res: any = await Api.deleteGoodType(id);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("删除成功");
    } else {
      this.$message.error("删除失败");
      console.log(res);
    }
  }

  //添加或编辑类型
  public async sureButon(): Promise<void> {
    this.dialogVisible = false;
    if (this.goodType.ID == 0) {
      await this.addGoodType();
      await this.getGoodType();
    } else {
      await this.editGoodType();
      await this.getGoodType();
    }
  }

  //添加按钮
  public openAdd(): void {
    this.goodType = {
      ID: 0,
      type: "",
    };
    this.dialogTitle = this.titleStr.add;
    this.dialogVisible = true;
  }

  //编辑按钮
  public editRow(row): void {
    console.log(row)
    this.goodType = {
      ID: row.id,
      type: row.type,
    };
    this.dialogTitle = this.titleStr.edit;
    this.dialogVisible = true;
  }

  //删除按钮
  public async deleteRow(row): Promise<void> {
    await this.deleteGoodType(row.id);
    await this.getGoodType();
  }
}
</script>