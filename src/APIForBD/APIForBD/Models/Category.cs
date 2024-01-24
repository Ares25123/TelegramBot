using System;
using System.Collections.Generic;

namespace APIForBD.Models
{
    public partial class Category
    {
        public Category()
        {
            Users = new HashSet<User>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
