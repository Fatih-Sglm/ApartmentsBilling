using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace ApartmentsBilling.BussinessLayer.Configuration.AuotFac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assm = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assm).Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
