using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepManagement.Common;
using RepManagement.Models;
using RepManagement.Web.Extension;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepManagement.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private RepManagementContext _context = null;
        private IHttpContextAccessor _httpContextAccessor = null;
        private IOptions<AppSettingsData> _settings = null;

        public UserController(RepManagementContext context, IHttpContextAccessor httpContextAccessor,IOptions<AppSettingsData> settings)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _settings = settings;
        }
        // GET: api/<controller>
        //[HttpGet]
        //public ApiResult GetUserList()
        //{
        //    return new ApiResult
        //    {
        //        ReturnCode = ApiReturnCode.Succeed,
        //        Data = _context.Users.ToList()
        //    };
        //}

        [HttpGet]
        [Route("Login")]
        public async Task<ApiResult> Login(string username,string password)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var user = _context.Users.FirstOrDefault(x=>EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted)!=true&&x.UserName==username&&x.Password==password);

            if (user == null)
            {
                return new ApiResult { Message="用户名或者密码错误！"};
            }

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, username));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.RoleValue.HasValue? user.RoleValue.Value.ToString():"0"));


            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                ExpiresUtc=DateTime.Now.AddDays(20)
            });

            return new ApiResult
            {
                ReturnCode = ApiReturnCode.Succeed,
                Data = new
                {
                    UserName=user.DisplayName,
                    RoleValue=user.RoleValue
                }
            };
        }

        [HttpGet]
        [Route("SignOut")]
        public ApiResult Signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return new ApiResult
            {
                ReturnCode = ApiReturnCode.Succeed
            };
        }

        [HttpGet]
        [Route("GetUserRoleList")]
        [Authorize]
        public ApiResult GetUserRoleList()
        {
            return new ApiResult
            {
                ReturnCode=ApiReturnCode.Succeed,
                Data=_context.Roles.Where(x=>EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted)!=true).OrderBy(x=>x.RoleType).ToList()
            };
        }

        [HttpPost]
        [Route("SaveRole")]
        [Authorize]
        public ApiResult SaveRole(Role role)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            var result = new ApiResult();
            if (role == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                if (role.Id == Guid.Empty)
                {
                    role.Id = Guid.NewGuid();
                    var count = _context.Roles.Count(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true);
                    role.RoleType = (int)Math.Pow(2.0, count*1.0);
                    _context.AttachAddEntity(role, userId);
                    _context.Roles.Add(role);
                }
                else
                {
                    _context.AttachUpdateEntity(role, userId);
                    _context.Roles.Update(role);
                }
                if (_context.SaveChanges() > 0)
                {
                    result.Data = role;
                    result.ReturnCode = ApiReturnCode.Succeed;
                }
            }
            return result;
           
        }

        [HttpGet]
        [Route("GetUserList")]
        [Authorize]
        public ApiResult GetUserList()
        {

            return new ApiResult
            {
                ReturnCode=ApiReturnCode.Succeed,
                Data=_context.Users.Where(x => EF.Property<bool>(x, ConstData.ShadowPropName_IsDeleted) != true).ToList()
            };
        }

        [HttpPost]
        [Route("PostUser")]
        [Authorize]
        public ApiResult PostUser(User user)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            var result = new ApiResult();
            if (user == null)
            {
                result.Message = MessageUtil.ParamInvalid;
            }
            else
            {
                if (user.Id == Guid.Empty)
                {
                    user.Id = Guid.NewGuid();

                    var strDefault=_settings.Value.DefaultPassword;
                    user.Password = UtilFunc.SHA256(strDefault);
                    _context.AttachAddEntity(user, userId);
                    _context.Users.Add(user);
                }
                else
                {
                    _context.AttachUpdateEntity(user, userId);
                    _context.Users.Update(user);
                }
                if (_context.SaveChanges() > 0)
                {
                    result.Data = user;
                    result.ReturnCode = ApiReturnCode.Succeed;
                }
            }
            return result;
        }


        [HttpGet]
        [Authorize]
        [Route("DeleteItem")]
        public ApiResult DeleteItem(Guid itemId)
        {
            ApiResult result = new ApiResult { ReturnCode = ApiReturnCode.Error };

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;

            var item = _context.Users.Find(itemId);
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
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            try
            {
                if (_context!=null && _context.Database!=null && _context.Database.EnsureCreated())
                {
                    if (_context.Users.FirstOrDefault(x => x.UserName == "admin")!=null)
                    {
                        return "exist";
                    }
                    _context.Users.Add(new User
                    {
                        Id=Guid.NewGuid(),
                        UserName="admin",
                        DisplayName="管理员",
                        Password=UtilFunc.SHA256("123abc"),
                        RoleValue=1
                    });
                    if (_context.SaveChanges() > 0)
                    {
                        return "success";
                    }
                    else
                    {
                        return "false";
                    }
                }
                else
                {
                    return "error";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
           
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
