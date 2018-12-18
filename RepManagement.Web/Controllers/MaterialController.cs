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
    public class MaterialController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;
        private IMemoryCache _cache = null;

        public MaterialController(RepManagementContext context, IHttpContextAccessor httpContextAccessor,IMemoryCache cache)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        [Route("GetMaterialList")]
        public ApiResult GetMaterialList()
        {
            var result = new ApiResult { ReturnCode=ApiReturnCode.Error};
            //result.Data=_context.Materials.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).ToList();

            var dataList = from mateItem in _context.Materials.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true)
                           join vendorItem in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on mateItem.VendorID equals vendorItem.Id
                           join typeItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on mateItem.TypeID equals typeItem.Id
                           select new
                           {
                               mateItem.Id,
                               mateItem.Color,
                               mateItem.ColorNum,
                               mateItem.LeftCount,
                               mateItem.MaterialQuality,
                               mateItem.OpenSize,
                               mateItem.Price,
                               mateItem.SerialNum,
                               mateItem.Spec,
                               mateItem.TypeID,
                               mateItem.VendorID,
                               mateItem.Weight,
                               mateItem.IconID,
                               VendorNum = vendorItem.SerialNum,
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
        public ApiResult DeleteItem(Guid materialId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Materials.Find(materialId);
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
        public ApiResult Post(Material material)
        {
            ApiResult result = new ApiResult { ReturnCode=ApiReturnCode.Error};

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (material == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                var dictItem = _context.Dictionaries.FirstOrDefault(x => x.Id == material.TypeID);
                var vendorItem = _context.Vendors.FirstOrDefault(x => x.Id == material.VendorID);
                if (material.Id == Guid.Empty)
                {
                    material.Id = Guid.NewGuid();
                    _context.AttachAddEntity(material, userId);
                    _context.Materials.Add(material);

                   
                  
                }
                else
                {
                    _context.AttachUpdateEntity(material, userId);
                    _context.Materials.Update(material);

                    
                       
                    
                }
                if (material.IconID.HasValue && material.IconID.Value != Guid.Empty)
                {
                    byte[] content;
                    if (_cache.TryGetValue<byte[]>(material.IconID.Value, out content))
                    {
                        if (_context.SystemImages.AsNoTracking().FirstOrDefault(x=>x.Id==material.IconID.Value) != null)
                        {
                            _context.SystemImages.Update(new SystemImage
                            {
                                Id = material.IconID.Value,
                                Content = content
                            });
                        }
                        else
                        {
                            _context.SystemImages.Add(new SystemImage
                            {
                                Id = material.IconID.Value,
                                Content = content
                            });
                        }
                       
                        _cache.Remove(material.IconID.Value);
                    }

                }
                else
                {
                    material.IconID = null;
                }

                if (_context.SaveChanges() > 0)
                {
                    var mateItem = material;
                    result.Data = new
                    {
                        mateItem.Id,
                        mateItem.Color,
                        mateItem.ColorNum,
                        mateItem.LeftCount,
                        mateItem.MaterialQuality,
                        mateItem.OpenSize,
                        mateItem.Price,
                        mateItem.SerialNum,
                        mateItem.Spec,
                        mateItem.TypeID,
                        mateItem.VendorID,
                        mateItem.Weight,
                        mateItem.IconID,
                        VendorNum = vendorItem == null ? "--" : vendorItem.SerialNum,
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
        [Route("UploadMaterialImg")]
        public ApiResult UploadMaterialImg(bool isFlag)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                
                using (var stream = file.OpenReadStream())
                {
                    var content = new byte[stream.Length];
                    stream.Read(content, 0, (int)stream.Length);
                    var mid = HttpContext.Request.Form["IconID"]+"";
                    Guid itemId = Guid.Empty;
                    if (!string.IsNullOrEmpty(mid)&&mid.ToLower()!="null")
                    {
                       
                        if(!Guid.TryParse(mid, out itemId))
                        {
                            return new ApiResult { ReturnCode=ApiReturnCode.Error,Message=MessageUtil.ParamInvalid};
                        }
                    }
                    else
                    {
                        itemId = Guid.NewGuid();
                    }
                    _cache.Set(itemId, content,new TimeSpan(0,10,0));

                    return new ApiResult { ReturnCode = ApiReturnCode.Succeed,Data=itemId };
                }

            }
            return new ApiResult { ReturnCode=ApiReturnCode.Error };
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
