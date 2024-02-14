using Microsoft.AspNetCore.Http;

namespace Core.Helpers
{
    public static class RequestExtensions
    {
        public static string BaseUrl(this HttpContext httpContext)
        {
            return "https"+$"://vusala-001-site1.gtempurl.com{httpContext.Request.PathBase}";
        }
    }
}
