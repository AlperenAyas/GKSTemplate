using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GKS.WebApi.Middlewares
{
    public static class ValidationExceptionMiddleware
    {
        public static void UseCatchError(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if(!(exception is ValidationException validationException))
                    {
                        throw exception;
                    }

                    Dictionary<string, string> errors = new Dictionary<string, string>();

                    foreach (var error in validationException.Errors)
                    {
                        errors.Add(error.PropertyName, error.ErrorMessage);
                    }

                    var errorText = JsonConvert.SerializeObject(errors);
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    if (context.Request.ContentType == null && context.Request.ContentType== "application/json")
                    {
                        context.Response.ContentType = "application/json";
                        errorText = JsonConvert.SerializeObject(errors);
                        await context.Response.WriteAsync(errorText, Encoding.UTF8);
                    }
                    else
                    {
                        context.Response.ContentType = "application/xml";
                        var myXml = JsonConvert.DeserializeXNode(errorText.ToString(), "root");
                        await context.Response.WriteAsync(myXml.ToString(), Encoding.UTF8);
                    }
                    
                });
            });
        }
    }
}
