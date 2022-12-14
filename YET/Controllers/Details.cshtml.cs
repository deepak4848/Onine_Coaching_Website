#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YET.Models;
using YET.Models.UserModel;

namespace YET.Controllers
{
    public class DetailsModel : PageModel
    {
        private readonly YET.Models.DatabaseContext _context;

        public DetailsModel(YET.Models.DatabaseContext context)
        {
            _context = context;
        }

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
    }
}
