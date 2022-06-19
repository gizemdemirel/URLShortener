using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLRedirectorController : ControllerBase
    {
       
        private readonly ILogger<URLRedirectorController> _logger;

        public URLRedirectorController(ILogger<URLRedirectorController> logger)
        {
            _logger = logger;
        }
     
    
        public class Errors
        {
            public List<string> ErrorMessages { get; set; } = new List<string>();
        }
        [HttpGet]
        public IEnumerable<URLInfo> Get(string shortURL)
        {
            var URLArray = new URLInfo[1];
            var urlKey = URLDictionary.URLDic.FirstOrDefault(x => x.Value == shortURL).Key;
            if (urlKey != null)
            {
                var URLInfo = new URLInfo { LongURL = urlKey, ShortURL = shortURL };
                URLArray[0] = URLInfo;
                return URLArray;
            }
            else
            {
                var URLInfo = new URLInfo { LongURL = string.Empty, ShortURL = string.Empty, Error = "URL Not Found" };
                URLArray[0] = URLInfo;
                return URLArray;
            }

        }
    }
}
