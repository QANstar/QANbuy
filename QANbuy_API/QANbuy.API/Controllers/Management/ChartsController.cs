using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QANbuy.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QANbuy.API.Controllers.Management
{
    [ApiController]
    [Route("api/Management/[controller]/[action]")]
    public class ChartsController : Controller
    {
        Qanbuy_DBContext Context;
        public ChartsController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 获取销量最高数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getTopSaleNumData(int num)
        {
            try
            {
                var result = Context.GoodView.OrderByDescending(x => x.saleNum).Take(num);
                return Ok(Tool.ChangeModel.goodViewToChartsTopNumModel(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 获取销售额最高数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getTopPaymentData(int num)
        {
            try
            {
                var result = Context.GoodView.OrderByDescending(x => x.saleNum*x.Price).Take(num);
                return Ok(Tool.ChangeModel.goodViewToChartsTopPaymentModel(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 获取类型总销售数
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getTypeSaleNumData()
        {
            try
            {
                var result = Context.GoodView.GroupBy(x=>x.type).Select(x=>new { 类型=x.Key, 总销售数 = x.Sum(y => y.saleNum) });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 显示各个类型数量
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getTypeNumData()
        {
            try
            {
                var result = Context.GoodView.GroupBy(x => x.type).Select(x => new { 类型 = x.Key, 数量 = x.Key.Count() });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
