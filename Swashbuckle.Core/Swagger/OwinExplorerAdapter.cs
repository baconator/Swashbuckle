using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
    public class OwinExplorerAdapter : ApiExplorerAdapter
    {
        private ApiDescription[] Descriptions;

        public OwinExplorerAdapter(
            ApiDescription[] descriptions,
            bool ignoreObsoleteActions,
            Func<ApiDescription, string, bool> resoveVersionSupport,
            Func<ApiDescription, string> resolveResourceName,
            Dictionary<Type, Func<DataType>> customTypeMappings,
            IEnumerable<PolymorphicType> polymorphicTypes,
            IEnumerable<IModelFilter> modelFilters,
            IEnumerable<IOperationFilter> operationFilters)
            : base(
                null,
                ignoreObsoleteActions,
                resoveVersionSupport,
                resolveResourceName,
                customTypeMappings,
                polymorphicTypes,
                modelFilters,
                operationFilters)
        {
            Descriptions = descriptions ?? new ApiDescription[0];
        }

        protected override IEnumerable<IGrouping<string, ApiDescription>> GetApplicableApiDescriptions(string version)
        {
            return Descriptions.Where(apiDesc => !_ignoreObsoleteActions || !apiDesc.IsMarkedObsolete())
                .Where(apiDesc => _versionSupportResolver(apiDesc, version))
                .GroupBy(apiDesc => _resourceNameResolver(apiDesc))
                .OrderBy(group => group.Key)
                .ToArray();
        }
    }
}
