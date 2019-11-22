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
            return JsonConvert.SerializeObject("1%");
        }
    }
}