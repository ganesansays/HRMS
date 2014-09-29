using HRMS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class PinCodeController : BaseCRUDController<PinCode> 
    {
        public PinCodeController()
            : base("PinCode")
        {

        }
    }
}