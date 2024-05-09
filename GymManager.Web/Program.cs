using GymManager.ApplicationServices.EquipmentTypes;
using GymManager.ApplicationServices.Members;
using GymManager.ApplicationServices.MembershipTypes;
using GymManager.ApplicationServices.Navigation;
using GymManager.Core;
using GymManager.Core.Members;
using GymManager.DataAccess;
using GymManager.DataAccess.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using System.Net;
using Wkhtmltopdf.NetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddTransient<IMembersAppService, MembersAppService>();
builder.Services.AddTransient<IMenuAppService, MenuAppService>();
builder.Services.AddTransient<IMembershipTypesAppService, MembershipTypesAppService>();
builder.Services.AddTransient<IEquipmentTypesAppService, EquipmentTypesAppService>();

builder.Services.AddTransient<IRepository<int, Member>, MembersRepository>();
builder.Services.AddTransient<IRepository<int, EquipmentType>, EquipmentTypesRepository>();
builder.Services.AddTransient<IRepository<int, MembershipType>, MembershipTypesRepository>();

builder.Services.AddWkhtmltopdf();

// Database configuration
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GymManagerContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<GymManagerContext>().AddUserStore<UserStore<IdentityUser, IdentityRole, GymManagerContext>>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");


// Serilog setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});


// Configure forwarded headers
//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
//});


var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});




app.UseAuthentication();
app.UseAuthorization();

var ci = new CultureInfo("es-MX");
// Configure the Localization middleware
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci), // Set te default culture
    SupportedCultures = new List<CultureInfo> { ci },
    SupportedUICultures = new List<CultureInfo> { ci },
});

app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");
//app.MapGet("/File1.html", () => DateTime.Now.ToString());

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "attendance", pattern: "{controller=Attendance}/{action=MemberIn}/{id?}");



app.Run();




