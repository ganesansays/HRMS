using Hrms.BusinessEntities;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Controllers
{
    public class ContactController : BaseCRUDController<Contact> 
    {
        public ContactController(IBaseRepository<Contact> repo)
            : base("Contact", repo)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            BaseRepository<PinCode> Repo = new BaseRepository<PinCode>(null);

            IEnumerable<SelectListItem> selectList =
                from pinCode in Repo.List
                select new SelectListItem
                {
                    Selected = (pinCode.Id == Container.Instance.PinCodeId),
                    Text = pinCode.Code,
                    Value = pinCode.Id.ToString()
                };

            Container.AddSelectList("PinCodes", selectList);
        }
    }
}