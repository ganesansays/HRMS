using Hrms.BusinessEntities;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Controllers
{
    public class EmployeeController : BaseCRUDController<Employee>
    {
        public EmployeeController(IRepositoryContext repoContext)
            : base("Employee", repoContext)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            
        }
    }
}