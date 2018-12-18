<template>
    <div class="dicttable dict-manager">
        <table class="filter_table" cellspacing="0" cellpadding="0">
            <tbody>
                <tr class="ct tb-tr">
                    <td class="name">字典类别 ：</td>
                    <td style="padding: 5px 0;">
                        <el-select
                            class="w220"
                            v-model="curCategoryType"
                            placeholder="请选择字典类别"
                            filterable
                            @change="getDataList"
                            @clear="getDataList"
                            clearable
                        >
                            <el-option
                                v-for="item in categoryTypeList"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                            ></el-option>
                        </el-select>
                    </td>
                </tr>
            </tbody>
        </table>
        <!-- 查询模块 -->
        <div class="inquire">
            <el-button class="added demand-add" type="primary" @click="addDict()">
                <i class="el-icon-plus"></i>&nbsp;新增类别
            </el-button>
            <el-button class="demand" type="primary" @click="getDataList()">
                <i class="el-icon-search"></i>&nbsp;刷新
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="fiteredTableData" border="" highlight-current-row>
                <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
                <el-table-column property="DictTypeName" label="字典类别" width="120" align="left"></el-table-column>
                <el-table-column property="TypeName" label="显示名称" width="150" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i class="el-icon-edit green operation" @click="editDict(scope.row)"></i>
                        <span class="slash"></span>
                        <i
                            class="el-icon-delete2 green operation"
                            @click="handleDelete(scope.row,scope.$index)"
                        ></i>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <el-dialog :title="editDictIndex==-1?'新增':'编辑'" :visible.sync="dialogdict">
            <el-form :model="formData" :rules="rules" ref="formData">
                <el-form-item label="所属类别" label-width="120px" prop="DicTypeID">
                    <el-select class="w220" v-model="formData.DicTypeID" placeholder="请选择字典类别">
                        <el-option
                            v-for="item in categoryTypeList"
                            :key="item.id"
                            :label="item.name"
                            :value="item.id"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="显示名称" label-width="120px" prop="TypeName">
                    <el-input v-model="formData.TypeName" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="描述" label-width="120px">
                    <el-input type="textarea" v-model="formData.Description" autocomplete="off"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogdict = false">取 消</el-button>
                <el-button type="primary" @click="saveDict('formData')">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { dictTypes, categoryTypeList } from "../../lib/consts";

import { copyObjectValue } from "../../lib/mUtils";

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
            curCategoryType: null,
            dialogdict: false,
            
            editDictIndex:-1,
            formData: {
                DicTypeID: "",
                TypeName: "",
                Description: "",
                Id: "",
                region: ""
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
            }
        };
    },
    methods: {
        getDataList() {
            let _self = this;
            this.$http.get("/api/dict").then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        _self.tableData = result.Data.map(item => {
                            item.DictTypeName =
                                _self.categoryTypeList[
                                    item.DicTypeID - 100
                                ].name;
                            return item;
                        });
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
        addDict() {
            //this.formData.DicTypeId=this.curCategoryType;
            this.formData={};
            this.dialogdict = true;
            this.editDictIndex=-1;
            //this.$data.dialogdict
        },
        editDict(item) {
            this.formData = Object.assign({},item);
            this.editDictIndex=this.tableData.indexOf(item);
            this.dialogdict = true;
        },
        handleDelete(item, index) {
            let _self = this;
            this.$http.get("/api/dict/DeleteDict?id=" + item.Id).then(
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
        saveDict(formData) {
            let _self = this;
            //_self.formData.DicTypeId=this.curCategoryType;

            this.$refs[formData].validate(valid => {
                if (valid) {
                    this.$http.post("/api/dict", _self.formData).then(
                        function(res) {
                            var result = res.data;

                            if (result.ReturnCode == 1) {
                                var resultData = result.Data;
                                resultData.DictTypeName =
                                    _self.categoryTypeList[
                                        resultData.DicTypeID - 100
                                    ].name;

                                
                                if (_self.editDictIndex === -1) {
                                    _self.tableData.push(resultData);
                                } else {
                                    _self.tableData.splice(
                                        _self.editDictIndex,
                                        1,
                                        resultData
                                    );
                                }
                                _self.dialogdict = false;
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
                } else {
                    return false;
                }
            });
        }
    },
    mounted: function() {
        this.getDataList();
    },
    computed: {
        fiteredTableData: function() {
            let _self = this;
            if (!this.curCategoryType) {
                return this.tableData;
            } else {
                return this.tableData.filter(item => {
                    return item.DicTypeID == _self.curCategoryType;
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
</style>


