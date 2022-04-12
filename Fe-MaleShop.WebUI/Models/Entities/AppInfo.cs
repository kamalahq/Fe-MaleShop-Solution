using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class AppInfo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string HashTag { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
