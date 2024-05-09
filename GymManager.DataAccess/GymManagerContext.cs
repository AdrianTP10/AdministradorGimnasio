using GymManager.Core;
using GymManager.Core.Members;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.DataAccess
{
    public class GymManagerContext : IdentityDbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MembershipType> MembershipTypes { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<Attendace> Attendances { get; set; }
        public virtual DbSet<Employee> Staff { get; set; }
        public GymManagerContext(DbContextOptions<GymManagerContext> options): base(options) 
        {
            

        }
        
    }
}
