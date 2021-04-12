using StoreFrontLab1.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFrontLab1.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
                if (ViewBag.Message != null)
                {
                    ViewBag.Message = "There are no items in your cart";
                }
                else
                {
                    ViewBag.Message = null;
                }
            }
            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int bookID, int qty)
        {
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            shoppingCart[bookID].Qty = qty;

            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            shoppingCart.Remove(id);

            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
    }
}