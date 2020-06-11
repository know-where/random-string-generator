using Microsoft.AspNetCore.Http;
using RndStrGen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RndStrGen.Services {
    public class IPService : IIPService {
        public string GetIP(HttpContext context) {
            return context.Connection.RemoteIpAddress.ToString();
        }
    }
}
