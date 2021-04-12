using StoreFrontLab1.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreFrontLab1.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public Painting_Copy Product { get; set; }

        public CartItemViewModel(int qty, Painting_Copy product)
        {
            Qty = qty;
            Product = product;
        }
    }
}