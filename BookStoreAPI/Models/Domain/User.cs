﻿using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Models.Domain
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public List<Order> Orders { get; set; }  // Navigation property
    }
}
