using Microsoft.EntityFrameworkCore;

namespace YET.Models
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)  
        {

        }

        public DbSet<UserModel.tbl_Teams> tbl_Teams { get; set; }
    }
}
