import axios from "axios";
const apiHost: string = "http://139.224.221.148:301";
// const apiHost: string = "https://localhost:44398";
export class Api {
    //登录
    static async login(admin: any): Promise<any> {
        return axios.post(apiHost + "/api/Management/Admin/Login", admin)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取商品类型
    static async getGoodType(): Promise<any> {
        return axios.get(apiHost + "/api/Management/GoodManage/getType")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //添加商品类型
    static async addGoodType(type: string): Promise<any> {
        return axios.post(apiHost + "/api/Management/GoodManage/addType?type=" + type)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //编辑商品类型
    static async editGoodType(good: any): Promise<any> {
        return axios.put(apiHost + "/api/Management/GoodManage/editType", good)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //删除商品类型
    static async deleteGoodType(id: number): Promise<any> {
        return axios.delete(apiHost + "/api/Management/GoodManage/deleteType?id=" + id)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取用户信息
    static async getUserData(): Promise<any> {
        return axios.get(apiHost + "/api/Management/UserManage/getUser")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }


    //获取订单状态
    static async getOrderStatus(): Promise<any> {
        return axios.get(apiHost + "/api/Management/OrderManage/getOrderState")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取订单数量
    static async getOrderNum(stateID:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/OrderManage/getOrderNumByState?stateID=" + stateID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取订单信息
    static async getOrderData(page:number,size:number,stateID:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/OrderManage/showOrderByState?page=" + page + "&size=" + size +"&stateID=" + stateID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //发货
    static async ship(id:number): Promise<any> {
        return axios.put(apiHost + "/api/Management/OrderManage/orderShip?id=" + id)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取分页数量
    static async getPageTotal(size:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/GoodManage/getPageTotal?size=" + size)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //分页获取商品
    static async getGoods(page:number,size:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/GoodManage/getGoods?page=" + page +"&size=" + size)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //添加商品
    static async addGood(good: any): Promise<any> {
        return axios.post(apiHost + "/api/Management/GoodManage/addGood",good)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
    
     //编辑商品
     static async editGood(good: any): Promise<any> {
        return axios.put(apiHost + "/api/Management/GoodManage/editGood", good)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //编辑商品状态
    static async editGoodState(id:number,stateID:number): Promise<any> {
        return axios.put(apiHost + "/api/Management/GoodManage/editGoodState?id=" + id + "&stateId=" + stateID)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //删除商品
    static async deleteGood(id: number): Promise<any> {
        return axios.delete(apiHost + "/api/Management/GoodManage/deleteGood?id=" + id)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取商品状态
    static async getGoodStatus(): Promise<any> {
        return axios.get(apiHost + "/api/Management/GoodManage/getStatus")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }

    //获取销量图表数据
    static async getTopNumCharts(num:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/Charts/getTopSaleNumData?num=" + num)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
    //获取销售图表数据
    static async getTopPaymentCharts(num:number): Promise<any> {
        return axios.get(apiHost + "/api/Management/Charts/getTopPaymentData?num=" + num)
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
    //获取类型数量图表数据
    static async getTypeNumCharts(): Promise<any> {
        return axios.get(apiHost + "/api/Management/Charts/getTypeNumData")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
    //获取类型数量图表数据
    static async getTypeSaleNumCharts(): Promise<any> {
        return axios.get(apiHost + "/api/Management/Charts/getTypeSaleNumData   ")
            .then((res: any) => {
                return res;
            }).catch((error: any) => {
                return error;
            });
    }
}