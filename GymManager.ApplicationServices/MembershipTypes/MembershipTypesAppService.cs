using GymManager.Core;
using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.ApplicationServices.MembershipTypes
{
    public class MembershipTypesAppService : IMembershipTypesAppService
    {
        private readonly IRepository<int, MembershipType> _repository;
        public MembershipTypesAppService(IRepository<int, MembershipType> repository)
        {
            _repository = repository;
        }

        public async Task<int> AddMembershipTypeAsync(MembershipType membership)
        {
            await _repository.AddAsync(membership);
            return membership.Id;

        }

        public async Task<MembershipType> GetMembershipTypeAsync(int membershipId)
        {
            return await _repository.GetAsync(membershipId);
        }



        public async Task<List<MembershipType>> GetMembershipTypesAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task DeleteMembershipTypeAsync(int membershipId)
        {
            await _repository.DeleteAsync(membershipId);
        }

        public async Task EditMembershipTypeAsync(MembershipType membership)
        {
            await _repository.UpdateAsync(membership);

        }

    }
}
