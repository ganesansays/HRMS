using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //private TransactionScope _transaction;
        private readonly DbContext _db;

        public UnitOfWork(DbContext context)
        {
            _db = context;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _db.Dispose();
            }

            // free native resources if there are any.

        }
    }
}
