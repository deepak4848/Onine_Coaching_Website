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
    public class IndexModel : PageModel
    {
        private readonly YET.Models.DatabaseContext _context;

        public IndexModel(YET.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IList<tbl_Teams> tbl_Teams { get;set; }

        public async Task OnGetAsync()
        {
            tbl_Teams = await _context.tbl_Teams.ToListAsync();
        }
    }
}
