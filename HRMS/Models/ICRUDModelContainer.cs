using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hrms.Models
{
    public enum Mode
    {
        List,
        Create,
        Edit,
        Detail
    }

    public interface ICRUDModelContainer
    {
        Mode Mode { get; }

        String Name { get; }

        object ModelInstance {get;}

        object ModelListOfItems { get; }

        Dictionary<string, IEnumerable<SelectListItem>> DomainValueDictionary { get; }
    }
}
