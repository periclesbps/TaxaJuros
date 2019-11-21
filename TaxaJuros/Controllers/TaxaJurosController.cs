using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Dynamic;

namespace TaxaJuros.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaxaJurosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> TaxaJuros()
        {
            return JsonConvert.SerializeObject("1%");
        }
    }
}