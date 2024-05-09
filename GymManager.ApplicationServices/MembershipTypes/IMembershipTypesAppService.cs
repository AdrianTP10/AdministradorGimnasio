using GymManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.ApplicationServices.MembershipTypes
{
    public interface IMembershipTypesAppService
    {
        Task<int> AddMembershipTypeAsync(MembershipType membership);

        Task<MembershipType> GetMembershipTypeAsync(int membershipId);

        Task<List<MembershipType>> GetMembershipTypesAsync();

        Task DeleteMembershipTypeAsync(int membershipId);

        Task EditMembershipTypeAsync(MembershipType membership);
    }
}
