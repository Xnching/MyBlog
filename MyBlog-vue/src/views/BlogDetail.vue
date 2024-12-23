<template>
  <div>
    <Header></Header>

    <div class="mblog">
      <h2> {{ blog.Title }}</h2>
      <div style="display: flex;">
        <p>作者：{{blog.WriterName}}</p>
        <p>创建时间：{{blog.time}}</p>
      </div>
      
      <el-link icon="el-icon-edit" v-if="ownBlog">
        <router-link :to="{name: 'BlogEdit', params: {blogId: blog.Id}}" >
        编辑
        </router-link>
      </el-link>
      <div class="circle flex-h" @click="handleClick" :class="isUp?'check':''">
        <img src="@/assets/zan.png" alt="" />
      </div>
      <el-divider></el-divider>
      <div style="display: flex;">
        <p>浏览数：{{blog.BrowseCount}}</p>
        <p>like：{{blog.LikeCount}}</p>
      </div>
      <div class="markdown-body" v-html="blog.Content"></div>

    </div>

  </div>
</template>

<script>
  import 'github-markdown-css'
  import Header from "../components/Header";

  export default {
    name: "BlogDetail.vue",
    components: {Header},
    data() {
      return {
        blog: {
          Id: "",
          Title: "",
          Content: "",
          BrowseCount:"",
          LikeCount:"",
          TypeName:"",
          WriterName:"",
        },
        ownBlog: false
      }
    },
    created() {
      const blogId = this.$route.params.blogId
      console.log(blogId)
      const _this = this
      this.$axios.get('/blog/' + blogId).then(res => {
        const blog = res.Data
        _this.blog.id = blog.Id
        _this.blog.title = blog.Title

        var MardownIt = require("markdown-it")
        var md = new MardownIt()

        var result = md.render(blog.Content)
        _this.blog.Content = result
        _this.ownBlog = (blog.WriterId === _this.$store.getters.getUser.Id)

      })
    },
    methods:{
      handleClick(){
        const blogId = this.$route.params.blogId
        this.$axios.get('/like/' + blogId).then(res => {
        })
        this.$message.success('已点赞成功！');
      }
    }
  }
</script>

<style scoped>
  .mblog {
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
    width: 100%;
    min-height: 700px;
    padding: 20px 15px;
  }

</style>