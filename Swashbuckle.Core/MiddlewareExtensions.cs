using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Swashbuckle.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseSwashbuckleDocumentation(this IAppBuilder app, bool hostUi = true)
        {
            var config = new HttpConfiguration();
            Bootstrapper.Init(config, hostUi);
        }
    }
}
