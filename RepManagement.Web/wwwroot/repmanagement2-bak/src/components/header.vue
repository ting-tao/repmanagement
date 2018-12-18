<template>
  <div class="header-logined">
    <div class="header-in">
      <img src="../assets/sys-logo.png" class="sys-logo" alt="">
      <ul class="header-loged">
        <li class="l1 header-usermsg">
          <span class="name">{{username}}</span>
        </li>
        <li class="l1 header-slash"></li>
        <li class="l1 header-logout" @click="logout">注销</li>
      </ul>
    </div>
  </div>
</template>

<script>

export default {
    data() {
        return {
            username: localStorage["username"] || "未登录"
        };
    },
    methods: {
        logout() {
            this.$http.get("/api/user/Signout").then(
                function(res) {
                    var result = res.data;

                    if (result.ReturnCode == 1) {
                        window.localStorage.removeItem("token");
                      
                        this.$router.push({ path: "/login" });
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
        }
    },
    components: {},
    mounted: function() {
        //console.log("get menu data");
    }
};
</script>

<style scoped>
.header-logined {
    width: 100%;
    height: 60px;
    overflow-x: hidden;
    overflow-y: visible;
    background: #272727;
    border: 1px solid #272727;
    box-sizing: border-box;
}
.header-in {
    width: 98%;
    max-width: 1200px;
    min-width: 640px;
    height: 32px;
    margin: 0 auto;
    overflow: visible;
    margin-top: 14px;
}

.header-in:after {
    clear: both;
}
.sys-logo {
    float: left;
}
.header-loged {
    float: right;
    font-size: 0;
    line-height: 0;
    color: #fff;
    height: 100%;
}
.header-loged .l1 {
    display: inline-block;
    *display: inline;
    *zoom: 1;
    font-size: 16px;
    color: #fff;
    vertical-align: middle;
    cursor: pointer;
}
.header-loged:after {
    content: "";
    width: 0;
    height: 100%;
    display: inline-block;
    *display: inline;
    *zoom: 1;
    vertical-align: middle;
}
.header-usermsg {
    height: 18px;
    padding-left: 30px;
    background: url("../assets/header-icons.png") 0 0 no-repeat;
    line-height: 18px;
}
.header-slash {
    width: 1px;
    height: 15px;
    background: #fff;
    margin: 0 30px;
    cursor: default;
}
.header-logout {
    height: 18px;
    padding-left: 26px;
    background: url("../assets/header-icons.png") -1px -33px no-repeat;
    line-height: 18px;
}
</style>