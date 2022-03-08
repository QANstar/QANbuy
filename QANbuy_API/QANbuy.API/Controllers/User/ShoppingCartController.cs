using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QANbuy.Entity;
using QANbuy.Model;
using System.Security.Claims;

namespace QANbuy.API.Controllers.User
{
    [ApiController]
    [Route("api/User/[controller]/[action]")]
    public class ShoppingCartController : Controller
    {
        Qanbuy_DBContext Context;
        public ShoppingCartController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getShoppingCart()
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                return Ok(Tool.ChangeModel.shoppingCartViewToShowModel(Context.ShoppingCartView.Where(x => x.UserID == userID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addGood(ShoppingCartAddModel addModel)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                ShoppingCart shoppingCartChk = Context.ShoppingCart.FirstOrDefault(x => x.GoodID == addModel.GoodID && x.UserID == userID);
                if (shoppingCartChk != null)
                {
                    shoppingCartChk.Num += addModel.Num;
                }
                else
                {
                    ShoppingCart shoppingCart = new ShoppingCart();
                    shoppingCart.UserID = userID;
                    shoppingCart.GoodID = addModel.GoodID;
                    shoppingCart.Num = addModel.Num;
                    Context.ShoppingCart.Add(shoppingCart);
                }
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 编辑购买数量
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult editNum(ShoppingCartAddModel addModel)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                ShoppingCart shoppingCartChk = Context.ShoppingCart.FirstOrDefault(x => x.GoodID == addModel.GoodID && x.UserID == userID);
                if (shoppingCartChk != null)
                {
                    shoppingCartChk.Num = addModel.Num;
                }
                else
                {
                    return BadRequest();
                }
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 删除购物车中商品
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteCart(int goodID)
        {
            var auth = HttpContext.AuthenticateAsync();
            int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
            ShoppingCart cart = Context.ShoppingCart.FirstOrDefault(x => x.GoodID == goodID && x.UserID == userID);
            if (cart != null)
            {
                Context.ShoppingCart.Remove(cart);
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
