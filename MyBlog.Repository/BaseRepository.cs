using MyBlog.IRepository;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity> ,IBaseRepository<TEntity> where TEntity : class,new()
    {
        public BaseRepository(ISqlSugarClient context = null):base(context) {
            base.Context = DbScoped.Sugar;
            //base.Context.DbMaintenance.CreateDatabase();
            //base.Context.CodeFirst.InitTables(
            //    typeof(BlogNews),
            //    typeof(TypeInfo),
            //    typeof(WriterInfo)
            //    );
        }
        public async Task<bool> DeleteById(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<bool> Insert(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public virtual async Task<List<TEntity>> ListAll()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> Page(int page, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .ToPageListAsync(page, pageSize, total);
        }

        public virtual async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public async Task<TEntity> QueryOne(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryPage(int page, int pageSize, RefAsync<int> total, Expression<Func<TEntity, bool>> func)
        {
            return await base.Context.Queryable<TEntity>()
                .Where(func)
                .ToPageListAsync (page, pageSize, total);
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }
    }
}
