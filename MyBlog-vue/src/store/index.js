import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: '',
    userInfo: JSON.parse(sessionStorage.getItem("userInfo"))
  },
  mutations: {
    // set
    SET_TOKEN: (state, token) => {
      state.token = token
      console.log("Token updated:", state.token);
      localStorage.setItem("token", token)
    },
    SET_USERINFO: (state, userInfo) => {
      state.userInfo = userInfo
      console.log("UserInfo updated:", state.userInfo);
      sessionStorage.setItem("userInfo", JSON.stringify(userInfo))
    },
    REMOVE_INFO: (state) => {
      state.token = ''
      state.userInfo = {}
      localStorage.setItem("token", '')
      sessionStorage.setItem("userInfo", JSON.stringify(''))
    }

  },
  getters: {
    // get
    getUser: state => {
      return state.userInfo
    }

  },
  actions: {
  },
  modules: {
  }
})
