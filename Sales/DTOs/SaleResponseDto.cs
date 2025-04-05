namespace SalesService.DTOs
{
    public class SaleResponseDto
    {
        public Guid SaleId { get; set; }
        public string CustomerName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDetailDto> Items { get; set; } = new List<SaleItemDetailDto>();
    }

    public class SaleItemDetailDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
