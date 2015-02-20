using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using savvy.Data;
using savvy.Web.Models.Converters;

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
                routeTemplate: "api/view/quizzes/{quizId}/questions/{sequenceNum}",
                defaults: new { controller = "ViewQuestion", sequenceNum = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EditQuestion",
                routeTemplate: "api/quizzes/{quizId}/questions/{sequenceNum}",
                defaults: new { controller = "EditQuestion", sequenceNum = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Convert JSON property names to camel case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Serialize enums as strings instead of using numeric values
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            // Register converters
            RegisterConverters(config);

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

        private void RegisterConverters(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new QuestionConverter());
        }
    }
}
