using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using savvy.Data;

[assembly: OwinStartup(typeof(savvy.Web.Startup))]
namespace savvy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API configuration and services

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ViewQuiz",
                routeTemplate: "api/view/quizzes/{id}",
                defaults: new { controller = "ViewQuiz", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EditQuiz",
                routeTemplate: "api/quizzes/{id}",
                defaults: new { controller = "EditQuiz", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ViewQuestion",
                routeTemplate: "api/view/quizzes/{quizId}/questions/{questionId}",
                defaults: new { controller = "ViewQuestion", questionId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EditQuestion",
                routeTemplate: "api/quizzes/{quizId}/questions/{questionId}",
                defaults: new { controller = "EditQuestion", questionId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseNinjectMiddleware(CreateKernel);
            app.UseNinjectWebApi(config);

            ConfigureAuth(app);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ISavvyRepository>().To<SavvyRepository>().InRequestScope();
            kernel.Bind<SavvyContext>().To<SavvyContext>().InRequestScope();
        }
    }
}
