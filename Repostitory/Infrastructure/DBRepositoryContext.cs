using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Repository
{
    public class DBRepositoryContext : IRepositoryContext
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public DBRepositoryContext(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }


        public IBaseRepository<T> GetRepository<T>()
            where T : class
        {
            return new BaseRepository<T>(UnitOfWork);
        }
    }
}
