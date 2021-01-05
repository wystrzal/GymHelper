using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Grams { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Carbohydrates { get; set; }
        public float Fats { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
