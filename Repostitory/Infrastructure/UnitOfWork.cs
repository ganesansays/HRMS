using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostitory
{
    public class UnitOfWork : IUnitOfWork
    {
        //private TransactionScope _transaction;
        private readonly DbContext _db;

        public UnitOfWork()
        {
            _db = new HRMSStore();
        }

        public void Dispose()
        {

        }

        public void StartTransaction()
        {
            //_transaction = new TransactionScope();
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _db.SaveChanges();
            //_transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }
    }
}
