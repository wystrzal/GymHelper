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
    }
}
