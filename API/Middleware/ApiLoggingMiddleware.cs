using DomainLayer.IRepositorys;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogRepository _logRepo;
        public ApiLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext, ILogRepository LogRepo)
        {
            try
            {

                _logRepo = LogRepo;
                var request = httpContext.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {

                    DateTime RequestDate = DateTime.Now;
                    string Method = request.Method;
                    string Path = request.Path;

                    _logRepo.Add(new DomainLayer.Models.Log(RequestDate, Method, Path));
                    await _next(httpContext);
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                await _next(httpContext);
            }
        }
    }
}
