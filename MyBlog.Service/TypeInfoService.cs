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
    public class TypeInfoService: BaseService<TypeInfo>,ITypeInfoService
    {
        private readonly ITypeInfoRepository _typeInfoRepository;
        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository) {
            base._repository = iTypeInfoRepository;
            _typeInfoRepository = iTypeInfoRepository;
        }
    }
}
