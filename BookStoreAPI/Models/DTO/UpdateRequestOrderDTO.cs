﻿namespace BookStoreAPI.Models.DTO
{
    public class UpdateRequestOrderDTO
    {
        public string UserId { get; set; }  // Foreign key
        public DateTime OrderDate { get; set; }
    }
}
