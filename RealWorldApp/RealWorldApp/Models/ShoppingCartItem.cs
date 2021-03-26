using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class ShoppingCartItem
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

    }

    public class ShowCartItem
    {
        public int id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string productName { get; set; }
    }
}
