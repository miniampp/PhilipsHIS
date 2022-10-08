using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhilipsHIS.Models
{
    public class VitalSignsModel
    {
        [Key]
        [Required]        
        public string HR { get; set; }
        public string SpO2 { get; set; }
        public string RR { get; set; }
        public string Temp { get; set; }
        public string BP { get; set; }
    }
}
