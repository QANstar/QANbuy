import { Main } from 'element-ui';
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';
import MainView from '../views/MainView.vue';
import BuyHome from '../components/BuyHome.vue'
import BuyFilter from '../components/BuyFilter.vue'
import BuySearch from '../components/BuySearch.vue'
import UserCenter from '../components/UserCenter.vue'
import GoodDetail from '../components/GoodDetail.vue'
import ShoppingCar from '../components/ShoppingCar.vue'
import Order from '../components/Order.vue'
import OrderView from '../components/OrderView.vue'

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/',
    name: 'Main',
    component: MainView,
    children:[
      {
        path: '/',
        name: "Main",
        redirect:'/Home'
      },
      {
        path: '/Home',
        component: BuyHome,
      },
      {
        path: '/Filter',
        component: BuyFilter,
      },
      {
        name: "Search",
        path: '/Search/searchStr=:searchStr',
        component: BuySearch,
      },
      {
        name: "UserCenter",
        path: '/UserCenter',
        component: UserCenter,
      },
      {
        path: '/GoodDetail/goodID=:goodID',
        component: GoodDetail,
      },
      {
        path: '/ShoppingCar',
        component: ShoppingCar,
      },
      {
        path: '/Order',
        component: Order,
      },
      {
        path: '/OrderView/orderID=:orderID',
        component: OrderView,
      },
    ]
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
