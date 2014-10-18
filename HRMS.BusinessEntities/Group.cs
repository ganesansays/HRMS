using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public class Group : EntityBase
    {
        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name cannot be blank")]
        public string Name { get; set; }
    }
}
