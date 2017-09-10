using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trading.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var readmeUri = "https://raw.githubusercontent.com/rnataoliveira/test-web-developer/master/README.md";

            //fazer uma request
            var client = new HttpClient();
            var content = await client.GetStringAsync(new Uri(readmeUri));
            var result = CommonMark.CommonMarkConverter.Convert(content);
            
            //utlizando um http client

            //retrnaro conteudo da request para a view

            return View(new HtmlString(result));
        }
    }
}
