using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MentorsBlog.Core.Web.Auth
{
    public class AuthOperationAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Distinct();
 
            if (authAttributes.Any())
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();
 
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "JWT access token",
                    Required = true
                });

                operation.Security = new List<OpenApiSecurityRequirement>();
 
                //Add JWT bearer type
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            }
                        },
                        new string[] { }   
                    }
                });
            }
        }
    }
}