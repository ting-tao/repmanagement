<template>
    <div class="dicttable dict-manager">
        <table class="filter_table" cellspacing="0" cellpadding="0">
            <tbody>
                <tr class="ct tb-tr">
                    <td class="name">客户类别 ：</td>
                    <td style="padding: 5px 0;">
                        <el-select
                            class="w220"
                            v-model="curCustomerType"
                            placeholder="请选择客户类别"
                            filterable
                            @change="getDataList"
                            @clear="getDataList"
                            clearable
                        >
                            <el-option
                                v-for="item in customerTypeList"
                                :key="item.Id"
                                :label="item.TypeName"
                                :value="item.Id"
                            ></el-option>
                        </el-select>
                    </td>
                    <td class="name">编号 ：</td>
                    <td>
                        <el-input class="w220" clearable/>
                    </td>
                    <td class="name">名称 ：</td>
                    <td>
                        <el-input class="w220" clearable/>
                    </td>
                </tr>
            </tbody>
        </table>
        <!-- 查询模块 -->
        <div class="inquire">
            <el-button class="added demand-add" type="primary" @click="addCustomer()">
                <i class="el-icon-plus"></i>&nbsp;新增客户
            </el-button>
            <el-button class="demand" type="primary" @click="getDataList()">
                <i class="el-icon-search"></i>&nbsp;搜索
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="fiteredTableData" border="" highlight-current-row>
                <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
                <el-table-column property="SerialNum" label="编号" width="80" align="left"></el-table-column>
                <el-table-column property="CustomerName" label="名称" width="auto" align="left"></el-table-column>
                <el-table-column property="TypeName" label="类别" width="80" align="left"></el-table-column>
                <el-table-column property="Style" label="风格" width="80" align="left"></el-table-column>
                <el-table-column property="Level" label="档次" width="80" align="left"></el-table-column>
                <el-table-column property="Amount" label="销量" width="80" align="left"></el-table-column>
                <el-table-column property="Demand" label="需求" width="80" align="left"></el-table-column>
                <el-table-column property="Years" label="合作年份" width="100" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i
                            class="el-icon-edit green operation"
                            @click="editCustomer(scope.row,scope.$index)"
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
        <el-dialog :title="dialogTitle" :visible.sync="dialogCustomer">
            <el-form :model="formData" :rules="rules" ref="formData">
                <el-row>
                    <el-col :span="24">
                        <el-form-item label="编号" label-width="120px" prop="SerialNum">
                            <el-input v-model="formData.SerialNum" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-form-item label="名称" label-width="120px" prop="CustomerName">
                            <el-input v-model="formData.CustomerName" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <!-- <el-row>
                    <el-col :span="24"></el-col>
                </el-row>-->
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="类别" label-width="120px" prop="TypeID">
                            <el-select class="w220" v-model="formData.TypeID" placeholder="请选择客户类别">
                                <el-option
                                    v-for="item in customerTypeList"
                                    :key="item.Id"
                                    :label="item.TypeName"
                                    :value="item.Id"
                                    clearable
                                ></el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="风格" label-width="120px" prop="Style">
                            <el-input v-model="formData.Style" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="档次" label-width="120px" prop="Level">
                            <el-input v-model="formData.Level" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="销量" label-width="120px" prop="Amount">
                            <el-input v-model="formData.Amount" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="客户评价" label-width="120px" prop="CustomerEval">
                    <el-input v-model="formData.CustomerEval" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="工厂评价" label-width="120px" prop="FactoryEval">
                    <el-input v-model="formData.FactoryEval" autocomplete="off"></el-input>
                </el-form-item>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="需求" label-width="120px" prop="Demand">
                            <el-input v-model="formData.Demand" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="联系人/电话" label-width="120px" prop="ContactPhone">
                            <el-input v-model="formData.ContactPhone" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="公司负责人" label-width="120px" prop="ManagerName">
                            <el-input v-model="formData.ManagerName" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="负责人/电话" label-width="120px" prop="ManagerPhone">
                            <el-input v-model="formData.ManagerPhone" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="11">
                        <el-form-item label="地址" label-width="120px" prop="Address">
                            <el-input v-model="formData.Address" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="11">
                        <el-form-item label="合作年份" label-width="120px" prop="Years">
                            <el-input v-model="formData.Years" autocomplete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogCustomer = false">取 消</el-button>
                <el-button type="primary" @click="saveCustomer('formData')">确 定</el-button>
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
            curCustomerType: null,
            dialogCustomer: false,
            dialogTitle: "新增",
            editItemIndex: -1,

            formData: {
                SerialNum: "",
                CustomerName: "",
                TypeID: "",
                Style: "",
                Level: "",
                Amount: "",
                Demand: "",
                CustomerEval: "",
                FactoryEval: "",
                FactoryEval: "",
                ContactPhone: "",
                ManagerPhone: "",
                ManagerName: "",
                Address: "",
                Years: ""
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

            customerTypeList: []
        };
    },
    methods: {
        getDataList() {
            let _self = this;
            this.$http.get("/api/customer/GetCustomerList").then(
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
        addCustomer() {
            //this.formData.DicTypeId=this.curCustomerType;
            this.dialogTitle = "新增";
            this.dialogCustomer = true;
            this.editItemIndex = -1;

            //this.$data.dialogCustomer
        },
        editCustomer(item, index) {
            this.formData = Object.assign({},item);
            this.dialogTitle = "编辑";
            this.editItemIndex = this.tableData.indexOf(item);
            this.dialogCustomer = true;
        },
        handleDelete(item, index) {
            let _self = this;
            this.$http.get("/api/vendor/DeleteVendor?vendorId=" + item.Id).then(
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
        saveCustomer(formData) {
            let _self = this;

            this.$http.post("/api/customer", _self.formData).then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        var resultData = result.Data;

                        if (_self.editItemIndex == -1) {
                            _self.tableData.push(resultData);
                        } else {
                            _self.tableData.splice(
                                _self.editItemIndex,
                                1,
                                resultData
                            );
                        }

                        _self.dialogCustomer = false;
                        _self.editItemIndex = -1;
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

        getcustomerTypeList() {
            let _self = this;
            this.$http.get("/api/dict/GetDictByType?typeId=103").then(
                function(res) {
                    var result = res.data;
                    if (result.ReturnCode == 1) {
                        _self.customerTypeList = result.Data;
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
        }
    },
    mounted: function() {
        this.getcustomerTypeList();
        this.getDataList();
    },
    computed: {
        fiteredTableData: function() {
            let _self = this;
            if (!this.curCustomerType) {
                return this.tableData;
            } else {
                return this.tableData.filter(item => {
                    return item.DicTypeID == _self.curCustomerType;
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


