using System;
using System.Collections.Generic;
using System.Text;

namespace RepManagement.Common
{
    public class ApiResult
    {

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }


        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回状态码，默认0代表失败，1代表成功
        /// </summary>
        public ApiReturnCode ReturnCode { get; set; }

        public ApiResult()
        {

            Data = null;
            ReturnCode = ApiReturnCode.Error;
            Message = "init class message";
        }
    }

    public enum ApiReturnCode
    {
        /// <summary>
        /// 失败
        /// </summary>
        Error = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 1,
        /// <summary>
        /// 认证失败
        /// </summary>
        AuthFailed = 2
    }
}

