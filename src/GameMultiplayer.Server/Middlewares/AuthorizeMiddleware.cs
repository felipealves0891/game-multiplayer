using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Middlewares
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string _token;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
            _token = Guid.NewGuid().ToString();
            Console.WriteLine("Token de acesso: {0}", _token);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Informe suas credencias");
                return;
            }

            if (!token.Equals($"Bearer {_token}"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Acesso não autorizado");
                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

    }
}
