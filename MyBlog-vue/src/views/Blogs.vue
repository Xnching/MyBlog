<template>
  <div class="mcontaner">
    <Header></Header>

    <div class="block">
      <el-timeline>

        <el-select v-model="typeId" filterable placeholder="博客文章类型" @change="page(1)" style="width: 150px;margin-right: 220px;">
          <el-option
              v-for="item in types"
              :key="item.id"
              :label="item.name"
              :value="item.id">
          </el-option>
        </el-select>

        <el-timeline-item :timestamp="formatDateTime(blog.time)" placement="top" v-for="blog in blogs">
          <el-card>
            <h4>
              <router-link :to="{name: 'BlogDetail', params: {blogId: blog.id}}">
                {{blog.title}}
              </router-link>
            </h4>
            <p>作者：{{blog.writerName}}</p>
            <p>文章类型：{{blog.typeName}}</p>
            <p>浏览数：{{blog.browseCount}}</p>
            <p>like：{{blog.likeCount}}</p>
          </el-card>
        </el-timeline-item>

      </el-timeline>

      <el-pagination class="mpage"
        background
        layout="prev, pager, next"
        :current-page="currentPage"
        :page-size="pageSize"
        :total="total"
        @current-change=page>
      </el-pagination>

    </div>

  </div>
</template>

<script>
  import Header from "../components/Header";

  export default {
    name: "Blogs.vue",
    components: {Header},
    data() {
      return {
        blogs: {},
        currentPage: 1,
        total: 0,
        pageSize: 5,
        typeId:0,
        types:[]
      }
    },
    methods: {
      page(newPage) {
        const _this = this

        _this.$axios.get("/BlogNews/page?pageNum=" + newPage+"&typeId="+_this.typeId, {
              headers: {
                "Authorization": 'Bearer '+localStorage.getItem("token")
              }
            }).then(res => {
          //console.log(res)
          _this.blogs = res.data.data
          _this.currentPage=newPage;
          _this.total = res.data.total
        })
      },
      getTypes(){
        const _this = this
        _this.$axios.get("/Type/types").then(res => {
          _this.types = res.data.data
        })
      },
      formatDateTime(isoDateString) {
        const date = new Date(isoDateString);
        const year = date.getFullYear();
        const month = date.getMonth() + 1;
        const day = date.getDate();
        const hours = date.getHours();
        const minutes = date.getMinutes();
        const seconds = date.getSeconds();

        return `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')} ${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
      },

    },
    created() {
      this.page(1);
      this.getTypes();
      
    }
  }
</script>

<style scoped>

  .mpage {
    margin: 0 auto;
    text-align: center;
  }

</style>