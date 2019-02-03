using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hello_world_3.Models;

namespace hello_world_3.Controllers
{
    public class HomeController : Controller
    {
        private static Cart _Cart = new Cart();
        public void CartItemCount() {
            var total = _Cart.CartItems.Sum(c => c.Quantity);
            ViewData["Shopping Cart Quantity"] = total;
        }
        public IActionResult Index()
        {
            CartItemCount();
            return View(Timepieces.Inventory);
        }
        public IActionResult Details(int Id)
        {
            CartItemCount();
            var item = Timepieces.Inventory.SingleOrDefault(i => i.Id == Id);
            if(item != null)
            {
                return View(item);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult ShowCart()
        {
            var cartVM = new CartViewModel();
            cartVM.CartItems = _Cart.CartItems;
            cartVM.OrderTotal = _Cart.CartItems.Sum(c => c.getTotalPrice());
            CartItemCount();
            return View("Cart", cartVM);
        }
        public IActionResult AddToCart(int itemId)
        {
            var item = Timepieces.Inventory.SingleOrDefault(i => i.Id == itemId);
            if(item != null)
            {
                var cartItem = new CartItem
                {
                    Item = item,
                    Quantity = 1
                };
                _Cart.addItem(cartItem);
            }
            return RedirectToAction("ShowCart");
        }
        public IActionResult RemoveFromCart(int itemId)
        {
            _Cart.removeItem(itemId);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            CartItemCount();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            CartItemCount();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
