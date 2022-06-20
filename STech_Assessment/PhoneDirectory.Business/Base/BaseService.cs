using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PhoneDirectory.Business.Interfaces;

namespace PhoneDirectory.Business.Base
{
    public abstract class BaseService<T>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IAppSettings _appSettings;
        private ILogger<T> _logger;
        private IMapper _mapper;

        protected BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected HttpContext HttpContext => _httpContextAccessor.HttpContext;

        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());

        protected IAppSettings AppSettings => _appSettings ?? (_appSettings = HttpContext.RequestServices.GetService<IAppSettings>());
        protected IMapper Mapper => _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>());

    }
}
