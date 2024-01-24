namespace APIForBD.Contracts.Meal
{
    public class CreateMealRequest
    {
        public string MealName { get; set; } = null!;
        public string MealDescription { get; set; } = null!;
        public TimeSpan PreparatonTime { get; set; }
        public double Kkal { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
