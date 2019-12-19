using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Fintrak.Client.Core.Bootstrapper;
using Fintrak.Presentation.WebClient.Core;
using systemCore = Fintrak.Client.SystemCore.Bootstrapper;
using core = Fintrak.Client.Core.Bootstrapper;
using ifrs = Fintrak.Client.IFRS.Bootstrapper;
//using mpr = Fintrak.Client.MPR.Bootstrapper;
//using cdqm = Fintrak.Client.CDQM.Bootstrapper;
//using scd = Fintrak.Client.Scorecard.Bootstrapper;
//using budget = Fintrak.Client.Budget.Bootstrapper;
using Fintrak.Presentation.WebClient.Logging;
using System.Web.Script.Serialization;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Security;

namespace Fintrak.Presentation.WebClient
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Setup our custom controller factory so that the [HandleErrorWithElmah] attribute
            // is automatically injected into all of the controllers
            ControllerBuilder.Current.SetControllerFactory(new ErrorHandlingControllerFactory());

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            catalog.Catalogs.Add(systemCore.MEFLoader.Init().Catalog);
            catalog.Catalogs.Add(ifrs.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(mpr.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(cdqm.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(scd.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(budget.MEFLoader.Init().Catalog);
         
            CompositionContainer container = core.MEFLoader.Init(catalog.Catalogs);

            DependencyResolver.SetResolver(new MefDependencyResolver(container)); // view controllers
            GlobalConfiguration.Configuration.DependencyResolver = new MefAPIDependencyResolver(container); // web api controllers     
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                AccountLoginModel serializeModel = serializer.Deserialize<AccountLoginModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.CompanyCode = serializeModel.CompanyCode;
                newUser.FirstName = serializeModel.FirstName;
                newUser.LastName = serializeModel.LastName;

                HttpContext.Current.User = newUser;
            }
        }
    }
}