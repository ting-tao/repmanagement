<template>
    <div class="wrap">
        <!-- 头部 -->
        <div class="header">
            <div class="header_content">
                <img src="../../assets/sys-logo.png" class="header-logo" />
                <div class="header_content_right">
                    <ul>
                        <li><a href="#" class="guide">操作指南</a></li>
                        <li><a href="#" class="contact">联系我们</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- 内容 -->
        <div class="content">
            <div class="content_mid">
                <div class="loginbar" >
                    <!-- 登录 -->
                    <div class="login" ref="loginbar" @keypress="keypressFn">
                        <!-- 登录头部 -->
                        <div class="login_top">
                            <p>登录</p>
                        </div>
                        <!-- 登陆主体 -->
                        <el-form :model="loginForm" :rules='rules' ref="loginForm" class="login_content">
                            <!-- 账号输入 -->
                            <el-form-item prop="username" class="login_content-li login_content_user">
                                <el-input ref="username" v-focus="true"  :autofocus="true" type="text" v-model="loginForm.username" auto-complete="off" class="login_content_user_input" placeholder="请输入用户名" />
                            </el-form-item>
                            <!-- 密码输入 -->
                            <el-form-item prop="password" class="login_content-li login_content_pass">
                                
                                <el-input ref="password" type="password" v-model="loginForm.password" auto-complete="off" class="login_content_pass_input" placeholder="请输入密码" />
                            </el-form-item>
                            <div class="login_content_submit">
                                <div class="caps-lock-tip" v-show="isCapsLock">大写锁定已打开</div>
                                <div class="login-errtip" >{{errtip}}</div>
                                <el-button type="primary" class="login_submit" style="border-radius:5px;" @click="loginFn('loginForm')">登录</el-button>
                            </div>
                            <!-- <div class="login_footer">
                                <span class="unregistered">还没注册吗？<a href="#" class="sign_now">立即注册</a></span><a href="#" class="unpassword">忘记密码？</a>
                            </div> -->
                            <div class="remind">
                                温馨提示：<br />本系统建议使用IE9及以上版本/Chrome/Edge浏览器，获得更好的用户体验.
                            </div>
                            
                        </el-form>
                      
                    </div>
                </div>
            </div>
        </div>
        <!-- 尾部 -->
        <foot></foot>
    </div>
</template>

<script>
    import foot from '../../components/footer'
  
   
    import {
        Remember
    } from '../../lib/mUtils';
    import {
        sha256
    } from '../../lib/sha256'
    
   
    export default {
        data() {
            return {
                loginForm: {
                    password: '',
                    username: ''
                },
                rules: {
                    password: [{
                        required: true,
                        message: '密码不能为空',
                        trigger: 'blur'
                    }],
                    username: [{
                        required: true,
                        message: '用户名不能为空',
                        trigger: 'blur'
                    }]
                },
                errtip: '',
                loginInputs: ['username','password'],// 需要进行判断的input的顺序
                isCapsLock: false, //判断大写键是否被打开
            };
        },
        
        directives: {
            focus: {
                inserted(el){
                    el.getElementsByTagName('input')[0].focus();
                }
            }
        },
        methods: {
            
            getRemember(){
                //进来的时候从cookie获取用户名和密码
                var user = Remember.get();
                let username = user.name;
                let password = user.psw;
                if (username!=null && username!="")
                { this.loginForm.username = username}
                if (password!=null && password!="")
                { this.loginForm.password = password}
            },
            keypressFn(e){
                //添加键盘事件
                var keyCode  =  e.keyCode||e.which; // 按键的keyCode 
                var isShift  =  e.shiftKey ||(keyCode  ==   16 ) || false ; // shift键是否按住
                this.isCapsLock=false;
                if(keyCode == 13){
                    this.loginFn('loginForm')
                }
                if(e.target.type == "password"){
                    if(((keyCode >= 65 && keyCode <= 90 )  &&  !isShift) 
                    || ((keyCode >=   97   &&  keyCode  <=   122 )  &&  isShift)){
                        //出现大写字符 并且 shift 键没有打开，说明打开了大写锁定 
                        //|| 出现小写字符，并且shift 键打开，说明打开了大写锁定
                        this.isCapsLock=true;
                    }
                }
            },
            setFocus(str){
                let o = this.isFocus || {};
                
                for(let key in o){
                    
                    this.isFocus[key] = false;
                }
                this.isFocus[str] = true;
            },
            loginFn: function(formName) {
                let _self = this,load=null;
                _self.errtip = '';
               
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        load = _self.$loading({
                            target: _self.$refs['loginbar'],
                            text: '正在登录...'
                        })
                        this.$http.get('/api/user/Login?username=' + this._data.loginForm.username + '&password=' + sha256(this._data.loginForm.password)).then(function(res) {
                            
                            load.close();
                            let result = res.data;
                            if (result.ReturnCode == 1) {
                                
                               
                                localStorage['username'] = result.Data.UserName;
                                // Remember.set(_self.loginForm['username'],_self.loginForm['password'],30);
                               
                                let retUrl=_self.$route.query.returnUrl;
                                if (retUrl) {
                                    retUrl=decodeURIComponent(retUrl);
                                    _self.$router.push(retUrl);
                                } else {
                                    _self.$router.push('/personal');
                                }
                             
                            } else {
                                
                                _self.errtip = result.Message;
                                _self.$alert(result.Message, ' ', {
                                    type:'info',
                                    confirmButtonText: '确定'
                                });
                            }
                        }).catch(function(err) {
                            load.close();
                            console.log('err:',err)
                            // _self.$alert(err, ' ', {
                            //     type:'error',
                            //     confirmButtonText: '确定'
                            // });
                        });
                        //login code here..
                    } else {
                       for(let i = 0,len = this.loginInputs.length; i<len; i++){
                           let key = this.loginInputs[i];
                           if(!this.$refs[key].value){
                               this.$refs[key].$el.getElementsByTagName('input')[0].focus();
                               return false;
                           }
                       }
                        return false;
                    }
                });
            },
            onResetClick: function(formName) {
                this.$refs[formName].resetFields();
            }
        },
        components: {
            foot
        },
        mounted: function() {
            // this.getRemember();
        }
    }
