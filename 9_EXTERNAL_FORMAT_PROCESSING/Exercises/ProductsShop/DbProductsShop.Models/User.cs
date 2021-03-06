﻿using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class User
    {
        public int Id { get; set; }

        public int? Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Product> ProductBuyers { get; set; } = new List<Product>();

        public ICollection<Product> ProductSellers { get; set; } = new List<Product>();
    }
}
