<template>
    <div v-loading="loading" class="centerContent">
        <el-dialog title="图片剪裁" :visible.sync="outVisible">
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
      <el-form label-position="top" :model="user">
        <el-form-item label="头像" label-width="auto">
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
              :src="user.headPic"
              alt=""
            />
            <i class="el-icon-upload"></i>
            <div class="el-upload__text">点击上传</div>
          </el-upload>
        </el-form-item>
        <el-form-item label="用户名称" label-width="auto">
          <el-input v-model="user.userName" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="邮箱" label-width="auto">
          <el-input v-model="user.email" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="收货地址" label-width="auto">
          <el-input v-model="user.address" autocomplete="off"></el-input>
        </el-form-item>
         <el-form-item>
            <el-button style="float:right" type="primary" @click="changeInfo">修改信息</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<style scoped>
.centerContent{
  padding: 40px 100px;
  background: #fff;
  max-width: 900px;
}
.cropper-content .cropper {
  width: auto;
  height: 300px;
}
.goodPicCho img{
  width: 100%;
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
export default class UserCenter extends Vue {
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
  private loading: boolean = false;
  private outVisible: boolean = false;
  private user: any = {
    id: 0,
    userName:"",
    headPic:"",
    address:"",
    email:"",
  };

  public mounted(): void {
    this.getUserInfo();
  }

  //api获取用户信息
  private async getUserInfo():Promise<void> {
    this.loading = true;
    let res: any = await Api.getUserInfo();
    this.loading = false;
    if (res.status == 200) {
      this.user = res.data;
    } else {
      this.$message.error("获取失败");
      console.log(res);
    }
  }

   //api修改用户信息
  private async editUserInfo(user:any):Promise<void> {
    this.loading = true;
    let res: any = await Api.editUserInfo(user);
    this.loading = false;
    if (res.status == 200) {
      this.$message.success("编辑成功");

    } else {
      this.$message.error(res);
    }
  }

  public changeUpload(file, fileList): void {
    // 上传成功后将图片地址赋值给裁剪框显示图片
    this.$nextTick(() => {
      this.option.img = window.URL.createObjectURL(file.raw);
      console.log(window.URL.createObjectURL(file.raw));
      this.outVisible = true;
    });
  }

  public finish(): void {
    (this.$refs.cropper as any).getCropData((data) => {
      this.user.headPic = data;
      this.outVisible = false;
    });
  }

  //修改用户信息按钮
  public async changeInfo():Promise<void>{
    await this.editUserInfo(this.user);
    this.getUserInfo();
  }

}
</script>