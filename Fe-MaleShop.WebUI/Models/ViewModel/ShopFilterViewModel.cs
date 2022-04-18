using Fe_MaleShop.WebUI.Models.Entities;
using System.Collections.Generic;

namespace Fe_MaleShop.WebUI.Models.ViewModel
{
    public class ShopFilterViewModel
    {
        public List <Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductSize> Sizes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
