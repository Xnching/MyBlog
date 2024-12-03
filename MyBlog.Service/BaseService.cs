using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;

namespace MyBlog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //从子类的构造函数传入
        protected IBaseRepository<TEntity> _repository;
        public async Task<bool> DeleteById(int id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Insert(TEntity entity)
        {
            return await _repository.Insert(entity);
        }

        public async Task<List<TEntity>> ListAll()
        {
            return await _repository.ListAll();
        }

        public async Task<List<TEntity>> Page(int page, int pageSize, RefAsync<int> total)
        {
            return await _repository.Page(page, pageSize, total);
        }

        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> func)
        {
            return await _repository.QueryList(func);
        }

        public async Task<TEntity> QueryOne(Expression<Func<TEntity, bool>> func)
        {
            return await _repository.QueryOne(func);
        }

        public async Task<List<TEntity>> QueryPage(int page, int pageSize, RefAsync<int> total, Expression<Func<TEntity, bool>> func)
        {
            return await _repository.QueryPage(page, pageSize,total, func);
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }
    }
}
