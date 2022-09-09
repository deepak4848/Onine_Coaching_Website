#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YET.Models;
using YET.Models.UserModel;

namespace YET.Controllers
{
    public class CreateModel : PageModel
    {
        private readonly YET.Models.DatabaseContext _context;

        public CreateModel(YET.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public tbl_Teams tbl_Teams { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.tbl_Teams.Add(tbl_Teams);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
