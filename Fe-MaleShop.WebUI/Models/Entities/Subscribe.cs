using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedDate { get; set; }
    }
}
