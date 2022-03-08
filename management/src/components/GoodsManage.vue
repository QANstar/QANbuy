<template>
  <div>
    <el-dialog :title="dialogTitle" :visible.sync="dialogVisible">
      <el-dialog title="图片剪裁" :visible.sync="outVisible" append-to-body>
        <div class="cropper-content">
          <div class="cropper" style="text-align: center">
            <vueCropper
              ref="cropper"
              :img="option.img"
              :outputSize="option.size"
              :outputType="option.outputType"
              :info="true"
              :full="option.full"
              :canMove="option.canMove"
              :canMoveBox="option.canMoveBox"
              :original="option.original"
              :autoCrop="option.autoCrop"
              :fixed="option.fixed"
              :fixedNumber="option.fixedNumber"
              :centerBox="option.centerBox"
              :infoTrue="option.infoTrue"
              :fixedBox="option.fixedBox"
            ></vueCropper>
          </div>
        </div>
        <div slot="footer" class="dialog-footer">
          <el-button @click="outVisible = false">取 消</el-button>
          <el-button type="primary" @click="finish">确认</el-button>
        </div>
      </el-dialog>
      <el-form label-position="top" :model="good">
        <el-form-item label="商品图片" label-width="auto">
          <el-upload
            class="goodPicCho"
            action=""
            drag
            :auto-upload="false"
            :show-file-list="false"
            :on-change="changeUpload"
          >
            <img
              class="el-upload-list__item-thumbnail dialogGoodPic"
              :src="good.goodPic"
              alt=""
            />
            <i class="el-icon-upload"></i>
            <div class="el-upload__text">点击上传</div>
          </el-upload>
        </el-form-item>
        <el-form-item label="商品名称" label-width="auto">
          <el-input v-model="good.name" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="商品价格" label-width="auto">
          <el-input v-model="good.price" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="邮费" label-width="auto">
          <el-input v-model="good.postage" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="发货地址" label-width="auto">
          <el-input v-model="good.shipAddress" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="商品类型" label-width="auto" prop="type">
          <el-select v-model="good.type" placeholder="请选择类型">
            <el-option
              v-for="item in goodType"
              :key="item.id"
              :label="item.type"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="库存" label-width="auto">
          <el-input v-model="good.inventory" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="描述" label-width="auto">
          <div class="quillStyle">
            <quill-editor
              v-model="good.description"
              ref="myQuillEditor"
              style="height: 500px"
            >
            </quill-editor>
          </div>
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
    <el-table v-loading="loading" :data="goodData" style="width: 100%">
      <el-table-column align="center" prop="id" label="商品ID">
      </el-table-column>
      <el-table-column align="center" prop="goodPic" label="商品图片">
        <template slot-scope="scope">
          <img class="goodTableImg" :src="scope.row.goodPic" />
        </template>
      </el-table-column>
      <el-table-column align="center" prop="name" label="商品名称">
      </el-table-column>
      <el-table-column align="center" prop="price" label="商品价格">
      </el-table-column>
      <el-table-column align="center" prop="postage" label="邮费">
      </el-table-column>
      <el-table-column align="center" prop="shipAddress" label="发货地址">
      </el-table-column>
      <el-table-column align="center" prop="createDate" label="创建时间">
        <template slot-scope="scope">
          <span>{{ scope.row.createDate.substr(0, 10) }}</span>
        </template>
      </el-table-column>
      <el-table-column align="center" prop="type" label="商品类型">
      </el-table-column>
      <el-table-column align="center" prop="saleNum" label="销售数量">
      </el-table-column>
      <el-table-column align="center" prop="inventory" label="库存">
      </el-table-column>
      <el-table-column align="center" prop="state" label="状态">
        <template slot-scope="scope">
          <el-select
            v-model="scope.row.stateId"
            @change="editStateChange(scope.row.id, scope.row.stateId)"
            placeholder="请选择状态"
          >
            <el-option
              v-for="item in goodStatus"
              :key="item.id"
              :label="item.state"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </template>
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
    <div class="pagination">
      <el-pagination layout="prev, pager, next" @current-change="handleCurrentChange" :page-size="pageSize" :current-page="pageNum" :total="pageTotal"> </el-pagination>
    </div>
  </div>
</template>

<style scoped>
.addBtn {
  float: right;
}
.cropper-content .cropper {
  width: auto;
  height: 300px;
}

.dialogGoodPic {
  width: 100%;
}

.goodTableImg {
  width: 100%;
}

