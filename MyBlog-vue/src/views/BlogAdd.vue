<template>
  <div>
    <Header></Header>

    <div class="m-content">

      <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">

        <el-select v-model="ruleForm.TypeId" filterable placeholder="博客文章类型" @change="page(1)" style="width: 150px;margin-right: 220px;">
          <el-option
              v-for="item in types"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id">
          </el-option>
        </el-select>

        <el-form-item label="标题" prop="title">
          <el-input v-model="ruleForm.Title"></el-input>
        </el-form-item>

        <el-form-item label="内容" prop="content">
          <mavon-editor v-model="ruleForm.Content"></mavon-editor>
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
  export default {
    name: "BlogEdit.vue",
    components: {Header},
    data() {
      return {
        ruleForm: {
          Id: '',
          Title: '',
          Content: '',
          TypeId:'',
        },
        rules: {
          Title: [
            { required: true, message: '请输入标题', trigger: 'blur' },
            { min: 3, max: 25, message: '长度在 3 到 25 个字符', trigger: 'blur' }
          ],
          Content: [
            { trequired: true, message: '请输入内容', trigger: 'blur' }
          ],
          TypeId: [
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
            this.$axios.post('/blog/add', this.ruleForm, {
              headers: {
                "Authorization": localStorage.getItem("token")
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
  }
</script>

<style scoped>
  .m-content {
    text-align: center;
  }
</style>