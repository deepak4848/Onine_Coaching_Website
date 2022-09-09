#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YET.Models;
using YET.Models.UserModel;

namespace YET.Controllers
{
    public class EditModel : PageModel
    {
        private readonly YET.Models.DatabaseContext _context;

        public EditModel(YET.Models.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public tbl_Teams tbl_Teams { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            tbl_Teams = await _context.tbl_Teams.FirstOrDefaultAsync(m => m.TeamId == id);

            if (tbl_Teams == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(tbl_Teams).State = EntityState.Modified;

            try
            {
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

            return RedirectToPage("./Index");
        }

        private bool tbl_TeamsExists(int id)
        {
            return _context.tbl_Teams.Any(e => e.TeamId == id);
        }
    }
}
