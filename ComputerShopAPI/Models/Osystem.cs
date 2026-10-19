using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPI.Models
{
    public class Osystem
    {
        [Key]
        [MaxLength(36)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int version { get; set; }
        public DateTime registerTime { get; set; }
        public DateTime updateTime { get; set; }
        public ICollection<Computer> Computers { get; set; }
    }
}
