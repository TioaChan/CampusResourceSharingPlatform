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
    public class MissionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MissionDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MissionDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MissionDetails.Include(m => m.AcceptUser).Include(m => m.MissionType).Include(m => m.PostUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MissionDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionDetail = await _context.MissionDetails
                .Include(m => m.AcceptUser)
                .Include(m => m.MissionType)
                .Include(m => m.PostUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missionDetail == null)
            {
                return NotFound();
            }

            return View(missionDetail);
        }

        // GET: MissionDetails/Create
        public IActionResult Create()
        {
            ViewData["AcceptUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.MissionTypes, "Id", "Id");
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: MissionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MissionName,TypeId,PostUserId,PostTime,InvalidTime,MissionDetails,MissionReward,IsAccepted,AcceptUserId,AcceptTime,Id")] MissionDetail missionDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missionDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcceptUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.AcceptUserId);
            ViewData["TypeId"] = new SelectList(_context.MissionTypes, "Id", "Id", missionDetail.TypeId);
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.PostUserId);
            return View(missionDetail);
        }

        // GET: MissionDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionDetail = await _context.MissionDetails.FindAsync(id);
            if (missionDetail == null)
            {
                return NotFound();
            }
            ViewData["AcceptUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.AcceptUserId);
            ViewData["TypeId"] = new SelectList(_context.MissionTypes, "Id", "Id", missionDetail.TypeId);
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.PostUserId);
            return View(missionDetail);
        }

        // POST: MissionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MissionName,TypeId,PostUserId,PostTime,InvalidTime,MissionDetails,MissionReward,IsAccepted,AcceptUserId,AcceptTime,Id")] MissionDetail missionDetail)
        {
            if (id != missionDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(missionDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionDetailExists(missionDetail.Id))
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
            ViewData["AcceptUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.AcceptUserId);
            ViewData["TypeId"] = new SelectList(_context.MissionTypes, "Id", "Id", missionDetail.TypeId);
            ViewData["PostUserId"] = new SelectList(_context.Users, "Id", "Id", missionDetail.PostUserId);
            return View(missionDetail);
        }

        // GET: MissionDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionDetail = await _context.MissionDetails
                .Include(m => m.AcceptUser)
                .Include(m => m.MissionType)
                .Include(m => m.PostUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missionDetail == null)
            {
                return NotFound();
            }

            return View(missionDetail);
        }

        // POST: MissionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var missionDetail = await _context.MissionDetails.FindAsync(id);
            _context.MissionDetails.Remove(missionDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionDetailExists(string id)
        {
            return _context.MissionDetails.Any(e => e.Id == id);
        }
    }
}
