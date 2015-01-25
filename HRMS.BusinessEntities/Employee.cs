using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public class Employee : EntityBase<Employee>
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name cannot be blank.")]
        [StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        public String Name { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birgth cannot be blank.")]
        public DateTime Dob { get; set; }

        public override bool CompareByValue(Employee otherEntity)
        {
            return true;
        }

        public override void Merge(Employee otherEntity)
        {
            
        }
    }
}
