using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using WebApplication14.ResultCode;
using WebApplication14.Service;

namespace WebApplication14.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index(IFormFile file)
        {
            var data = MinioCore<ResultObject>.UpdateFile("ABDSasda.jpg");
            if (data.Result.Code == 1)
            {
                return Ok(data.Result.Message + ", " + data.Result.Url);
            }
            else
            {
                return Ok(data.Result.Message);
            }
        }
    }
}
