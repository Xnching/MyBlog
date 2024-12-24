<template>
  <div>
    <Header></Header>

    <div class="m-content">

      <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">

        选择博客文章类型：
        <el-select v-model="ruleForm.typeid" filterable placeholder="博客文章类型" style="width: 150px;margin-right: 470px;margin-bottom: 30px;">
          <el-option
              v-for="item in types"
              :key="item.id"
              :label="item.name"
              :value="item.id">
          </el-option>
        </el-select>

        <el-form-item label="标题" prop="title">
          <el-input v-model="ruleForm.title"></el-input>
        </el-form-item>

        <el-form-item label="内容" prop="content">
          <wang-editor ref="myEditor" v-model="ruleForm.createdontent"></wang-editor>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="submitForm('ruleForm')">提交</el-button>
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
    name: "BlogAdd.vue",
    components: {Header,WangEditor},
    data() {
      return {
        types:[],
        ruleForm: {
          title: '',
          content: '',
          typeid:'1',

        },
        rules: {
          title: [
            { required: true, message: '请输入标题', trigger: 'blur' },
            { min: 3, max: 25, message: '长度在 3 到 25 个字符', trigger: 'blur' }
          ],
          content: [
            { trequired: true, message: '请输入内容', trigger: 'blur' }
          ],
          typeid: [
            { required: true, message: '请选择博客文章类型', trigger: 'change' },
            { validator: (rule, value, callback) => {
                if (value === '0') {
                  callback(new Error('不能选择全部'));
                } else {
                  callback();
                }
              }, trigger: 'change'
            }
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
            _this.ruleForm.content = htmlContent;
            this.$axios.post('/BlogNews/add', this.ruleForm, {
              headers: {
                "Authorization": "Bearer "+localStorage.getItem("token")
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
      },
      getTypes(){
        const _this = this
        _this.$axios.get("/Type/types").then(res => {
          _this.types = res.data.data
        })
      },
    },
    created(){
      this.getTypes();
    }
  }
</script>

<style scoped>
  .m-content {
    text-align: center;
  }
</style>