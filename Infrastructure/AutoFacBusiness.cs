using Autofac;
using Domain.IRepositories;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public class AutoFacBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<AppUserRoleRepository>().As<IAppUserRoleRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<AppUserNominationRepository>().As<IAppUserNominationRepository>();
            builder.RegisterType<NominationRepository>().As<INominationRepository>();

            //builder.RegisterType<TEMP_CRON_SERVICE>().As<IHostedService>().SingleInstance();
        }
    }
}