namespace APIForBD.Contracts.Category
{
    public class CreateCategoryRequest
    {
        public string CategoryName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
