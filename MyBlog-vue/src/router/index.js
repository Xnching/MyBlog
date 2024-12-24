import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Blogs from '../views/Blogs.vue'
import BlogEdit from '../views/BlogEdit.vue'
import BlogDetail from '../views/BlogDetail.vue'
import BlogAdd from '../views/BlogAdd.vue'
import TypeInfo from '../views/manger/TypeInfo.vue'
import BlogNews from '../views/manger/BlogNews.vue'
import WriteInfo from '../views/manger/WriteInfo.vue'
import Register from '../views/Register.vue'



Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Index',
    redirect: {name: "Login"}
  },
  {
    path: '/blogs',
    name: 'Blogs',
    component: Blogs
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/blog/add',
    name: 'BlogAdd',
    component: BlogAdd,
    meta: {
      requireAuth: true
    }
  },
  {
    path: '/blog/:blogId',
    name: 'BlogDetail',
    component: BlogDetail
  },
  {
    path: '/blog/:blogId/edit',
    name: 'BlogEdit',
    component: BlogEdit,
    meta: {
      requireAuth: true
    }
  },
  {
    path: '/manger/blogNews',
    name: 'BlogNews',
    component: BlogNews
  },
  {
    path: '/manger/typeInfo',
    name: 'TypeInfo',
    component: TypeInfo
  },
  {
    path: '/manger/writerInfo',
    name: 'WriterInfo',
    component: WriteInfo
  },
  {
    path: '/manger',
    name: 'BlogNews',
    component: BlogNews
  }

]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})



export default router
