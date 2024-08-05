using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bext.Reps.Api.Swagger.Filters;

public class FileOperationFilter : IOperationFilter
{
    private const string MultipartFormData = "multipart/form-data";
    private const string FilesKey = "files";


    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (IsMultipartRequest(context))
        {
            operation.RequestBody = CreateMultipartRequestBody();
        }
    }

    private bool IsMultipartRequest(OperationFilterContext context)
    {
        return context.ApiDescription.ActionDescriptor.ActionConstraints?
            .OfType<ConsumesAttribute>()
            .Any(consumesAttribute =>
                consumesAttribute.ContentTypes.Any(contentType => contentType == MultipartFormData)) ?? false;
    }

    private OpenApiRequestBody CreateMultipartRequestBody()
    {
        return new OpenApiRequestBody
        {
            Content =
            {
                [MultipartFormData] = new OpenApiMediaType
                {
                    Encoding =
                    {
                        [FilesKey] = new OpenApiEncoding
                        {
                            Style = ParameterStyle.Form
                        }
                    },
                    Schema = CreateMultipartSchema()
                }
            }
        };
    }

    private OpenApiSchema CreateMultipartSchema()
    {
        return new OpenApiSchema
        {
            Type = "object",
            Properties =
            {
                [FilesKey] = new OpenApiSchema
                {
                    Description = "Select file",
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    }
                }
            }
        };
    }
}
