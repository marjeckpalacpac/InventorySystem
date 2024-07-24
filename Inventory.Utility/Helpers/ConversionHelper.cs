using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Utility.Helpers
{
    public static class ConversionHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(this List<LookupListing> list)
        {
            List<SelectListItem> selectLists = new List<SelectListItem>();
            if (list != null)
            {

                selectLists = list.Select(s => new SelectListItem() { 
                    Text = s.Name!,
                    Value = s.Value ?? s.Id.ToString()
                }).ToList();

            }
            return selectLists;
        }
    }
}
