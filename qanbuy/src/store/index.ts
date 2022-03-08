import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    token: window.localStorage.getItem('token'),
  },
  mutations: {
    GET_USER: (state, data) => {
      // 把token存起来
      state.token = data
      window.localStorage.setItem('token', data)
    },
    LOGOUT: (state) => {
      // 登出的时候要清除token
      state.token = null
      window.localStorage.removeItem('token')
    }
  },
  actions: {
  },
  modules: {
  },
});
