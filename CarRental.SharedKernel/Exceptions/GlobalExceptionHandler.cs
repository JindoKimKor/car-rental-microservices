using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.SharedKernel.Exceptions
{
	public sealed class GlobalExceptionHandler : IExceptionHandler
	{
		private readonly ILogger<GlobalExceptionHandler> _logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
			=> _logger = logger;

		public async ValueTask<bool> TryHandleAsync(
			HttpContext context,
			Exception exception,
			CancellationToken cancellationToken)
		{
			var status = exception switch
			{
				ArgumentException => StatusCodes.Status400BadRequest,
				KeyNotFoundException => StatusCodes.Status404NotFound,
				UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
				_ => StatusCodes.Status500InternalServerError
			};

			_logger.LogError(exception,
				"Unhandled exception mapped to {Status} at {Method} {Path}",
				status, context.Request.Method, context.Request.Path);

			var problem = new ProblemDetails
			{
				Status = status,
				Title = status == 500 ? "An unexpected error occurred." : exception.Message,
				Type = "about:blank",
				Instance = context.Request.Path
			};

			context.Response.StatusCode = status;
			await context.Response.WriteAsJsonAsync(problem, options: null, contentType: "application/problem+json", cancellationToken);
			return true;
		}
	}
}
