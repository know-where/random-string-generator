using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RndStrGen.Interfaces;

namespace RndStrGen.Controllers {

    [Route("")]
    [Produces("application/json")]
    [ApiController]
    public class JsonController : ControllerBase {

        private readonly IGeneratorService Generator;
        private readonly IIPService IP;

        public JsonController(IGeneratorService generator, IIPService ip) {
            Generator = generator;
            IP = ip;
        }

        [HttpGet("random/guid")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetGuid([FromQuery] int len = 1, [FromQuery] int count = 1) {
            List<string> returnList = new List<string>();
            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetGuid(len));
            }

            return Ok(returnList);
        }

        [HttpGet("random/string")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetString([FromQuery] int length = 12, [FromQuery] int count = 1,
            [FromQuery] bool lowercase = true, [FromQuery] bool uppercase = true,
            [FromQuery] bool numbers = true, [FromQuery] bool symbols = true) {

            List<string> returnList = new List<string>();

            if (length < 1 || length > 256) {
                return BadRequest(new { error = "Incorrect length specified (1 < length <= 256)" });
            }

            if (count < 1 || count > 256) {
                return BadRequest(new { error = "Incorrect count specified (1 < count <= 256)" });
            }

            if (!lowercase && !uppercase && !numbers && !symbols) {
                return BadRequest(new { error = "Must have at least one type selected" });
            }

            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetString(length, numbers, lowercase, uppercase, symbols));
            }

            return Ok(returnList);
        }

        [HttpGet("ip")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetIP() {
            string ip = string.Empty;

            ip = IP.GetIP(HttpContext);

            if (!string.IsNullOrEmpty(ip)) {
                return Ok(ip);
            } else {
                return new ObjectResult("Cannot return your IP address") {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
