using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class ColorTonerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Index()
        {
            return View();
        }
        public ColorTonerController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult GetcolorData(DateTime date)
        {
            DateTime predate = DateTime.Parse(DateTime.Now.Month + "/1/" + DateTime.Now.Year);
            ViewData["CustomerId"] = _context.Customers.ToList();
            ViewData["ProjectId"] = _context.Projects.ToList();
            List<ColorViewModel> colorViewdata = new List<ColorViewModel>();
            var testdate = _context.PaperUseage.GroupBy(b => b.MachineId).Select(s => s.OrderByDescending(o => o.PaperUseageID).FirstOrDefault()).ToList();
            var machineLIst = (from Cust in _context.Customers
                               join pro in _context.Projects on Cust.CustomerId equals pro.CustomerId
                               join man in _context.Machines.Where(l => l.colorType == true) on pro.ProjectId equals man.ProjectId
                               select new { Cust.CustomerName, pro.ProjectName, man.MachineSN, man.MachineModel, man.colorType, man.MachineId }).ToList();
            //query getting data from database from joining two tables and storing data in customerlist

            foreach (var item in machineLIst)

            {
                var check = testdate.Where(w => w.MachineId == item.MachineId).FirstOrDefault();
                ColorViewModel colorobj = new ColorViewModel(); // ViewModel

                colorobj.CustomerName = item.CustomerName;

                colorobj.ProjectName = item.ProjectName;

                colorobj.MachineModel = item.MachineModel;

                colorobj.MachineSN = item.MachineSN;
                colorobj.Date = date;
                colorobj.PreviousUses = check == null ? 0 : check.CurrentUses;


            }
            return View(colorViewdata);
            // ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", colorobj.ProjectId);
            // ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineSN", colorobj.MachineId);
            //ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineModel", blackView.MachineId);
        }


        [HttpPost]
        public async Task<IActionResult> GetcolorData(ColorViewModel model)
        {

            if (model.MachineIDs.Length > 0)
            {

                for (int i = 0; i < model.MachineIDs.Length; i++)
                {

                    PaperUseage paperUseage = new PaperUseage
                    {
                        MachineId = model.MachineIDs[i],
                        CurrentUses = model.CurrentUses[i],
                        DateCreated = DateTime.Now,
                        DateModified = model.Date
                    };
                    _context.Add(paperUseage);
                    await _context.SaveChangesAsync();
                }
            }
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", blackView.ProjectId);
            //ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineSN", blackView.MachineId);
            return RedirectToAction("GetColorData");
        }

        public JsonResult LoadProject(int id)
        {
            var project = _context.Projects.Where(p => p.CustomerId == id).ToList();
            return Json(project);
        }
        public JsonResult LoadMachine(int id)
        {
            var machine = _context.Machines.Where(p => p.ProjectId == id && p.colorType == true).Include(e => e.TonerConfigs).ThenInclude(e => e.Toner).OrderByDescending(e => e.TonerConfigs.FirstOrDefault().TonarID).ToList();
            return Json(machine); ;
        }
    }

}