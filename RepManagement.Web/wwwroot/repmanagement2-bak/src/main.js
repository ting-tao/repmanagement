// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import {
  isBlankString,
  getStore
} from './lib/mUtils'

import './assets/css/reset.css'
//import element-ui
import ElementUi from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import {
  utilConst
} from './lib/consts'
import vueres from 'vue-resource'

Vue.use(ElementUi)
Vue.use(vueres)

Vue.http.options.emulateJSON = true;

Vue.config.productionTip = false;

Vue.http.interceptors.push((request, next) => {
  // ...
  // 请求发送前的处理逻辑
  // ...
  next((response) => {
    // ...
    // 请求发送后的处理逻辑
    // ...
    // 根据请求的状态，response参数会返回给successCallback或errorCallback
    if(response.status==401){
      this.$router.push({ path: "/login" });
    }
    return response
  })
})


/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: {
    App
  },
  template: '<App/>'
})
