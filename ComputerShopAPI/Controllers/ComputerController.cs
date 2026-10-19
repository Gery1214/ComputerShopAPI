using ComputerShopAPI.Models;
using ComputerShopAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ComputerShopAPI.Controllers
{
    [Route("computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputerShopDBContext _context = new();


        [HttpGet("GetAllComputers")]
        public object GetAllComputers()
        {
            var computer = _context.Computers.ToList();
            Exception exception = new();

            if (computer.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }
            else if (computer.Count < 1)
            {
                return StatusCode(200, new { message = "Üres tábla", result = computer });
            }
            else
            {
                return StatusCode(400, new { message = "Hibás lekérdezés", result = exception.Message });
            }
        }

        [HttpPost("AddNewComputer")]

        public object AddNewComputer([FromBody] AddNewComputerDTO addNewComputerDTO)
        {
            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = addNewComputerDTO.Brand,
                Type = addNewComputerDTO.Type,
                Display = addNewComputerDTO.Display,
                Memory = addNewComputerDTO.Memory,
                CreatedTime = DateTime.Now,
                OsId = addNewComputerDTO.OsId,
            };


            if (computer != null)
            {
                _context.Computers.Add(computer);
                _context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres felvétel", result = computer });
            }
            return StatusCode(400, new { message = "Sikertelen felvétel", result = computer });
        }

        [HttpPut("UpdateComputer")]
        public object UpdateComputer([FromQuery] Guid id, [FromQuery] UpdateComputerDTO updateComputerDTO)
        {
            var computer = _context.Computers.FirstOrDefault(computer => computer.Id == id);
            Exception exception = new();
            if (computer != null)
            {
                computer.Brand = updateComputerDTO.Brand;
                computer.Display = updateComputerDTO.Display;
                computer.Type = updateComputerDTO.Type;
                computer.Memory = updateComputerDTO.Memory;
                computer.OsId = updateComputerDTO.OsId;

                _context.Computers.Update(computer);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = computer });
            }
            else if (computer == null)
            {
                return StatusCode(404, new { message = "Nincs ilyen ID-vel osystem", result = computer });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a frissítésben", result = computer });
            }
        }
        [HttpDelete("deleteComputerById")]

        public object DeleteComputer([FromQuery] Guid id)
        {
            var computer = _context.Computers.SingleOrDefault(computer => computer.Id == id);
            Exception exception = new();
            if (computer != null)
            {

                _context.Computers.Remove(computer);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres törlés", result = computer });
            }
            else if (computer == null)
            {
                return StatusCode(404, new { message = "Nincs ilyen ID-vel Computer", result = computer });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a frissítésben", result = computer });
            }
        }

        [HttpGet("GetComputerById")]
        public object GetComputerById([FromQuery] Guid id)
        {
            var computer = _context.Computers.SingleOrDefault(computer => computer.Id == id);
            Exception exception = new();

            if (computer != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }
            else
            {
                return StatusCode(400, new { message = "Hibás lekérdezés", result = exception.Message });
            }
        }

        [HttpGet("GetAllComputerWithOsystem")]
        public object GetAllComputerWithOsystem()
        {
            var computers = _context.Computers.Join(_context.Osystems, computer => computer.OsId, osystem => osystem.Id,(computer, osystem) => new { Márka = computer.Brand, Típus = computer.Type, OsNév = osystem.Name, OsVerzió = osystem.version});

            return StatusCode(200, new { message = "Sikeres lekérdezés", result = computers });
        }
    }
    
}
