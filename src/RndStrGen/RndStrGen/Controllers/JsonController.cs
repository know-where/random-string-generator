using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RndStrGen.Interfaces;

namespace RndStrGen.Controllers {

    [Route("api/json")]
    [Produces("application/json")]
    [ApiController]
    public class JsonController : ControllerBase {

        private readonly IGeneratorService Generator;

        public JsonController(IGeneratorService generator) {
            Generator = generator;
        }

        [HttpGet("guid")]
        public IActionResult GetGuid([FromQuery] int len = 1, [FromQuery] int count = 1) {
            List<string> returnList = new List<string>();
            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetGuid(len));
            }

            return Ok(returnList);
        }

        [HttpGet("string")]
        public IActionResult GetString([FromQuery] int len = 1, [FromQuery] int count = 1, [FromQuery] bool symbols = true, [FromQuery] bool numbers = true, [FromQuery] bool letters = true) {
            List<string> returnList = new List<string>();

            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetString(len, numbers, letters, symbols));
            }

            return Ok(returnList);
        }

    }
}
