using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class Diet
    {
        public int DietId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public float TotalCalories { get; set; }
        public float TotalProteins { get; set; }
        public float TotalCarbohydrates { get; set; }
        public float TotalFats { get; set; }
    }
}
