namespace Resturant.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; } // foreign key to Product
        public Product Product { get; set; } // navigation property to Product
        public int IngredientId { get; set; } // foreign key to Ingredient
        public Ingredient Ingredient { get; set; } 
    }
}