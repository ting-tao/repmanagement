using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepManagement.Common;
using RepManagement.Models;
using RepManagement.Web.Extension;

namespace RepManagement.Web.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;

        public VendorController(RepManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        [Route("GetVendorList")]
        public ApiResult GetVendorList()
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };
            result.Data = (from vendor in _context.Vendors where EF.Property<bool>(vendor, ConstData.ShadowPropName_IsDeleted) != true
                           join dict in _context.Dictionaries on vendor.TypeID equals dict.Id
                           select new
                           {
                               vendor.Id,
                               vendor.Level,
                               vendor.Address,
                               vendor.ContactPhone,
                               vendor.CustomerEval,
                               vendor.Deadline,
                               vendor.FactoryEval,
                               vendor.ManagerName,
                               vendor.ManagerPhone,
                               vendor.Production,
                               vendor.SerialNum,
                               vendor.Style,
                               vendor.TypeID,
                               vendor.VendorName,
                               vendor.Years,
                               TypeName = dict.TypeName
                           }).ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("GetVendorForMaterial")]
        public ApiResult GetVendorForMaterial()
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };
            result.Data = (from vendor in _context.Vendors
                           where EF.Property<bool>(vendor, ConstData.ShadowPropName_IsDeleted) != true
                           select new
                           {
                               vendor.Id,
                               //vendor.Level,
                               //vendor.Address,
                               //vendor.ContactPhone,
                               //vendor.CustomerEval,
                               //vendor.Deadline,
                               //vendor.FactoryEval,
                               //vendor.ManagerName,
                               //vendor.ManagerPhone,
                               //vendor.Production,
                               vendor.SerialNum,
                               //vendor.Style,
                               //vendor.TypeID,
                               vendor.VendorName,
                               //vendor.Years,
                               //TypeName = dict.TypeName
                           }).ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }





        // GET api/values/5
        [HttpGet]
        [Authorize]
        [Route("DeleteVendor")]
        public ApiResult DeleteVendor(Guid vendorId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Vendors.Find(vendorId);
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
        public ApiResult Post(Vendor vendor)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (vendor == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                if (vendor.Id == Guid.Empty)
                {
                    vendor.Id = Guid.NewGuid();
                    _context.AttachAddEntity(vendor, userId);
                    _context.Vendors.Add(vendor);                
                }
                else
                {
                    _context.AttachUpdateEntity(vendor, userId);
                    _context.Vendors.Update(vendor);
                }
                if (_context.SaveChanges() > 0)
                {
                    var dict = _context.Dictionaries.FirstOrDefault(x => x.Id == vendor.TypeID);
                    result.Data = new
                    {
                        vendor.Id,
                        vendor.Level,
                        vendor.Address,
                        vendor.ContactPhone,
                        vendor.CustomerEval,
                        vendor.Deadline,
                        vendor.FactoryEval,
                        vendor.ManagerName,
                        vendor.ManagerPhone,
                        vendor.Production,
                        vendor.SerialNum,
                        vendor.Style,
                        vendor.TypeID,
                        vendor.VendorName,
                        vendor.Years,
                        TypeName = dict.TypeName
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
