using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RepManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepManagement.Web.Filter
{
    public class ApiExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //base.OnException(context);

            if (!context.ExceptionHandled)
            {
                ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };
                result.Message = context.Exception.Message;
                var responseMessage = new HttpResponseMessage();
                responseMessage.Content = new StringContent(JsonConvert.SerializeObject(result));
                context.Result = new ObjectResult(result);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
          
        }
    }
}
