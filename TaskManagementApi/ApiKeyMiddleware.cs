using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
	private readonly RequestDelegate _next;
	private readonly string _apiKey;

	public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		_next = next;
		_apiKey = configuration["ApiKey"];
	}

	public async Task InvokeAsync(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
		{
			context.Response.StatusCode = 401; // Desautorizado
			await context.Response.WriteAsync("API Key no proporcionada.");
			return;
		}

		if (!string.Equals(extractedApiKey, _apiKey))
		{
			context.Response.StatusCode = 401; // Desautorizado
			await context.Response.WriteAsync("API Key no válida.");
			return;
		}

		await _next(context);
	}
}
