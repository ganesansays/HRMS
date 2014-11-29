using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]
namespace Hrms.BusinessEntities
{
    public abstract class EntityBase<T>
        where T : EntityBase<T>, new()
    {
        public int Id { get; set; }

        public abstract bool CompareByValue(T otherEntity);

        public abstract void Merge(T otherEntity);

        public bool CompareByIdentity(T otherEntity)
        {
            if (otherEntity == null) return false;
            return this.Id == otherEntity.Id;
        }
    }
}
