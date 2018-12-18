<template>
    <div class="user user-manager">
        <!-- 查询模块 -->
        <div class="inquire">
            <el-button class="added demand-add" type="primary" @click="adduser()">
                <i class="el-icon-plus"></i>&nbsp;添加用户
            </el-button>
            <el-button class="demand" type="primary" @click="search()">
                <i class="el-icon-search"></i>&nbsp;查询
            </el-button>
        </div>
        <!-- 左侧导航 -->
        <div class="table">
            <el-table ref="singleTable" :data="tableData" border="" highlight-current-row>
                <el-table-column type="index" label="序号" width="80" align="left"></el-table-column>
                <el-table-column property="UserName" label="登录账号" width="120" align="left"></el-table-column>
                <el-table-column property="DisplayName" label="用户姓名" width="150" align="left"></el-table-column>
                <el-table-column property="PhoneNumber" label="电话" width="150" align="left"></el-table-column>
                <el-table-column property="Email" label="邮箱" width="auto" align="left"></el-table-column>
                <el-table-column property="Address" label="地址" width="auto" align="left"></el-table-column>
                <el-table-column label="用户操作" width="120">
                    <template slot-scope="scope">
                        <i class="el-icon-edit green operation" @click="edituser(scope.row)"></i>
                        <span class="slash"></span>
                        <i
                            class="el-icon-delete2 green operation"
                            @click="handleDelete(scope.row.Id,scope.$index)"
                        ></i>
                    </template>
                </el-table-column>
            </el-table>
            <!-- <div class="block green mt20">
                <el-pagination  @current-change="handleCurrentChange" :current-page="currentPage" :page-size="pageSize" :total="totalSize" @size-change="handleSizeChange":page-sizes="[10, 20, 30]" layout="total, sizes, prev, pager, next, jumper" >
                </el-pagination>
            </div>-->
        </div>
        <!-- <el-dialog title="用户角色编辑" :visible.sync="dialogroles">
            <div style="margin: 15px 0;"></div>
            <el-checkbox-group v-model="checkedRoles">
                <template v-for="(role,index) in rolelist">
                    <el-checkbox v-if="role.isChecked" checked :label="role.Name" :key="role.ID" @change="handlecheckedRolesChange(index)">{{role.Name}}</el-checkbox>
                    <el-checkbox v-else :label="role.Name" :key="role.ID" @change="handlecheckedRolesChange(index)">{{role.Name}}</el-checkbox>
                </template>
            </el-checkbox-group>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogroles = false">取 消</el-button>
                <el-button type="primary" @click="saveroles()">确 定</el-button>
            </div>
        </el-dialog>-->

        <el-dialog :title="curUserIndex==-1?'新增':'编辑'" :visible.sync="showdialoguser" >
            <el-form :model="formData" :rules="rules" ref="userform">
                <el-form-item label="登录账号" :label-width="formLabelWidth" prop="UserName">
                    <el-input v-model="formData.UserName" autocomplete="off" placeholder="登录账号只能包括字母和数字"></el-input>
                </el-form-item>
                <el-form-item label="用户姓名" :label-width="formLabelWidth" prop="DisplayName">
                    <el-input v-model="formData.DisplayName" autocomplete="off"></el-input>
                </el-form-item>
                <!-- <el-form-item label="用户密码" :label-width="formLabelWidth">
                    <el-input
                        placeholder="密码不少于6位且不多于16位"
                        v-model="formData.Password"
                        autocomplete="off"
                        name="password"
                        type="PassWord"
                        v-on:change="checkIntensity(formData.Password,'J-psw-varify-new')"
                    ></el-input>
                </el-form-item>
                <el-form-item label="密码强度" :label-width="formLabelWidth">
                    <div
                        class="J-psw-varify"
                        id="J-psw-varify-new"
                        border="0"
                        cellpadding="0"
                        cellspacing="0"
                    >
                        <div class="pwd pwd_c"></div>
                        <div class="pwd pwd_c pwd_f">无</div>
                        <div class="pwd pwd_c pwd_c_r"></div>
                    </div>
                </el-form-item>
                <el-form-item label="确认密码" :label-width="formLabelWidth">
                    <el-input v-model="formData.ConfirmPassword" autocomplete="off" type="PassWord"></el-input>
                </el-form-item> -->
                <el-form-item label="电话" :label-width="formLabelWidth">
                    <el-input v-model="formData.PhoneNumber" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="邮箱" :label-width="formLabelWidth">
                    <el-input v-model="formData.Email" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="地址" :label-width="formLabelWidth">
                    <el-input v-model="formData.Address" autocomplete="off"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="showdialoguser = false">取 消</el-button>
                <el-button type="primary" @click="saveUser('userform')">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>


