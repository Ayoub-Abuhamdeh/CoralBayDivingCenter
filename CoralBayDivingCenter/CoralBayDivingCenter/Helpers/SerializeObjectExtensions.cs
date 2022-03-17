
using CoralBayDivingCenter.Utils;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SerializeObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings.Settings());
        }
    }
}
