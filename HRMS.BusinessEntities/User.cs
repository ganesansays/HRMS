using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.BusinessEntities
{
    public class User : EntityBase<User>
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name Cannot be blank.")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be blank.")]
        [StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [ForeignKey("Group")]
        [Required(ErrorMessage = "Select a group for this user.")]
        public int? GroupId { get; set; }
        
        public virtual Group Group { get; set; }

        public void PopulateDummyValues()
        {
            Name = "User Name";
            Password = "Password";
        }

        public override bool CompareByValue(User otherEntity)
        {
            if (otherEntity == null) return false;
            return (
                    this.Name == otherEntity.Name && 
                    this.Password == otherEntity.Password &&
                    this.GroupId == otherEntity.GroupId
                );
        }

        public override void Merge(User otherEntity)
        {
            if (otherEntity == null) return;
            this.Name = otherEntity.Name;
            this.Password = otherEntity.Password;
            this.GroupId = otherEntity.GroupId;
        }
    }
}
