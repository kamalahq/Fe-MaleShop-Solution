using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class Category : BaseEntity
    {
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public virtual Category Parent  { get; set; }
        public virtual ICollection<Category> Children { get; set; }
       

    }
}
