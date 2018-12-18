<template>
    <div class="dicttable dict-manager">
        <table class="filter_table" cellspacing="0" cellpadding="0">
            <tbody>
                <tr class="ct tb-tr">
                    <td class="name">模具类别：</td>
                    <td style="padding: 5px 0;">
                        <el-select
                            class="w220"
                            v-model="curCategoryId"
                            placeholder="请选择模具类别"
                            filterable
                        >
                            <el-option
                                v-for="item in mouldCategoryList"
                                :key="item.Id"
                                :label="item.TypeName"
                                :value="item.Id"
                            ></el-option>
                        </el-select>
                    </td>
                    <td class="name">编号：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                    <td class="name">帽围：</td>
                    <td style="padding: 5px 0;">
                        <el-input class="w220" clearable></el-input>
                    </td>
                </tr>
            </tbody>
        </table>
        <!-- 查询模块 -->
        <div class="inquire">
            <el-button class="added demand-add" type="primary" @click="addMould()">
                <i class="el-icon-plus"></i>&nbsp;新增模具
            </el-button>
            <el-button class="demand" type="primary" @click="getMouldList()">
                <i class="el-icon-search"></i>&nbsp;搜索
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="fiteredTableData" border="" highlight-current-row>
                <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
                <el-table-column property="SerialNum" label="编号" width="80" align="left"></el-table-column>
                <el-table-column property="TypeName" label="模具类别" width="auto" align="left"></el-table-column>
                <el-table-column property="Size" label="尺寸" width="100" align="left"></el-table-column>
                <el-table-column property="Spec" label="规格" width="80" align="left"></el-table-column>
                <el-table-column property="Creator" label="制造者" width="80" align="left"></el-table-column>
                <el-table-column property="RelateMaterial" label="配对面料" width="100" align="left"></el-table-column>
                <el-table-column property="Height" label="帽头高度" width="80" align="left"></el-table-column>
                <el-table-column property="Width" label="帽边宽度" width="100" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i
                            class="el-icon-edit green operation"
                            @click="editMould(scope.row,scope.$index)"
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
        <el-dialog :title="editMouldIndex==-1?'新增':'编辑'" :visible.sync="dialogmould">
            <el-form :model="formData" :rules="rules" ref="formData">
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="模具编号" label-width="120px" prop="SerialNum">
                            <el-input v-model="formData.SerialNum" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="模具类别" label-width="120px" prop="TypeID">
                            <el-select class="w220" v-model="formData.TypeID" placeholder="请选择模具类别">
                                <el-option
                                    v-for="item in mouldCategoryList"
                                    :key="item.Id"
                                    :label="item.TypeName"
                                    :value="item.Id"
                                ></el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="尺寸" label-width="120px" prop="Size">
                            <el-input v-model="formData.Size" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="规格" label-width="120px" prop="Spec">
                            <el-input v-model="formData.Spec" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="制造者" label-width="120px" prop="Creator">
                            <el-input v-model="formData.Creator" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="配对面料" label-width="120px" prop="RelateMaterial">
                            <el-input v-model="formData.RelateMaterial" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="帽头高度" label-width="120px" prop="Height">
                            <el-input v-model="formData.Height" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="帽边宽度" label-width="120px" prop="Width">
                            <el-input v-model="formData.Width" autocomplete="off"  class="w220"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="" label-width="0px" prop="Height" class="center-text">
                            <el-upload
                                class="avatar-uploader"
                                action="/api/mould/UploadMouldImg?flag=0"
                                :show-file-list="false"
                                :on-success="handleAvatarSuccess"
                                :before-upload="beforeAvatarUpload"
                                :data="formData"
                            >
                                <img v-if="frontImgUrl" :src="frontImgUrl" class="avatar">
                                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                            </el-upload>
                            <div>正面图</div>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="" label-width="0px" prop="Height" class="center-text">
                            <el-upload
                                class="avatar-uploader"
                                action="/api/mould/UploadMouldImg?flag=1"
                                :show-file-list="false"
                                :on-success="handleAvatarSuccess"
                                :before-upload="beforeAvatarUpload"
                                :data="formData"
                            >
                                <img v-if="sideImgUrl" :src="sideImgUrl" class="avatar">
                                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                            </el-upload>
                            <div>侧面图</div>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="" label-width="0px" prop="Height" class="center-text">
                            <el-upload
                                class="avatar-uploader"
                                action="/api/mould/UploadMouldImg?flag=2"
                                :show-file-list="false"
                                :on-success="handleAvatarSuccess"
                                :before-upload="beforeAvatarUpload"
                                :data="formData"
                            >
                                <img v-if="backImgUrl" :src="backImgUrl" class="avatar">
                                <!-- <i v-else class="el-icon-plus avatar-uploader-icon"></i> -->
                                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                            </el-upload>
                            <div>背面图</div>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogmould=false;">取 消</el-button>
                <el-button type="primary" @click="saveMould('formData')">确 定</el-button>
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

            dialogmould: false,
            editMouldIndex: -1,

            formData: {
                SerialNum: "",
                TypeID: "",
                Spec: "",
                Size: "",
                Creator: "",
                Height: "",
                RelateMaterial: "",
                Width: "",
                Id: "",
                FrontImgID: "",
                SideImgID: "",
                BackImgID: ""
            },
            rules: {},

            mouldCategoryList: [],
            frontImgUrl: "",
            backImgUrl: "",
            sideImgUrl: ""
        };
    },
    methods: {
        addMould() {
            //this.formData.DicTypeId=this.curCategoryType;
            this.editMouldIndex = -1;
            this.dialogmould = true;
            this.formData = {};
            this.frontImgUrl = "";
            this.sideImgUrl = "";
            this.backImgUrl = "";
            //this.$data.dialogmould
        },
        editMould(item, index) {
            this.frontImgUrl = "";
            this.sideImgUrl = "";
            this.backImgUrl = "";
            this.formData = Object.assign({}, item);
            this.editMouldIndex = this.tableData.indexOf(item);
            this.dialogmould = true;
            if (this.formData.FrontImgID) {
                this.frontImgUrl =
                    "/api/image?imgId=" + this.formData.FrontImgID+"&data="+new Date();
            } 
            if (this.formData.SideImgID) {
                this.sideImgUrl = "/api/image?imgId=" + this.formData.SideImgID+"&data="+new Date();;
            }
            if (this.formData.BackImgID) {
                this.backImgUrl = "/api/image?imgId=" + this.formData.BackImgID+"&data="+new Date();;
            } 
        },
        handleDelete(item, index) {
            let _self = this;
            this.$http.get("/api/material/DeleteItem?mouldId=" + item.Id).then(
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
        saveMould(formData) {
            let _self = this;
            this.$http.post("/api/mould", _self.formData).then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        var resultData = result.Data;

                        if (_self.editMouldIndex == -1) {
                            _self.tableData.push(resultData);
                        } else {
                            _self.tableData.splice(
                                _self.editMouldIndex,
                                1,
                                resultData
                            );
                        }

                        _self.dialogmould = false;
                        _self.editMouldIndex = -1;
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

        getMouldCategoryList() {
            let _self = this;
            this.$http.get("/api/dict/GetDictByType?typeId=101").then(
                function(res) {
                    var result = res.data;
                    if (result.ReturnCode == 1) {
                        _self.mouldCategoryList = result.Data;
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
        getMouldList() {
            let _self = this;
            this.$http.get("/api/mould/getMouldList").then(
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
                var imgId = res.Data.Id;
                switch (res.Data.Flag) {
                    case 0:
                        this.formData.FrontImgID = imgId;
                        this.frontImgUrl = URL.createObjectURL(file.raw);
                        break;
                    case 1:
                        this.formData.SideImgID = imgId;
                        this.sideImgUrl = URL.createObjectURL(file.raw);
                        break;
                    case 2:
                        this.formData.BackImgID = imgId;
                        this.backImgUrl = URL.createObjectURL(file.raw);
                        break;
                    default:
                        break;
                }
            } else {
                this.imageUrl = "";
            }
        },
        beforeAvatarUpload(file) {
            const isJPG =
                file.type === "image/png" || file.type === "image/jpg";
            const isLt2M = file.size / 1024 <= 200;
            //const isLt2M = true;

            if (!isJPG) {
                this.$message.error("上传头像图片只能是 JPG/png 格式!");
            }
            if (!isLt2M) {
                this.$message.error("上传头像图片大小不能超过 200kb!");
            }
            return isJPG && isLt2M;
        }
    },
    mounted: function() {
        //this.getMouldList();

        this.getMouldCategoryList();
        this.getMouldList();
    },
    computed: {
        fiteredTableData: function() {
            let _self = this;
            return this.tableData.filter(item => {
                return (
                    _self.curCategoryId == null ||
                    item.TypeID == _self.curCategoryId
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

.center-text {
    text-align: center;
}
</style>


