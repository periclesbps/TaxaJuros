using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TaxaJuros.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaxaJurosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> TaxaJuros()
        {
            var result = new Taxa { Value = 0.01M };

            return JsonConvert.SerializeObject(result);
        }
    }

    public class Taxa
    {
        public decimal Value { get; set; }
    }
}