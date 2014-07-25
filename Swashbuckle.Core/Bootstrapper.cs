using System.Web.Http;
using Swashbuckle.Application;

namespace Swashbuckle
{
    public static class Bootstrapper
    {
        public static void Init(HttpConfiguration config)
        {
            InitSpec(config);
            InitUi(config);
        }

        internal static void InitSpec(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "swagger_api_docs",
                "swagger/api-docs/{resourceName}",
                new { resourceName = RouteParameter.Optional },
                null,
                new SwaggerSpecHandler());
        }

        internal static void InitUi(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                    "swagger_root",
                    "swagger",
                    null,
                    null,
                    new RedirectHandler("swagger/ui/index.html"));

            config.Routes.MapHttpRoute(
                "swagger_ui",
                "swagger/ui/{*uiPath}",
                null,
                new { uiPath = @".+" },
                new SwaggerUiHandler());
        }
    }
}