using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace MyBlog.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class,new()
    {
        Task<bool> Insert(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> DeleteById(int id);
        Task<TEntity> GetById(int id);
        Task<TEntity> QueryOne(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> ListAll();
        /// <summary>
        /// 根据查询条件获取符合条件的全部
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> func);
        /// <summary>
        /// 全部的分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> Page(int page, int pageSize,RefAsync<int> total);
        /// <summary>
        /// 带查询条件的分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryPage(int page, int pageSize, RefAsync<int> total,Expression<Func<TEntity, bool>> func);
    }
}
