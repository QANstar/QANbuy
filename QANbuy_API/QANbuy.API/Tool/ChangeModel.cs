using QANbuy.Entity;
using QANbuy.Model;

namespace QANbuy.API.Tool
{
    public static class ChangeModel
    {
        public static IQueryable<SimpleGoodShowModel> goodViewToSimpleModel(IQueryable<GoodView> query)
        {
            return query.Select(x => new SimpleGoodShowModel()
            {
                ID = x.ID,
                Name = x.Name,
                GoodPic = Tool.byteToStr(x.GoodPic),
                Price = x.Price,
                saleNum = x.saleNum
            });
        }

        public static IQueryable<GoodShowModel> goodViewToShowModel(IQueryable<GoodView> query)
        {
            return query.Select(x => new GoodShowModel()
            {
                ID = x.ID,
                stateId = x.StateId,
                typeId = x.TypeId,
                Name = x.Name,
                Price = x.Price,
                GoodPic = Tool.byteToStr(x.GoodPic),
                state = x.state,
                type = x.type,
                Description = x.Description,
                Postage = x.Postage,
                ShipAddress = x.ShipAddress,
                CreateDate = x.CreateDate,
                saleNum = x.saleNum,
                Inventory = x.Inventory,
            });
        }

        public static IQueryable<ShoppingCartShowModel> shoppingCartViewToShowModel(IQueryable<ShoppingCartView> query)
        {
            return query.Select(x => new ShoppingCartShowModel()
            {
                Name = x.Name,
                Price = x.Price,
                Num = x.Num,
                UserID = x.UserID,
                GoodID = x.GoodID,
                GoodPic = Tool.byteToStr(x.GoodPic),
                Postage = x.Postage
            });
        }

        public static IQueryable<CommentShowModel> commentViewToShowModel(IQueryable<CommentView> query)
        {
            return query.Select(x => new CommentShowModel()
            {
                ID = x.ID,
                UserID = x.UserID,
                UserName = x.UserName,
                HeadPic = Tool.byteToStr(x.HeadPic),
                GoodID = x.GoodID,
                Comment = x.Comment,
                CommentDate = x.CommentDate,
            });
        }

        public static IQueryable<OrderShowModel> orderViewToShowModel(IQueryable<OrderView> query)
        {
            return query.Select(x => new OrderShowModel()
            {
                ID = x.ID,
                UserID = x.UserID,
                GoodPic = Tool.byteToStr(x.GoodPic),
                GoodID = x.GoodID,
                Num = x.Num,
                state = x.state,
                Payment = x.Payment,
                ShipAddress = x.ShipAddress,
                HarvestAddress = x.HarvestAddress,
                PayDate = x.PayDate,
                SendDate = x.SendDate,
                HarvestDate = x.HarvestDate,
                StateID = x.StateID,
                GoodName = x.GoodName,
                GoodPrice = x.GoodPrice,
                Postage = x.Postage
            });
        }
        public static OrderShowModel orderViewToShowModel(OrderView x)
        {
            return  new OrderShowModel()
            {
                ID = x.ID,
                UserID = x.UserID,
                GoodPic = Tool.byteToStr(x.GoodPic),
                GoodID = x.GoodID,
                Num = x.Num,
                state = x.state,
                Payment = x.Payment,
                ShipAddress = x.ShipAddress,
                HarvestAddress = x.HarvestAddress,
                PayDate = x.PayDate,
                SendDate = x.SendDate,
                HarvestDate = x.HarvestDate,
                StateID = x.StateID,
                GoodName = x.GoodName,
                GoodPrice = x.GoodPrice,
                Postage = x.Postage
            };
        }

        public static GoodShowModel goodViewToShowModel(GoodView x)
        {
            return new GoodShowModel()
            {
                ID = x.ID,
                stateId = x.StateId,
                typeId = x.TypeId,
                Name = x.Name,
                Price = x.Price,
                GoodPic = Tool.byteToStr(x.GoodPic),
                state = x.state,
                type = x.type,
                Description = x.Description,
                Postage = x.Postage,
                ShipAddress = x.ShipAddress,
                CreateDate = x.CreateDate,
                saleNum = x.saleNum,
                Inventory = x.Inventory,
            };
        }
        public static IQueryable<ChartsTopNumModel> goodViewToChartsTopNumModel(IQueryable<GoodView> query)
        {
            return query.Select(x => new ChartsTopNumModel()
            {
                name = x.Name,
                saleNum = x.saleNum,
            });
        }
        public static IQueryable<ChartsTopPaymentModel> goodViewToChartsTopPaymentModel(IQueryable<GoodView> query)
        {
            return query.Select(x => new ChartsTopPaymentModel()
            {
                name = x.Name,
                allPayment = x.saleNum * x.Price,
            });
        }
        public static IQueryable<UserShowModel> userToShowListModel(IQueryable<User> query)
        {
            return query.Select(x => new UserShowModel()
            {
                ID = x.ID,
                Email = x.Email,
                UserName = x.UserName,
                Address = x.Address,
                HeadPic = Tool.byteToStr(x.HeadPic),
            });
        }

        public static User signUpToUser(SignUpModel x)
        {
            return new User()
            {
                Email = x.Email,
                Password = x.Password,
                UserName = x.UserName,
            };
        }

        public static UserShowModel userToShowModel(User x)
        {
            return new UserShowModel()
            {
                ID = x.ID,
                Email = x.Email,
                UserName = x.UserName,
                Address = x.Address,
                HeadPic = Tool.byteToStr(x.HeadPic),
            };
        }
    }
}
