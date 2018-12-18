webpackJsonp([10],{"6HB0":function(t,a,e){"use strict";Object.defineProperty(a,"__esModule",{value:!0});var l=e("woOf"),o=e.n(l),i={data:function(){return{tableData:[],curCategoryId:null,dialogmould:!1,editMouldIndex:-1,formData:{SerialNum:"",TypeID:"",Spec:"",Size:"",Creator:"",Height:"",RelateMaterial:"",Width:"",Id:"",FrontImgID:"",SideImgID:"",BackImgID:""},rules:{},mouldCategoryList:[],frontImgUrl:"",backImgUrl:"",sideImgUrl:""}},methods:{addMould:function(){this.editMouldIndex=-1,this.dialogmould=!0,this.formData={},this.frontImgUrl="",this.sideImgUrl="",this.backImgUrl=""},editMould:function(t,a){this.frontImgUrl="",this.sideImgUrl="",this.backImgUrl="",this.formData=o()({},t),this.editMouldIndex=this.tableData.indexOf(t),this.dialogmould=!0,this.formData.FrontImgID&&(this.frontImgUrl="/api/image?imgId="+this.formData.FrontImgID+"&data="+new Date),this.formData.SideImgID&&(this.sideImgUrl="/api/image?imgId="+this.formData.SideImgID+"&data="+new Date),this.formData.BackImgID&&(this.backImgUrl="/api/image?imgId="+this.formData.BackImgID+"&data="+new Date)},handleDelete:function(t,a){var e=this;this.$http.get("/api/material/DeleteItem?mouldId="+t.Id).then(function(a){var l=a.data;if(1==l.ReturnCode){var o=e.tableData.indexOf(t);e.tableData.splice(o,1)}else e.$alert(l.Message," ",{type:"info",confirmButtonText:"确定"})},function(t){console.log(t)})},saveMould:function(t){var a=this;this.$http.post("/api/mould",a.formData).then(function(t){var e=t.data;if(1==e.ReturnCode){var l=e.Data;-1==a.editMouldIndex?a.tableData.push(l):a.tableData.splice(a.editMouldIndex,1,l),a.dialogmould=!1,a.editMouldIndex=-1,a.formData={}}else a.$alert(e.Message," ",{type:"info",confirmButtonText:"确定"})},function(t){console.log(t)})},getMouldCategoryList:function(){var t=this;this.$http.get("/api/dict/GetDictByType?typeId=101").then(function(a){var e=a.data;1==e.ReturnCode?t.mouldCategoryList=e.Data:t.$alert(e.Message," ",{type:"info",confirmButtonText:"确定"})},function(t){console.log(t)})},getMouldList:function(){var t=this;this.$http.get("/api/mould/getMouldList").then(function(a){var e=a.data;1==e.ReturnCode?t.tableData=e.Data:t.$alert(e.Message," ",{type:"info",confirmButtonText:"确定"})},function(t){console.log(t)})},handleAvatarSuccess:function(t,a){if(1==t.ReturnCode){var e=t.Data.Id;switch(t.Data.Flag){case 0:this.formData.FrontImgID=e,this.frontImgUrl=URL.createObjectURL(a.raw);break;case 1:this.formData.SideImgID=e,this.sideImgUrl=URL.createObjectURL(a.raw);break;case 2:this.formData.BackImgID=e,this.backImgUrl=URL.createObjectURL(a.raw)}}else this.imageUrl=""},beforeAvatarUpload:function(t){var a="image/png"===t.type||"image/jpg"===t.type,e=t.size/1024<=200;return a||this.$message.error("上传头像图片只能是 JPG/png 格式!"),e||this.$message.error("上传头像图片大小不能超过 200kb!"),a&&e}},mounted:function(){this.getMouldCategoryList(),this.getMouldList()},computed:{fiteredTableData:function(){var t=this;return this.tableData.filter(function(a){return null==t.curCategoryId||a.TypeID==t.curCategoryId})}}},r={render:function(){var t=this,a=t.$createElement,e=t._self._c||a;return e("div",{staticClass:"dicttable dict-manager"},[e("table",{staticClass:"filter_table",attrs:{cellspacing:"0",cellpadding:"0"}},[e("tbody",[e("tr",{staticClass:"ct tb-tr"},[e("td",{staticClass:"name"},[t._v("模具类别：")]),t._v(" "),e("td",{staticStyle:{padding:"5px 0"}},[e("el-select",{staticClass:"w220",attrs:{placeholder:"请选择模具类别",filterable:""},model:{value:t.curCategoryId,callback:function(a){t.curCategoryId=a},expression:"curCategoryId"}},t._l(t.mouldCategoryList,function(t){return e("el-option",{key:t.Id,attrs:{label:t.TypeName,value:t.Id}})}))],1),t._v(" "),e("td",{staticClass:"name"},[t._v("编号：")]),t._v(" "),e("td",{staticStyle:{padding:"5px 0"}},[e("el-input",{staticClass:"w220",attrs:{clearable:""}})],1),t._v(" "),e("td",{staticClass:"name"},[t._v("帽围：")]),t._v(" "),e("td",{staticStyle:{padding:"5px 0"}},[e("el-input",{staticClass:"w220",attrs:{clearable:""}})],1)])])]),t._v(" "),e("div",{staticClass:"inquire"},[e("el-button",{staticClass:"added demand-add",attrs:{type:"primary"},on:{click:function(a){t.addMould()}}},[e("i",{staticClass:"el-icon-plus"}),t._v(" 新增模具\n        ")]),t._v(" "),e("el-button",{staticClass:"demand",attrs:{type:"primary"},on:{click:function(a){t.getMouldList()}}},[e("i",{staticClass:"el-icon-search"}),t._v(" 搜索\n        ")])],1),t._v(" "),e("div",{staticClass:"table"},[e("el-table",{ref:"singleTable",attrs:{data:t.fiteredTableData,border:"","highlight-current-row":""}},[e("el-table-column",{attrs:{type:"index",label:"序号",width:"80",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"SerialNum",label:"编号",width:"80",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"TypeName",label:"模具类别",width:"auto",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"Size",label:"尺寸",width:"100",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"Spec",label:"规格",width:"80",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"Creator",label:"制造者",width:"80",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"RelateMaterial",label:"配对面料",width:"100",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"Height",label:"帽头高度",width:"80",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{property:"Width",label:"帽边宽度",width:"100",align:"left"}}),t._v(" "),e("el-table-column",{attrs:{label:"用户操作",width:"120"},scopedSlots:t._u([{key:"default",fn:function(a){return[e("i",{staticClass:"el-icon-edit green operation",on:{click:function(e){t.editMould(a.row,a.$index)}}}),t._v(" "),e("span",{staticClass:"slash"}),t._v(" "),e("i",{staticClass:"el-icon-delete2 green operation",on:{click:function(e){t.handleDelete(a.row,a.$index)}}})]}}])})],1)],1),t._v(" "),e("el-dialog",{attrs:{title:-1==t.editMouldIndex?"新增":"编辑",visible:t.dialogmould},on:{"update:visible":function(a){t.dialogmould=a}}},[e("el-form",{ref:"formData",attrs:{model:t.formData,rules:t.rules}},[e("el-row",[e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"模具编号","label-width":"120px",prop:"SerialNum"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.SerialNum,callback:function(a){t.$set(t.formData,"SerialNum",a)},expression:"formData.SerialNum"}})],1)],1),t._v(" "),e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"模具类别","label-width":"120px",prop:"TypeID"}},[e("el-select",{staticClass:"w220",attrs:{placeholder:"请选择模具类别"},model:{value:t.formData.TypeID,callback:function(a){t.$set(t.formData,"TypeID",a)},expression:"formData.TypeID"}},t._l(t.mouldCategoryList,function(t){return e("el-option",{key:t.Id,attrs:{label:t.TypeName,value:t.Id}})}))],1)],1)],1),t._v(" "),e("el-row",[e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"尺寸","label-width":"120px",prop:"Size"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.Size,callback:function(a){t.$set(t.formData,"Size",a)},expression:"formData.Size"}})],1)],1),t._v(" "),e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"规格","label-width":"120px",prop:"Spec"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.Spec,callback:function(a){t.$set(t.formData,"Spec",a)},expression:"formData.Spec"}})],1)],1)],1),t._v(" "),e("el-row",[e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"制造者","label-width":"120px",prop:"Creator"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.Creator,callback:function(a){t.$set(t.formData,"Creator",a)},expression:"formData.Creator"}})],1)],1),t._v(" "),e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"配对面料","label-width":"120px",prop:"RelateMaterial"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.RelateMaterial,callback:function(a){t.$set(t.formData,"RelateMaterial",a)},expression:"formData.RelateMaterial"}})],1)],1)],1),t._v(" "),e("el-row",[e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"帽头高度","label-width":"120px",prop:"Height"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.Height,callback:function(a){t.$set(t.formData,"Height",a)},expression:"formData.Height"}})],1)],1),t._v(" "),e("el-col",{attrs:{span:11}},[e("el-form-item",{attrs:{label:"帽边宽度","label-width":"120px",prop:"Width"}},[e("el-input",{staticClass:"w220",attrs:{autocomplete:"off"},model:{value:t.formData.Width,callback:function(a){t.$set(t.formData,"Width",a)},expression:"formData.Width"}})],1)],1)],1),t._v(" "),e("el-row",[e("el-col",{attrs:{span:8}},[e("el-form-item",{staticClass:"center-text",attrs:{label:"","label-width":"0px",prop:"Height"}},[e("el-upload",{staticClass:"avatar-uploader",attrs:{action:"/api/mould/UploadMouldImg?flag=0","show-file-list":!1,"on-success":t.handleAvatarSuccess,"before-upload":t.beforeAvatarUpload,data:t.formData}},[t.frontImgUrl?e("img",{staticClass:"avatar",attrs:{src:t.frontImgUrl}}):e("i",{staticClass:"el-icon-plus avatar-uploader-icon"})]),t._v(" "),e("div",[t._v("正面图")])],1)],1),t._v(" "),e("el-col",{attrs:{span:8}},[e("el-form-item",{staticClass:"center-text",attrs:{label:"","label-width":"0px",prop:"Height"}},[e("el-upload",{staticClass:"avatar-uploader",attrs:{action:"/api/mould/UploadMouldImg?flag=1","show-file-list":!1,"on-success":t.handleAvatarSuccess,"before-upload":t.beforeAvatarUpload,data:t.formData}},[t.sideImgUrl?e("img",{staticClass:"avatar",attrs:{src:t.sideImgUrl}}):e("i",{staticClass:"el-icon-plus avatar-uploader-icon"})]),t._v(" "),e("div",[t._v("侧面图")])],1)],1),t._v(" "),e("el-col",{attrs:{span:8}},[e("el-form-item",{staticClass:"center-text",attrs:{label:"","label-width":"0px",prop:"Height"}},[e("el-upload",{staticClass:"avatar-uploader",attrs:{action:"/api/mould/UploadMouldImg?flag=2","show-file-list":!1,"on-success":t.handleAvatarSuccess,"before-upload":t.beforeAvatarUpload,data:t.formData}},[t.backImgUrl?e("img",{staticClass:"avatar",attrs:{src:t.backImgUrl}}):e("i",{staticClass:"el-icon-plus avatar-uploader-icon"})]),t._v(" "),e("div",[t._v("背面图")])],1)],1)],1)],1),t._v(" "),e("div",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[e("el-button",{on:{click:function(a){t.dialogmould=!1}}},[t._v("取 消")]),t._v(" "),e("el-button",{attrs:{type:"primary"},on:{click:function(a){t.saveMould("formData")}}},[t._v("确 定")])],1)],1)],1)},staticRenderFns:[]};var s=e("VU/8")(i,r,!1,function(t){e("rUC/")},null,null);a.default=s.exports},"rUC/":function(t,a){}});
//# sourceMappingURL=10.0410cad51f7179c82a18.js.map