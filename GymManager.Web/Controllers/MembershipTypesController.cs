using GymManager.ApplicationServices.Members;
using GymManager.ApplicationServices.MembershipTypes;
using GymManager.Core;
using GymManager.Core.Members;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Web.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly IMembershipTypesAppService _membershipsAppService;

        public MembershipTypesController(IMembershipTypesAppService membershipsAppService)
        {
            _membershipsAppService = membershipsAppService;
        }
        public  async Task <IActionResult> Index()
        {
            List<MembershipType> memberships = await _membershipsAppService.GetMembershipTypesAsync();
            return View(memberships);
        }

        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(MembershipType membershipType)
        {
            await _membershipsAppService.AddMembershipTypeAsync(membershipType);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int membershipId)
        {
            await _membershipsAppService.DeleteMembershipTypeAsync(membershipId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int membershipId)
        {
            MembershipType membership = await _membershipsAppService.GetMembershipTypeAsync(membershipId);
           
            return View(membership);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MembershipType membership)
        {
           
            await _membershipsAppService.EditMembershipTypeAsync(membership);
            return RedirectToAction("Index");
        }
    }
}
