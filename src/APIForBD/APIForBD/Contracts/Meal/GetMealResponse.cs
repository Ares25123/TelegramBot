namespace APIForBD.Contracts.Meal
{
    public class GetMealResponse
    {
        public int MealId { get; set; }
        public string MealName { get; set; } = null!;
        public string MealDescription { get; set; } = null!;
        public TimeSpan PreparatonTime { get; set; }
        public double Kkal { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
