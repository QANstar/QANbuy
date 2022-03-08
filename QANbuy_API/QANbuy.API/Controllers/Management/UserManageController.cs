using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QANbuy.Entity;

namespace QANbuy.API.Controllers.Management
{
    [ApiController]
    [Route("api/Management/[controller]/[action]")]
    public class UserManageController : Controller
    {
        Qanbuy_DBContext Context;
        public UserManageController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 显示注册会员
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getUser()
        {
            try
            {
                return Ok(Tool.ChangeModel.userToShowListModel(Context.User));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }                                                                                                                                  
        }   
    }
}
