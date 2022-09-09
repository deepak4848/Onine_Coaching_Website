#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YET.Models;
using YET.Models.UserModel;

namespace YET.Controllers.demo
{
    public class test1Controller : Controller
    {
        private readonly DatabaseContext _context;

        public test1Controller(DatabaseContext context)
        {
            _context = context;
        }

        // GET: test1
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_Teams.ToListAsync());
        }

        // GET: test1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }

            return View(tbl_Teams);
        }

        // GET: test1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: test1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName,TeamDesignation,TeamDescription,TeamImage,TeamCreatedDate,TeamDOJ,TeamEOS,CreatedBy,ModifiedBy")] tbl_Teams tbl_Teams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_Teams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_Teams);
        }

        // GET: test1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams.FindAsync(id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }
            return View(tbl_Teams);
        }

        // POST: test1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName,TeamDesignation,TeamDescription,TeamImage,TeamCreatedDate,TeamDOJ,TeamEOS,CreatedBy,ModifiedBy")] tbl_Teams tbl_Teams)
        {
            if (id != tbl_Teams.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_Teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_TeamsExists(tbl_Teams.TeamId))
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
            return View(tbl_Teams);
        }

        // GET: test1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }

            return View(tbl_Teams);
        }

        // POST: test1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_Teams = await _context.tbl_Teams.FindAsync(id);
            _context.tbl_Teams.Remove(tbl_Teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_TeamsExists(int id)
        {
            return _context.tbl_Teams.Any(e => e.TeamId == id);
        }
    }
}
