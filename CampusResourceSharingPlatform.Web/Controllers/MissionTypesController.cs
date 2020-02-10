using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusResourceSharingPlatform.Web.Data;
using CampusResourceSharingPlatform.Web.Models;

namespace CampusResourceSharingPlatform.Web
{
    public class MissionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MissionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MissionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MissionTypes.ToListAsync());
        }

        // GET: MissionTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionType = await _context.MissionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missionType == null)
            {
                return NotFound();
            }

            return View(missionType);
        }

        // GET: MissionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MissionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeName,Id")] MissionType missionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(missionType);
        }

        // GET: MissionTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionType = await _context.MissionTypes.FindAsync(id);
            if (missionType == null)
            {
                return NotFound();
            }
            return View(missionType);
        }

        // POST: MissionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TypeName,Id")] MissionType missionType)
        {
            if (id != missionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(missionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionTypeExists(missionType.Id))
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
            return View(missionType);
        }

        // GET: MissionTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionType = await _context.MissionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missionType == null)
            {
                return NotFound();
            }

            return View(missionType);
        }

        // POST: MissionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var missionType = await _context.MissionTypes.FindAsync(id);
            _context.MissionTypes.Remove(missionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionTypeExists(string id)
        {
            return _context.MissionTypes.Any(e => e.Id == id);
        }
    }
}
