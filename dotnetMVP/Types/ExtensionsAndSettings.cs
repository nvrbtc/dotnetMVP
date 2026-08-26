using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using System.IdentityModel.Tokens.Jwt;

namespace dotnetMVP.Types
{
    //later in development
    public static class AppExtensions
    {
        public static void ConfigureSerilog(this IHostBuilder builder)
        {
            builder.UseSerilog((context, config) =>
            {
                config.Enrich.FromLogContext()
                        .WriteTo.Console(
                            outputTemplate:
                                "[{Timestamp:HH:mm:ss} {Level:u3}] " +
                                "{Message:lj} " +
                                "{Properties:j}{NewLine}{Exception}")
                        .WriteTo.GrafanaLoki("http://loki:3100"); 
            });
        }
        public static IActionResult ResultToHttpCode<T>(this ControllerBase controller, 
                                                            ServiceResult<T> result)
        {
            return result.OpResult switch
            {
                OperationResult.Success => controller.Ok(result.Data),
                OperationResult.AccessDenied => controller.Unauthorized(result.Message),
                OperationResult.ObjectNotFound => controller.NotFound(result.Message),
                OperationResult.FailedAuth => controller.Unauthorized(result.Message),
                OperationResult.Forbidden => controller.Forbid(result.Message), // change enum name or delete later
                OperationResult.ValidationFail => controller.ValidationProblem(result.Message),
                OperationResult.CreationFailed => controller.BadRequest(result.Message),
                _ => controller.BadRequest("Went wrong.")
            };
        }
        public static Guid GetUserIdFromClaims(this ControllerBase controller)
        {
            var userIdClaim = controller.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return default;
        }
    }
}
