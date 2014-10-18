using Hrms.BusinessEntities;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Controllers
{
    public class GroupController : BaseCRUDController<Group> 
    {
        public GroupController(IRepositoryContext repoContext)
            : base("Group", repoContext)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            //No domain value to populate
        }
    }
}