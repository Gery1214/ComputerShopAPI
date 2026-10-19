using ComputerShopAPI.Models;
using ComputerShopAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopAPI.Controllers
{
    [Route("osystems")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public ComputerShopDBContext _context = new();

        [HttpPost("AddNewOsystem")]
        public object AddNewOsystem([FromBody]AddNewOsystemDTO addNewOsystemDTO)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = addNewOsystemDTO.Name,
                version = addNewOsystemDTO.Version,
                registerTime = DateTime.Now,
                updateTime = DateTime.Now,
            };


            if (osystem != null) 
            { 
                _context.Osystems.Add(osystem);
                _context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres felvétel", result = osystem });
            }
            return StatusCode(400, new { message = "Sikertelen felvétel", result = osystem });
        }

        [HttpGet("GetAllOsystem")]
        public object GetAllOsystem()
        {
            var osystem = _context.Osystems.ToList();
            Exception exception = new();

            if (osystem.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystem });
            }
            else if (osystem.Count < 1)
            {
                return StatusCode(200, new { message = "Üres tábla", result = osystem });
            }
            else 
            {
                return StatusCode(400, new { message = "Hibás lekérdezés", result = exception.Message });
            }
        }
        [HttpPut("UpdateOsystem")]
        public object UpdateOsystem([FromQuery] Guid id, [FromQuery] UpdateOsystemDTO updateOsystemDTO)
        {
            var osystem = _context.Osystems.FirstOrDefault(osystem => osystem.Id == id);
            Exception exception = new();
            if (osystem != null)
            {
                osystem.Name = updateOsystemDTO.Name;
                osystem.version = updateOsystemDTO.Version;
                osystem.updateTime = DateTime.Now;

                _context.Osystems.Update(osystem);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = osystem });
            }
            else if (osystem == null)
            {
                return StatusCode(404, new { message = "Nincs ilyen ID-vel osystem", result = osystem });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a frissítésben", result = osystem });
            }
        }

        [HttpDelete("deleteOsystemById")]

        public object DeleteOsystem([FromQuery] Guid id)
        {
            var osystem = _context.Osystems.SingleOrDefault(osystem => osystem.Id == id);
            Exception exception = new();
            if (osystem != null)
            {

                _context.Osystems.Remove(osystem);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres törlés", result = osystem });
            }
            else if (osystem == null)
            {
                return StatusCode(404, new { message = "Nincs ilyen ID-vel osystem", result = osystem });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a frissítésben", result = osystem });
            }
        }

        [HttpGet("AllOsystemsWithComputer")]
        public object AllOsystemsWithComputer()
        {
            var osystems = _context.Osystems.Include(osystem => osystem.Computers);

            return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystems });
        }

        [HttpGet("GetOsystemWithComputers")]
        public object GetOsystemWithComputers([FromQuery]Guid id)
        {
            var osystem = _context.Osystems.SingleOrDefault(osystem => osystem.Id == id);

            var computers = _context.Computers.Where(computer => computer.OsId == osystem.Id);

            return StatusCode(200, new { message = "Sikeres lekérdezés", result = new { osystem.Name, computers }});
        }
    }
}
