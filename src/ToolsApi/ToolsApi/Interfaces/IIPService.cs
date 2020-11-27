using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolsApi.Interfaces {
    public interface IIPService {
        public string GetIP(HttpContext context);
    }
}
