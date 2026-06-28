using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Globomantics.Api;

public static class SwaggerApiKeySecurity
{
    public static void AddSwaggerApiKeySecurity(this SwaggerGenOptions c)
    {
        c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "ApiKey must appear in header",
            Type = SecuritySchemeType.ApiKey,
            Name = "XApiKey",
            In = ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });
        c.AddSecurityRequirement(document => 
            new OpenApiSecurityRequirement
            { 
                [new OpenApiSecuritySchemeReference("ApiKey", document)]
                   = []
            });
    }
}
