using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhilipsHIS.Models
{
    public class List
    {
        [Key]
        [Required]
        public string HN { get; set; }
        public string Title { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        [DisplayName("Blood Group")]
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Allergies { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        [DisplayName("Emergency Number")]
        public string EmergNo { get; set; }
        [DisplayName("Relationship")]
        public string EmergRela { get; set; }
        public string Email { get; set; }
        [DisplayName("Address 1")]
        public string Address1 { get; set; }
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        //VitalSigns
        public string HR { get; set; }
        public string SpO2 { get; set; }
        public string RR { get; set; }
        public string Temp { get; set; }
        public string BP { get; set; }
    }
}
