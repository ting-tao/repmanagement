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
    public class ProductionController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;
        private IMemoryCache _cache = null;

        public ProductionController(RepManagementContext context, IHttpContextAccessor httpContextAccessor,IMemoryCache cache)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        [Route("GetProductionList")]
        public ApiResult GetProductionList()
        {
            var result = new ApiResult { ReturnCode=ApiReturnCode.Error};
            //result.Data=_context.Materials.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).ToList();

            var dataList = from prodItem in _context.Productions.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true)
                           join matItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MatTypeId equals matItem.Id
                           join styleItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.StyleTypeId equals styleItem.Id
                           join mouldItem in _context.Moulds.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MouldId equals mouldItem.Id
                           join mainVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MainVendorId equals mainVendor.Id
                           join decorateVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.DecorateVendorId equals decorateVendor.Id
                           join sweatbandVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.SweatbandVendorId equals sweatbandVendor.Id
                           join customerItem in _context.Customers.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.CustomerId equals customerItem.Id into tempCustomerList from tempCustomer in tempCustomerList.DefaultIfEmpty()
                           select new
                           {
                               prodItem.Id,
                              
                               prodItem.SerialNum,
                               prodItem.MatTypeId,
                               MatTypeName= matItem.TypeName,
                               prodItem.StyleTypeId,
                               StyleTypeName=styleItem.TypeName,
                               prodItem.Color,
                               prodItem.Size,
                               prodItem.MouldId,
                               MouldNum=mouldItem.SerialNum,
                               prodItem.ProcessCost,
                               prodItem.MainVendorId,
                               MainVendorNum=mainVendor.SerialNum,
                               prodItem.MainCost,
                               prodItem.DecorateVendorId,
                               DecorateVendorNum=decorateVendor.SerialNum,
                               prodItem.DecorateCost,
                               prodItem.SweatbandVendorId,
                               SweatbandVendorNum=sweatbandVendor.SerialNum,
                               prodItem.SweatbandCost,
                               prodItem.PacketCost,
                               prodItem.TransportCost,
                               prodItem.TotalCost,
                               prodItem.CustomerId,
                               CustomerNum=tempCustomer==null?"":tempCustomer.SerialNum       
                           };
            result.Data = dataList.ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }



        // GET api/values/5
        [HttpGet]
        [Authorize]
        [Route("DeleteItem")]
        public ApiResult DeleteItem(Guid itemId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Productions.Find(itemId);
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
        public ApiResult Post(Production production)
        {
            ApiResult result = new ApiResult { ReturnCode=ApiReturnCode.Error};

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (production == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                if (production.Id == Guid.Empty)
                {
                    production.Id = Guid.NewGuid();
                    _context.AttachAddEntity(production, userId);
                    _context.Productions.Add(production);
                }
                else
                {
                    _context.AttachUpdateEntity(production, userId);
                    _context.Productions.Update(production);
                }

                if (_context.SaveChanges()>0)
                {
                    result.Data = (from prodItem in _context.Productions.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true && x.Id == production.Id)
                                    join matItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MatTypeId equals matItem.Id
                                    join styleItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.StyleTypeId equals styleItem.Id
                                    join mouldItem in _context.Moulds.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MouldId equals mouldItem.Id
                                    join mainVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MainVendorId equals mainVendor.Id
                                    join decorateVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.DecorateVendorId equals decorateVendor.Id
                                    join sweatbandVendor in _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.SweatbandVendorId equals sweatbandVendor.Id
                                    join customerItem in _context.Customers.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.CustomerId equals customerItem.Id
                                    select new
                                    {
                                        prodItem.Id,

                                        prodItem.SerialNum,
                                        prodItem.MatTypeId,
                                        MatTypeName = matItem.TypeName,
                                        prodItem.StyleTypeId,
                                        StyleTypeName = styleItem.TypeName,
                                        prodItem.Color,
                                        prodItem.Size,
                                        prodItem.MouldId,
                                        MouldNum = mouldItem.SerialNum,
                                        prodItem.ProcessCost,
                                        prodItem.MainVendorId,
                                        MainVendorNum = mainVendor.SerialNum,
                                        prodItem.MainCost,
                                        prodItem.DecorateVendorId,
                                        DecorateVendorNum = decorateVendor.SerialNum,
                                        prodItem.DecorateCost,
                                        prodItem.SweatbandVendorId,
                                        SweatbandVendorNum = sweatbandVendor.SerialNum,
                                        prodItem.SweatbandCost,
                                        prodItem.PacketCost,
                                        prodItem.TransportCost,
                                        prodItem.TotalCost,
                                        prodItem.CustomerId,
                                        CustomerNum = customerItem.SerialNum
                                    }).FirstOrDefault() ;
                    result.ReturnCode = ApiReturnCode.Succeed;
                }
            }
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("GetProductionSelectData")]
        public ApiResult GetProductionSelectData()
        {
            var typeList = _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true && x.DicTypeID == DictType.ProductionStyle)
                          .Select(x => new { Id = x.Id, TypeName = x.TypeName }).ToList();
            var matTypeList = _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true && x.DicTypeID == DictType.ProductionMatType)
                          .Select(x => new { Id = x.Id, TypeName = x.TypeName }).ToList();

            var vendorList = _context.Vendors.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).Select(x => new { x.Id, x.VendorName, x.SerialNum }).ToList();

            var mouldList = _context.Moulds.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).Select(x => new { x.Id, x.SerialNum }).ToList();
            var customerList= _context.Customers.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).Select(x => new { x.Id, x.SerialNum,x.CustomerName }).ToList();
            return new ApiResult
            {
                ReturnCode=ApiReturnCode.Succeed,
                Data=new
                {
                    StyleTypeList=typeList,
                    MatTypeList=matTypeList,
                    VendorList=vendorList,
                    MouldList=mouldList,
                    CustomerList=customerList
                }
            };
        }

        [HttpPost]
        [Route("UploadProductionImg")]
        public ApiResult UploadProductionImg()
        {
            
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var itemId = HttpContext.Request.Form["prodId"];
                Guid prodId = Guid.Empty;
                if (!Guid.TryParse(itemId, out prodId))
                {
                    return new ApiResult { Message = MessageUtil.ParamInvalid };
                }

                var file = HttpContext.Request.Form.Files[0];

                using (var stream = file.OpenReadStream())
                {
                    var content = new byte[stream.Length];
                    stream.Read(content, 0, (int)stream.Length);

                    var picId = Guid.NewGuid();

                    _context.ProductionImages.Add(new ProductionImage
                    {
                        Id= picId,
                        Content=content,
                        ProdId=prodId
                    });

                    if (_context.SaveChanges() > 0)
                    {
                        return new ApiResult { ReturnCode = ApiReturnCode.Succeed, Data = picId };
                    }
                    else
                    {
                        return new ApiResult
                        {
                            Message = "保存图片"
                        };
                    }

                   
                }

            }
          
            return new ApiResult { Message=MessageUtil.ParamInvalid};
        }

        [HttpGet]
        [Route("DeleteProductionImg")]
        public ApiResult DeleteProductionImg(Guid picId)
        {
            _context.ProductionImages.Remove(new ProductionImage { Id = picId });
            if (_context.SaveChanges() > 0)
            {
                return new ApiResult { ReturnCode=ApiReturnCode.Succeed};
            }
            else
            {
                return new ApiResult { Message=MessageUtil.ParamInvalid};
            }
           
        }

        [HttpGet]
        [Route("GetProdImgList")]
        public ApiResult GetProdImgList(Guid prodId)
        {
            var result = new ApiResult { };

            result.Data = _context.ProductionImages.Where(x => x.ProdId == prodId).Select(x => new
            {
                name="a.jpg",
                url= "/api/image/ShowProdPic?prodPicId="+x.Id,
                id=x.Id
            });
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;


        }

        [HttpGet]
        [Route("GetProdListByImg")]
        public ApiResult GetProdListByImg()
        {
            var result = new ApiResult { };
            result.Data = (from prodItem in _context.Productions.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true)
                           join img in _context.ProductionImages.Where(x => x.IsCover == true) on prodItem.Id equals img.ProdId
                           join matItem in _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true) on prodItem.MatTypeId equals matItem.Id
                           select new
                           {
                               prodItem.Id,
                               prodItem.SerialNum,
                               prodItem.Color,
                               prodItem.MatTypeId,
                               ImgId = img.Id,
                               MatName= matItem.TypeName
                           }).ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
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
