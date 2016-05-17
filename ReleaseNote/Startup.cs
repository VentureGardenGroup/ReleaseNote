using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReleaseNote.Startup))]
namespace ReleaseNote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
