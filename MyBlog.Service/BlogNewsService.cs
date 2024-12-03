using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class BlogNewsService : BaseService<BlogNews>,IBlogNewsService
    {
        private readonly IBlogNewsRepository _blogNewsRepository;
        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository) {
            base._repository = iBlogNewsRepository;
            _blogNewsRepository = iBlogNewsRepository;
        }

    }
}
