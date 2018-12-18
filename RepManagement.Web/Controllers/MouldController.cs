using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RepManagement.Common;
using RepManagement.Models;
using RepManagement.Web.Extension;

namespace RepManagement.Web.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class MouldController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;
        private IMemoryCache _cache = null;

        public MouldController(RepManagementContext context, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        [Route("GetMouldList")]
        public ApiResult GetMouldList()
        {
            var result = new ApiResult { ReturnCode=ApiReturnCode.Error};
            //result.Data=_context.Materials.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).ToList();

            var dataList = from mouldItem in _context.Moulds.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true)
                          
                           join typeItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on mouldItem.TypeID equals typeItem.Id
                           select new
                           {
                               mouldItem.Id,
                               mouldItem.Creator,
                               mouldItem.BackImgID,
                               mouldItem.FrontImgID,
                               mouldItem.Height,
                               mouldItem.RelateMaterial,
                               mouldItem.SerialNum,
                               mouldItem.SideImgID,
                               mouldItem.Size,
                               mouldItem.Spec,
                               mouldItem.TypeID,
                               mouldItem.Width,
                               TypeName = typeItem.TypeName
                           };
            result.Data = dataList.ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }



        // GET api/values/5
        [HttpGet]
        [Authorize]
        [Route("DeleteItem")]
        public ApiResult DeleteItem(Guid mouldId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Moulds.Find(mouldId);
            if (item == null)
            {
                result.Message = MessageUtil.ItemNotFound;
            }
            else
            {
                _context.AttachDeleteEntity(item, userId);
                _context.SaveChanges();
                result.ReturnCode = ApiReturnCode.Succeed;
            }
            return result;

        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public ApiResult Post(Mould mould)
        {
            ApiResult result = new ApiResult { ReturnCode=ApiReturnCode.Error};

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (mould == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                var dictItem = _context.Dictionaries.FirstOrDefault(x => x.Id == mould.TypeID);
               
                if (mould.Id == Guid.Empty)
                {
                    mould.Id = Guid.NewGuid();
                    _context.AttachAddEntity(mould, userId);
                    _context.Moulds.Add(mould);
                }
                else
                {
                    _context.AttachUpdateEntity(mould, userId);
                    _context.Moulds.Update(mould);
                }
                #region update img

                if (mould.FrontImgID.HasValue && mould.FrontImgID.Value != Guid.Empty)
                {
                    byte[] content;
                    if (_cache.TryGetValue<byte[]>(mould.FrontImgID.Value, out content))
                    {
                        if (_context.SystemImages.AsNoTracking().FirstOrDefault(x=>x.Id==mould.FrontImgID.Value) != null)
                        {
                            _context.SystemImages.Update(new SystemImage
                            {
                                Id = mould.FrontImgID.Value,
                                Content = content
                            });
                        }
                        else
                        {
                            _context.SystemImages.Add(new SystemImage
                            {
                                Id = mould.FrontImgID.Value,
                                Content = content
                            });
                        }
                        _cache.Remove(mould.FrontImgID.Value);
                    }
                   
                }
                else
                {
                    mould.FrontImgID = null;
                }

                if (mould.SideImgID.HasValue && mould.SideImgID.Value != Guid.Empty)
                {
                    byte[] content;
                    if (_cache.TryGetValue<byte[]>(mould.SideImgID.Value, out content))
                    {
                        if (_context.SystemImages.AsNoTracking().FirstOrDefault(x=>x.Id==mould.SideImgID.Value) != null)
                        {
                            _context.SystemImages.Update(new SystemImage
                            {
                                Id = mould.SideImgID.Value,
                                Content = content
                            });
                        }
                        else
                        {
                            _context.SystemImages.Add(new SystemImage
                            {
                                Id = mould.SideImgID.Value,
                                Content = content
                            });
                        }
                        //_cache.Remove(mould.FrontImgID.Value);
                    }
                   
                }
                else
                {
                    mould.SideImgID = null;
                }

                if (mould.BackImgID.HasValue && mould.BackImgID.Value != Guid.Empty)
                {
                    byte[] content;
                    if (_cache.TryGetValue<byte[]>(mould.BackImgID.Value, out content))
                    {
                        if (_context.SystemImages.AsNoTracking().FirstOrDefault(x=>x.Id==mould.BackImgID.Value) != null)
                        {
                            _context.SystemImages.Update(new SystemImage
                            {
                                Id = mould.BackImgID.Value,
                                Content = content
                            });
                        }
                        else
                        {
                            _context.SystemImages.Add(new SystemImage
                            {
                                Id = mould.BackImgID.Value,
                                Content = content
                            });
                        }
                           
                        _cache.Remove(mould.FrontImgID.Value);
                    }
                  
                }
                else
                {
                    mould.BackImgID = null;
                }

                #endregion

                if (_context.SaveChanges() > 0)
                {
                    var mouldItem = mould;
                    result.Data = new
                    {
                        mouldItem.Id,
                        mouldItem.Creator,
                        mouldItem.BackImgID,
                        mouldItem.FrontImgID,
                        mouldItem.Height,
                        mouldItem.RelateMaterial,
                        mouldItem.SerialNum,
                        mouldItem.SideImgID,
                        mouldItem.Size,
                        mouldItem.Spec,
                        mouldItem.TypeID,
                        mouldItem.Width,
                        TypeName = dictItem == null ? "--" : dictItem.TypeName
                    };
                    result.ReturnCode = ApiReturnCode.Succeed;
                }
                else
                {
                    result.Message = "更新失败";
                }
            }
            return result;
        }

        [HttpPost]
        [Authorize]
        [Route("UploadMouldImg")]
        public ApiResult UploadMouldImg(int flag)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                using (var stream = file.OpenReadStream())
                {
                    var content = new byte[stream.Length];
                    stream.Read(content, 0, (int)stream.Length);
                    string mid ="";
                    switch (flag)
                    {
                        case 0:
                            mid = HttpContext.Request.Form["FrontImgID"] + "";
                            break;
                        case 1:
                            mid = HttpContext.Request.Form["SideImgID"] + "";
                            break;
                        case 2:
                            mid = HttpContext.Request.Form["BackImgID"] + "";
                            break;
                        default:
                            throw new Exception(MessageUtil.ParamInvalid);
                    }
                    Guid itemId = Guid.Empty;
                    if (!string.IsNullOrEmpty(mid) && mid.ToLower() != "null")
                    {

                        if (!Guid.TryParse(mid, out itemId))
                        {
                            return new ApiResult { ReturnCode = ApiReturnCode.Error, Message = MessageUtil.ParamInvalid };
                        }
                    }
                    else
                    {
                        itemId = Guid.NewGuid();
                    }
                    _cache.Set(itemId, content, new TimeSpan(0, 10, 0));

                    return new ApiResult { ReturnCode = ApiReturnCode.Succeed, Data = new {Id=itemId,Flag=flag } };
                }
            }

                return new ApiResult { };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
