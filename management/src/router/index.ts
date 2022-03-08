import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';
import Management from '../views/Management.vue';
import GoodTypeMangement from '../components/GoodTypeMangement.vue';
import GoodsManage from '../components/GoodsManage.vue';
import UserManage from '../components/UserManage.vue';
import OrderManage from '../components/OrderManage.vue';
import Charts from '../components/Charts.vue';
import Login from '../views/Log.vue';

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/',
    name: 'Management',
    component: Management,
    children:[
      {
        path: '/',
        name: "GoodTypeMangement",
        redirect:'/Charts'
      },
      {
        path: '/GoodTypeMangement',
        component: GoodTypeMangement,
      },
      {
        path: '/GoodsManage',
        component: GoodsManage,
      },
      {
        path: '/UserManage',
        component: UserManage,
      },
      {
        path: '/OrderManage',
        component: OrderManage,
      },
      {
        path: '/Charts',
        component: Charts,
      },
    ]
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to,from,next)=>{
  if (to.path === '/login') {
    next();
  } else {
    let token = localStorage.getItem('token');
 
    if (token === null || token === '') {
      next('/login');
    } else {
      next();
    }
  }
});

export default router;
