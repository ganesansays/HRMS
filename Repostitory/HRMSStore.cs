using Hrms.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Repository
{
    public partial class HrmsStore : DbContext
    {
        public HrmsStore()
            : base("name=HRMSStore")
        {
        }

        public HrmsStore(bool dropDb)
            : base("name=HRMSStore")
        {
            if (dropDb)
            {
                Database.SetInitializer<HrmsStore>(new DropCreateDatabaseAlways<HrmsStore>());
            }
        }
    
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }*/
    
        //Add your new class below
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
