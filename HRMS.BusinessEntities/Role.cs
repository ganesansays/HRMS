using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public class Role : EntityBase<Role>
    {
        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Role Name Required.")]
        [StringLength(255, ErrorMessage = "Role Name must be less than 256 characters.")]
        public string RoleName { get; set; }

        public void PopulateDummyValues()
        {
            RoleName = "Role Name";
        }

        public override bool CompareByValue(Role otherEntity)
        {
            if (otherEntity == null) return false;
            return (
                    this.RoleName == otherEntity.RoleName
                );
        }

        public override void Merge(Role otherEntity)
        {
            if (otherEntity == null) return;
            this.RoleName = otherEntity.RoleName;
        }
    }
}
