namespace SalesService.DTOs
{
    public class SaleDto
    {
        public Guid CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }
}
