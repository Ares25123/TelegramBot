namespace APIForBD.Contracts.Product
{
    public class GetProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string Season { get; set; } = null!;
        public double Price { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
