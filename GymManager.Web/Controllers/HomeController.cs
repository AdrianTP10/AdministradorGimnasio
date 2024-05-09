using GymManager.ApplicationServices.Members;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMembersAppService _membersAppService;
        public HomeController(IMembersAppService membersAppService)
        {
            _membersAppService = membersAppService;
        }


        public async Task<IActionResult> Index()
        {
            var members = await _membersAppService.GetMembersAsync();
            return View(members);
        }
    }
}
