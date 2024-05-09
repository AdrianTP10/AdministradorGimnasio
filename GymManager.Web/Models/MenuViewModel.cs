using GymManager.Core.Navigation;

namespace GymManager.Web.Models
{
    public class MenuViewModel
    {
        public string CurrentPageName { get; set; }
        public List<UserMenuItem> Menu { get; set;}
    }
}
