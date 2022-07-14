using DataUtils;
using System.ComponentModel.DataAnnotations;

namespace Services.Platforms.Data.Models
{
    public class Platform : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}
