using HRMS.BusinessEntities;
using Repostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class UserController : BaseController<User> 
    {
        public UserController()
            : base("User")
        {

        }

        public override void PopulateDomainValueDictionary()
        {
            BaseRepository<Group> Repo = new BaseRepository<Group>(uow);

            IEnumerable<SelectListItem> selectList =
                from c in Repo.GetAll()
                select new SelectListItem
                {
                    Selected = (c.Id == Container.Instance.GroupId ),
                    Text = c.Name,
                    Value = c.Id.ToString()
                };

            Add("Groups", selectList);
        }
    }
}