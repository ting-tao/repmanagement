<template>
  <div class="dicttable dict-manager">
    <table class="filter_table" cellspacing="0" cellpadding="0">
      <tbody>
        <tr class="ct tb-tr">
          <td class="name">款号：</td>
          <td>
            <el-input class="w220" clearable/>
          </td>
          <td class="name">款式 ：</td>
          <td style="padding: 5px 0;">
            <el-select
              class="w220"
              v-model="curStypeTypeId"
              placeholder="请选择供款式"
              filterable
              @change="getDataList"
              @clear="getDataList"
              clearable
            >
              <el-option
                v-for="item in styleTypeList"
                :key="item.Id"
                :label="item.TypeName"
                :value="item.Id"
              ></el-option>
            </el-select>
          </td>
          <td class="name">材质 ：</td>
          <td style="padding: 5px 0;">
            <el-select
              class="w220"
              v-model="curMatTypeId"
              placeholder="请选择供材质"
              filterable
              @change="getDataList"
              @clear="getDataList"
              clearable
            >
              <el-option
                v-for="item in matTypeList"
                :key="item.Id"
                :label="item.TypeName"
                :value="item.Id"
              ></el-option>
            </el-select>
          </td>
          <td class="name">模具 ：</td>
          <td style="padding: 5px 0;">
            <el-select
              class="w220"
              v-model="curMouldId"
              placeholder="请选择供模具"
              filterable
              @change="getDataList"
              @clear="getDataList"
              clearable
            >
              <el-option
                v-for="item in mouldList"
                :key="item.Id"
                :label="item.TypeName"
                :value="item.Id"
              ></el-option>
            </el-select>
          </td>
        </tr>
      </tbody>
    </table>
    <!-- 查询模块 -->
    <div class="inquire">
      <el-button class="added demand-add" type="primary" @click="addProduction()">
        <i class="el-icon-plus"></i>&nbsp;新增成品
      </el-button>
      <el-button class="demand" type="primary" @click="getDataList()">
        <i class="el-icon-search"></i>&nbsp;搜索
      </el-button>
    </div>
    <!-- 左侧导航 -->
    <div class="table">
      <el-table ref="singleTable" :data="fiteredTableData" border highlight-current-row>
        <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
        <el-table-column property="SerialNum" label="编号" width="80" align="left"></el-table-column>
        <el-table-column property="MatTypeName" label="材质" width="80" align="left"></el-table-column>
        <el-table-column property="StyleTypeName" label="款式" width="80" align="left"></el-table-column>
        <el-table-column property="Color" label="颜色" width="80" align="left"></el-table-column>
        <el-table-column property="Size" label="帽围" width="80" align="left"></el-table-column>
        <el-table-column property="MouldNum" label="模具" width="80" align="left"></el-table-column>
        <el-table-column property="ProcessCost" label="加工费" width="80" align="left"></el-table-column>
        <el-table-column property="MainVendorNum" label="主料供应商" width="80" align="left"></el-table-column>
        <el-table-column property="MainCost" label="主料成本" width="80" align="left"></el-table-column>
        <el-table-column property="DecorateVendorNum" label="装饰供应商" width="80" align="left"></el-table-column>
        <el-table-column property="DecorateCost" label="装饰成本" width="80" align="left"></el-table-column>
        <el-table-column property="SweatbandVendorNum" label="汗带供应商" width="80" align="left"></el-table-column>
        <el-table-column property="SweatbandCost" label="汗带成本" width="80" align="left"></el-table-column>
        <el-table-column property="PacketCost" label="包装成本" width="80" align="left"></el-table-column>
        <el-table-column property="TransportCost" label="运输成本" width="80" align="left"></el-table-column>
        <el-table-column property="TotalCost" label="总成本" width="80" align="left"></el-table-column>
        <el-table-column property="CustomerNum" label="定款客户" width="80" align="left"></el-table-column>
        <el-table-column label="用户操作" width="130">
          <template slot-scope="scope">
            <i class="el-icon-edit green operation" @click="editProduction(scope.row,scope.$index)"></i>
            <span class="slash"></span>
            <i
              class="el-icon-delete2 green operation"
              @click="handleDelete(scope.row,scope.$index)"
            ></i>
            <span class="slash"></span>
            <i class="el-icon-upload green operation" @click="uploadImg(scope.row.Id)"></i>
            <!-- <span class="slash"></span>
            <i class="el-icon-view green operation" @click="editProduction(scope.row,scope.$index)"></i> -->
          </template>
        </el-table-column>
      </el-table>
    </div>
    <el-dialog :title="dialogTitle" :visible.sync="dialogProduction">
      <el-form :model="formData" :rules="rules" ref="formData">
        <el-row>
          <el-col :span="24">
            <el-form-item label="编号" label-width="120px" prop="SerialNum">
              <el-input v-model="formData.SerialNum" autocomplete="off"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="材质" label-width="120px" prop="MatTypeId">
              <el-select class="w220" v-model="formData.MatTypeId" placeholder="请选择成品材质">
                <el-option
                  v-for="item in matTypeList"
                  :key="item.Id"
                  :label="item.TypeName"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="款式" label-width="120px" prop="StyleTypeId">
              <el-select class="w220" v-model="formData.StyleTypeId" placeholder="请选择成品款式">
                <el-option
                  v-for="item in styleTypeList"
                  :key="item.Id"
                  :label="item.TypeName"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="颜色" label-width="120px" prop="Color">
              <el-input v-model="formData.Color" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="帽围" label-width="120px" prop="Size">
              <el-input v-model="formData.Size" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="模具" label-width="120px" prop="MouldId">
              <el-select class="w220" v-model="formData.MouldId" placeholder="请选择模具">
                <el-option
                  v-for="item in mouldList"
                  :key="item.Id"
                  :label="item.SerialNum"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="加工费" label-width="120px" prop="ProcessCost">
              <el-input v-model="formData.ProcessCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="主料供应商" label-width="120px" prop="MainVendorId">
              <el-select class="w220" v-model="formData.MainVendorId" placeholder="请选择主料供应商">
                <el-option
                  v-for="item in vendorList"
                  :key="item.Id"
                  :label="item.SerialNum"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="主料成本" label-width="120px" prop="MainCost">
              <el-input v-model="formData.MainCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="装饰供应商" label-width="120px" prop="DecorateVendorId">
              <el-select class="w220" v-model="formData.DecorateVendorId" placeholder="请选择装饰供应商">
                <el-option
                  v-for="item in vendorList"
                  :key="item.Id"
                  :label="item.SerialNum"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="装饰成本" label-width="120px" prop="DecorateCost">
              <el-input v-model="formData.DecorateCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="汗带供应商" label-width="120px" prop="SweatbandVendorId">
              <el-select class="w220" v-model="formData.SweatbandVendorId" placeholder="请选择汗带供应商">
                <el-option
                  v-for="item in vendorList"
                  :key="item.Id"
                  :label="item.SerialNum"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="汗带成本" label-width="120px" prop="SweatbandCost">
              <el-input v-model="formData.SweatbandCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="包装成本" label-width="120px" prop="PacketCost">
              <el-input v-model="formData.PacketCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="运输成本" label-width="120px" prop="TransportCost">
              <el-input v-model="formData.TransportCost" autocomplete="off" class="w220"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="11">
            <el-form-item label="总成本" label-width="120px" prop="TotalCost">
              <el-input v-model="formData.TotalCost" autocomplete="off" class="w220" readonly></el-input>
            </el-form-item>
            <el-form-item label="定款客户" label-width="120px" prop="CustomerId">
              <el-select class="w220" v-model="formData.CustomerId" placeholder="请选择汗带供应商">
                <el-option
                  v-for="item in customerList"
                  :key="item.Id"
                  :label="item.SerialNum"
                  :value="item.Id"
                  clearable
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogProduction = false">取 消</el-button>
        <el-button type="primary" @click="saveProduction('formData')">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog title="成品图片" :visible.sync="dialogUploadImg">
      <template>
        <el-upload
          action="/api/production/UploadProductionImg"
          list-type="picture-card"
          :on-remove="handlePictureRemove"
          :before-remove="test"
          :on-success="handleUploadSuccess"
          :before-upload="beforePictureUpload"
          :data="uploadPicParam"
          :file-list="prodPicList"
        >
          <i class="el-icon-plus"></i>
        </el-upload>
      </template>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogUploadImg = false">关 闭</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { dictTypes, categoryTypeList } from "../../lib/consts";

