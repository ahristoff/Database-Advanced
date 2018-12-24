using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {       
        public int ProductId { get; set; }
       
        public string Name { get; set; }
        [Range(50, 0, ErrorMessage = "Range must be >= 50")]
        public double Quantity { get; set; }
        [Range(20, 20, ErrorMessage = "Range must be <= 20")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
