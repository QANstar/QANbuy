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
    public class GoodController : Controller
    {
        Qanbuy_DBContext Context;
        public GoodController(Qanbuy_DBContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 显示商品类型列表
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
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
        /// 分页显示商品大概信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getGoodsSimple(int page, int size)
        {
            try
            {
                var result = Context.GoodView.Where(x=> x.StateId != Const.GoodStatus.PULL).OrderByDescending(x => x.ID).Skip((page - 1) * size).Take(size);
                return Ok(Tool.ChangeModel.goodViewToSimpleModel(result).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 按类型分页显示商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getGoodsByType(int page, int size, int type)
        {
            try
            {
                if (type == 0)
                {
                    var result = Context.GoodView.Where(x => x.StateId != Const.GoodStatus.PULL).OrderByDescending(x => x.saleNum).Skip((page - 1) * size).Take(size);
                    return Ok(Tool.ChangeModel.goodViewToSimpleModel(result).ToList());
                }
                else
                {
                    var result = Context.GoodView.Where(x => x.TypeId == type && x.StateId != Const.GoodStatus.PULL).OrderByDescending(x => x.saleNum).Skip((page - 1) * size).Take(size);
                    return Ok(Tool.ChangeModel.goodViewToSimpleModel(result).ToList());
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 筛选返回分页数量
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getTotalNumByType(int typeId)
        {
            try
            {
                int result;
                if (typeId == 0)
                {
                    result = Context.GoodView.Where(x => x.StateId != Const.GoodStatus.PULL).Count();

                }
                else
                {
                    result = Context.GoodView.Where(x => x.TypeId == typeId && x.StateId != Const.GoodStatus.PULL).Count();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 按销量最高返回商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getGoodsTop(int topNum)
        {
            try
            {
                var result = Context.GoodView.Where(x => x.StateId != Const.GoodStatus.PULL).OrderByDescending(x => x.saleNum).Take(topNum);
                return Ok(Tool.ChangeModel.goodViewToSimpleModel(result).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 分页显示搜索结果
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult searchGoods(int page, int size, string searchStr)
        {
            try
            {
                var result = Context.GoodView.Where(x => x.Name.Contains(searchStr) && x.StateId != Const.GoodStatus.PULL).OrderByDescending(x => x.saleNum).Skip((page - 1) * size).Take(size);
                return Ok(Tool.ChangeModel.goodViewToSimpleModel(result).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 模糊查找结果的数量
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult searchGoodsNum(string searchStr)
        {
            try
            {
                int result = Context.GoodView.Where(x => x.StateId != Const.GoodStatus.PULL && x.Name.Contains(searchStr)).Count();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 显示商品细节
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getGoodDetail(int goodID)
        {
            try
            {
                var result = Context.GoodView.FirstOrDefault(x => x.ID == goodID);
                return Ok(Tool.ChangeModel.goodViewToShowModel(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult sentComment(PutCommentModel comment)
        {
            try
            {
                var auth = HttpContext.AuthenticateAsync();
                int userID = int.Parse(auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Sid))?.Value);
                Comments comments = new Comments();
                comments.UserID = userID;
                comments.CommentDate = DateTime.Now;
                comments.GoodID = comment.GoodID;
                comments.Comment = comment.Comment;
                Context.Comments.Add(comments);
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 显示评论
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        public IActionResult getComments(int goodID)
        {
            try
            {
                return Ok(Tool.ChangeModel.commentViewToShowModel(Context.CommentView.Where(x => x.GoodID == goodID)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
