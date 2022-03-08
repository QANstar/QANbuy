using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QANbuy.Entity;
using QANbuy.Model;

namespace QANbuy.API.Controllers.Management
{
    [ApiController]
    [Route("api/Management/[controller]/[action]")]
    public class GoodManageController : Controller
    {
        Qanbuy_DBContext Context;
        public GoodManageController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 显示商品类型列表
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getType()
        {
            try
            {
                return Ok(Context.GoodType.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 新增商品类型
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addType(string type)
        {
            try
            {
                GoodType typeT = new GoodType();
                typeT.type = type;
                Context.GoodType.Add(typeT);
                Context.SaveChanges();
                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 编辑商品类型
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editType(GoodType type)
        {
            GoodType typeT = Context.GoodType.FirstOrDefault(x => x.ID == type.ID);
            if (typeT != null)
            {
                typeT.type = type.type;
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteType(int id)
        {
            GoodType typeT = Context.GoodType.FirstOrDefault(x => x.ID == id);
            if (typeT != null)
            {
                Context.GoodType.Remove(typeT);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 显示商品状态列表
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getStatus()
        {
            try
            {
                return Ok(Context.GoodStatus.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 新增商品状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addState(string state)
        {
            try
            {
                GoodStatus stateT = new GoodStatus();
                stateT.state = state;
                Context.GoodStatus.Add(stateT);
                Context.SaveChanges();
                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 编辑商品状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editState(GoodStatus state)
        {
            GoodStatus stateT = Context.GoodStatus.FirstOrDefault(x => x.ID == state.ID);
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
        /// 删除商品状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteState(int id)
        {
            GoodStatus stateT = Context.GoodStatus.FirstOrDefault(x => x.ID == id);
            if (stateT != null)
            {
                Context.GoodStatus.Remove(stateT);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 返回分页数量
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getPageTotal(int size)
        {
            try
            {
                int result = Context.GoodView.Count();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 分页选择商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getGoods(int page, int size)
        {
            try
            {
                var result = Context.GoodView.OrderBy(x => x.ID).Skip((page - 1) * size).Take(size);
                return Ok(Tool.ChangeModel.goodViewToShowModel(result).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addGood(GoodAddModel good)
        {
            try
            {
                Goods goodsT = new Goods();
                goodsT.Name = good.Name;
                goodsT.Description = good.Description;
                goodsT.Price = good.Price;
                goodsT.Postage = good.Postage;
                goodsT.ShipAddress = good.ShipAddress;
                goodsT.State = Const.GoodStatus.PULL;
                goodsT.Type = good.Type;
                goodsT.Inventory = good.Inventory;
                goodsT.GoodPic = System.Text.Encoding.Default.GetBytes(good.GoodPic);
                goodsT.CreateDate = DateTime.Now;
                Context.Goods.Add(goodsT);
                Context.SaveChanges();
                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editGood(GoodEditModel good)
        {
            Goods goodsT = Context.Goods.FirstOrDefault(x => x.ID == good.ID);
            if (goodsT != null)
            {
                goodsT.Name = good.Name;
                goodsT.Description = good.Description;
                goodsT.Price = good.Price;
                goodsT.Postage = good.Postage;
                goodsT.ShipAddress = good.ShipAddress;
                goodsT.Inventory = good.Inventory;
                goodsT.Type = good.Type;
                goodsT.GoodPic = System.Text.Encoding.Default.GetBytes(good.GoodPic);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 编辑商品状态
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editGoodState(int id,int stateId)
        {
            Goods goodsT = Context.Goods.FirstOrDefault(x => x.ID == id);
            if (goodsT != null)
            {
                goodsT.State = stateId;
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteGood(int id)
        {
            Goods goodsT = Context.Goods.FirstOrDefault(x => x.ID == id);
            if (goodsT != null)
            {
                Context.Goods.Remove(goodsT);
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
