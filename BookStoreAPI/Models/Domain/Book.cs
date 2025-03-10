﻿namespace BookStoreAPI.Models.Domain
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }  // Foreign key
        public Category Category { get; set; }  // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
