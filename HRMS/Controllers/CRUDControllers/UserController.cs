using Hrms.BusinessEntities;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Controllers
{
    public class UserController : BaseCRUDController<User> 
    {
        public UserController(IBaseRepository<User> repo)
            : base("User", repo)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            BaseRepository<Group> Repo = new BaseRepository<Group>(null);

            IEnumerable<SelectListItem> selectList =
                from c in Repo.List
                select new SelectListItem
                {
                    Selected = (c.Id == Container.Instance.GroupId ),
                    Text = c.Name,
                    Value = c.Id.ToString()
                };

            Container.AddSelectList("Groups", selectList);
        }
    }
}