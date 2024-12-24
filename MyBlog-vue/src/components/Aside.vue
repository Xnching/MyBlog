<template>
  <!--   此处更改默认打开的导航，1，3对应导航，style为隐藏边线多余部分   -->
  <el-menu :default-openeds="['1', '3']"    
      background-color=rgb(48,65,86)
      text-color=#ccc
      active-text-color=red
      router=""
      style="min-height:100%; overflow-x:hidden">
    <!-- LOGO位置-->
    <div style="height:60px; line-height:60px; text-align:center">
      <img src="../assets/logo.png" style="width:20px;position:relative;top:5px;margin-right:5px"/>
      <b style="color:white">博客后台管理系统</b>
    </div>
    <el-menu-item index="/manger/blogNews">
        <i class="el-icon-menu"></i>
        <span slot="title">文章管理</span>
      </el-menu-item>
      <el-menu-item index="/manger/typeInfo">
        <i class="el-icon-document"></i>
        <span slot="title">类型管理</span>
      </el-menu-item>
      <el-menu-item index="/manger/writerInfo">
        <i class="el-icon-setting"></i>
        <span slot="title">用户管理</span>
      </el-menu-item>
  </el-menu>
</template>


<script>
export default {
    //输出组件
    name: "Aside",
    data(){
      return{
        menus:localStorage.getItem("menus")?JSON.parse(localStorage.getItem("menus")):[],  
        opens:localStorage.getItem("menus")?JSON.parse(localStorage.getItem("menus")).map(v=>v.id+''):[],
      }
    },
    methods:{
      showmst(){
        console(this.opens)
      }
    }
}
</script>

<style scoped>
.el-menu {
  overflow-y: auto;
}

.el-menu::-webkit-scrollbar {
  display: none;
}

.el-menu {
  -ms-overflow-style: none;
}
</style>
