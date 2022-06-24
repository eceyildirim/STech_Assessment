using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Report.Business.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Report.Business.Base
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
