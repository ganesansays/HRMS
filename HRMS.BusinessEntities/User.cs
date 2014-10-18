using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public class User : EntityBase
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name Cannot be blank")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide a password")]
        public string Password { get; set; }
        
        [ForeignKey("Group")]
        [Required(ErrorMessage = "Select a group for this user")]
        public int? GroupId { get; set; }
        
        public virtual Group Group { get; set; }
    }
}