.quillStyle {
  padding-bottom: 50px;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { Api } from "@/assets/ts/API";
import { VueCropper } from "vue-cropper";
import { quillEditor } from "vue-quill-editor";
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import "quill/dist/quill.bubble.css";
@Component({
  components: {
    VueCropper,
    quillEditor,
  },
})
export default class GoodTypeMangement extends Vue {
  private option: any = {
    img: "", // 裁剪图片的地址
    info: true, // 裁剪框的大小信息
    outputSize: 0.5, // 裁剪生成图片的质量
    outputType: "jpeg", // 裁剪生成图片的格式
    canScale: false, // 图片是否允许滚轮缩放
    autoCrop: true, // 是否默认生成截图框
    autoCropWidth: 300, // 默认生成截图框宽度
    autoCropHeight: 300, // 默认生成截图框高度
    fixedBox: true, // 固定截图框大小 不允许改变
    fixed: true, // 是否开启截图框宽高固定比例
    fixedNumber: [5, 5], // 截图框的宽高比例
    full: true, // 是否输出原图比例的截图
    canMoveBox: false, // 截图框能否拖动
    original: false, // 上传图片按照原始比例渲染
    centerBox: false, // 截图框是否被限制在图片里面
    infoTrue: true, // true 为展示真实输出图片宽高 false 展示看到的截图框宽高
  };
  private titleStr: any = {
    add: "添加商品",
    edit: "编辑商品",
  };
  private goodData: any = [];
  private loading: boolean = true;
  private dialogVisible: boolean = false;
  private outVisible: boolean = false;
  private dialogTitle: string = this.titleStr.edit;
  private pageSize = 5;
  private pageNum = 1;
  private pageTotal = 1;
  private good: any = {
    id: 0,
    goodPic: "",
    name: "",
    price: "",
    postage: "",
    shipAddress: "",
    type: "",
    inventory: "",
    description: "",
  };
  private goodType: any = {
    id: "",
    type: "",
  };
  private goodStatus: any = {
    id: "",
    state: "",
  };
  public mounted(): void {
    this.getPageTotal(this.pageSize);
    this.getGoodType();
    this.getGoodStatus();
    this.getGoodData(this.pageNum, this.pageSize);
  }

  public changeUpload(file, fileList): void {
    // 上传成功后将图片地址赋值给裁剪框显示图片
    this.$nextTick(() => {
      this.option.img = window.URL.createObjectURL(file.raw);
      console.log(this.option.img);
      this.outVisible = true;
    });
  }

  public finish(): void {
    (this.$refs.cropper as any).getCropData((data) => {
      this.good.goodPic = data;
      this.outVisible = false;
    });
  }

  //api获取商品状态
  private async getGoodStatus(): Promise<void> {
    let res: any = await Api.getGoodStatus();
    if (res.status == 200) {
      this.goodStatus = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api获取商品类型
  private async getGoodType(): Promise<void> {
    let res: any = await Api.getGoodType();
    if (res.status == 200) {
      this.goodType = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api获取商品
  private async getGoodData(page: number, size: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.getGoods(page, size);
    this.loading = false;
    if (res.status == 200) {
      this.goodData = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

  //api获取分页数量
  private async getPageTotal(size: number): Promise<void> {
    let res: any = await Api.getPageTotal(size);
    if (res.status == 200) {
      this.pageTotal = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }
  //api添加商品
  private async addGood(): Promise<void> {
    this.loading = true;
    let res: any = await Api.addGood(this.good);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("添加成功");
    } else {
      this.$message.error("添加失败");
      console.log(res);
    }
  }

  //api编辑商品状态
  private async editGoodState(id: number, stateId: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.editGoodState(id, stateId);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("编辑成功");
    } else {
      this.$message.error("编辑失败");
      console.log(res);
    }
  }
  //api编辑商品
  private async editGood(): Promise<void> {
    this.loading = true;
    let res: any = await Api.editGood(this.good);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("编辑成功");
    } else {
      this.$message.error("编辑失败");
      console.log(res);
    }
  }

  //api删除商品
  private async deleteGood(id: number): Promise<void> {
    this.loading = true;
    let res: any = await Api.deleteGood(id);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("删除成功");
    } else {
      this.$message.error("删除失败");
      console.log(res);
    }
  }

  //添加或编辑商品
  public async sureButon(): Promise<void> {
    this.dialogVisible = false;
    console.log(this.good);
    if (this.good.id == 0) {
      await this.addGood();
      await this.getGoodData(this.pageNum, this.pageSize);
    } else {
      await this.editGood();
      await this.getGoodData(this.pageNum, this.pageSize);
    }
  }

  //添加按钮
  public openAdd(): void {
    this.good = {
      id: 0,
      goodPic: "",
      name: "",
      price: "",
      postage: "",
      shipAddress: "",
      type: "",
      inventory: "",
      description: "",
    };
    this.dialogTitle = this.titleStr.add;
    this.dialogVisible = true;
  }

  //编辑按钮
  public editRow(row): void {
    this.good = {
      id: row.id,
      goodPic: row.goodPic,
      name: row.name,
      price: row.price,
      postage: row.postage,
      shipAddress: row.shipAddress,
      type: row.typeId,
      inventory: row.inventory,
      description: row.description,
    };
    this.dialogTitle = this.titleStr.edit;
    this.dialogVisible = true;
  }

  //删除按钮
  public async deleteRow(row): Promise<void> {
    await this.deleteGood(row.id);
    await this.getGoodData(this.pageNum, this.pageSize);
  }

  //编辑状态选择框改变
  public async editStateChange(id, state): Promise<void> {
    await this.editGoodState(id, state);
  }

  //分页
  public handleCurrentChange(page):void{
    this.pageNum = page;
    this.getGoodData(page, this.pageSize);
  }
}
</script>