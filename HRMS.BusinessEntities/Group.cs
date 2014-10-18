using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public partial class Group : EntityBase
    {
        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name cannot be blank")]
        [MaxLength(20)]
        public string Name { get; set; }

        public static Group Sample()
        {
            return new Group() { Name = "Group Name" };
        }
    }
}
