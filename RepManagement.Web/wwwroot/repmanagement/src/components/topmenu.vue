<template>
  <div class="menu-bar">
  
    <div class="menu-bar-in">
      <img class="menu-logo" :class="{nofixed:!isfix}" src="../assets/sys-menulogo.png" />
      <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" @select="handleSelect" :router="true">
        <template v-for="menuItem in menuItems">
          <!-- <el-menu-item v-if="menuItem.submenuItems==null||menuItem.submenuItems.length==0" :index="menuItem.index">{{menuItem.title}}</el-menu-item> -->
          <el-submenu :key="menuItem.index" :index="menuItem.index">
             <template slot="title">
              <el-menu-item class="menu-title-item" :index="menuItem.index">
                <a  class="menu-link" v-show="menuItem.index != '/forbidden'" :href="menuItem.index" >{{menuItem.title}}</a>
                <span v-show="menuItem.index == '/forbidden'">{{menuItem.title}}</span>
              </el-menu-item> 
            <!-- <template slot="title">{{menuItem.title}} -->
            </template>
            <el-menu-item v-for="submenuItem in menuItem.submenuItems"  :class="submenuItem.nouse? 'nouse-menu':''" :key="submenuItem.index" :index="submenuItem.index">
              <a class="menu-link" v-show="submenuItem.index != '/forbidden'" :href="submenuItem.index">{{submenuItem.title}}</a>
              <span v-show="submenuItem.index == '/forbidden'">{{submenuItem.title}}</span>
            </el-menu-item>
          </el-submenu>
        </template>
      </el-menu>
  
    </div>
  </div>
</template>

<script>

export default {
  props: {
    isfix: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      preActive: '/index.html#/material/admin',
      activeIndex: '/index.html#/material/admin',
      rootMenuItems: [//基本的菜单权限

      ],
      menuItems: [  {
        index: '/index.html#/material/admin',
        title: '物料管理',
        msg: '/index.html#/material',
        submenuItems: [{
          index: '/index.html#/material/admin',
          title: '物料管理',
        },{
          index: '/index.html#/material/mouldadmin',
          title: '模具管理',
        },
        ]
      },{
        index: '/index.html#/vendor/admin',
        title: '供应商管理',
        msg: '/vendor',
        submenuItems: [{
          index: '/index.html#/vendor/admin',
          title: '供应商管理',
        }
        ]
      }, {
        index: '/index.html#/production/list',
        title: '成品管理',
        msg: '/production',
        submenuItems: [{
          index: '/index.html#/production/admin',
          title: '成品管理',
        },{
          index: '/index.html#/production/list',
          title: '成品列表',
        }]
      }, {
        index: '/index.html#/customer/admin',
        title: '客户管理',
        msg: '/customer',
        submenuItems: [{
          index: '/index.html#/customer/admin',
          title: '客户管理',
        }]
      }, {
        index: '/personal',
        title: '财务管理',
        msg: '/personal',
        // submenuItems: [{
        //   index: '/personal/admin',
        //   title: '待办事项'
        // }, {
        //   index: '/personalSave',
        //   title: '我的收藏'
        // }
        // ]
      }, {
        index: '/index.html#/admin/user',
        title: '系统管理',
        msg: '/admin',
        submenuItems: [ {
          index: '/index.html#/admin/user',
          title: '用户管理'
        },{
          index: '/index.html#/admin/role',
          title: '角色管理'
        },{
          index: '/index.html#/admin/dict',
          title: '字典管理'
        }
       
        ]
      }],
      count: 4
    };
  },
  methods: {
    handleSelect(key, keyPath) {
      this.preActive = key;
    }
  },
  watch: {
    'preActive'() {
      var index = this.activeIndex;
      var _self = this;
      if(this.preActive == '/forbidden'){
        this.activeIndex = '';
        setTimeout(function(){
          _self.activeIndex = index;
          _self.preActive = index;
        },0)
      }
    },
    '$route'(to, from) {
      // 对路由变化作出响应...
      var m = to.matched;
      for (var len = m.length, i = len - 1; i >= 0; i--) {
        var r = m[i];
        if (r.path) {
          for (let i = 0, leng = this.menuItems.length; i < leng; i++) {
            let menu = this.menuItems[i];
            if (r.path.indexOf(menu.msg) == 0) {
              if (menu.submenuItems) {
                for (let j = 0, length = menu.submenuItems.length; j < length; j++) {
                  let submenun = menu.submenuItems[j];
                  let query = '?';
                  for(let q in this.$route.query){
                    query += q + '=' + this.$route.query[q] + '&';
                  }
                  query = query.slice(0,query.length-1);
                  let wholepath = r.path+query;
                  if (r.path.indexOf(submenun.index) == 0 || wholepath.indexOf(submenun.index)==0) {
                    this.activeIndex = submenun.index;
                    break;
                  }
                }
              }
              this.activeIndex = menu.index;
              break;
            }
          }
        }
      }

    }
  },
  
  components: {},
  mounted: function () {
    let path = this.$route.path != '/'? this.$route.path : '/personal';
    this.activeIndex = path;
    //this.getConditions();
  }
}
</script>

<style>
.menu-link{
  display: inline-block;
  width: 100%;
  height: 100%;
}
.menu-bar-in .el-submenu__title{
  padding: 0;
}
.el-menu--horizontal .el-submenu .el-menu-item.menu-title-item{
  background: transparent;
  height: 60px;
  line-height: 60px;
  padding: 0 20px;
}
.menu-logo {
  position: absolute;
  left: 0;
  top: 14px;
}

.menu-logo.nofixed {
  display: none;
}

@media screen and (max-width:980px) {
  .menu-logo {
    display: none;
  }
}

.menu-bar {
  width: 100%;
  height: 60px;
  box-sizing: border-box;
  background: #f8f8f8;
  text-align: center;
  border-bottom: 1px solid #d4d2d2;
  box-shadow: 0px 0px 5px 3px rgba(0, 0, 0, 0.1);
}

.menu-bar-in {
  width: 1200px;
  margin: 0 auto;
  position: relative;
}

@media screen and (max-width:1200px) {
  .menu-bar-in {
    width: 100%;
    min-width: 640px;
  }
}

.el-menu-demo {
  display: inline-block;
  *display: inline;
  *zoom: 1;
  vertical-align: top;
  margin: 0 auto;
}

.el-menu-item,
.el-submenu__title {
  color: #272727;
}

.menu-bar .el-menu {
  background: #f8f8f8;
}


.menu-bar .el-menu--horizontal .el-submenu .el-submenu__title {
  height: 59px;
}

.menu-bar .el-submenu__icon-arrow.el-icon-caret-bottom {
  display: none;
}

.menu-bar .el-menu--horizontal>.el-menu-item:hover,
.menu-bar .el-menu--horizontal>.el-submenu.is-active .el-submenu__title,
.menu-bar .el-menu--horizontal>.el-submenu:hover .el-submenu__title {
  border-bottom-color: #49c9c4;
}

.menu-bar .el-menu--horizontal.el-menu--dark .el-submenu .el-menu-item.is-active,
.menu-bar .el-menu-item.is-active {
  color: #49c9c4;
}

.menu-bar .el-menu--horizontal .el-submenu>.el-menu {
  top: 60px;
}

.menu-bar .el-menu-item:hover {
  background-color: #eafaf9;
}
.menu-bar .nouse-menu{
  color: #a2a2a2;
  cursor:not-allowed;
}
</style>