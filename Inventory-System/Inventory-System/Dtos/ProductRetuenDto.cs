namespace Inventory_System.Dtos
{
    public class ProductRetuenDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string ProductBrand { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public decimal Total => Price * Quantity;

    }
}
