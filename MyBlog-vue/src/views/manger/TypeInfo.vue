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
        <div style="padding:10px; margin:10px; margin-bottom: -5px;">
          <el-button type="primary" @click="handleAdd" style="margin-left: 80%;">新增<i class="el-icon-circle-plus"></i></el-button>
        </div>  
        <div>    
          <el-table
            ref="multipleTable"
            :data="tableData"
            stripe
            tooltip-effect="dark"
            style="width: 100%">
            <el-table-column
              prop="id"
              label="类型编号"
              width="95">
            </el-table-column>
            <el-table-column
              prop="name"
              label="类型名称"
              width="200">
            </el-table-column>
            <el-table-column fixed="right" label="操作">                         
              <template slot-scope="scope">
                  <el-button type="danger" size="small"  icon="el-icon-delete" @click="handleDelete(scope.row)">删除</el-button>
              </template>
            </el-table-column> 

          </el-table>

        </div>
         
      </el-main>


	  </el-container>
	</el-container>

  <!-- 新增系统用户的弹窗 -->
  <el-dialog
    title="请输入新增类型的信息"
    :visible.sync="dialogVisible"
    width="40%"
    destroy-on-close
    :close-on-click-modal="false" >
    <!-- 用于提示新增用户时的数据不能为空-->
    <el-form :model="ruleForm"  ref="ruleForm" label-width="100px" class="demo-ruleForm">
      <el-form-item label="类型" prop="Name"> <el-input v-model="ruleForm.name"></el-input> </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit()">确定</el-button>
      </span>
    </template>
  </el-dialog>


  </div>
  
</template>
<script>
// @ is an alias to /src
import Aside from '@/components/Aside.vue'
import MangerHeader from '../../components/MangerHeader.vue';


export default {
  name: "TypeInfo",
  //此处添加组件
  components:{
    Aside,
    MangerHeader
  },
  data() {
    return {
      tableData:[],
      dialogVisible:false,
      ruleForm:{
        name:'',
      }
    }
  },

  methods: {
    page() {
      const _this = this
      _this.$axios.get("/Type/types", {
            headers: {
              "Authorization": 'Bearer '+localStorage.getItem("token")
            }
          }).then(res => {
        const types = res.data.data; // 假设后端返回的数据在 response.data.data 中
        // 过滤掉 Id 为 0 的项
        _this.tableData = types.filter(type => type.Id !== 0);
      })
    },
    handleDelete(row){
      this.$confirm('确认删除该记录吗?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        // 调用后端删除接口
        console.log("kanxia");
        console.log(row.id);
        
        
        this.$axios.delete(`/Type/Delete/${row.id}`).then(res => {
          if(res.data.code == 200){
            this.$message.success('删除类型数据成功！');
            this.page();
          }else{
            this.$message.error('删除类型数据失败，原因：'+res.data.msg);
          }
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });          
      });
    },
    handleAdd(){
      this.dialogVisible=true;
    },
    handleSubmit(){
      this.$axios.post("/Type/add?name="+this.ruleForm.name).then(res=>{
        if(res.data.code == 200){
          this.$message.success('新增类型数据成功！');
          this.page();
        } else {
          this.$message.error('新增类型数据失败，原因：' + res.data.msg);
        }
      })
      // 重置表单
      this.$refs.ruleForm.resetFields(); 
      this.dialogVisible = false;  //关闭弹窗
    }
  },
  created(){
    this.page();
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