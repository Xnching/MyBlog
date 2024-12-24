<template>
  <div>
    <Header></Header>

    <div class="mblog">
      <h2> {{ blog.title }}</h2>
      <div style="display: flex;">
        <p>作者：{{blog.writerName}}</p>
        <p style="margin-left: 50%;">创建时间：{{blog.time}}</p>
      </div>
      <div style="display: flex;">
        <p>浏览数：{{blog.browseCount}}</p>
        <p style="margin-left: 50px;">like：{{blog.likeCount}}</p>
      </div>
      
      <el-link icon="el-icon-edit" v-if="ownBlog">
        <router-link :to="{name: 'BlogEdit', params: {blogId: blog.id}}" >
        编辑
        </router-link>
      </el-link>
      <div class="circle flex-h" style="margin-left: 80%;">
        like
        <img src="@/assets/zan.png" alt="" @click="handleClick"/>
      </div>
      <el-divider></el-divider>
      
      
      <div style="width: 100%;">
        <div class="markdown-body" v-html="blog.content"></div>
      </div>

    </div>

  </div>
</template>

<script>
  import Header from "../components/Header";


  export default {
    name: "BlogDetail.vue",
    components: {Header},
    data() {
      return {
        isliked:"0",
        blog: {
          id: "",
          title: "",
          content: "",
          browseCount:"",
          likeCount:"",
          typeName:"",
          writerName:"",
        },
        ownBlog: false
      }
    },
    created() {
      const blogId = this.$route.params.blogId
      console.log(blogId)
      const _this = this
      this.$axios.get('/BlogNews/blog/' + blogId, {
            headers: {
              "Authorization": 'Bearer '+localStorage.getItem("token")
            }
          }).then(res => {
        const blog = res.data.data
        _this.blog = res.data.data
        _this.blog.id = blog.id
        _this.blog.title = blog.title
        _this.blog.content = blog.content;
        console.log("看下原的");
        console.log(blog.content);
        
        _this.ownBlog = (blog.writerId === _this.$store.getters.getUser.id)

      })
    },
    methods:{
      handleClick(){
        if(this.isliked=='0'){
          const blogId = this.$route.params.blogId
          this.$axios.get('/BlogNews/like/' + blogId, {
                headers: {
                  "Authorization": 'Bearer '+localStorage.getItem("token")
                }
              }).then(res => {
          })
          this.$message.success('已点赞成功！');
          this.isliked=1;
        }else{
          this.$message.error('已点赞过了！');
        }
        
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
