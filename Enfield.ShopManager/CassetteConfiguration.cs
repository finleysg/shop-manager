using Cassette.Configuration;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Enfield.ShopManager
{
    /// <summary>
    /// Configures the Cassette asset modules for the web application.
    /// </summary>
    public class CassetteConfiguration : ICassetteConfiguration
    {
        public void Configure(BundleCollection bundles, CassetteSettings settings)
        {
            bundles.AddPerSubDirectory<StylesheetBundle>("Content", excludeTopLevel: true);
            bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
        }
    }
}