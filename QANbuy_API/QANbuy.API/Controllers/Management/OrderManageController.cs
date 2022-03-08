using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QANbuy.Entity;

namespace QANbuy.API.Controllers.Management
{
    [ApiController]
    [Route("api/Management/[controller]/[action]")]
    public class OrderManageController : Controller
    {
        Qanbuy_DBContext Context;
        public OrderManageController(Qanbuy_DBContext context)
        {
            Context = context;
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
        /// 添加订单状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addOrderState(string state)
        {
            try
            {
                OrderStatus stateT = new OrderStatus();
                stateT.state = state;
                Context.OrderStatus.Add(stateT);
                Context.SaveChanges();
                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 编辑订单状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editGood(OrderStatus state)
        {
            OrderStatus stateT = Context.OrderStatus.FirstOrDefault(x => x.ID == state.ID);
            if (stateT != null)
            {
                stateT.state = state.state;
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 删除订单状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteOrderState(int id)
        {
            OrderStatus stateT = Context.OrderStatus.FirstOrDefault(x => x.ID == id);
            if (stateT != null)
            {
                Context.OrderStatus.Remove(stateT);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 按状态分页显示订单
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult showOrderByState(int page, int size,int stateID)
        {
            try
            {
                var order = Context.OrderView.Where(x => x.StateID == stateID).Skip((page - 1) * size).Take(size);
                return Ok(Tool.ChangeModel.orderViewToShowModel(order));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        /// <summary>
        /// 按状态显示订单数量
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getOrderNumByState(int stateID)
        {
            try
            {
                List<OrderView> order = Context.OrderView.Where(x => x.StateID == stateID).ToList();
                return Ok(order.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult orderShip(int id)
        {
            Order order = Context.Order.FirstOrDefault(x => x.ID == id);
            if(order != null)
            {
                if(order.State == Const.OrderStatus.UNSHIPPED)
                {
                    order.State = Const.OrderStatus.SHIPPED;
                    order.SendDate = DateTime.Now;
                    Context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("订单未处于未发货状态");
                }
            }
            else
            {
                return BadRequest("订单不存在");
            }

        }
    }
}
