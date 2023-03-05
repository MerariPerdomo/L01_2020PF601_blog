using System.ComponentModel.DataAnnotations;

namespace L01_2020PF601.Models
{
    public class roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol { get; set; }
    }
}
