<template>
  <div id="app">
    <hd v-if="!$route.meta.isHideTopMenu"></hd>
    <div class="replace-menu" v-show="isMenuFix"></div>
    <topmenu class="menu-cls" :isfix="isMenuFix" :class="{fixed:isMenuFix}"  v-if="!$route.meta.isHideTopMenu"></topmenu>
    <rb v-if="!$route.meta.isHideTopMenu"></rb>
    <transition name="router-fade" mode="out-in">
      <keep-alive>
        <router-view v-if="$route.meta.keepAlive"></router-view>
      </keep-alive>
    </transition>
    <transition name="router-fade" mode="out-in">
      <router-view v-if="!$route.meta.keepAlive"></router-view>
    </transition>
    <foot  v-if="!$route.meta.isHideTopMenu"></foot>
  </div>
</template>

<script>
import hd from './components/header'
import topmenu from './components/topmenu'
import rb from './components/routebar'
import foot from './components/footer'
  export default {
    name: 'app',
    data() {
      return {
        isMenuFix: false
      }
    },
    components: {
      topmenu,
      hd,
      rb,
      foot
    },
    methods: {
      setDatas(a,b,c) {
        var a = document.body.scrollTop || document.documentElement.scrollTop;  //滚动条的高度
        var b = document.documentElement.clientHeight    //可视区的高度
        var c = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);   //文档的总高度
        
        if(b < c){
            if(a >= 60){
              this.isMenuFix = true;
            }else{
              this.isMenuFix = false;
            }
        }else{
            this.isMenuFix = false;
        }
      }
    },
    mounted: function() {
        var _self = this;
        _self.setDatas();
        window.addEventListener('scroll',function(){
          _self.setDatas();
        })
        window.addEventListener('resize',function(){
            _self.setDatas();
        })
        window.onbeforeunload = function()
        {
         
        }


    }
  }
</script>

<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  .menu-cls.fixed{
    position: fixed;
    left: 0;
    top: 0;
    z-index: 100;
  }
  .replace-menu{
    height: 60px;
    width: 100%;
  }
</style>
