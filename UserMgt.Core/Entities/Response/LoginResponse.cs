using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Core.Entities.Response
{
    public class LoginResponse
    {
        public HttpStatusCode? StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public object? Data { get; set; }
    }
}