export default {
  data() {
    var checkDicTypeID = (rule, value, callback) => {
      if (!value) {
        callback(new Error("字典类型不能为空"));
      } else {
        callback();
      }
    };
    return {
      tableData: [],

      categoryTypeList: categoryTypeList,
      curStypeTypeId: null,
      curMatTypeId: null,
      curMouldId: null,
      dialogProduction: false,
      dialogTitle: "新增",
      editItemIndex: -1,


      //上传图片
      dialogUploadImg: false,
      uploadPicParam: {
        prodId: ''
      },
      prodPicList: [],

      formData: {},
      rules: {
        TypeName: [
          {
            required: true,
            message: "请输入显示名称",
            trigger: "blur"
          }
        ],
        DicTypeID: [
          { message: "请选择字典类别", validator: checkDicTypeID }
          //  { required: true, message: '请选择字典类别',trigger:'change',validator: validatePass }
        ]
      },

      vendorTypeList: [],

      vendorList: [],
      styleTypeList: [],
      matTypeList: [],
      mouldList: [],
      customerList: []
    };
  },
  methods: {
    getDataList() {
      let _self = this;
      this.$http.get("/api/production/GetProductionList").then(
        function (res) {
          var result = res.data;

          if (result.ReturnCode == 1) {
            _self.tableData = result.Data;
          } else {
            _self.$alert(result.Message, " ", {
              type: "info",
              confirmButtonText: "确定"
            });
          }
        },
        function (err) {
          console.log(err);
        }
      );
    },
    addProduction() {
      //this.formData.DicTypeId=this.curStypeTypeId;
      this.dialogTitle = "新增";
      this.dialogProduction = true;
      this.editItemIndex = -1;

      //this.$data.dialogProduction
    },
    editProduction(item, index) {
      this.formData = Object.assign({}, item);
      this.dialogTitle = "编辑";
      this.editItemIndex = this.tableData.indexOf(item);
      this.dialogProduction = true;
    },
    handleDelete(item, index) {
      let _self = this;
      this.$http
        .get("/api/production/DeleteProduction?itemId=" + item.Id)
        .then(
          function (res) {
            var result = res.data;
            if (result.ReturnCode == 1) {
              var curIndex = _self.tableData.indexOf(item);
              _self.tableData.splice(curIndex, 1);
            } else {
              _self.$alert(result.Message, " ", {
                type: "info",
                confirmButtonText: "确定"
              });
            }
          },
          function (err) {
            console.log(err);
          }
        );
    },
    saveProduction(formData) {
      let _self = this;

      //_self.formData.DicTypeId=this.curStypeTypeId;

      this.$http.post("/api/production", _self.formData).then(
        function (res) {
          var result = res.data;

          if (result.ReturnCode == 1) {
            var resultData = result.Data;

            if (_self.editItemIndex == -1) {
              _self.tableData.push(resultData);
            } else {
              //            var curIndex=_self.tableData.indexOf(formData);
              // _self.tableData.splice(curIndex,1);
              _self.tableData.splice(
                _self.editItemIndex,
                1,
                resultData
              );
            }

            _self.dialogProduction = false;
            _self.editItemIndex = -1;
            _self.formData = {};
          } else {
            _self.$alert(result.Message, " ", {
              type: "info",
              confirmButtonText: "确定"
            });
          }
        },
        function (err) {
          console.log(err);
        }
      );
    },
    getProductionSelectData() {
      let _self = this;
      this.$http.get("/api/production/GetProductionSelectData").then(
        function (res) {
          var result = res.data;
          if (result.ReturnCode == 1) {
            var data = result.Data;
            _self.styleTypeList = data.StyleTypeList;
            _self.matTypeList = data.MatTypeList;
            _self.vendorList = data.VendorList;
            _self.mouldList = data.MouldList;
            _self.customerList = data.CustomerList;
          } else {
            _self.$alert(result.Message, " ", {
              type: "info",
              confirmButtonText: "确定"
            });
          }
        },
        function (err) {
          console.log(err);
        }
      );
    },
    uploadImg(prodId) {
      this.prodPicList = [];
      this.dialogUploadImg = true;
      this.uploadPicParam.prodId = prodId;
      this.loadProdPictureList(prodId);
    },
    handlePictureRemove(file, fileList) {
      var fileId = file.id;
      let _self = this;

     
      this.$http.get("/api/production/DeleteProductionImg?picId=" + fileId).then(
        function (res) {
          var result = res.data;
          if (result.ReturnCode == 1) {
            //return Promise.resolve;
          } else {
            //return Promise.reject;
          }
        },
        function (err) {
          //return Promise.reject;
        }
      );
      //console.log(file, fileList);
    },
    handleUploadSuccess(res, file) {
      //var data=res.data;
      let self = this;
      if (res.ReturnCode == 1) {
        self.prodPicList.push({
          name: 'pic' + self.prodPicList.length,
          url: '/api/image/ShowProdPic?prodPicId=' + res.Data
        });
      } else {
        this.imageUrl = "";
      }
    },
    beforePictureUpload(file) {
      const isJPG =
        file.type === "image/png" || file.type === "image/jpg";
      //const isLt2M = file.size / 1024 <= 200;
      const isLt2M = true;

      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG/png 格式!");
      }
      // if (!isLt2M) {
      //     this.$message.error("上传头像图片大小不能超过 200kb!");
      // }
      return isJPG && isLt2M;
    },
    loadProdPictureList(prodId) {
      let _self = this;
      this.$http.get("/api/production/GetProdImgList?prodId=" + prodId).then(
        function (res) {
          var result = res.data;
          if (result.ReturnCode == 1) {
            var data = result.Data;
            _self.prodPicList = data;
          } else {
            _self.prodPicList = [];
            _self.$alert(result.Message, " ", {
              type: "info",
              confirmButtonText: "确定"
            });
          }
        },
        function (err) {
          console.log(err);
        }
      );
    },
    test(){
        alert("11");
    }
  },
  mounted: function () {
    this.getProductionSelectData();
    this.getDataList();
  },
  computed: {
    fiteredTableData: function () {
      let _self = this;
      if (!this.curStypeTypeId) {
        return this.tableData;
      } else {
        return this.tableData.filter(item => {
          return item.DicTypeID == _self.curStypeTypeId;
        });
      }
    }
  }
};
</script>


<style>
.operation {
  cursor: pointer;
}
.table {
  width: 100%;
  margin-top: 10px;
}

.dicttable {
  width: 1200px;
  text-align: left;
  margin: 10px auto 0 auto;
  overflow: hidden;
  padding-bottom: 20px;
}
.label-width {
}
</style>


