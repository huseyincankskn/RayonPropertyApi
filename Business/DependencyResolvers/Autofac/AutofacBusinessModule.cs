using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Abstract.Project;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Helpers;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Repositories;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<BlogService>().As<IBlogService>();
            builder.RegisterType<SitePropertyService>().As<ISitePropertyService>();

            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<DistrictRepository>().As<IDistrictRepository>();
            builder.RegisterType<TownRepository>().As<ITownRepository>();
            builder.RegisterType<BlogRepository>().As<IBlogRepository>();
            builder.RegisterType<CurrencyRepository>().As<ICurrencyRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<SitePropertyRepository>().As<ISitePropertyRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<BlogRepository>().As<IBlogRepository>();

            builder.RegisterType<HttpAccessorHelper>().As<IHttpAccessorHelper>();

            var assemblyBusiness = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyDataAccess = System.Reflection.Assembly.Load("DataAccess");

            builder.RegisterAssemblyTypes(assemblyBusiness, assemblyDataAccess).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                });
        }
    }
}