using GymManager.Core;
using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.ApplicationServices.EquipmentTypes
{
    public class EquipmentTypesAppService : IEquipmentTypesAppService
    {
        private readonly IRepository<int, EquipmentType> _repository;
        public EquipmentTypesAppService(IRepository<int, EquipmentType> repository)
        {
            _repository = repository;
        }
        public async Task<int> AddEquipmentTypeAsync(EquipmentType equipment)
        {
            await _repository.AddAsync(equipment);
            return equipment.Id;
        }

        public async Task DeleteEquipmentTypeAsync(int equipmentId)
        {
            await _repository.DeleteAsync(equipmentId);
        }

        public async Task EditEquipmentTypeAsync(EquipmentType equipment)
        {
            await _repository.UpdateAsync(equipment);
        }

        public async Task<EquipmentType> GetEquipmentTypeAsync(int equipmentId)
        {
            return await _repository.GetAsync(equipmentId);
        }

        public async Task<List<EquipmentType>> GetEquipmentTypesAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }
    }
}
