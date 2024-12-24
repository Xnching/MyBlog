import axios from 'axios'
import Element from 'element-ui'
import router from './router'
import store from './store'


axios.defaults.baseURL = "http://localhost:8081/api"

// 前置拦截器：在发送请求前添加 JWT 令牌到请求头
axios.interceptors.request.use(config => {
  // 从 Vuex store 中获取令牌
  const token = store.state.token; // 确保这里的 token 对应你在 Vuex store 中存储令牌的状态名
  if (token) {
    // 如果存在令牌，则添加到请求头
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, error => {
  // 对请求错误做些什么
  return Promise.reject(error);
});

axios.interceptors.response.use(response => {
    let res = response.data;

    console.log("=================")
    console.log(res)
    console.log("=================")

    if (res.code === 200) {
      return response
    } else {

      Element.Message.error('错了哦，这是一条错误消息', {duration: 3 * 1000})

      return Promise.reject(response.data.msg)
    }
  },
  error => {
    console.log(error)
    if(error.response.data) {
      error.message = error.response.data.msg
    }

    if(error.response.status === 401) {
      store.commit("REMOVE_INFO")
      router.push("/login")
    }

    Element.Message.error(error.message, {duration: 3 * 1000})
    return Promise.reject(error)
  }
)