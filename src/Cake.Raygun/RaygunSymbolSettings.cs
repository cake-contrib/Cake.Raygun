using Cake.Core.IO.Arguments;
using Cake.Core.Tooling;

namespace Cake.Raygun
{
    /// <summary>
    ///     Contains common settings used by Raygun />.
    /// </summary>
    public class RaygunSymbolSettings : ToolSettings
    {
        public string ApplicationIdentifier { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}