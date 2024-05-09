using GymManager.ApplicationServices.Members;
using GymManager.ApplicationServices.MembershipTypes;
using GymManager.Core;
using GymManager.Core.Members;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GymManager.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMembersAppService _membersAppService;
        private readonly IMembershipTypesAppService _membershipAppService;
        readonly ILogger<MembersController> _logger;
        public MembersController(IMembersAppService membersAppService, IMembershipTypesAppService membershipAppService, ILogger<MembersController> logger) 
        {
            _membershipAppService = membershipAppService;
            _membersAppService = membersAppService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        
        public IActionResult Renewal(MemberViewModel viewModel)
        {
            return View(viewModel);
        }

        public async Task <IActionResult> OnSearchMember(int memberId)
        {
            // Lógica para procesar el formulario
            Member member = await _membersAppService.GetMemberAsync(memberId);
            if (member!=null)
            {
                MemberViewModel viewModel = new MemberViewModel
                {
                    AllowNewsletter = member.AllowNewsletter,
                    BirthDay = member.BirthDay,
                    CityId = member.City.Id,
                    MembershipTypeId = member.MembershipTypeId,
                    Email = member.Email,
                    Id = member.Id,
                    LastName = member.LastName,
                    Name = member.Name,
                };
                return RedirectToAction("Renewal", viewModel);
            }
            else { return RedirectToAction("Renewal"); }   
        }


        public async Task<IActionResult> Index()
        {
            // _logger.LogInformation("Ejecutando accion Index");
            //Log.Information("Ejecutando accion Index");

            List<Member> members = await _membersAppService.GetMembersAsync();
            MembersListViewModel viewModel = new MembersListViewModel();
            viewModel.NewMembersCount = 2;
            viewModel.Members = members;
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

       
       

        [HttpPost]
        public async Task<IActionResult> Create(MemberViewModel viewModel)
        {
            Member member = new Member
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                City = new City
                {
                    Id = viewModel.CityId
                },
                MembershipTypeId = viewModel.MembershipTypeId,
                BirthDay = viewModel.BirthDay,
                AllowNewsletter = viewModel.AllowNewsletter,
            };
            await _membersAppService.AddMemberAsync(member);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int memberId)
        {
            await _membersAppService.DeleteMemberAsync(memberId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int memberId)
        {
            Member member = await _membersAppService.GetMemberAsync(memberId);
            MemberViewModel viewModel = new MemberViewModel
            {
                AllowNewsletter = member.AllowNewsletter,
                BirthDay = member.BirthDay,
                CityId = member.City.Id,
                MembershipTypeId=member.MembershipTypeId,
                Email = member.Email,
                Id = member.Id,
                LastName = member.LastName,
                Name = member.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MemberViewModel viewModel)
        {
            Member member = new Member
            {
                Id= viewModel.Id,
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                City = new City
                {
                    Id = viewModel.CityId
                },
                MembershipTypeId = viewModel.MembershipTypeId,
                BirthDay = viewModel.BirthDay,
                AllowNewsletter = viewModel.AllowNewsletter,
            };
            await _membersAppService.EditMemberAsync(member);
            return RedirectToAction("Index");
        }


        

        public IActionResult Test()
        {
            return View();
        }

       
    }
}
