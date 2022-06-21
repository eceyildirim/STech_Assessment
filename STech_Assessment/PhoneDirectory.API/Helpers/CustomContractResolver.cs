using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Helpers
{
    public class CustomContractResolver : IContractResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContractResolver _camelCase;
        private readonly IContractResolver _default;

        public CustomContractResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _camelCase = new CamelCasePropertyNamesContractResolver();
            _default = new DefaultContractResolver();
        }

        public JsonContract ResolveContract(Type type)
        {
            var resolverName = _httpContextAccessor.HttpContext.Request.Headers["Json-Naming"].ToString();

            if (resolverName != null && resolverName == "camel_case")
                return _camelCase.ResolveContract(type);

            return _default.ResolveContract(type);
        }
    }
}
