using GymManager.Core;
using GymManager.Core.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.DataAccess.Repositories
{
    public class MembershipTypesRepository : Repository<int, MembershipType>
    {
        public MembershipTypesRepository(GymManagerContext context) : base(context)
        {
        }
    }
}
