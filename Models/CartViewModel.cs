using System.Collections.Generic;

namespace hello_world_3.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }
        public decimal OrderTotal { get; set; }
    }
}