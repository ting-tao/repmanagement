import Vue from 'vue'
import Router from 'vue-router'


const Login = r => require.ensure([], () => r(require('../page/Login/Login')), 'login')
const Home = r => require.ensure([], () => r(require('../page/home/home')), 'home')
const UserAdmin=r=>require.ensure([],()=>(require('../page/SysAdmin/UserAdmin')),'useradmin')
const RoleAdmin=r=>require.ensure([],()=>(require('../page/SysAdmin/UserRoleAdmin')),'roleadmin')
const DictAdmin=r=>require.ensure([],()=>(require('../page/SysAdmin/DictAdmin')),'dict')
const VendorAdmin=r=>require.ensure([],()=>(require('../page/Vendor/VendorAdmin')),'vendoradmin')
const MaterialAdmin=r=>require.ensure([],()=>(require('../page/Material/MaterialAdmin')),'materialadmin')
const MouldAdmin=r=>require.ensure([],()=>(require('../page/Material/MouldAdmin')),'mouldadmin')
const CustomerAdmin=r=>require.ensure([],()=>(require('../page/Customer/CustomerAdmin')),'customeradmin')
const ProductionAdmin=r=>require.ensure([],()=>(require('../page/Production/ProductionAdmin')),'productionadmin')
const ProductionList=r=>require.ensure([],()=>(require('../page/Production/ProductionList')),'productionlist')


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: 'index.html#/admin/user',
      component: UserAdmin,
      meta: {
        routeName: [{title:'管理中心',path:'index.html#/admin/user'}, {title:'用户管理',path:'index.html#/admin/user'}]
      }
    },{
      path: '/admin/dict',
      component: DictAdmin,
      meta: {
        routeName: [{title:'管理中心',path:'/admin/user'}, {title:'字典管理',path:'/admin/dict'}]
      }
    },{
      path: '/admin/role',
      component: RoleAdmin,
      meta: {
        routeName: [{title:'管理中心',path:'/admin/user'}, {title:'角色管理',path:'/admin/role'}]
      }
    },{
      path: '/vendor/admin',
      component: VendorAdmin,
      meta: {
        routeName: [{title:'供应商',path:'/vendor/admin'}, {title:'供应商管理',path:'/vendor/admin'}]
      }
    },{
      path: '/material/admin',
      component: MaterialAdmin,
      meta: {
        routeName: [{title:'物料',path:'/material/admin'}, {title:'物料管理',path:'/material/admin'}]
      }
    },{
      path: '/material/mouldadmin',
      component: MouldAdmin,
      meta: {
        routeName: [{title:'物料',path:'/material/admin'}, {title:'模具管理',path:'/material/mouldadmin'}]
      }
    },{
      path: '/customer/admin',
      component: CustomerAdmin,
      meta: {
        routeName: [{title:'客户',path:'/customer/admin'}, {title:'客户管理',path:'/customer/admin'}]
      }
    },{
      path: '/production/admin',
      component: ProductionAdmin,
      meta: {
        routeName: [{title:'成品',path:'/production/admin'}, {title:'成品管理',path:'/production/admin'}]
      }
    },{
      path: '/production/list',
      component: ProductionList,
      meta: {
        routeName: [{title:'成品',path:'/production/list'}, {title:'成品列表',path:'/production/list'}]
      }
    },{
      path: '/login',
      component: Login,
      meta: {
        isAnonymous: true,//是否允许匿名
        isHideTopMenu: true//是否隐藏顶部菜单
      }
    }
  ]
})
