using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Tests.Utilities
{
    class DbValidationErrorComparer : FuncEqualityComparer<DbValidationError>
    {
        public DbValidationErrorComparer()
            : base(delegate(DbValidationError a, DbValidationError b)
                            {
                                return a.ErrorMessage == b.ErrorMessage && a.PropertyName == b.PropertyName;
                            })
        {

        }
    }
}
