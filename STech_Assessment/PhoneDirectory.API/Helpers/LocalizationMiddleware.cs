using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Helpers
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = new List<string> { "tr", "en" };

            var language = context.Request.Headers["Accept-Language"].FirstOrDefault();

            if (!supportedLanguages.Contains(language))
            {
                language = supportedLanguages[0];
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);

            await _next(context);
        }
    }
}
