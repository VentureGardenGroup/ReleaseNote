using System.Configuration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReleaseNote.Models;
using ReleaseNote.Repositories;
using ReleaseNote.Repositories.API;
using ReleaseNote.Repositories.Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReleaseNote.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ReleaseNote.App_Start.NinjectWebCommon), "Stop")]

namespace ReleaseNote.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IOctopusRepositoryReleaseNote>().To<OctopusRepositoryReleaseNote>().Named("octopus")
                .WithConstructorArgument("server", ConfigurationManager.AppSettings.Get("server"))
                .WithConstructorArgument("apiKey", ConfigurationManager.AppSettings.Get("apiKey"));

            kernel.Bind<IJiraRepository>().To<JiraRepository>().Named("Jira")
                .WithConstructorArgument("server", ConfigurationManager.AppSettings.Get("jiraServer"))
                .WithConstructorArgument("username", ConfigurationManager.AppSettings.Get("username"))
                .WithConstructorArgument("password", ConfigurationManager.AppSettings.Get("password"));

            // Bind Entity Framework repository and DbContext
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind(typeof(DbContext)).To<ApplicationDbContext>().InRequestScope(); ;

        }        
    }
}