import { sha256 } from "../../lib/sha256";

export default {
    props: {},
    data() {
        return {
            tableData: [],
            // currentRow: null,
            // currentPage: 1,
            // pageSize: 10,
            // input: '',
            // //totalSize: 0,//数据总条数
            defaultPassword:'123456',
            dialogroles: false,
            rolelist: [],

            showdialoguser: false,
            curUserIndex: -1,
            formData: {},

            formLabelWidth: "120px",

            rules: {
                UserName: [
                    {
                        required: true,
                        message: "请输入登录账号",
                        trigger: "blur"
                    },
                    {
                        type: "string",
                        pattern: /^[a-zA-Z0-9]*$/,
                        message: "请输入正确的登录账号",
                        trigger: "blur,change"
                    }
                ],
                DisplayName:[
                    {
                        required: true,
                        message: "请输入用户名称",
                        trigger: "blur"
                    }
                ]

            }
        };
    },
    computed: {
        // totalSize: function () {
        //     return this.$data.tableData.length;
        // },
        // showData: function () {
        //      var dt = this.$data.tableData.slice(this.$data.pageSize * (this.$data.currentPage - 1), this.$data.pageSize * this.$data.currentPage);
        //     var dt = this.$data.tableData.slice(this.$data.pageSize * (this.$data.currentPage - 1), this.$data.pageSize * this.$data.currentPage);
        //     return dt;
        // },
        // // 格式化数据源
        // data: function () {
        //     let me = this
        //     if (me.treeStructure) {
        //         let data = Utils.MSDataTransfer.treeToArray(me.dataSource, null, null, me.defaultExpandAll)
        //         return data
        //     }
        //     return me.dataSource
        // },
    },

    methods: {
        getUsers(groupid, searchKey) {
            var _self = this;
            this.$http.get("/api/user/GetUserList").then(
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

        getRoles() {
            var _self = this;
            this.$http.get("/api/user/GetUserRoleList").then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        _self.rolelist = result.Data;
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

        adduser() {
           this.curUserIndex=-1;
           this.showdialoguser=true;
           this.formData={};
        },
        edituser(row) {
            this.curUserIndex=this.tableData.indexOf(row);
            this.formData=Object.assign({},row);
            this.showdialoguser=true;
        },

        search() {
            this.getUsers();
        },
        saveUser(formName) {
            let _self = this;

            this.$refs[formName].validate(valid => {
                if (valid) {
                    this.$http.post("/api/user/PostUser", _self.formData).then(
                        function(res) {
                            var result = res.data;

                            if (result.ReturnCode == 1) {
                                var resultData = result.Data;
                                if (_self.curUserIndex === -1) {
                                    _self.tableData.push(resultData);
                                } else {
                                    _self.tableData.splice(
                                        _self.curUserIndex,
                                        1,
                                        resultData
                                    );
                                }
                                _self.showdialoguser = false;
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
        },

        checkIntensity(value, id) {
            var len = value.length;
            var level = 0, //程度
                i = 0,
                Modes = 0;
            if (value) {
                for (i = 0; i < len; i++) {
                    var charType = 0;
                    var t = value.charCodeAt(i);
                    if (t >= 48 && t <= 57) {
                        charType = 1;
                    } else if (t >= 65 && t <= 90) {
                        charType = 2;
                    } else if (t >= 97 && t <= 122) {
                        charType = 4;
                    } else {
                        charType = 4;
                    }
                    Modes |= charType;
                }
                for (i = 0; i < 4; i++) {
                    if (Modes & 1) {
                        level++;
                    }
                    Modes >>>= 1;
                }
                if (len <= 4) {
                    level = 1;
                }
            } else {
                level = 0;
            }

            var pswVarify = document.getElementById(id);
            var colorhtml = pswVarify.getElementsByClassName("pwd")[1];

            var clsN = this.clearPswVarify();
            switch (level) {
                case 1:
                    pswVarify.className = clsN + " J-weak";
                    colorhtml.innerHTML = "弱";
                    break;
                case 2:
                    pswVarify.className = clsN + " J-mediun";
                    colorhtml.innerHTML = "中";
                    break;
                case 3:
                    pswVarify.className = clsN + " J-strong";
                    colorhtml.innerHTML = "强";
                    break;
                default:
                    colorhtml.innerHTML = "无";
                    break;
            }
        },
        clearPswVarify() {
            var pswVarify = document.getElementsByClassName("J-psw-varify");
            var clsN = "";
            if (pswVarify) {
                for (var i = 0, len = pswVarify.length; i < len; i++) {
                    pswVarify[i].className = clsN = pswVarify[i].className
                        .replace(" J-weak", "")
                        .replace(" J-mediun", "")
                        .replace(" J-strong", "");
                }
            }
            return clsN;
        },

        handleDelete(id,index) {
            var _self = this;
            this.$confirm("此操作将永久删除该用户, 是否继续?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "error"
            })
                .then(() => {
                    var _self = this;
                    this.$http.get("/api/user/DeleteItem?itemId="+id).then(
                        function(res) {
                            var result = res.data;

                            if (result.ReturnCode == 1) {
                                _self.tableData.splice(index,1);
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

                    //刷新数据
                })
                .catch(() => {
                    this.$message({
                        type: "info",
                        message: "已取消删除"
                    });
                });
        }
    },

    mounted: function() {
        this.getUsers();
        this.getRoles();
    }
};
</script>


<style scoped>
</style>
<style>
.user {
    width: 1200px;
    text-align: left;
    margin: 10px auto 0 auto;
    overflow: hidden;
    padding-bottom: 20px;
}

.table {
    width: 100%;
    margin-top: 10px;
}

.ms-tree-space::before {
    content: "";
    /*content 属性与 :before 及 :after 伪元素配合使用，来插入生成内容。*/
}

table td {
    line-height: 26px;
}

.inquire {
    width: 1200px;
    margin: 0 auto;
    overflow: hidden;
}

.search {
    float: left;
    margin-left: 10px;
    width: 30%;
    height: 40px;
}
.demand-add {
    border: none;
    float: left;
    /*margin-left: 10px;*/
    background: #49c9c4;
}
.demand {
    border: none;
    float: left;
    margin-left: 10px;
    background: #49c9c4;
}

.demand-checkbox {
    border: none;
    float: left;
    margin-left: 10px;
    margin: 10px;
}
.slash {
    display: inline-block;
    *display: inline;
    *zoom: 1;
    width: 8px;
    height: 15px;
    border-right: 1px dashed #ddd;
    margin-right: 8px;
}

.pwd {
    width: 100px;
    height: 33px;
    line-height: 33px;
    float: left;
}
.pwd_f {
    color: #bbbbbb;
}
.pwd_c {
    background-color: #f3f3f3;
    border-top: 1px solid #d0d0d0;
    border-bottom: 1px solid #d0d0d0;
    border-left: 1px solid #d0d0d0;
}
.J-psw-varify {
    width: 306px;
    height: 35px;
    text-align: center;
}
.J-psw-varify.J-weak .pwd_c:nth-of-type(1) {
    background-color: #ff4545;
    border-top: 1px solid #bb2b2b;
    border-bottom: 1px solid #bb2b2b;
    border-left: 1px solid #bb2b2b;
}
.J-psw-varify.J-mediun .pwd_c:nth-of-type(1),
.J-psw-varify.J-mediun .pwd_c:nth-of-type(2) {
    background-color: #ffd35e;
    border-top: 1px solid #e9ae10;
    border-bottom: 1px solid #e9ae10;
    border-left: 1px solid #e9ae10;
    color: #fff;
}
.J-psw-varify.J-strong .pwd_c {
    background-color: #3abb1c;
    border-top: 1px solid #267a12;
    border-bottom: 1px solid #267a12;
    border-left: 1px solid #267a12;
    color: #fff;
}
.J-psw-varify.J-strong .pwd_c:nth-of-type(3) {
    border-right: 1px solid #267a12;
}

.pwd_c_r {
    border-right: 1px solid #d0d0d0;
}
.pwd_Weak_c_r {
    border-right: 1px solid #bb2b2b;
}
.pwd_Medium_c_r {
    border-right: 1px solid #e9ae10;
}
.pwd_Strong_c_r {
    border-right: 1px solid #267a12;
}
.operation {
    cursor: pointer;
}
</style>
