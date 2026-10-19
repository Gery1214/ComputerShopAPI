using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComputerShopAPI.Models.DTOs
{
    public class AddNewComputerDTO
    {
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public float Display { get; set; }
        public int Memory { get; set; }
        public Guid OsId { get; set; }
    }
}
