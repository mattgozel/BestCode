using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlooringMasteryRefactored.UI.Startup))]
namespace FlooringMasteryRefactored.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
