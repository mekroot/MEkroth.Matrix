using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.InteropServices;

namespace MEkroth.Matrix.Api.Shared.Extensions
{
    public static class MinimalDocumentationExtensions
    {
        public static RouteHandlerBuilder AddApiDocumentation(this RouteHandlerBuilder endpoint, string tag, [Optional] string summary, [Optional] string description, [Optional] IDictionary<int, string> responses)
        {
            endpoint.WithTags(tag);

            endpoint.WithMetadata(new SwaggerOperationAttribute(summary: summary, description: description));

            if (responses != null && responses.Count > 0)
            {
                foreach (var response in responses)
                {
                    endpoint.WithMetadata(new SwaggerResponseAttribute(response.Key, response.Value));
                }
            }
            else
            {
                endpoint.WithMetadata(new SwaggerResponseAttribute(200, "OK"));
            }

            return endpoint.WithOpenApi();
        }
        public static RouteHandlerBuilder AddApiDocumentation<T>(this RouteHandlerBuilder endpoint, string tag, [Optional] string summary, [Optional] string description, [Optional] IDictionary<int, string> responses)
        {
            endpoint.WithTags(tag);
            endpoint.WithMetadata(new SwaggerOperationAttribute(summary: summary, description: description));

            endpoint.WithMetadata(new SwaggerResponseAttribute(200, type: typeof(T)));

            if (responses != null && responses.Count > 0)
            {
                foreach (var response in responses)
                {
                    endpoint.WithMetadata(new SwaggerResponseAttribute(response.Key, response.Value));
                }
            }

            return endpoint.WithOpenApi();
        }
    }
}
