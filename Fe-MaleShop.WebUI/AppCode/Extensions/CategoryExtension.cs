using Fe_MaleShop.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public IEnumerable<Category> GetchildCategories(this Category parent)
        {
            yield return parent;

            foreach (var child in parent.Children.SelectMany(c=>c.GetchildCategories()))
            {
                yield return child;
            }
        }
    }
}
