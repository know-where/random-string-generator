﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolsApi.Interfaces;
using System.Collections.Generic;

namespace ToolsApi.Controllers {

    [Route("")]
    [Produces("application/json")]
    [ApiController]
    public class ToolsController : ControllerBase {

        private readonly IGeneratorService Generator;
        private readonly IIPService IP;

        public ToolsController(IGeneratorService generator, IIPService ip) {
            Generator = generator;
            IP = ip;
        }

        [HttpGet("random/guid")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetGuid([FromQuery] int len = 1, [FromQuery] int count = 1) {
            if (len < 1 || len > 256) {
                return BadRequest(new { error = "Incorrect length specified (1 < len <= 256)" });
            }

            if (count < 1 || count > 256) {
                return BadRequest(new { error = "Incorrect count specified (1 < count <= 256)" });
            }

            List<string> returnList = new List<string>();
            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetGuid(len));
            }

            return Ok(returnList);
        }

        [HttpGet("random/string")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetString([FromQuery] int len = 16, [FromQuery] int count = 5,
            [FromQuery] bool lower = true, [FromQuery] bool upper = true,
            [FromQuery] bool num = true, [FromQuery] bool sym = false) {

            List<string> returnList = new List<string>();

            if (len < 1 || len > 256) {
                return BadRequest(new { error = "Incorrect length specified (1 < len <= 256)" });
            }

            if (count < 1 || count > 256) {
                return BadRequest(new { error = "Incorrect count specified (1 < count <= 256)" });
            }

            if (!lower && !upper && !upper && !sym) {
                return BadRequest(new { error = "You must have at least one type selected" });
            }

            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetString(len, num, lower, upper, sym));
            }

            return Ok(returnList);
        }

        [HttpGet("random/sentence")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetSentence([FromQuery] int len = 5, [FromQuery] int count = 5, [FromQuery] string sep = "-", [FromQuery] bool random_casing = true) {
            List<string> returnList = new List<string>();

            if (!string.IsNullOrEmpty(sep) && sep.Length != 1) {
                return BadRequest(new { error = "Incorrect separator length (1)" });
            }

            if (len < 1 || len > 256) {
                return BadRequest(new { error = "Incorrect length specified (1 < len <= 256)" });
            }

            if (count < 1 || count > 256) {
                return BadRequest(new { error = "Incorrect count specified (1 < count <= 256)" });
            }

            for (int i = 0; i < count; i++) {
                returnList.Add(Generator.GetSentence(len, sep, random_casing));
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
