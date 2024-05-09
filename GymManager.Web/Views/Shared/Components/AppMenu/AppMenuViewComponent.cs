using GymManager.ApplicationServices.Navigation;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Web.Views.Shared.Components.AppMenu
{
    public class AppMenuViewComponent : ViewComponent
    {
        private readonly IMenuAppService _menuAppService;
        public AppMenuViewComponent(IMenuAppService menuAppService) 
        { 
            _menuAppService = menuAppService;
            
        }

        public async Task<IViewComponentResult> InvokeAsync(string currentPageName = null)
        {
            var model = new MenuViewModel
            {
                CurrentPageName = currentPageName,
                Menu = _menuAppService.GetMenu()
            };
            return View("Default",model);
        }
    }
}
