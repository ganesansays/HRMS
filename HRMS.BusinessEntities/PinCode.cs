using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessEntities
{
    public class PinCode
    {
        public int Id { get; set; }

        [Display(Name = "Pin Code")]
        [Required(ErrorMessage = "Pin Code Cannot be blank")]
        public String Code { get; set; }

        [Display(Name = "Area")]
        [Required(ErrorMessage = "Area Cannot be blank")]
        public string Area { get; set; }
    }
}
