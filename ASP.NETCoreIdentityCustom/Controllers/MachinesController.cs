using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Authorization;
using ASP.NETCoreIdentityCustom.Core;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class MachinesController : Controller
    {

        private readonly ILogger<CustomersController> logger;
        private readonly ApplicationDbContext _context;

        public MachinesController(ApplicationDbContext context, ILogger<CustomersController> logger)
        {
            this.logger = logger;
            _context = context;
        }

        // GET: Machines
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Machines.Include(m => m.Project).AsNoTracking().ToListAsync();
            return View(applicationDbContext);
        }

        //GET: Machines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.MachineId == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = _context.Customers.ToList();
            //ViewData["ProjectId"]= _context.Projects.ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machine machine)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    machine.Project = null;
                    _context.Add(machine);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", machine.ProjectId);
                //ViewDara["MachineId"]= new (_context.Machiness, "MachineId", "MachineName", tonner.MachineId);
                return View(machine);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                ViewData["CustomerId"] = _context.Customers.ToList();
                ViewData["msg"] = "Serial number available database";
                return View();

            }
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Machines == null)
                {
                    return NotFound();
                }

                var machine = await _context.Machines.FindAsync(id);
                var project = await _context.Projects.FindAsync(machine.ProjectId);

                machine.Project = project;
                if (machine == null)
                {
                    return NotFound();
                }
                ViewData["CustomerId"] = _context.Customers.ToList();
                ViewData["ProjectId"] = _context.Projects.Where(x => x.CustomerId == project.CustomerId).ToList();
                return View(machine);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Machine machine)
        {
            try
            {
                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
                if (ModelState.IsValid)
                {
                    try
                    {
                        machine.Project = null;
                        _context.Update(machine);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MachineExists(machine.MachineId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", machine.ProjectId);
                return View(machine);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }

            //if (id != machine.MachineId)
            //{
            //    return NotFound();
            //}

        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var firstEntity = _context.Machines.Where(c => c.MachineId == id).FirstOrDefault();
                _context.Machines.Remove(firstEntity);
                _context.SaveChanges();
            }
            finally
            {

            }
            return RedirectToAction("Index");
        }



        private bool MachineExists(int id)
        {
            return (_context.Machines?.Any(e => e.MachineId == id)).GetValueOrDefault();
        }

        public JsonResult LoadProject(int id)
        {
            var project = _context.Projects.Where(p => p.CustomerId == id).ToList();
            return Json(project);
        }
        public JsonResult GetMachineById(int id)
        {
            var machine = _context.Machines.Where(p => p.MachineId == id).FirstOrDefault();
            return Json(machine);
        }

    }
}
