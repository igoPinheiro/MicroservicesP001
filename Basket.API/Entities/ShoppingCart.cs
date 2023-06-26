﻿namespace Basket.API.Entities;

public class ShoppingCart
{
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;

            foreach (var item in Items)
                totalPrice += item.Price * item.Quantity;

            return totalPrice;
        }
    }
}
