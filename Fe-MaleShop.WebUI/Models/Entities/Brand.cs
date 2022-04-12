using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Fe_MaleShop.WebUI.Models.Entities
{
    public class Brand : BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        //public virtual ICollection <Product>  Products { get; set; }

    }
}
