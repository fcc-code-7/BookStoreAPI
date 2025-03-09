﻿namespace BookStoreAPI.Models.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }  // Navigation property
    }
}
