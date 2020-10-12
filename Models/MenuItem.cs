using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruYumAPI.Models
{
    public class MenuItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string FoodName { get; set; }

        public int Price { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string Category { get; set; }

        public bool IsFreeDelivery { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime LaunchDate { get; set; }

        [Required]
        public string ImageURL { get; set; }

    }
}
