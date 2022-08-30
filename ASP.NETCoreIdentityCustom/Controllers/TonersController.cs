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
    public class TonarsController : Controller
    {
        private readonly ILogger<TonarsController> logger;
        private readonly ApplicationDbContext _context;

        public TonarsController(ApplicationDbContext context, ILogger<TonarsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: Tonars
        public async Task<IActionResult> Index()
        {

            var AppDb = _context.Toners;

            return View(await AppDb.ToListAsync());

        }


        // GET: Tonars/Create
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Create()
        {
            ViewData["MachineId"] = new SelectList(_context.Machines, "MachineId", "MachineModel");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Toner tonar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tonar);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(tonar);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                ViewData["msg"] = "Toner model available database";
                return View();

            }


        }

        // GET: Tonars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Toners == null)
                {
                    return NotFound();
                }

                var tonar = await _context.Toners.FindAsync(id);
                if (tonar == null)
                {
                    return NotFound();
                }
                return View(tonar);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Toner toner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(toner);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TonarExists(toner.TonarId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToAction("Index");
                }
                return View(toner);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            //if (id != toner.TonarId)
            //{
            //    return NotFound();
            //}

        }


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Customers == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(customer);
        //}




        public ActionResult Delete(int? id)
        {
            try
            {
                var firstEntity = _context.Toners.Where(c => c.TonarId == id).FirstOrDefault();
                _context.Toners.Remove(firstEntity);
                _context.SaveChanges();
            }
            finally
            {

            }
            return RedirectToAction("Index");
        }

        private bool TonarExists(int id)
        {
            return (_context.Toners?.Any(e => e.TonarId == id)).GetValueOrDefault();
        }
    }
}
