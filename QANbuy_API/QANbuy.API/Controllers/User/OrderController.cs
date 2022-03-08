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
    public class OrderController : Controller
    {
        Qanbuy_DBContext Context;
        public OrderController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 购买/新增订单接口
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult createOrder(OrderCreateModel orderInfo)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                Goods good = Context.Goods.FirstOrDefault(x => x.ID == orderInfo.GoodID);
                if (orderInfo.Num > good.Inventory)
                {
                    return BadRequest("购买失败，库存不足");
                }
                else
                {
                    Entity.User user = Context.User.FirstOrDefault(x => x.ID == userID);
                    Order order = new Order();
                    order.GoodID = orderInfo.GoodID;
                    order.GoodName = good.Name;
                    order.GoodPic = good.GoodPic;
                    order.GoodPrice = good.Price;
                    order.Postage = good.Postage;
                    order.UserID = userID;
                    order.Num = orderInfo.Num;
                    order.State = Const.OrderStatus.UNSHIPPED;
                    order.Payment = good.Price * order.Num + good.Postage;
                    order.ShipAddress = good.ShipAddress;
                    order.HarvestAddress = user.Address;
                    order.PayDate = DateTime.Now;

                    good.Inventory -= orderInfo.Num;
                    good.saleNum += orderInfo.Num;

                    Context.Order.Add(order);
                    Context.SaveChanges();
                    return Ok("购买成功");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 分页获取订单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getOrderList(int page, int size, int state)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                return Ok(Tool.ChangeModel.orderViewToShowModel(Context.OrderView.Where(x => x.UserID == userID && x.StateID==state).Skip((page - 1) * size).Take(size)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getOrderState()
        {
            try
            {
                return Ok(Context.OrderStatus.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getOrderNum(int state)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                return Ok(Tool.ChangeModel.orderViewToShowModel(Context.OrderView.Where(x => x.UserID == userID && x.StateID == state)).Count());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult sureGet(int orderId)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                Order order = Context.Order.FirstOrDefault(x => x.ID == orderId && x.UserID == userID);
                order.State = Const.OrderStatus.COMPLETED;
                order.HarvestDate = DateTime.Now;
                Context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 根据id获取订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getOrderById(int orderID)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                return Ok(Tool.ChangeModel.orderViewToShowModel(Context.OrderView.FirstOrDefault(x => x.UserID == userID && x.ID == orderID)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 购物车 购买/新增多个订单接口
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult createOrders(List<int> goodList)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                for (int i = 0; i < goodList.Count(); i++)
                {
                    ShoppingCart cart = Context.ShoppingCart.FirstOrDefault(x => x.GoodID == goodList[i] && x.UserID == userID);
                    Goods good = Context.Goods.FirstOrDefault(x => x.ID == goodList[i]);
                    if (cart.Num > good.Inventory)
                    {
                        return BadRequest("购买失败，商品存在库存不足");
                    }
                    else
                    {
                        Entity.User user = Context.User.FirstOrDefault(x => x.ID == userID);
                        Order order = new Order();
                        order.GoodID = cart.GoodID;
                        order.GoodName = good.Name;
                        order.GoodPic = good.GoodPic;
                        order.GoodPrice = good.Price;
                        order.Postage = good.Postage;
                        order.UserID = userID;
                        order.Num = cart.Num;
                        order.State = Const.OrderStatus.UNSHIPPED;
                        order.Payment = good.Price * order.Num + good.Postage;
                        order.ShipAddress = good.ShipAddress;
                        order.HarvestAddress = user.Address;
                        order.PayDate = DateTime.Now;

                        good.Inventory -= cart.Num;
                        good.saleNum += cart.Num;

                        Context.Order.Add(order);
                        Context.ShoppingCart.Remove(cart);
                    }
                
                
                }
                Context.SaveChanges();
                return Ok("购买成功");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
