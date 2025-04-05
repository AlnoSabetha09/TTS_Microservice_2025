namespace SalesService.Entities
{
    public class SaleItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
