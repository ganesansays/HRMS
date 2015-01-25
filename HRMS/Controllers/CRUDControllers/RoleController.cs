using Hrms.BusinessEntities;
using Hrms.Controllers;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.WebUI.Controllers.CRUDControllers
{
    public class RoleController : BaseCRUDController<Role> 
    {
        public RoleController(IRepositoryContext repoContext)
            : base("Role", repoContext)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            //No domain value to populate
        }
    }
}