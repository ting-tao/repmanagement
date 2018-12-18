<template>
    <div class="dicttable dict-manager">
        <table class="filter_table" cellspacing="0" cellpadding="0">
            <tbody>
                <tr class="ct tb-tr">
                    <td class="name">编号：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                    <td class="name">色号：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                    <td class="name">规格：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                </tr>
                <tr class="ct tb-tr">
                    <td class="name">物料类别：</td>
                    <td style="padding: 5px 0;">
                        <el-select
                            class="w220"
                            v-model="curCategoryId"
                            placeholder="请选择物料类别"
                            filterable
                            @change="getMaterialList"
                            @clear="getMaterialList"
                        >
                            <el-option
                                v-for="item in materialCategoryList"
                                :key="item.Id"
                                :label="item.TypeName"
                                :value="item.Id"
                            ></el-option>
                        </el-select>
                    </td>
                    <td class="name">所属供应商:</td>
                    <td style="padding: 5px 0;">
                        <el-select
                            class="w220"
                            v-model="curVendorId"
                            placeholder="请选择供应商"
                            filterable
                            @change="getMaterialList"
                            @clear="getMaterialList"
                        >
                            <el-option
                                v-for="item in vendorList"
                                :key="item.Id"
                                :label="item.VendorName"
                                :value="item.Id"
                            >
                                <span style="float: left">{{ item.VendorName }}</span>
                                <span
                                    style="float: right; color: #8492a6; font-size: 13px"
                                >{{ item.SerialNum }}</span>
                            </el-option>
                        </el-select>
                    </td>
                    <td class="name">克重：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                </tr>
            </tbody>
        </table>
        <!-- 查询模块 -->
        <div class="inquire" style="margin-top:10px">
            <el-button class="added demand-add" type="primary" @click="addMaterial()">
                <i class="el-icon-plus"></i>&nbsp;新增物料
            </el-button>
            <el-button class="demand" type="primary" @click="getMaterialList()">
                <i class="el-icon-search"></i>&nbsp;搜索
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="fiteredTableData" border="" highlight-current-row>
                <el-table-column label="图文" width="100" fixed>
                    <template slot-scope="scope">
                        <img
                            v-if="scope.row.IconID"
                            :src="'/api/image?imgId='+scope.row.IconID"
                            style="width:50px;heigh:50px"
                        >
                    </template>
                </el-table-column>
                <el-table-column type="index" label="序号" width="80" fixed align="left"></el-table-column>
                <el-table-column property="SerialNum" label="编号" width="80" fixed align="left"></el-table-column>
                <el-table-column property="TypeName" label="物料类别" width="auto" fixed align="left"></el-table-column>
                <el-table-column property="VendorNum" label="供应商" width="100" align="left"></el-table-column>
                <el-table-column property="MaterialQuality" label="材质" width="80" align="left"></el-table-column>
                <el-table-column property="Spec" label="规格" width="80" align="left"></el-table-column>
                <el-table-column property="OpenSize" label="开边尺寸" width="100" align="left"></el-table-column>
                <el-table-column property="Weight" label="克重" width="80" align="left"></el-table-column>
                <el-table-column property="ColorNum" label="色号" width="100" align="left"></el-table-column>
                <el-table-column property="Color" label="颜色" width="100" align="left"></el-table-column>
                <el-table-column property="Price" label="单价" width="100" align="left"></el-table-column>
                <el-table-column property="LeftCount" label="库存" width="100" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i
                            class="el-icon-edit green operation"
                            @click="editMaterial(scope.row,scope.$index)"
                        ></i>
                        <span class="slash"></span>
                        <i
                            class="el-icon-delete2 green operation"
                            @click="handleDelete(scope.row,scope.$index)"
                        ></i>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <el-dialog
            :title="editMaterialIndex==-1?'新增':'编辑'"
            :visible.sync="dialogmaterial"
            class="material-dlg"
        >
            <el-form :model="formData" :rules="rules" ref="formData">
                <el-row>
                    <el-col :span="24">
                        <el-form-item label="材料编号" label-width="120px" prop="SerialNum">
                            <el-input v-model="formData.SerialNum" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="材料类别" label-width="120px" prop="TypeID">
                            <el-select class="w220" v-model="formData.TypeID" placeholder="请选择材料类别">
                                <el-option
                                    v-for="item in materialCategoryList"
                                    :key="item.Id"
                                    :label="item.TypeName"
                                    :value="item.Id"
                                ></el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="供应商" label-width="120px" prop="VendorID">
                            <el-select
                                class="w220"
                                v-model="formData.VendorID"
                                placeholder="请选择供应商"
                            >
                                <el-option
                                    v-for="item in vendorList"
                                    :key="item.Id"
                                    :label="item.VendorName"
                                    :value="item.Id"
                                >
                                    <span style="float: left">{{ item.VendorName }}</span>
                                    <span
                                        style="float: right; color: #8492a6; font-size: 13px"
                                    >{{ item.SerialNum }}</span>
                                </el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="物料材质" label-width="120px" prop="MaterialQuality">
                            <el-input v-model="formData.MaterialQuality" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="规格" label-width="120px" prop="Spec">
                            <el-input v-model="formData.Spec" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="开边尺寸" label-width="120px" prop="OpenSize">
                            <el-input v-model="formData.OpenSize" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="克重" label-width="120px" prop="Weight">
                            <el-input v-model="formData.Weight" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="色卡号" label-width="120px" prop="ColorNum">
                            <el-input v-model="formData.ColorNum" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="颜色" label-width="120px" prop="Color">
                            <el-input v-model="formData.Color" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="单价" label-width="120px" prop="Price">
                            <el-input v-model="formData.Price" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="库存量" label-width="120px" prop="LeftCount">
                            <el-input style="w220" v-model="formData.LeftCount" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="图片" label-width="120px">
                            <el-upload
                                class="avatar-uploader"
                                action="/api/material/UploadMaterialImg?isFlag=true"
                                :show-file-list="false"
                                :on-success="handleAvatarSuccess"
                                :before-upload="beforeAvatarUpload"
                                :data="formData"
                            >
                                <img v-if="imageUrl" :src="imageUrl" class="avatar">
                                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                            </el-upload>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogmaterial = false">取 消</el-button>
                <el-button type="primary" @click="saveMaterial('formData')">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
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
            curCategoryId: null,
            curVendorId: null,
            dialogmaterial: false,
            editMaterialIndex: -1,

            imageUrl: "",

            formData: {
                SerialNum: "",
                TypeID: "",
                VendorID: "",
                MaterialQuality: "",
                Spec: "",
                OpenSize: "",
                Weight: "",
                ColorNum: "",
                Color: "",
                Price: "",
                LeftCount: "",
                IconID: "",
                Id: ""
            },
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

            vendorList: [],
            materialCategoryList: []
        };
    },
    methods: {
        addMaterial() {
            //this.formData.DicTypeId=this.curCategoryType;
            this.editMaterialIndex = -1;
            this.dialogmaterial = true;

            //this.$data.dialogmaterial
        },
        editMaterial(item, index) {
            this.imageUrl="";
            this.formData = Object.assign({},item);
            if(this.formData.IconID){
                this.imageUrl='/api/image?imgId='+this.formData.IconID+"&date="+new Date();
            }
            this.editMaterialIndex = this.tableData.indexOf(item);
            this.dialogmaterial = true;
        },
        handleDelete(item, index) {
            let _self = this;
            this.$http
                .get("/api/material/DeleteItem?materialId=" + item.Id)
                .then(
                    function(res) {
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
                    function(err) {
                        console.log(err);
                    }
                );
        },
        saveMaterial(formData) {
            let _self = this;
            this.$http.post("/api/material", _self.formData).then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        var resultData = result.Data;

                        if (_self.editMaterialIndex == -1) {
                            _self.tableData.push(resultData);
                        } else {
                            _self.tableData.splice(
                                _self.editMaterialIndex,
                                1,
                                resultData
                            );
                        }

                        _self.dialogmaterial = false;
                        _self.editMaterialIndex = -1;
                        _self.formData = {};
                    } else {
                        _self.$alert(result.Message, " ", {
                            type: "info",
                            confirmButtonText: "确定"
                        });
                    }
                },
                function(err) {
                    console.log(err);
                }
            );
        },

        getVendorList() {
            let _self = this;
            this.$http.get("/api/vendor/GetVendorForMaterial").then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        _self.vendorList = result.Data;
                    } else {
                        _self.$alert(result.Message, " ", {
                            type: "info",
                            confirmButtonText: "确定"
                        });
                    }
                },
                function(err) {
                    console.log(err);
                }
            );
        },
        getMaterialCategoryList() {
            let _self = this;
            this.$http.get("/api/dict/GetDictByType?typeId=100").then(
                function(res) {
                    var result = res.data;
                    if (result.ReturnCode == 1) {
                        _self.materialCategoryList = result.Data;
                    } else {
                        _self.$alert(result.Message, " ", {
                            type: "info",
                            confirmButtonText: "确定"
                        });
                    }
                },
                function(err) {
                    console.log(err);
                }
            );
        },
        getMaterialList() {
            let _self = this;
            this.$http.get("/api/material/GetMaterialList").then(
                function(res) {
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
                function(err) {
                    console.log(err);
                }
            );
        },
        handleAvatarSuccess(res, file) {
            //var data=res.data;
            if (res.ReturnCode == 1) {
                this.formData.IconID = res.Data;
                this.imageUrl = URL.createObjectURL(file.raw);
            } else {
                this.imageUrl = "";
            }
        },
        beforeAvatarUpload(file) {
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
        }
    },
    mounted: function() {
        //this.getMaterialList();
        this.getVendorList();
        this.getMaterialCategoryList();
        this.getMaterialList();
    },
    computed: {
        fiteredTableData: function() {
            let _self = this;
            return this.tableData.filter(item => {
                return (
                    (_self.curCategoryId == null ||
                        item.TypeID == _self.curCategoryId) &&
                    (_self.curVendorId == null ||
                        _self.curVendorId == item.VendorID)
                );
            });
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

.material-dlg .el-input {
    width: 220px;
}

.avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
}
.avatar-uploader .el-upload:hover {
    border-color: #409eff;
}
.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 108px;
    height: 108px;
    line-height: 108px;
    text-align: center;
}
.avatar {
    width: 108px;
    height: 108px;
    display: block;
}
</style>


