using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepManagement.Models;
using RepManagement.Web.Extension;

namespace RepManagement.Web.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;
        public ImageController(RepManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/values
        //[Authorize]
        [HttpGet]
        public HttpResponseMessage Get(Guid? imgId)
        {
            if (!imgId.HasValue || imgId == Guid.Empty)
            {
                throw new Exception(MessageUtil.ParamInvalid);
            }
            else
            {


                var imgItem = _context.SystemImages.Find(imgId.Value);
                if (imgItem == null)
                {
                    throw new Exception(MessageUtil.ParamInvalid);
                }
                else
                {
                    if (imgItem.Content != null && imgItem.Content.Length > 0)
                    {
                        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                        result.Content = new ByteArrayContent(imgItem.Content);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                        return result;
                    }
                    else
                    {
                        //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                        //var imgPath = HttpContext.Current.Server.MapPath("/Resource/userdefault.jpg");

                        //using (FileStream fs = File.OpenRead(imgPath))
                        //{
                        //    byte[] content = new byte[fs.Length];
                        //    fs.Read(content, 0, (int)fs.Length);
                        //    result.Content = new ByteArrayContent(content);
                        //}

                        //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                        //return result;
                        throw new Exception(MessageUtil.ParamInvalid);
                    }
                }
            }
        }

        [HttpGet]
        [Route("ShowProdPic")]
        public HttpResponseMessage Get(Guid prodPicId)
        {
            var imgItem = _context.ProductionImages.Find(prodPicId);
            if (imgItem == null)
            {
                throw new Exception(MessageUtil.ParamInvalid);
            }
            else
            {
                if (imgItem.Content != null && imgItem.Content.Length > 0)
                {
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(imgItem.Content);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                    return result;
                }
                else
                {
                    throw new Exception(MessageUtil.ParamInvalid);
                }
            }
        }

        
    }
}
