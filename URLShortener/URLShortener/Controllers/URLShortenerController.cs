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
    public class URLShortenerController : ControllerBase
    {
        
        private readonly ILogger<URLShortenerController> _logger;

        public URLShortenerController(ILogger<URLShortenerController> logger)
        {
            _logger = logger;
        }
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public class Errors
        {
            public List<string> ErrorMessages { get; set; } = new List<string>();
        }
        [HttpGet]
        public IEnumerable<URLInfo> Get(string longURL)
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
                    shortURL = RandomString(6);
                    URLDictionary.URLDic.Add(longURL, baseUri + "/" + shortURL);
                }
                var URLInfo = new URLInfo { LongURL = longURL, ShortURL = baseUri + "/" + shortURL };
                URLArray[0] = URLInfo;
                return URLArray;
            }
            else
            {
                var URLInfo = new URLInfo { LongURL = string.Empty, ShortURL = string.Empty, Error ="Invalid URL" };
                URLArray[0] = URLInfo;
                return URLArray;
            }
        

        }
    }
}
