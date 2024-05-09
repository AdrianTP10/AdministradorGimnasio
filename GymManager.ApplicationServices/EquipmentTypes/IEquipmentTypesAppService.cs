using GymManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.ApplicationServices.EquipmentTypes
{
    public interface IEquipmentTypesAppService
    {
        Task<int> AddEquipmentTypeAsync(EquipmentType equipment);

        Task<EquipmentType> GetEquipmentTypeAsync(int equipmentId);

        Task<List<EquipmentType>> GetEquipmentTypesAsync();

        Task DeleteEquipmentTypeAsync(int equipmentId);

        Task EditEquipmentTypeAsync(EquipmentType equipment);
    }
}
