namespace APIForBD.Contracts.Product
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string Season { get; set; } = null!;
        public double Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
