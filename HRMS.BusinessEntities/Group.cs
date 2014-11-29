using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public partial class Group : EntityBase<Group>
    {
        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group name cannot be blank.")]
        [StringLength(20, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength=1)]
        public string Name { get; set; }

        public void PopulateDummyValues()
        {
            this.Name = "Group Name";
        }

        public override bool CompareByValue(Group otherEntity)
        {
            if (otherEntity == null) return false;
            return this.Name == otherEntity.Name;
        }

        public override void Merge(Group otherEntity)
        {
            if (otherEntity == null) return;
            this.Name = otherEntity.Name;
        }
    }
}
