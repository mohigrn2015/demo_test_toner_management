using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModel;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class TonerConfigsController : Controller
    {
        private readonly ILogger<TonerConfigsController> logger;
        private readonly ApplicationDbContext _context;

        public TonerConfigsController(ApplicationDbContext context, ILogger<TonerConfigsController> logger)
        {
            this.logger = logger;
            _context = context;
        }

        // GET: TonerConfigs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TonerConfigs.Include(t => t.Machine).Include(t => t.Toner);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: TonerConfigs/Create
        public IActionResult Create()
        {
            try
            {
                TonarViewModel model = new TonarViewModel
                {
                    Toners = _context.Toners.ToList(),
                    Machines = _context.Machines.ToList(),
                };

                return View(model);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TonarViewModel model)
        {
            try
            {
                var data = _context.TonerConfigs.Where(e => e.MachinID == model.MachinID).ToList();
                if (data.Count > 0)
                {
                    _context.TonerConfigs.RemoveRange(data);
                    _context.SaveChanges();
                }

                if (ModelState.IsValid)
                {
                    if (model.TonarIDs.Length == 4 && model.TonarIDs.Length != 0)
                    {
                        for (int i = 0; i < model.TonarIDs.Length; i++)
                        {
                            TonerConfig tonerConfig = new TonerConfig
                            {
                                MachinID = model.MachinID,
                                TonarID = model.TonarIDs[i],
                            };
                            _context.Add(tonerConfig);
                            await _context.SaveChangesAsync();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else if (model.TonarIDs.Length == 0 && model.TonarId != 0)
                    {
                        TonerConfig tonerConfig = new TonerConfig
                        {
                            MachinID = model.MachinID,
                            TonarID = Convert.ToInt32(model.TonarId),
                        };
                        _context.Add(tonerConfig);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        model.errmsg = "Please Select At least 4 items";
                    }
                }
                // ViewData["MachinID"] = new SelectList(_context.Machines, "MachineId", "MachineSN", model.tonerConfig.MachinID);
                // ViewData["TonarID"] = new SelectList(_context.Toners, "TonarId", "TonarModel", model.tonerConfig.TonarID);
                return View("Create");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // GET: TonerConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.TonerConfigs == null)
                {
                    return NotFound();
                }

                var tonerConfig = await _context.TonerConfigs.FindAsync(id);
                if (tonerConfig == null)
                {
                    return NotFound();
                }
                ViewData["MachinID"] = new SelectList(_context.Machines, "MachineId", "MachineSN", tonerConfig.MachinID);
                ViewData["TonarID"] = new SelectList(_context.Toners, "TonarId", "TonarModel", tonerConfig.TonarID);
                return View(tonerConfig);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TonerConfig tonerConfig)
        {
            //if (id != tonerConfig.TConfligID)
            //{
            //    return NotFound();
            //}

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tonerConfig);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TonerConfigExists(tonerConfig.TConfligID))
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
                ViewData["MachinID"] = new SelectList(_context.Machines, "MachineId", "MachineSN", tonerConfig.MachinID);
                ViewData["TonarID"] = new SelectList(_context.Toners, "TonarId", "TonarId", tonerConfig.TonarID);
                return View(tonerConfig);

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
                var firstEntity = _context.TonerConfigs.Where(c => c.TConfligID == id).FirstOrDefault();
                _context.TonerConfigs.Remove(firstEntity);
                _context.SaveChanges();
            }
            finally
            {

            }
            return RedirectToAction("Index");
        }

        private bool TonerConfigExists(int id)
        {
            return _context.TonerConfigs.Any(e => e.TConfligID == id);
        }
    }
}