<template>
  <div>
    <Header></Header>

    <div class="m-content">

      <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
        <el-form-item label="标题" prop="title">
          <el-input v-model="ruleForm.Title"></el-input>
        </el-form-item>

        <el-form-item label="内容" prop="content">
          <wang-editor ref="myEditor" v-model="ruleForm.Content"></wang-editor>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="submitForm('ruleForm')">修改</el-button>
          <el-button @click="resetForm('ruleForm')">重置</el-button>
        </el-form-item>
      </el-form>

    </div>

  </div>
</template>

<script>
  import Header from "../components/Header";
  import WangEditor from "../components/wangEditor.vue";
  export default {
    name: "BlogEdit.vue",
    components: {Header,WangEditor},
    data() {
      return {
        ruleForm: {
          Id: '',
          Title: '',
          Content: '',
        },
        rules: {
          Title: [
            { required: true, message: '请输入标题', trigger: 'blur' },
            { min: 3, max: 25, message: '长度在 3 到 25 个字符', trigger: 'blur' }
          ],
          Content: [
            { required: true, message: '请输入内容', trigger: 'blur' }
          ]
        }
      };
    },
    methods: {
      submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            const _this = this
            let htmlContent = this.$refs.myEditor.valueHtml;
            _this.ruleForm.Content = htmlContent;
            this.$axios.put('/BlogNews/edit', this.ruleForm, {
              headers: {
                "Authorization": 'Bearer '+localStorage.getItem("token")
              }
            }).then(res => {
              console.log(res)
              _this.$alert('操作成功', '提示', {
                confirmButtonText: '确定',
                callback: action => {
                  _this.$router.push("/blogs")
                }
              });

            })

          } else {
            console.log('error submit!!');
            return false;
          }
        });
      },
      resetForm(formName) {
        this.$refs[formName].resetFields();
      }
    },
    created() {
      const blogId = this.$route.params.blogId
      console.log(blogId)
      const _this = this
      
      if(blogId) {
        this.$axios.get('/BlogNews/blog/' + blogId, {
              headers: {
                "Authorization": 'Bearer '+localStorage.getItem("token")
              }
            }).then(res => {
          const blog = res.data.data
          _this.ruleForm.Id = blog.id
          _this.ruleForm.Title = blog.title
          _this.ruleForm.Content = blog.content
        })
      }

    }
  }
</script>

<style scoped>
  .m-content {
    text-align: center;
  }
</style>