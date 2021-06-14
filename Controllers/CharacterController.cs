using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character{Id = 1, Name= "Sam", HitPoints=40},
            new Character{Id = 2, Name= "Mosh", HitPoints=50}
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }
    }
}