using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Middlewares
{
    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizeMiddleware>();
        }
    }
}
