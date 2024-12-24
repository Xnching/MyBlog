<template>
    <el-dropdown style="width:250px; cursor:pointer">
        <span>{{user.username}}</span><i class="el-icon-arrow-down" style="margin-left:5px"></i>
        <el-dropdown-menu slot="dropdown">
            <el-dropdown-item>个人信息</el-dropdown-item>
            <el-dropdown-item>
                <span style="text-decoration: none" @click="logout">退出</span>
            </el-dropdown-item>
        </el-dropdown-menu>
    </el-dropdown>  
</template>
<script>
export default {
    name: "MangerHeader",
    data() {
        return{
            user: {
                username: '请先登录',
                avatar: 'https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png'
            },
        }
    },
    methods:{
        logout() {
            const _this = this
            _this.$store.commit("REMOVE_INFO")
            _this.$router.push("/login")
        }
    },
    created() {
      if(this.$store.getters.getUser.name) {
        this.user.username = this.$store.getters.getUser.name
      }

    }
}
</script>

<style>

</style>
