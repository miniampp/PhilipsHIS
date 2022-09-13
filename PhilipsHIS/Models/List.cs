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
        //public DateOnly DOB { get; set; }
        public string Sex { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        [DisplayName("Emergency Number")]
        public string EmergNo { get; set; }
        public string Email { get; set; }
        [DisplayName("Address 1")]
        public string Address1 { get; set; }
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

    }
}
