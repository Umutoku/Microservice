using Autofac;

namespace FreeEducation.Services.Catalog.Services
{
    public class AutofacModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EducationService>().As<IEducationService>().SingleInstance();
        }
    }
}
