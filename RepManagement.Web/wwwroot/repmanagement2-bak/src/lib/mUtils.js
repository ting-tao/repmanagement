/**
 * 存储localStorage
 */
export const setStore = (name, content) => {
    if (!name) return;
    if (typeof content !== 'string') {
        content = JSON.stringify(content);
    }
    window.localStorage.setItem(name, content);
}

/**
 * 获取localStorage
 */
export const getStore = name => {
    if (!name) return;
    return window.localStorage.getItem(name);
}

/**
 * 删除localStorage
 */
export const removeStore = name => {
    if (!name) return;
    window.localStorage.removeItem(name);
}

/**
 * 
 * 判断字符串是否为空或者undefined 
 */
export const isBlankString = str => {
    return (!str || /^\s*$/.test(str));
}

export const copyObjectValue=(src,target)=>{
    for(var prop in src){
        if(src.hasOwnProperty(prop)){
            src[prop]=target[prop];
        }
    }
    return src;
}

/**
 * guid
 */
export const guid = () => {
    function S4() {
       return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
    }
    return (S4()+S4()+"-"+S4()+"-"+S4()+"-"+S4()+"-"+S4()+S4()+S4());
}

//设置cookie
export const Cookie = {
    set(c_name,value,expiredays){
        var exdate=new Date()
        exdate.setDate(exdate.getDate()+expiredays)
        document.cookie=c_name+ "=" +escape(value)+
        ((expiredays==null) ? "" : ";expires="+exdate.toGMTString())
    },
    get(c_name){
        let c_start = 0, c_end = 0;
        if (document.cookie.length>0){
            c_start=document.cookie.indexOf(c_name + "=")
            if (c_start!=-1)
                { 
                c_start=c_start + c_name.length+1 
                c_end=document.cookie.indexOf(";",c_start)
                if (c_end==-1) c_end=document.cookie.length
                return unescape(document.cookie.substring(c_start,c_end))
            } 
        }
        return ""
    },
    clear(c_name){
        this.set(c_name,'',-1);
    }
}
import {
        RSA
    } from './RSA';
import {
        hex_md5
    } from './md5';
    var C = {
        NAME_ENCRYPTED: hex_md5('N_ENCRYPTED'),
        NAME_ENCRYPTEDKEY: hex_md5('NAME_ENCRYPTEDKEY'),
        PSW_ENCRYPTED: hex_md5('P_ENCRYPTED'),
        PSW_ENCRYPTEDKEY: hex_md5('P_ENCRYPTEDKEY'),
        PUBLICKEY: hex_md5('PUBLICKEY'),
    }

//记住密码功能

export const Remember = {
    set(name,psw,expiredays){//C是常量集合
        RSA.createNewUserKey().then(function(keyPairs){
            Cookie.set(C.PUBLICKEY,JSON.stringify(keyPairs[1]),expiredays);
            RSA.encrypt(name, keyPairs[0]).then(function(res){
                Cookie.set(C.NAME_ENCRYPTED,JSON.stringify(res.encrypted),expiredays);
                Cookie.set(C.NAME_ENCRYPTEDKEY,res.encryptedKey,expiredays);
            });
            RSA.encrypt(psw, keyPairs[0]).then(function(res){
                Cookie.set(C.PSW_ENCRYPTED,JSON.stringify(res.encrypted),expiredays);
                Cookie.set(C.PSW_ENCRYPTEDKEY,res.encryptedKey,expiredays);
            });
        })
    },
    get(){
        var name_encrypted = Cookie.get(C.NAME_ENCRYPTED);
        if(name_encrypted){
            var name_encryptedkey = Cookie.get(C.NAME_ENCRYPTEDKEY);
            var psw_encrypted = Cookie.get(C.PSW_ENCRYPTED);
            var psw_encryptedkey = Cookie.get(C.PSW_ENCRYPTEDKEY);
            var publickey = Cookie.get(C.PUBLICKEY);
            var name_res = null, psw_res, key = '', rname='', rpsw='';
            if(name_encrypted && name_encryptedkey){
                name_res = {
                    encrypted: name_encrypted,
                    encryptedKey: name_encryptedkey
                }
            }
            if(psw_encrypted && psw_encryptedkey){
                psw_res = {
                    encrypted: psw_encrypted,
                    encryptedKey: psw_encryptedkey
                }
            }
            if(publickey){
                key = JSON.parse(publickey);
            }
            if(name_res && key){
                RSA.decrypt(name_res, key).then(function(decrypted){
                    rname = decrypted;
                    console.log('decrypted', decrypted);
                });
            }
            if(psw_res && key){
                RSA.decrypt(name_res, key).then(function(decrypted){
                    rpsw = decrypted;
                    console.log('decrypted', decrypted);
                });
            }
            return {name:rname, psw:rpsw};
        }else{
            Cookie.clear(C.NAME_ENCRYPTED);
            Cookie.clear(C.NAME_ENCRYPTEDKEY);
            Cookie.clear(C.PSW_ENCRYPTED);
            Cookie.clear(C.PSW_ENCRYPTEDKEY);
            Cookie.clear(C.PUBLICKEY);
            return {name:'',psw:''};
        }
    }
}


Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}