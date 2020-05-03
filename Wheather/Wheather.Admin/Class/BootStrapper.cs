using Autofac;
using Autofac.Integration.Mvc;
using Wheather.Core.Infrastructure;
using Wheather.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheather.Admin;

namespace Eblog.Admin.Class
{
    public class BootStrapper
    {
        // Boot aşamasında çalışacak

        public static void RunConfig()
        {
            BuildAutoFac();
        }

        private static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<KullaniciRepository>().As<IKullaniciRepository>();
            builder.RegisterType<YetkiRepository>().As<IYetkiRepository>();
            builder.RegisterType<UygulamaRepository>().As<IUygulamaRepository>();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}