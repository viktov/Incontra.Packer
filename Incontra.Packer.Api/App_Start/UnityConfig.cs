using Incontra.Packer.Data.Repository;
using Incontra.Packer.Data.Service;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Incontra.Packer.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<IPackRequestRepository, PackRequestRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ILicenseRepository, LicenseRepository>();
            container.RegisterType<IPackerService, PackerService>();
            container.RegisterType<IUserService, UserService>();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}