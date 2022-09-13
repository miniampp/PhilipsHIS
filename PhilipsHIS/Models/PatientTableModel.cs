using System.ComponentModel.DataAnnotations;

namespace PhilipsHIS.Models
{
    public class PatientTableModel
    {
        [Key]
        [Required]
        public int NumberofRoom { get; set; }
    }
}
