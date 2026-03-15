using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CarRental.SharedKernel.Middleware
{
    public class GatewayOnlyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secret;

        public GatewayOnlyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _secret = configuration["GatewaySecret"]!;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Gateway-Secret", out var secret) || secret != _secret)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Forbidden",
                    message = "Direct access not allowed. Use API Gateway."
                });
                return;
            }
            await _next(context);
        }
    }
}
