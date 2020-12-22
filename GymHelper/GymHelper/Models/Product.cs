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
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
