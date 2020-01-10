using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace FeatureManagerTest.Filters
{
    [FilterAlias("BrowserFilter")]
    public class BrowserFeatureFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var useAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var setting = context.Parameters.Get<BrowserFilterSettings>();
            return Task.FromResult(setting.AllowedBrowsers.Any(useAgent.Contains));
        }
    }

    public class BrowserFilterSettings
    {
        public string[] AllowedBrowsers { get; set; }
    }
}