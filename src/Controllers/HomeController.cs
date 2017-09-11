﻿using System;
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
    [Route("",Name = "Home")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var readmeUri = "https://raw.githubusercontent.com/rnataoliveira/test-web-developer/master/README.md";

            //faz uma request usando Httpcliente, converte um arquivo markdown em um html
            var client = new HttpClient();
            var content = await client.GetStringAsync(new Uri(readmeUri));
            var result = CommonMark.CommonMarkConverter.Convert(content);
           
            //retorna o conteúdo do request para a view
            return View(new HtmlString(result));
        }
    }
}
