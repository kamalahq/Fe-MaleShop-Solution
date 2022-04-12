using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad sahəsi boş buraxıla bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mail sahəsi boş buraxıla bilməz")]
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "Mesaj sahəsi boş buraxıla bilməz")]
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int? AnswerByUserId { get; set; }
    }
}
