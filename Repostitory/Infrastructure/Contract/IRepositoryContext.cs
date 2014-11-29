using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Repository
{
    public interface IRepositoryContext
    {
        IBaseRepository<T> GetRepository<T>() where T : class;
    }
}
