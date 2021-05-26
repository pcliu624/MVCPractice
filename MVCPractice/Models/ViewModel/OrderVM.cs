using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPractice.Models.ViewModel
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public  OrderDetails OrderDetails { get; set; }
        public IEnumerable<SelectListItem> ProductSelectList { get; set; }
    }
}
