webpackJsonp([1],{"9bBU":function(e,t,a){a("mClu");var l=a("FeBl").Object;e.exports=function(e,t,a){return l.defineProperty(e,t,a)}},C4MV:function(e,t,a){e.exports={default:a("9bBU"),__esModule:!0}},N2T3:function(e,t,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var l=a("woOf"),o=a.n(l),r=a("bOdI"),n=a.n(r),i=a("IqEw"),s={data:function(){var e;return{tableData:[],categoryTypeList:i.a,curVendorType:null,dialogVendor:!1,dialogTitle:"新增",editItemIndex:-1,formData:(e={SerialNum:"",VendorName:"",TypeID:"",Style:"",Level:"",Production:"",Deadline:"",CustomerEval:"",FactoryEval:""},n()(e,"FactoryEval",""),n()(e,"ContactPhone",""),n()(e,"ManagerPhone",""),n()(e,"ManagerName",""),n()(e,"Address",""),n()(e,"Years",""),e),rules:{TypeName:[{required:!0,message:"请输入显示名称",trigger:"blur"}],DicTypeID:[{message:"请选择字典类别",validator:function(e,t,a){t?a():a(new Error("字典类型不能为空"))}}]},vendorTypeList:[]}},methods:{getDataList:function(){var e=this;this.$http.get("/api/vendor/GetVendorList").then(function(t){var a=t.data;1==a.ReturnCode?e.tableData=a.Data:e.$alert(a.Message," ",{type:"info",confirmButtonText:"确定"})},function(e){console.log(e)})},addVendor:function(){this.dialogTitle="新增",this.dialogVendor=!0,this.editItemIndex=-1},editVendor:function(e,t){this.formData=o()({},e),this.dialogTitle="编辑",this.editItemIndex=this.tableData.indexOf(e),this.dialogVendor=!0},handleDelete:function(e,t){var a=this;this.$http.get("/api/vendor/DeleteVendor?vendorId="+e.Id).then(function(t){var l=t.data;if(1==l.ReturnCode){var o=a.tableData.indexOf(e);a.tableData.splice(o,1)}else a.$alert(l.Message," ",{type:"info",confirmButtonText:"确定"})},function(e){console.log(e)})},saveVendor:function(e){var t=this;this.$http.post("/api/vendor",t.formData).then(function(e){var a=e.data;if(1==a.ReturnCode){var l=a.Data;-1==t.editItemIndex?t.tableData.push(l):t.tableData.splice(t.editItemIndex,1,l),t.dialogVendor=!1,t.editItemIndex=-1,t.formData={}}else t.$alert(a.Message," ",{type:"info",confirmButtonText:"确定"})},function(e){console.log(e)})},getVendorTypeList:function(){var e=this;this.$http.get("/api/dict/GetDictByType?typeId=102").then(function(t){var a=t.data;1==a.ReturnCode?e.vendorTypeList=a.Data:e.$alert(a.Message," ",{type:"info",confirmButtonText:"确定"})},function(e){console.log(e)})}},mounted:function(){this.getVendorTypeList(),this.getDataList()},computed:{fiteredTableData:function(){var e=this;return this.curVendorType?this.tableData.filter(function(t){return t.DicTypeID==e.curVendorType}):this.tableData}}},c={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{staticClass:"dicttable dict-manager"},[a("table",{staticClass:"filter_table",attrs:{cellspacing:"0",cellpadding:"0"}},[a("tbody",[a("tr",{staticClass:"ct tb-tr"},[a("td",{staticClass:"name"},[e._v("供应商类别 ：")]),e._v(" "),a("td",{staticStyle:{padding:"5px 0"}},[a("el-select",{staticClass:"w220",attrs:{placeholder:"请选择供应商类别",filterable:"",clearable:""},on:{change:e.getDataList,clear:e.getDataList},model:{value:e.curVendorType,callback:function(t){e.curVendorType=t},expression:"curVendorType"}},e._l(e.vendorTypeList,function(e){return a("el-option",{key:e.Id,attrs:{label:e.TypeName,value:e.Id}})}))],1),e._v(" "),a("td",{staticClass:"name"},[e._v("编号 ：")]),e._v(" "),a("td",[a("el-input",{staticClass:"w220",attrs:{clearable:""}})],1),e._v(" "),a("td",{staticClass:"name"},[e._v("名称 ：")]),e._v(" "),a("td",[a("el-input",{staticClass:"w220",attrs:{clearable:""}})],1)])])]),e._v(" "),a("div",{staticClass:"inquire"},[a("el-button",{staticClass:"added demand-add",attrs:{type:"primary"},on:{click:function(t){e.addVendor()}}},[a("i",{staticClass:"el-icon-plus"}),e._v(" 新增供应商\n        ")]),e._v(" "),a("el-button",{staticClass:"demand",attrs:{type:"primary"},on:{click:function(t){e.getDataList()}}},[a("i",{staticClass:"el-icon-search"}),e._v(" 搜索\n        ")])],1),e._v(" "),a("div",{staticClass:"table"},[a("el-table",{ref:"singleTable",attrs:{data:e.fiteredTableData,border:"","highlight-current-row":""}},[a("el-table-column",{attrs:{type:"index",label:"序号",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"SerialNum",label:"编号",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"VendorName",label:"名称",width:"auto",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"TypeName",label:"类别",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"Style",label:"风格",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"Level",label:"档次",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"Production",label:"产量",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"Deadline",label:"交期",width:"80",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{property:"Years",label:"合作年份",width:"100",align:"left"}}),e._v(" "),a("el-table-column",{attrs:{label:"用户操作",width:"120"},scopedSlots:e._u([{key:"default",fn:function(t){return[a("i",{staticClass:"el-icon-edit green operation",on:{click:function(a){e.editVendor(t.row,t.$index)}}}),e._v(" "),a("span",{staticClass:"slash"}),e._v(" "),a("i",{staticClass:"el-icon-delete2 green operation",on:{click:function(a){e.handleDelete(t.row,t.$index)}}})]}}])})],1)],1),e._v(" "),a("el-dialog",{attrs:{title:e.dialogTitle,visible:e.dialogVendor},on:{"update:visible":function(t){e.dialogVendor=t}}},[a("el-form",{ref:"formData",attrs:{model:e.formData,rules:e.rules}},[a("el-row",[a("el-col",{attrs:{span:24}},[a("el-form-item",{attrs:{label:"编号","label-width":"120px",prop:"SerialNum"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.SerialNum,callback:function(t){e.$set(e.formData,"SerialNum",t)},expression:"formData.SerialNum"}})],1)],1)],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:24}},[a("el-form-item",{attrs:{label:"名称","label-width":"120px",prop:"VendorName"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.VendorName,callback:function(t){e.$set(e.formData,"VendorName",t)},expression:"formData.VendorName"}})],1)],1)],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"类别","label-width":"120px",prop:"TypeID"}},[a("el-select",{staticClass:"w220",attrs:{placeholder:"请选择供应商类别"},model:{value:e.formData.TypeID,callback:function(t){e.$set(e.formData,"TypeID",t)},expression:"formData.TypeID"}},e._l(e.vendorTypeList,function(e){return a("el-option",{key:e.Id,attrs:{label:e.TypeName,value:e.Id,clearable:""}})}))],1)],1),e._v(" "),a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"风格","label-width":"120px",prop:"Style"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Style,callback:function(t){e.$set(e.formData,"Style",t)},expression:"formData.Style"}})],1)],1)],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"档次","label-width":"120px",prop:"Level"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Level,callback:function(t){e.$set(e.formData,"Level",t)},expression:"formData.Level"}})],1)],1),e._v(" "),a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"产量","label-width":"120px",prop:"Production"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Production,callback:function(t){e.$set(e.formData,"Production",t)},expression:"formData.Production"}})],1)],1)],1),e._v(" "),a("el-form-item",{attrs:{label:"客户评价","label-width":"120px",prop:"CustomerEval"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.CustomerEval,callback:function(t){e.$set(e.formData,"CustomerEval",t)},expression:"formData.CustomerEval"}})],1),e._v(" "),a("el-form-item",{attrs:{label:"工厂评价","label-width":"120px",prop:"FactoryEval"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.FactoryEval,callback:function(t){e.$set(e.formData,"FactoryEval",t)},expression:"formData.FactoryEval"}})],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"交期","label-width":"120px",prop:"Deadline"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Deadline,callback:function(t){e.$set(e.formData,"Deadline",t)},expression:"formData.Deadline"}})],1)],1),e._v(" "),a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"联系人/电话","label-width":"120px",prop:"ContactPhone"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.ContactPhone,callback:function(t){e.$set(e.formData,"ContactPhone",t)},expression:"formData.ContactPhone"}})],1)],1)],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"公司负责人","label-width":"120px",prop:"ManagerName"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.ManagerName,callback:function(t){e.$set(e.formData,"ManagerName",t)},expression:"formData.ManagerName"}})],1)],1),e._v(" "),a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"负责人/电话","label-width":"120px",prop:"ManagerPhone"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.ManagerPhone,callback:function(t){e.$set(e.formData,"ManagerPhone",t)},expression:"formData.ManagerPhone"}})],1)],1)],1),e._v(" "),a("el-row",[a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"地址","label-width":"120px",prop:"Address"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Address,callback:function(t){e.$set(e.formData,"Address",t)},expression:"formData.Address"}})],1)],1),e._v(" "),a("el-col",{attrs:{span:11}},[a("el-form-item",{attrs:{label:"合作年份","label-width":"120px",prop:"Years"}},[a("el-input",{attrs:{autocomplete:"off"},model:{value:e.formData.Years,callback:function(t){e.$set(e.formData,"Years",t)},expression:"formData.Years"}})],1)],1)],1)],1),e._v(" "),a("div",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[a("el-button",{on:{click:function(t){e.dialogVendor=!1}}},[e._v("取 消")]),e._v(" "),a("el-button",{attrs:{type:"primary"},on:{click:function(t){e.saveVendor("formData")}}},[e._v("确 定")])],1)],1)],1)},staticRenderFns:[]};var d=a("VU/8")(s,c,!1,function(e){a("u11u")},null,null);t.default=d.exports},bOdI:function(e,t,a){"use strict";t.__esModule=!0;var l,o=a("C4MV"),r=(l=o)&&l.__esModule?l:{default:l};t.default=function(e,t,a){return t in e?(0,r.default)(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}},mClu:function(e,t,a){var l=a("kM2E");l(l.S+l.F*!a("+E39"),"Object",{defineProperty:a("evD5").f})},u11u:function(e,t){}});
//# sourceMappingURL=1.1ddade4490e6b9460ea9.js.map