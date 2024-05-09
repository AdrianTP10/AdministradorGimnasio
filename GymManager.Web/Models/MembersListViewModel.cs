using GymManager.Core.Members;

namespace GymManager.Web.Models
{
    public class MembersListViewModel
    {
        public int NewMembersCount { get; set; }
        public List<Member> Members { get; set; }
    }
}
