using HRMS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostitory
{
    partial class HRMSStore : DbContext
    {
        public HRMSStore()
            : base("name=HRMSStore")
        {
        }
    
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }*/
    
        //Add your new class below
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<PinCode> PinCodes { get; set; }
    }
}
