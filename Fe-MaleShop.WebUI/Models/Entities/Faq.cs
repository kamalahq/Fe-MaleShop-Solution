using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class Faq : BaseEntity
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Anwser { get; set; }
    }
}
