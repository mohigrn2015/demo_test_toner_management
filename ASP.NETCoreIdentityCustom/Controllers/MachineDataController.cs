using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class MachineDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Guid userId = Guid.Empty;
        public IActionResult Index()
        {
            return View();
        }
        public MachineDataController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            userId = new Guid(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public IActionResult GetMAchine(DateTime date)
        {
            ViewData["CustomerId"] = _context.Customers.ToList();
            ViewData["ProjectId"] = _context.Projects.ToList();
            List<BlackColorViewModel> blackViews = new List<BlackColorViewModel>();
            BWAViewModel nn = new BWAViewModel();// to hold list of Customer and order details
            var machineLIst = (from Cust in _context.Customers
                               join pro in _context.Projects on Cust.CustomerId equals pro.CustomerId
                               join man in _context.Machines.Where(c => c.colorType == false) on pro.ProjectId equals man.ProjectId
                               select new { Cust.CustomerName, pro.ProjectName, man.MachineSN, man.MachineModel, man.colorType }).ToList();
            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in machineLIst)
            {
                BlackColorViewModel blackobj = new BlackColorViewModel(); // ViewModel
                List<BlackViewModel> black = new List<BlackViewModel>();
                List<ColorViewModel> color = new List<ColorViewModel>();
                foreach (var i in machineLIst)
                {
                    black.Add(new BlackViewModel { });
                }
                //blackobj.BlackPanel.CustomerName = item.CustomerName;

                //blackobj.BlackPanel.ProjectName = item.ProjectName;

                //blackobj.BlackPanel.MachineModel = item.MachineModel;

                //blackobj.BlackPanel.MachineSN = item.MachineSN;
                //blackobj.BlackPanel.Date = date;
                //blackobj.ColorsPanel.CustomerName = item.CustomerName;

                //blackobj.ColorsPanel.ProjectName = item.ProjectName;

                //blackobj.ColorsPanel.MachineModel = item.MachineModel;

                //blackobj.ColorsPanel.MachineSN = item.MachineSN;
                blackobj.BlackPanel = black;
                blackobj.ColorsPanel = color;
                blackViews.Add(blackobj);
            }
            nn.bwViewModels = blackViews;
            return View(blackViews);
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", blackView.ProjectId);
            //ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineSN", blackView.MachineId);
            //ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineModel", blackView.MachineId);
        }
        [HttpPost]
        public async Task<IActionResult> GetMAchine(BlackViewModel model)
        {
            try
            {
                PaperUseage paperUseage = new PaperUseage(); 
                if (model.MachineId != null)
                {
                    if (model.MachineId.Length > 0)
                    {
                        for (int i = 0; i < model.MachineId.Length; i++)
                        {
                            paperUseage = new PaperUseage
                            {
                                MachineId = model.MachineId[i],
                                CurrentUses = model.CurrentUses[i],
                                TotalCounter = model.TotalCounter,
                                TonerPercentage = model.TotalPercentage,
                                CurrentStock = model.CurrentStock,
                                //CurrentPercentage = model.CurrentPercentage,
                                TotalToner = model.TotalToner,
                                DateCreated = DateTime.Now,
                                DateModified = model.Date,
                                CreatedBy = userId

                            };
                            _context.Add(paperUseage);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                if (model.MachineIDs != null)
                {

                    if (model.MachineIDs.Length > 0)
                    {

                        for (int i = 0; i < model.MachineIDs.Length; i++)
                        {

                            paperUseage = new PaperUseage
                            {
                                MachineId = model.MachineIDs[i],
                                CurrentUses = model.CurrentsUses[i],
                                TotalCounter = model.TotalsCounter,
                                TonerPercentage = model.TotalsPercentage,
                                CurrentStock = model.CurrentsStock,
                                //CurrentPercentage = model.CurrentsPercentage,
                                TotalToner = model.TotalsToner,
                                DateCreated = DateTime.Now,
                                DateModified = model.Date,
                                CreatedBy = userId
                            };
                            _context.Add(paperUseage);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }            

            //ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", blackView.ProjectId);
            //ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineSN", blackView.MachineId);
            return RedirectToAction("GetMAchine");

        }


        public JsonResult LoadProject(int id)
        {
            var project = _context.Projects.Where(p => p.CustomerId == id).ToList();
            return Json(project);
        }
        public JsonResult LoadMachine(int id, string date)
        {
            var data = _context.Machines
                .Include(e => e.TonerConfigs)
                .ThenInclude(e => e.Toner)
                .OrderByDescending(e => e.TonerConfigs
                .FirstOrDefault().Toner.TonarId)
                .AsNoTracking()
                .Where(i => i.ProjectId.Equals(id) && !i.colorType)
                .Select(x => new PaperUseageViewModel()
                {
                    ProjectId = x.ProjectId,
                    MachineId = x.MachineId,
                    CurrentUses = 0,
                    MachineModel = x.MachineModel,
                    MachineSN = x.MachineSN,
                    colorType = x.colorType,
                    TonerConfigs = x.TonerConfigs
                }).ToList();
            var machineIdList = data.Select(d => d.MachineId).ToList();
            var currentUsesList = _context.PaperUseage
                        .Where(x => machineIdList.Contains(x.MachineId))
                        .Select(y => new
                        {
                            y.MachineId,
                            y.CurrentUses,
                            y.TotalCounter,
                            y.TonerPercentage,
                            y.DateCreated
                        }).ToList();
            data.ForEach(x =>
            {
                x.CurrentUses = currentUsesList.FirstOrDefault(y => y.MachineId.Equals(x.MachineId) && GetMonth(y.DateCreated, date))?.CurrentUses ?? default;
            });


            var data1 = _context.Machines
               .Include(e => e.TonerConfigs)
               .ThenInclude(e => e.Toner)
               .OrderByDescending(e => e.TonerConfigs
               .FirstOrDefault().Toner.TonarId)
               .AsNoTracking()
               .Where(i => i.ProjectId.Equals(id) && i.colorType)
               .Select(x => new PaperUseageViewModel()
               {
                   ProjectId = x.ProjectId,
                   MachineId = x.MachineId,
                   CurrentUses = 0,
                   MachineModel = x.MachineModel,
                   MachineSN = x.MachineSN,
                   colorType = x.colorType,
                   TonerConfigs = x.TonerConfigs
               }).ToList();
            var machineIdList1 = data1.Select(d => d.MachineId).ToList();
            var currentUsesList1 = _context.PaperUseage
                        .Where(x => machineIdList1.Contains(x.MachineId))
                        .Select(y => new
                        {
                            y.MachineId,
                            y.CurrentUses,
                            y.TotalCounter,
                            y.TonerPercentage,
                            y.DateCreated
                        }).ToList();
            data1.ForEach(x =>
            {
                x.CurrentUses = currentUsesList1.FirstOrDefault(y => y.MachineId.Equals(x.MachineId) && GetMonth(y.DateCreated, date))?.CurrentUses ?? default;
            });
            List<Object> obj = new List<Object>();
            obj.Add(data);
            obj.Add(data1);
            return Json(obj);
        }
        //public JsonResult LoadCLMachine(int id, string date)
        //{
        //    var data = _context.Machines
        //       .Include(e => e.TonerConfigs)
        //       .ThenInclude(e => e.Toner)
        //       .OrderByDescending(e => e.TonerConfigs
        //       .FirstOrDefault().Toner.TonarId)
        //       .AsNoTracking()
        //       .Where(i => i.ProjectId.Equals(id) && i.colorType)
        //       .Select(x => new PaperUseageViewModel()
        //       {
        //           ProjectId = x.ProjectId,
        //           MachineId = x.MachineId,
        //           CurrentUses = 0,
        //           MachineModel = x.MachineModel,
        //           MachineSN = x.MachineSN,
        //           colorType = x.colorType,
        //           TonerConfigs = x.TonerConfigs
        //       }).ToList();
        //    var machineIdList1 = data.Select(d => d.MachineId).ToList();
        //    var currentUsesList1 = _context.PaperUseage
        //                .Where(x => machineIdList1.Contains(x.MachineId))
        //                .Select(y => new
        //                {
        //                    y.MachineId,
        //                    y.CurrentUses,
        //                    y.DateCreated
        //                }).ToList();
        //    data.ForEach(x =>
        //    {
        //        x.CurrentUses = currentUsesList1.FirstOrDefault(y => y.MachineId.Equals(x.MachineId) && GetMonth(y.DateCreated, date))?.CurrentUses ?? default;
        //    });
        //    return Json(data);
        //}


        private bool GetMonth(DateTime? firstDateTime, string secondDateTime)
        {
            var month = Convert.ToDateTime(secondDateTime).AddMonths(-1).Month;
            var firstMonth = firstDateTime?.Month ?? 15;
            return month.Equals(firstMonth);
        }
    }

}