</script>

<style scoped>

    input:-webkit-autofill {
        background: none!important;
        outline: none!important;
        -webkit-box-shadow: 0 0 0px 1000px #646b6f inset;
        border: none!important;
        border-radius: 0;
        color: #fff!important;
        -webkit-text-fill-color: #fff;
    }
    .wrap {
        position: absolute;
        top: 0;
        bottom: 0;
        width: 100%;
    }
    /*header部分*/
    .header {
        width: 100%;
        height: 60px;
        background: #272727;
        border: 1px solid #272727;
        box-sizing: border-box;
        position: absolute;
        top: 0;
        left: 0;
        z-index: 100;
    }
    .header_content {
        width: 98%;
        max-width: 1200px;
        min-width: 640px;
        height: 32px;
        margin: 0 auto;
        overflow: visible;
        margin-top: 14px;
        position: relative;
    }
    .header_content::after {
        clear: both;
    }
    .header-logo {
        float: left;
    }
    .header_content_right {
        float: right;
        line-height: 32px;
    }
    .header_content_right li {
        display: inline-block;
        *display: inline;
        *zoom: 1;
        margin-left: 25px;
    }
    .header_content_right a {
        display: inline-block;
        *display: inline;
        *zoom: 1;
        color: #fff;
        font-size: 16px;
        padding-left: 30px;
        background: url(../../assets/loging-header-icons.png) no-repeat 0 2px;
    }
    .header_content_right .guide {
        background-position: 0 -34px;
    }
    /*footer部分*/
    .footer {
        background: #272727;
        position: absolute;
        bottom: 0;
        left: 0;
        z-index: 100;
        width: 100%;
    }
    .footer .copyright {
        margin: 10px 0;
        color: #555555;
    }
    /*content部分*/
    .content {
        width: 100%;
        height: 100%;
        background: url(../../assets/mid.jpg) center center no-repeat;
        overflow: hidden;
        box-sizing: border-box;
        position: relative;
        z-index: 50;
    }
    .content_mid {
        width: 100%;
        height: 100%;
        max-width: 1200px;
        margin: 0 auto;
        position: relative;
    }
    .content .loginbar {
        position: absolute;
        right: 0;
        top: 50%;
        margin-top: -22%;
        width: 28%;
        min-width: 270px;
        max-width: 350px;
        background: #646b6f;
        /*border: 1px solid red;*/
    }
      @media screen and (max-width: 1080px) {
        .content .loginbar {
            top: 0;
            margin-top: 26%;
        }
    } 
     @media screen and (max-width: 640px) {
        .content .loginbar {
            top: 0;
            margin-top: 165px;
        }
    }
    @media screen and (max-height: 640px) {
        .content .loginbar {
            top: 0;
            margin-top: 165px;
        }
    }  
    /*login部分*/
    .content .login {
        display: block;
        width: 100%;
        min-width: 270px;
        max-width: 350px;
        overflow: hidden;
        /*切换到其他登录方式时，隐藏*/
    }
    .content .login:after {
        /*内容没有撑开盒子的处理方法*/
        content: '';
        display: block;
        clear: both;
    }
    .login_content {
        font-size: 12px;
        width: 74.8%;
        margin: 0 auto;
    }
    .login_content-li {
        text-align: left;
        padding-left: 8.57%;
    }
     :-moz-placeholder {
        /* Mozilla Firefox 4 to 18 */
        color: #fff;
        opacity: 1;
    }
     ::-moz-placeholder {
        /* Mozilla Firefox 19+ */
        color: #fff;
        opacity: 1;
    }
    input:-ms-input-placeholder {
        color: #fff;
        opacity: 1;
    }
    input::-webkit-input-placeholder {
        color: #fff;
        opacity: 1;
    }
    .content .login .login_top p {
        font-size: 24px;
        padding-top: 9.14%;
        color: #49c9c4;
        cursor: pointer;
    }
    .content .login .login_content .login_content_user {
        /*border: 1px solid red;*/
        margin-top: 8%;
        background: url(../../assets/user.png) no-repeat 0 50%;
        border-bottom: 2px solid #dedede;
        margin-bottom: 0;
    }
    .content .login .login_content .login_content_pass {
        background: url(../../assets/password.png) no-repeat 0 50%;
        border-bottom: 2px solid #dedede;
        margin-bottom: 0;
        margin-top: 5%;
    }
    .login_content_submit {
        margin-top: 15%;
        margin-bottom: 0;
        position: relative;
        text-align: center;
    }
    .caps-lock-tip{
        position: absolute;
        top: 0;
        right: 15%;
        margin-top: -18%;
        background: #f2f2f2;
        padding: 5px;
        border-radius: 3px;
        font-size: 12px;
    }
    .caps-lock-tip::after{
        content: '';
        position: absolute;
        top: -5px;
        left: 10%;
        border-left: 6px solid transparent;
        border-right: 6px solid transparent;
        border-bottom: 6px #f2f2f2 solid;
        
    }
    .login-errtip{
        width: 100%;
        position: absolute;
        font-size: 12px;
        color: #ff4949;
        top: -1.8em;
        text-align: center;
    }
    .content .login .login_content button {
        width: 100%;
        height: 8.62%;
        border: none;
        font-size: 18px;
        background: #49c9c4;
        cursor: pointer;
        color: white;
    }
    /*清除IE10中的text和password的默认图标*/
     ::-ms-clear {
        display: none;
    }
     ::-ms-reveal {
        display: none;
    }
    .content .login .login_content .login_footer span.unregistered {
        float: left;
        color: #fff;
        /*border: 1px solid red;*/
    }
    .content .login .login_footer {
        margin-top: 2.86%;
        overflow: hidden;
    }
    .content .login .login_content a {
        color: #49c9c4;
    }
    
    .content .login .login_content a.unpassword {
        float: right;
        /*border: 1px solid red;*/
    }
    .content .login .login_content .remind {
        clear: both;
        color: #fff;
        text-align: left;
        margin-top: 12%;
        margin-bottom: 15.5%;
        /*border: 1px solid red;*/
    }
</style>
<style>
    .login_content .el-input__inner {
        background: transparent;
        border: none;
        background: #636b6e;
        height: 30px;
        line-height: 35px;
        font-size: 100%;
        color: #fffefe;
    }
    .login_content input:-webkit-autofill {
        background: none!important;
        outline: none!important;
        -webkit-box-shadow: 0 0 0px 1000px #646b6f inset;
        border: none!important;
        color: #fff!important;
        -webkit-text-fill-color: #fff!important;
        border-radius: 0;
        caret-color: #fff;
    }
   
</style>