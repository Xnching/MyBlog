using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.IRepository;

namespace MyBlog.IService
{
    public interface IBaseService<TEntity> : IBaseRepository<TEntity> where TEntity : class ,new()
    {

    }
}
