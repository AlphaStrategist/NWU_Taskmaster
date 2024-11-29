using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NWU_Taskmaster.Services;

namespace NWU_Taskmaster
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Task.Run(() =>
            {
                var reminderService = new ReminderService();

                while (true)
                {
                    reminderService.CheckAndNotifyReminders();
                    Thread.Sleep(60000);
                }
            });
        }
    }
}
