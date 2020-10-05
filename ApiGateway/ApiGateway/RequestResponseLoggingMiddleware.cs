using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace ApiGateway
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILoggerService _logger;
        private readonly RequestDelegate _next;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var builder = new StringBuilder();
            var request = await FormatRequest(context.Request);
            builder.Append("Request  ==> ").AppendLine(request);
            
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            
            await _next(context);
            
            var response = await FormatResponse(context.Response);
            builder.Append("Response ==> ").AppendLine(response);
            
            _logger.Information(builder.ToString());
            
            await responseBody.CopyToAsync(originalBodyStream);
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            using var reader = new StreamReader(request.Body, Encoding.UTF8, false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            
            var formattedRequest = $"IP:{request.HttpContext.Connection.RemoteIpAddress} | Method: {request.Method} | {request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
            
            request.Body.Position = 0;
            return formattedRequest;
        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}