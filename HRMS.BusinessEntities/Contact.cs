using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessEntities
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "Contact Name cannot be blank")]
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [ForeignKey("PinCode")]
        [Required(ErrorMessage = "Select a pin code for this contact")]
        public int? PinCodeId { get; set; }

        public virtual PinCode PinCode { get; set; }
    }
}
