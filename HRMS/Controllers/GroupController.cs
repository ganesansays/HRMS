using HRMS.BusinessEntities;
using Repostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class GroupController : BaseController<Group> 
    {
        public GroupController()
            : base("Group")
        {

        }
    }
}