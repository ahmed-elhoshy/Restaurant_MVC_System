using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Resturant.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; } // fk

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // for uploading image
        public string? ImageUrl { get; set; }
        
        [ValidateNever] // This property is not validated by the model state
        public Category? Category { get; set; } // A product belongs to a category
        
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }  // A product can be in many order items
        
        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients { get; set; } // A product can have many ingredients

    }
}