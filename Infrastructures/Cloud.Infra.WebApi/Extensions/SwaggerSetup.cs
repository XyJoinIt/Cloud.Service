namespace Cloud.Infra.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerSetup
    {
        /// <summary>
        /// swagger注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
                var file = Path.Combine(AppContext.BaseDirectory, $"{builder.Environment.ApplicationName}.xml");
                var path = Path.Combine(AppContext.BaseDirectory, file);
                options.IncludeXmlComments(path, true);
                options.OrderActionsBy(x => x.RelativePath);

            });
            return services;

        }
    }

    internal class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (!authAttributes!.Any()) return;
            var securityRequirement = new OpenApiSecurityRequirement()
        {
            {
                // Put here you own security scheme, this one is an example
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type =  ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                },
                 new[] { "readAccess", "writeAccess" }
            }
        };
            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }

    public interface IServiceInfo
    {
        /// <summary>
        /// xxx-xxx-webapi-188933
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// xxx-xxx-webapi
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// corsPolicy
        /// </summary>
        public string CorsPolicy { get; set; }

        /// <summary>
        ///  usr or maint or cus or xxx
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// 0.9.2.xx
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// assembly  of start's project
        /// </summary>
        public Assembly StartAssembly { get; }
    }
}
