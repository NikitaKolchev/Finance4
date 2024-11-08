using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finance4;
using Finance4.Models;

namespace Finance4.Controllers
{
    public class BudgetPeriodsController : Controller
    {
        private readonly FinanceDbContext _context;

        public BudgetPeriodsController(FinanceDbContext context)
        {
            _context = context;
        }

        // GET: BudgetPeriods
        [ResponseCache(Duration = 264, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            var financeDbContext = _context.BudgetPeriods.Include(b => b.User);
            return View(await financeDbContext.ToListAsync());
        }

        // GET: BudgetPeriods/Details/5
        [ResponseCache(Duration = 264, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetPeriod = await _context.BudgetPeriods
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.PeriodId == id);
            if (budgetPeriod == null)
            {
                return NotFound();
            }

            return View(budgetPeriod);
        }

        // GET: BudgetPeriods/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: BudgetPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeriodId,UserId,Budget,NeededBalance,StartDate,EndDate")] BudgetPeriod budgetPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetPeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", budgetPeriod.UserId);
            return View(budgetPeriod);
        }

        // GET: BudgetPeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetPeriod = await _context.BudgetPeriods.FindAsync(id);
            if (budgetPeriod == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", budgetPeriod.UserId);
            return View(budgetPeriod);
        }

        // POST: BudgetPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeriodId,UserId,Budget,NeededBalance,StartDate,EndDate")] BudgetPeriod budgetPeriod)
        {
            if (id != budgetPeriod.PeriodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetPeriodExists(budgetPeriod.PeriodId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", budgetPeriod.UserId);
            return View(budgetPeriod);
        }

        // GET: BudgetPeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetPeriod = await _context.BudgetPeriods
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.PeriodId == id);
            if (budgetPeriod == null)
            {
                return NotFound();
            }

            return View(budgetPeriod);
        }

        // POST: BudgetPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetPeriod = await _context.BudgetPeriods.FindAsync(id);
            if (budgetPeriod != null)
            {
                _context.BudgetPeriods.Remove(budgetPeriod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetPeriodExists(int id)
        {
            return _context.BudgetPeriods.Any(e => e.PeriodId == id);
        }
    }
}
