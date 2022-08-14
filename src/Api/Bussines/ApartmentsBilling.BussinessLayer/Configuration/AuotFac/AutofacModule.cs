using ApartmentsBilling.DataAccesLayer.Abstract;
using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace ApartmentsBilling.BussinessLayer.Configuration.AuotFac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            var assm = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assm).Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();



            var repoassm = Assembly.GetAssembly(typeof(ApartmentDbContext));
            builder.RegisterAssemblyTypes(repoassm).Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();



            base.Load(builder);
        }
    }
}
