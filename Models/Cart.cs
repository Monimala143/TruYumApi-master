using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TruYumAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("FoodItem")]
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual MenuItem FoodItem { get; set; }


    }
}
