using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {
        public async override Task<BlogNews> GetById(int id)
        {
            var result = await base.Context.Queryable<BlogNews>()
                .Where(c => c.Id == id)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();

            return result.FirstOrDefault();
        }
        public async override Task<List<BlogNews>> ListAll()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo,c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }

        public async override Task<List<BlogNews>> QueryList(Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }

        public override async Task<List<BlogNews>> Page(int page, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .ToPageListAsync(page, pageSize, total);
        }

        public override async Task<List<BlogNews>> QueryPage(int page, int pageSize, RefAsync<int> total, Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .ToPageListAsync(page, pageSize, total);
        }
    }
}
