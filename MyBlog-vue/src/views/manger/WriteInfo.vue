<template>
  <div class="home" style="height:100%;">
   <el-container style="height: 100%; border: 1px solid #eee" >
    <!--此处为侧边栏-->
	  <el-aside width="200px" style="background-color: rgb(238, 241, 246); height: 100%; overflow: auto;">
        <Aside></Aside>
    </el-aside>

	  <el-container>

      <!--  此处为header -->
	    <el-header style="text-align: right; font-size: 12px; border-bottom: 1px solid red; line-height:60px">
        <MangerHeader name="王小虎测试"/>
      </el-header>


      <!-- 此处为主体部分 -->
	    <el-main>
        
        <!--当前页面的子路由将在<router-view/>中展示-->
        
        <!--keep-alive使得每个页面不会重新刷新展现，而是保存-->
        <keep-alive>
          <div>    
            <el-table
              ref="multipleTable"
              :data="tableData"
              stripe
              tooltip-effect="dark"
              style="width: 100%">
              <el-table-column
                prop="id"
                label="用户编号"
                width="95">
              </el-table-column>
              <el-table-column
                prop="name"
                label="名称"
                width="190">
              </el-table-column>
              <el-table-column
                prop="userName"
                label="用户名"
                width="190">
              </el-table-column>
              <el-table-column fixed="right" label="操作">                         
                <template slot-scope="scope">
                    <el-button type="danger" size="small"  icon="el-icon-delete" @click="handleDelete(scope.row)">删除</el-button>
                </template>
              </el-table-column> 

            </el-table>

            <!-- 分页栏-->
            <el-pagination class="mpage"
              background
              layout="prev, pager, next"
              :current-page="currentPage"
              :page-size="pageSize"
              :total="total"
              @current-change=page>
            </el-pagination>

          </div>
        </keep-alive>
         
      </el-main>


	  </el-container>
	</el-container>
  </div>
</template>
<script>
// @ is an alias to /src
import Aside from '@/components/Aside.vue'
import MangerHeader from '../../components/MangerHeader.vue';


export default {
  name: "WriteInfo",
  //此处添加组件
  components:{
    Aside,
    MangerHeader
  },
  data() {
    return {
      tableData:[],
      currentPage:1,
      pageSize:5,
      total:0,
    }
  },

  methods: {
    page(newPage) {
      const _this = this
      _this.$axios.get("/WriterInfo/page?pageNum=" + newPage, {
            headers: {
              "Authorization": 'Bearer '+localStorage.getItem("token")
            }
          }).then(res => {
        //console.log(res)
        _this.tableData = res.data.data
        _this.currentPage=newPage;
        _this.total = res.data.total
      })
    },
    handleDelete(row){
      this.$confirm('确认删除该记录吗?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        // 调用后端删除接口
        this.$axios.delete(`/WriterInfo/Delete/${row.id}`, {
              headers: {
                "Authorization": 'Bearer '+localStorage.getItem("token")
              }
            }).then(res => {
          if(res.data.code == 200){
            this.$message.success('删除用户数据成功！');
            this.page(1);
          }else{
            this.$message.error('删除用户数据失败，原因：'+res.data.msg);
          }
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });          
      });
    },
  },
  created(){
    this.page(1);
  }
}

</script>

<style>

/*下面是隐藏滚动条*/ 
.el-aside::-webkit-scrollbar {
  display: none;
}
.el-main::-webkit-scrollbar{
  display: none;
}

</style>