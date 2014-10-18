using Hrms.BusinessEntities;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Controllers
{
    public class PinCodeController : BaseCRUDController<PinCode> 
    {
        public PinCodeController(IBaseRepository<PinCode> repo)
            : base("PinCode", repo)
        {

        }

        protected override void PopulateDomainValueDictionary()
        {
            //No domain value to populate
        }
    }
}