using GymManager.DataAccess.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Wkhtmltopdf.NetCore;
using MySql.Data.MySqlClient;
using GymManager.Core;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using MySqlX.XDevAPI.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;

namespace GymManager.Web.Controllers
{
    public class ReportsController : Controller
    {
        const string mysqlConnectionString = "server=localhost;port=3306;database=gymmanager;user=root;CharSet=utf8;SslMode=none;Pooling=false;AllowPublicKeyRetrieval=True;Password=3468";
        private readonly IGeneratePdf _generatePdf;
        private readonly IWebHostEnvironment _environment;
        public ReportsController(IWebHostEnvironment enviroment, IGeneratePdf generate) 
        { 
            _generatePdf = generate;
            _environment = enviroment;
        }
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Attendance()
        {
            string path = System.IO.Path.Combine(_environment.ContentRootPath, "Reports\\StaffAttendance.rdlc");
            Stream reportDefinition = System.IO.File.OpenRead(path);

            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;
            report.LoadReportDefinition(reportDefinition);

            AttendanceDataSet dataSet = new AttendanceDataSet();

            using (MySqlConnection connection = new MySqlConnection(mysqlConnectionString))
            {
                connection.Open();
                // Create table adapters
                MySqlDataAdapter adapterStaff = new MySqlDataAdapter("SELECT s.Name, s.LastName,s.Id, COUNT(a.id) AS AttendanceCount FROM staff s LEFT JOIN attendances a ON s.Id = a.EmployeeId GROUP BY s.Id;", connection);
                MySqlDataAdapter adapterAttendance = new MySqlDataAdapter("SELECT a.Id AS StaffId, a.Date AS AttendanceDay FROM attendances a WHERE Date >= DATE_SUB(NOW(), INTERVAL 7 DAY);", connection);

                // Filling Tables in DataSet
                adapterStaff.Fill(dataSet, "Staff");
                adapterAttendance.Fill(dataSet, "Attendance");

                connection.Close();
            }

            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "pdf";
            string reportName = "Attendance";
            string[] streamIds = null;
            Warning[] warnings = null;

            report.SetParameters(new ReportParameter[] { new ReportParameter("DateFrom", DateTime.Today.AddDays(-7).ToString()),
            new ReportParameter("DateTo", DateTime.Today.ToString()) });


            report.DataSources.Add(new ReportDataSource("Attendance", (System.Data.DataTable)dataSet.Attendance));
            report.DataSources.Add(new ReportDataSource("Staff", (System.Data.DataTable)dataSet.Staff));
            streamBytes = report.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamIds, out warnings);
            
            return File(streamBytes, mimeType, $"{reportName}.{filenameExtension}");
        }

        public IActionResult NewMembers()
        {
            string path = System.IO.Path.Combine(_environment.ContentRootPath,"Reports\\NewMembers.rdlc");
            Stream reportDefinition = System.IO.File.OpenRead(path);

            LocalReport  report = new LocalReport();
            report.EnableExternalImages = true;
            report.LoadReportDefinition(reportDefinition);

            MembersDataSet dataSet = new MembersDataSet();

            Random random = new Random();
            string[] membershipTypes = new string[] { "Basic", "Family", "Gold" };
            for (int i = 0; i <28; i++)
            {
                int day = random.Next(1, 10) * -1;
                int membershipIndex = random.Next(0,membershipTypes.Length);
               
                MembersDataSet.MemberRow row = dataSet.Member.NewMemberRow();
                row.Name = $"Member Pérez {i}";
                row.RegisteredOn = DateTime.Today.AddDays(day);
                row.MembershipType = membershipTypes[membershipIndex];

                dataSet.Member.Rows.Add(row);
            }
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "pdf";
            string reportName = "NewMembers";
            string[] streamIds = null;
            Warning[] warnings = null;

            report.SetParameters(new ReportParameter[] { new ReportParameter("DateFrom", DateTime.Today.AddDays(-10).ToString()),
            new ReportParameter("DateTo", DateTime.Today.ToString())
            });


            report.DataSources.Add(new ReportDataSource("Members", (System.Data.DataTable) dataSet.Member));
            streamBytes = report.Render("PDF", null,out mimeType,out encoding, out filenameExtension, out streamIds, out warnings);
            return File(streamBytes,mimeType,$"{reportName}.{filenameExtension}");
        }
    }
}
