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
    public class WriterInfoService: BaseService<WriterInfo>,IWriterInfoService
    {
        private readonly IWriterInfoRepository _writerInfoRepository;
        public WriterInfoService(IWriterInfoRepository writerInfoRepository) {
            base._repository = writerInfoRepository;
            _writerInfoRepository = writerInfoRepository;   
        }
    }
}
