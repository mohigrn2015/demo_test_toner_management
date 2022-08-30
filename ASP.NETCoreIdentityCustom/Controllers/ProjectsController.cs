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
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context, ILogger<ProjectsController> logger)
        {
            this.logger = logger;
            _context = context;
        }
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            try
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", project.CustomerId);
                //if (ModelState.IsValid)
                //{
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}

                //return View(project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Projects == null)
                {
                    return NotFound();
                }

                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    return NotFound();
                }
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", project.CustomerId);
                return View(project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            try
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", project.CustomerId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var firstEntity = _context.Projects.Where(c => c.ProjectId == id).FirstOrDefault();
                _context.Projects.Remove(firstEntity);
                _context.SaveChanges();
            }
            finally
            {

            }
            return RedirectToAction("Index");
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
