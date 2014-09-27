using HRMS.BusinessEntities;
using Repostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class ContactController : BaseController<Contact> 
    {
        public ContactController()
            : base("Contact")
        {

        }

        public override void PopulateDomainValueDictionary()
        {
            BaseRepository<PinCode> Repo = new BaseRepository<PinCode>(uow);

            IEnumerable<SelectListItem> selectList =
                from pinCode in Repo.GetAll()
                select new SelectListItem
                {
                    Selected = (pinCode.Id == Container.Instance.PinCodeId),
                    Text = pinCode.Code,
                    Value = pinCode.Id.ToString()
                };

            Add("PinCodes", selectList);
        }
    }
}