using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YET.Models.UserModel
{
    public class tbl_Teams
    {
        [Key]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Plan Name cannot be blank")]
        public  string? TeamName { get; set; }

        
        public string? TeamDesignation { get; set; }

        
        public string? TeamDescription { get; set; }
        public string? TeamImage { get; set; }
        public DateTime TeamCreatedDate { get; set; }

       
        public DateTime TeamDOJ { get; set; }
        public DateTime?  TeamEOS { get; set; } 
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        [NotMapped]

        public IFormFile ImageFile { get; set; }
    }
}
