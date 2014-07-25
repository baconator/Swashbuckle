using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Swashbuckle.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseSwashbuckleDocumentation(this IAppBuilder app, Action<SwaggerSpecConfig> configurator)
        {
            var config = new HttpConfiguration();
            app.UseWebApi(config);
            Bootstrapper.InitSpec(config);
            SwaggerSpecConfig.Customize(configurator);
        }

        public static void UseSwaggerUi(this IAppBuilder app)
        {
            var config = new HttpConfiguration();
            Bootstrapper.InitUi(config);

            // TODO: finishme
            //app.UseStaticFiles
        }

        public static void ExportApiDescriptions(this IAppBuilder app, HttpConfiguration config)
        {
            ExportApiDescriptions(app, config.Services.GetApiExplorer().ApiDescriptions);
        }

        public static void ExportApiDescriptions(this IAppBuilder app, IEnumerable<ApiDescription> descriptions)
        {
            app.Use(async (context, next) => {
                var owinKey = SwaggerSpecConfig.StaticInstance.OwinDescriptionsKey;

                var existing = context.Get<ApiDescription[]>(owinKey) ?? new ApiDescription[0];
                var concatenatedDescriptions = descriptions.Concat(existing);

                context.Set(owinKey, concatenatedDescriptions.ToArray());

                await next();
            });
        }
    }
}
