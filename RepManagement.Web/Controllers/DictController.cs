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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepManagement.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DictController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;

        public DictController(RepManagementContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public ApiResult Get()
        {
            return new ApiResult
            {
                ReturnCode = ApiReturnCode.Succeed,
                Data = _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).ToList()
            };
        }

        
        [HttpGet]
        [Route("GetDictByType")]
        public ApiResult GetDictByType(int typeId)
        {
            return new ApiResult
            {
                ReturnCode = ApiReturnCode.Succeed,
                Data = _context.Dictionaries.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true&&x.DicTypeID==(DictType)Enum.Parse(typeof(DictType),typeId.ToString())).ToList()
            };
        }

        [Authorize]
        [HttpGet]
        [Route("DeleteDict")]
        public ApiResult DeleteDict(Guid id)
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;



            var item = _context.Dictionaries.FirstOrDefault(x => x.Id == id && EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true);
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



        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [Authorize]
        [HttpPost]
        public ApiResult Post(Dictionary dictionary)
        {
            var result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            if (dictionary.Id != Guid.Empty)
            {
                //update
                //_context.
                var item = _context.Dictionaries.FirstOrDefault(x => x.Id == dictionary.Id && EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true);
                if (item == null)
                {
                    result.Message = MessageUtil.ItemNotFound;
                }
                else
                {
                    item.DicTypeID = dictionary.DicTypeID;
                    item.TypeName = dictionary.TypeName;
                    item.Description = dictionary.Description;
                    _context.AttachUpdateEntity(item, userId);
                    _context.SaveChanges();
                    result.Data = item;
                    result.ReturnCode = ApiReturnCode.Succeed;
                }
            }
            else
            {
                dictionary.Id = Guid.NewGuid();
                _context.AttachAddEntity(dictionary, userId);
                _context.Dictionaries.Add(dictionary);
                _context.SaveChanges();
                result.Data = dictionary;
                result.ReturnCode = ApiReturnCode.Succeed;
                //_context.Entry<dictionary>().Property(ConstData.ShadowPropName_CreatedDate).CurrentValue = DateTime.Now;
            }
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
