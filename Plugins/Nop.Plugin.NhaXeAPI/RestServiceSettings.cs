using Nop.Core.Configuration;

namespace Nop.Plugin.NhaXeAPI
{
    public class RestServiceSettings : ISettings
    {
        public string ApiToken { get; set; }
    }
}