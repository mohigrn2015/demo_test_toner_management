using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public ReportController(IWebHostEnvironment env)
        {
            _env = env;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Print()
        //{
        //    string mimtype = "";
        //    int extention = 1;
        //    var path = $"{this._env.WebRootPath}\\Reports\\Report1.rdlc";
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("rp1", "welcome to reports");
        //    LocalReport localReport = new LocalReport(path);
        //    var  result =localReport.Execute(RenderType.Pdf,extention,parameters,mimtype);
        //    return File(result.MainStream, "application/pdf");

        //}
    }

}
