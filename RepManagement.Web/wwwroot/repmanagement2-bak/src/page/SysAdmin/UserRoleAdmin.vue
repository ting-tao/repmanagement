<template>
    <div class="dicttable dict-manager">
        
        <!-- 查询模块 -->
        <div class="inquire">
            <el-button class="added demand-add" type="primary" @click="addRole()">
                <i class="el-icon-plus"></i>&nbsp;新增角色
            </el-button>
            <el-button class="demand" type="primary" @click="getDataList()">
                <i class="el-icon-search"></i>&nbsp;刷新
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="tableData" border="" highlight-current-row>
                <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
                <el-table-column property="RoleName" label="角色名称" width="120" align="left"></el-table-column>
                <el-table-column property="RoleType" label="角色数值" width="150" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i class="el-icon-edit green operation" @click="editRole(scope.row)"></i>
                        
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <el-dialog :title="editRoleIndex==-1?'新增':'编辑'" :visible.sync="dialogrole">
            <el-form :model="formData" :rules="rules" ref="formData">
               
                <el-form-item label="角色名称" label-width="120px" prop="RoleName">
                    <el-input v-model="formData.RoleName" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="角色数值" label-width="120px">
                    <el-input v-model="formData.RoleType" autocomplete="off" readonly></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogrole = false">取 消</el-button>
                <el-button type="primary" @click="saveRole('formData')">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { dictTypes, categoryTypeList } from "../../lib/consts";

import { copyObjectValue } from "../../lib/mUtils";

export default {
    data() {
        
        return {
            tableData: [],

            categoryTypeList: categoryTypeList,
            curCategoryType: null,
            dialogrole: false,
            
            editRoleIndex:-1,
            formData: {
                DicTypeID: "",
                TypeName: "",
                Description: "",
                Id: "",
                region: ""
            },
            rules: {
                RoleName: [
                    {
                        required: true,
                        message: "请输入显示名称",
                        trigger: "blur"
                    }
                ]
               
            }
        };
    },
    methods: {
        getDataList() {
            let _self = this;
            this.$http.get("/api/user/GetUserRoleList").then(
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
        addRole() {
            //this.formData.DicTypeId=this.curCategoryType;
            this.formData={};
            this.dialogrole = true;
            this.editRoleIndex=-1;
            //this.$data.dialogrole
        },
        editRole(item) {
            this.formData = Object.assign({},item);
            this.editRoleIndex=this.tableData.indexOf(item);
            this.dialogrole = true;
        },
        saveRole(formData) {
            let _self = this;

            this.$refs[formData].validate(valid => {
                if (valid) {
                    this.$http.post("/api/user/SaveRole", _self.formData).then(
                        function(res) {
                            var result = res.data;

                            if (result.ReturnCode == 1) {
                                var resultData = result.Data;
                                if (_self.editRoleIndex === -1) {
                                    _self.tableData.push(resultData);
                                } else {
                                    _self.tableData.splice(
                                        _self.editRoleIndex,
                                        1,
                                        resultData
                                    );
                                }
                                _self.dialogrole = false;
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
    margin:  0 auto;
    overflow: hidden;
    padding-bottom: 20px;
}
</style>


