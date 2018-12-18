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
    public class CustomerController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;

        public CustomerController(RepManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/values
        //[Authorize]
        [HttpGet]
        [Route("GetCustomerList")]
        public ApiResult GetCustomerList()
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };
            result.Data = (from customer in _context.Customers where EF.Property<bool>(customer, ConstData.ShadowPropName_IsDeleted) != true
                           join dict in _context.Dictionaries on customer.TypeID equals dict.Id
                           select new
                           {
                               customer.Id,
                               customer.Level,
                               customer.Address,
                               customer.ContactPhone,
                               customer.CustomerEval,
                               customer.Demand,
                               customer.FactoryEval,
                               customer.ManagerName,
                               customer.ManagerPhone,
                               customer.Amount,
                               customer.SerialNum,
                               customer.Style,
                               customer.TypeID,
                               customer.CustomerName,
                               customer.Years,
                               
                               TypeName = dict.TypeName
                           }).ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }

        [HttpGet]
        [Route("GetCustomerForProduction")]
        public ApiResult GetCustomerForProduction()
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };
            result.Data = (from customer in _context.Customers
                           where EF.Property<bool>(customer, ConstData.ShadowPropName_IsDeleted) != true
                           select new
                           {
                               customer.Id,
                               customer.SerialNum,
                               customer.CustomerName
                             
                           }).ToList();
            result.ReturnCode = ApiReturnCode.Succeed;
            return result;
        }





        // GET api/values/5
        [HttpGet]
        [Authorize]
        [Route("DeleteCustomer")]
        public ApiResult Customer(Guid customerId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Customers.Find(customerId);
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
        public ApiResult Post(Customer customer)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (customer == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                if (customer.Id == Guid.Empty)
                {
                    customer.Id = Guid.NewGuid();
                    _context.AttachAddEntity(customer, userId);
                    _context.Customers.Add(customer);
                    
                }
                else
                {
                    _context.AttachUpdateEntity(customer, userId);
                    _context.Customers.Update(customer);
                    
                }
                if (_context.SaveChanges() > 0)
                {
                    var dict = _context.Dictionaries.FirstOrDefault(x => x.Id == customer.TypeID);
                    result.Data = new
                    {
                        customer.Id,
                        customer.Level,
                        customer.Address,
                        customer.ContactPhone,
                        customer.CustomerEval,
                        customer.Demand,
                        customer.FactoryEval,
                        customer.ManagerName,
                        customer.ManagerPhone,
                        customer.Amount,
                        customer.SerialNum,
                        customer.Style,
                        customer.TypeID,
                        customer.CustomerName,
                        customer.Years,
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



        
    }
}
