<template>
  <div class="routebar">
    <div class="routewp" v-for="(menuItem,index) in routeName" :key="index">
        <span class="next" v-if="index!=0"> > </span>
        <span class="focus" v-if="index == routeName.length-1">{{menuItem.title}}</span>
        <a class="not-focus" :href="'/#'+menuItem.path" v-if="index !=routeName.length-1">{{menuItem.title}}</a>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        routeName: this.$route.meta? this.$route.meta.routeName : []
      };
    },
    methods: {

    },
    watch: {
     '$route' (to, from) {
      // 对路由变化作出响应...
      
      this.routeName = to.meta.routeName;
      // for(var len = m.length, i = len-1; i>=0; i--){
      //     var r = m[i];
      //     if(r.meta.routeName){
      //       var routeNamePath = from.meta.routeName;
      //       var routeNamePath = from.fullPath.indexOf('interview');
      //       var routeNamePath2 = routeNamePath + 1;
      //           if(((r.meta.routeName).length == 3)&&routeNamePath2){
      //           var kkk = from.fullPath.substr(from.fullPath.lastIndexOf('0')+1);
      //           if(kkk == '2&a=1'){
      //             kkk = 2 ;
      //           }
      //           if(kkk == 1){
      //             r.meta.routeName[1].path = '/interview/one?state=1301';
      //             r.meta.routeName[1].title = '待安排';
      //           }
      //           else if(kkk==2){
      //             r.meta.routeName[1].path = '/interview/two?state=1302&a=1';
      //             r.meta.routeName[1].title = '初试中';
      //           }
      //           else if(kkk==3){
      //             r.meta.routeName[1].path = '/interview/three?state=1303';
      //             r.meta.routeName[1].title = '复试中';
      //           }
      //           else if(kkk==4){
      //             r.meta.routeName[1].path = '/interview/four?state=1304';
      //             r.meta.routeName[1].title = '已面试';
      //           }
      //           else{
      //             r.meta.routeName[1].path = '/interview/five?state=1305';
      //             r.meta.routeName[1].title = '已关闭';
      //           }  
      //         }
      //         this.routeName = r.meta.routeName;
      //       break;
      //     }
          
      // }
      
     }
    },
    components: {},
    mounted:function(){
    }
  }
</script>

<style scoped>

  .routebar{
      width: 1200px;
      height: 40px;
      background: #f8f8f8;
      overflow: hidden;
      padding-left: 20px;
      box-sizing: border-box;
      margin: 0 auto;
      margin-top: 10px;
      text-align: left;
      font-size: 0;
      line-height: 0;
  }
  @media screen and (max-width: 1200px){
    .routebar{
      width: 100%;
      min-width: 480px;
    }
  }
  .routebar .routewp{
    display: inline-block;
    *display: inline;
    *zoom:1;
    color: #666;
    font-size: 14px;
    line-height: 40px;
    vertical-align: top;
  }
  .routebar .next{
      padding: 0 10px;
  }
  .routebar .not-focus{
    color: #666;
  }
  .routebar .focus{
    color: #272727;
  }
</style>