using RestaurantManager.Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RestaurantManager.WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RestaurantManager.WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace RestaurantManager.WebApp.App_Start
{
    using System;
    using System.Web;
    using ViewAndBindingModels.Mapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using RestaurantManager.DataBase;
    using Services.Implementations;
    using Services.Interfaces;

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
            kernel.Bind<IRestaurantManagerDbContext>().To<RestaurantManagerDbContext>();
            kernel.Bind<IRestaurantManagerData>().To<RestaurantManagerData>();
            kernel.Bind<IMappingService>().To<MappingService>();
            
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IMenuService>().To<MenuService>();
        }
    }
}
