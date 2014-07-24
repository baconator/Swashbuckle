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
        public static void UseSwashbuckleDocumentation(this IAppBuilder app, string owinEnvironmentKey)
        {
            var config = new HttpConfiguration();
            app.UseWebApi(config);
            SwaggerSpecConfig.Customize(c => {
                c.PreferOwinDescriptions(owinEnvironmentKey);
            });
            Bootstrapper.Init(config, false);
        }

        public static void ExportApiDescriptions(this IAppBuilder app, HttpConfiguration config)
        {
            app.Use(async (context, next) => {
                var owinKey = SwaggerSpecConfig.StaticInstance.OwinDescriptionsKey;

                var existing = context.Get<ApiDescription[]>(owinKey) ?? new ApiDescription[0];
                var descriptions = config.Services.GetApiExplorer().ApiDescriptions.Concat(existing);

                context.Set(owinKey, descriptions.ToArray());

                await next();
            });
        }
    }
}
