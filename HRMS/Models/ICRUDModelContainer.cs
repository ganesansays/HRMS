using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Models
{
    public enum Mode
    {
        LIST,
        CREATE,
        EDIT,
        DETAIL
    }

    public interface ICRUDModelContainer
    {
        Mode Mode { get; }

        String Name { get; }
    }
}
