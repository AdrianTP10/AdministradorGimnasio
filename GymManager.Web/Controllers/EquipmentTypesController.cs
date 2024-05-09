using GymManager.ApplicationServices.EquipmentTypes;
using GymManager.ApplicationServices.MembershipTypes;
using GymManager.Core;
using GymManager.DataAccess.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Web.Controllers
{
    public class EquipmentTypesController : Controller
    {
        private readonly IEquipmentTypesAppService _equipmentTypesAppService;

        public EquipmentTypesController(IEquipmentTypesAppService equipmentAppService)
        {
            _equipmentTypesAppService = equipmentAppService;
        }
        public async Task<IActionResult> Index()
        {
            List<EquipmentType> memberships = await _equipmentTypesAppService.GetEquipmentTypesAsync();
            return View(memberships);
        }

        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(EquipmentType equipmentType)
        {
            await _equipmentTypesAppService.AddEquipmentTypeAsync(equipmentType);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int equipmentId)
        {
            await _equipmentTypesAppService.DeleteEquipmentTypeAsync(equipmentId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int equipmentId)
        {
            EquipmentType equipment = await _equipmentTypesAppService.GetEquipmentTypeAsync(equipmentId);
            return View(equipment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EquipmentType equipment)
        {

            await _equipmentTypesAppService.EditEquipmentTypeAsync(equipment);
            return RedirectToAction("Index");
        }
    }
}
