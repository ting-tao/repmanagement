<template>
  <div class="dicttable dict-manager">
    <!-- 查询模块 -->
    <!-- <div class="inquire">
      <el-button class="added demand-add" type="primary" @click="addRole()">
        <i class="el-icon-plus"></i>&nbsp;新增角色
      </el-button>
      <el-button class="demand" type="primary" @click="getDataList()">
        <i class="el-icon-search"></i>&nbsp;刷新
      </el-button>
    </div> -->
    <!-- 左侧导航 -->
    <div class="table">

        <div v-for="prodItem in tableData" :key="prodItem.Id" class="div-proditem">
            <img :src="'/api/image/ShowProdPic?prodPicId='+prodItem.ImgId">

            <div class='div-prodinfo'>
                <span>{{prodItem.SerialNum}}</span> <span>{{prodItem.Color}}</span> <span>{{prodItem.MatName}}</span>
            </div>
        </div>
      <!-- <el-row>
        <el-col v-for="prodItem in tableData" :key="prodItem.Id"> -->
          <!-- <el-card v-for="prodItem in tableData" :key="prodItem.Id" :body-style="{ padding: '20px' }">
            <img :src="'/api/image/ShowProdPic?prodPicId='+prodItem.ImgId" class="image">
            <div style="padding: 14px;">
             
              <div class="bottom clearfix">
               <span>{{SerialNum}}</span> <span>{{Color}}</span> <span>{{MatName}}</span>
              </div>
            </div>
          </el-card> -->
        <!-- </el-col>
      </el-row> -->
    </div>
  </div>
</template>

<script>
import { dictTypes, categoryTypeList } from "../../lib/consts";

import { copyObjectValue } from "../../lib/mUtils";

export default {
  data() {

    return {
      tableData:[]
    };
  },
  methods: {
    getDataList() {
      let _self = this;
      this.$http.get("/api/production/GetProdListByImg").then(
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
    
  },
  mounted: function () {
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
  display: flex;
  display: -webkit-flex;
}

.dicttable {
  width: 1200px;
  text-align: left;
  margin: 10px auto 0 auto;
  overflow: hidden;
  padding-bottom: 20px;
}


  
.div-proditem{
    width:200px;
    height: 230px;
    text-align: center;
    margin: 20px 10px;
    border: 1px solid #c0ccda;
    position: relative;
}

.div-proditem img{
    width:180px;
    height: 180px;
    margin: 10px auto;
}

.div-prodinfo{
    position: absolute; 
    height: 30px;
    top:200px;
    width: 100%;
}

</style>


