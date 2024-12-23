<template>
  <div class="mcontaner">
    <Header></Header>

    <div class="block">
      <el-timeline>

        <el-select v-model="typeId" filterable placeholder="博客文章类型" @change="page(1)" style="width: 150px;margin-right: 220px;">
          <el-option
              v-for="item in types"
              :key="item.Id"
              :label="item.Name"
              :value="item.Id">
          </el-option>
        </el-select>

        <el-timeline-item :timestamp="blog.time" placement="top" v-for="blog in blogs">
          <el-card>
            <h4>
              <router-link :to="{name: 'BlogDetail', params: {blogId: blog.Id}}">
                {{blog.Title}}
              </router-link>
            </h4>
            <p>作者：{{blog.WriterName}}</p>
            <p>文章类型：{{blog.TypeName}}</p>
            <p>浏览数：{{blog.BrowseCount}}</p>
            <p>like：{{blog.LikeCount}}</p>
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
        pageSize: 10,
        typeId:0,
        types:[]
      }
    },
    methods: {
      page(currentPage) {
        const _this = this
        _this.$axios.get("/page?pageNum=" + currentPage+"&typeId="+_this.typeId).then(res => {
          console.log(res)
          _this.blogs = res.Data.records
          _this.currentPage = res.Data.pageNum
          _this.total = res.Total
          _this.pageSize = res.Data.size
        })
      },
      getTypes(){
        const _this = this
        _this.$axios.get("/types").then(res => {
          _this.types = res.Data
        })
      },

    },
    created() {
      this.page(1);
      this.getTypes;
    }
  }
</script>

<style scoped>

  .mpage {
    margin: 0 auto;
    text-align: center;
  }

</style>