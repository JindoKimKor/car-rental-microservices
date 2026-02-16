using Inventory.Domain.Exceptions;

namespace JK_Inventory.WebAPI.Middleware
{
	public class JK_ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public JK_ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (KeyNotFoundException ex)
			{
				context.Response.StatusCode = 404;
				await context.Response.WriteAsJsonAsync(new { error = ex.Message });
			}
			catch (InvalidVehicleStateException ex)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsJsonAsync(new { error = ex.Message });
			}
			catch (ArgumentException ex)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsJsonAsync(new { error = ex.Message });
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsJsonAsync(new { error = ex.Message });
			}
		}
	}
}
