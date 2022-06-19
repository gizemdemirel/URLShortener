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
    public class URLCustomController : ControllerBase
    {
       
        private readonly ILogger<URLCustomController> _logger;

        public URLCustomController(ILogger<URLCustomController> logger)
        {
            _logger = logger;
        }
     
    
        public class Errors
        {
            public List<string> ErrorMessages { get; set; } = new List<string>();
        }
        [HttpGet]
        public IEnumerable<URLInfo> Get(string longURL, string customUrl)
        {
            var URLArray = new URLInfo[1];
            if (Uri.IsWellFormedUriString(longURL, UriKind.Absolute))
            {
                string shortURL;
                bool isFound = URLDictionary.URLDic.TryGetValue(longURL, out shortURL);
                var uri = new Uri(longURL);
                var baseUri = uri.GetLeftPart(System.UriPartial.Authority);
                if (!isFound)
                {
                    shortURL = baseUri + "/" + customUrl;
                    URLDictionary.URLDic.Add(longURL, shortURL);
                }
                var URLInfo = new URLInfo { LongURL = longURL, ShortURL = shortURL };
                URLArray[0] = URLInfo;
                return URLArray;
            }
            else
            {
                var URLInfo = new URLInfo { LongURL = string.Empty, ShortURL = string.Empty, Error = "Invalid URL" };
                URLArray[0] = URLInfo;
                return URLArray;
            }



        }
    }
}
