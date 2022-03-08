using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QANbuy.Entity;
using QANbuy.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QANbuy.API.Controllers.User
{
    [ApiController]
    [Route("api/User/[controller]/[action]")]
    public class UserController : Controller
    {
        Qanbuy_DBContext Context;
        public UserController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        public IActionResult userSginUp(SignUpModel userInfo)
        {
            try
            {
                bool isEmailHave = Context.User.ToList().Exists(x => x.Email == userInfo.Email );
                bool isNameHave = Context.User.ToList().Exists(x => x.UserName == userInfo.UserName);
                if (isEmailHave)
                {
                    return BadRequest("邮箱已被注册");
                }
                if (isNameHave)
                {
                    return BadRequest("用户名已被注册");
                }
                Entity.User user = Tool.ChangeModel.signUpToUser(userInfo);
                Context.User.Add(user);
                Context.SaveChanges();
                return Ok("注册成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        public IActionResult Login(LoginModel user)
        {
            Entity.User result = Context.User.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (result != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(60*24*7)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Sid, result.ID.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QANstarAndSuoMi1931"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "QANstar",
                    audience: "QANstar",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60 * 24 * 7),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [Authorize]
        [HttpGet]
        public IActionResult showUserInfo()
        {
            var auth = HttpContext.AuthenticateAsync();
            int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
            Entity.User user = Context.User.FirstOrDefault(x => x.ID == userID);
            if (user != null)
            {
                return Ok(Tool.ChangeModel.userToShowModel(user));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult editUserInfo(UserShowModel user)
        {
            var auth = HttpContext.AuthenticateAsync();
            int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
            bool isEmailHave = Context.User.ToList().Exists(x => x.Email == user.Email && x.ID!= userID);
            bool isNameHave = Context.User.ToList().Exists(x => x.UserName == user.UserName && x.ID != userID);
            if (isEmailHave)
            {
                return BadRequest("邮箱已被注册");
            }
            if (isNameHave)
            {
                return BadRequest("用户名已被注册");
            }
            Entity.User userT = Context.User.FirstOrDefault(x => x.ID == userID);
            if (userT != null)
            {
                userT.UserName = user.UserName;
                userT.HeadPic = System.Text.Encoding.Default.GetBytes(user.HeadPic);
                userT.Email = user.Email;
                userT.Address = user.Address;
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
