using System;
using System.Collections.Generic;

namespace APIForBD.Models
{
    public partial class Meal
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

        public virtual User User { get; set; } = null!;
    }
}
