using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessEntities
{
    public class Contact : EntityBase
    {
        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "Contact Name cannot be blank")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data of birth")]
        [Required(ErrorMessage = "Enter a valid data of birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Web Site")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [ForeignKey("PinCode")]
        [Display(Name = "Pin Code")]
        [Required(ErrorMessage = "Select a pin code for this contact")]
        public int? PinCodeId { get; set; }

        public virtual PinCode PinCode { get; set; }
    }
}
