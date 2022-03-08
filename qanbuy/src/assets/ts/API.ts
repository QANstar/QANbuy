import axios from "axios";
const apiHost: string = "http://139.224.221.148:301";
// const apiHost: string = "https://localhost:44398";
export class Api {
    //获取销量排行前几的商品
    static async getTopGoods(num: number): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getGoodsTop?topNum=" + num)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //按类型获取销量排行前几的商品
    static async getTypeGoods(page:number,size:number,type: number): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getGoodsByType?page=" + page + "&size=" + size + "&type=" + type)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取商品类型
    static async getGoodType(): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getType")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //筛选获取商品数量数量
    static async getNumByType(typeId:number): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getTotalNumByType?typeId=" + typeId)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //搜索获取商品
    static async searchGoods(page:number,size:number,searchStr: string): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/searchGoods?page=" + page + "&size=" + size + "&searchStr=" + searchStr)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //模糊查找数量
    static async searchNum(searchStr:string): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/searchGoodsNum?searchStr=" + searchStr)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

     //注册api
     static async signUpApi(userInfo:any): Promise<any> {
        return axios.post(apiHost + "/api/User/User/userSginUp", userInfo)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

     //登录api
     static async loginAPi(user: any): Promise<any> {
        return axios.post(apiHost + "/api/User/User/Login", user)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取用户信息
    static async getUserInfo(): Promise<any> {
        return axios.get(apiHost + "/api/User/User/showUserInfo")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //修改用户信息
    static async editUserInfo(user:any): Promise<any> {
        return axios.post(apiHost + "/api/User/User/editUserInfo",user)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //显示商品细节
    static async getGoodDetail(goodID:number): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getGoodDetail?goodID=" + goodID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //发布评论
    static async putComment(comment:any): Promise<any> {
        return axios.post(apiHost + "/api/User/Good/sentComment",comment)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //显示评论
    static async getComment(goodID:number): Promise<any> {
        return axios.get(apiHost + "/api/User/Good/getComments?goodID=" + goodID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取购物车
    static async getShoppingCart(): Promise<any> {
        return axios.get(apiHost + "/api/User/ShoppingCart/getShoppingCart")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //添加购物车
    static async addShoppingCart(goodAddInfo:any): Promise<any> {
        return axios.post(apiHost + "/api/User/ShoppingCart/addGood",goodAddInfo)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //编辑购物车商品数量
    static async editShoppingCartNum(goodAddInfo:any): Promise<any> {
        return axios.post(apiHost + "/api/User/ShoppingCart/editNum",goodAddInfo)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

     //购买商品
     static async buyGood(buyInfo:any): Promise<any> {
        return axios.post(apiHost + "/api/User/Order/createOrder",buyInfo)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //批量购买商品
    static async buyGoods(goodList:any): Promise<any> {
        return axios.post(apiHost + "/api/User/Order/createOrders",goodList)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取订单
    static async getOrderByState(page:number,size:number,state: number): Promise<any> {
        return axios.get(apiHost + "/api/User/Order/getOrderList?page=" + page + "&size=" + size + "&state=" + state)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取订单状态
    static async getOrderState(): Promise<any> {
        return axios.get(apiHost + "/api/User/Order/getOrderState")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
     //获取订单数量
     static async getOrderNum(state:number): Promise<any> {
        return axios.get(apiHost + "/api/User/Order/getOrderNum?state=" + state)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //确认收获
    static async suerGet(orderId:number): Promise<any> {
        return axios.put(apiHost + "/api/User/Order/sureGet?orderId=" + orderId)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //根据id获取订单信息
    static async getOrderById(orderId:number): Promise<any> {
        return axios.get(apiHost + "/api/User/Order/getOrderById?orderID=" + orderId)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //购物车删除商品
    static async deleteCartGood(goodID:number): Promise<any> {
        return axios.delete(apiHost + "/api/User/ShoppingCart/deleteCart?goodID=" + goodID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
}