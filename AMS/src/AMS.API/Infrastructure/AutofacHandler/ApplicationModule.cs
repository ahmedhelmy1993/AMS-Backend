﻿using Autofac;

namespace AMS.API.Infrastructure.AutofacHandler
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Microsoft.AspNetCore.Http.HttpContextAccessor>().As<Microsoft.AspNetCore.Http.IHttpContextAccessor>().InstancePerLifetimeScope();

            ResolveRepositories(builder);
        }

        private void ResolveRepositories(ContainerBuilder builder)
        {
            System.Type[] repositories = System.Reflection.Assembly.Load(typeof(AMS.Infrastructure.Repositories.Patient.PatientRepository).Assembly.GetName()).GetTypes().ToArray();
            System.Type[] iRepositories = System.Reflection.Assembly.Load(typeof(Domain.Patient.Repository.IPatientRepository).Assembly.GetName()).GetTypes().Where(r => r.IsInterface).ToArray();
            Resolve(builder, repositories, iRepositories);
        }

        private void Resolve(ContainerBuilder builder, System.Type[] repos, System.Type[] irepos)
        {
            foreach (var repoInterface in irepos)
            {
                System.Type classType = repos.FirstOrDefault(r => repoInterface.IsAssignableFrom(r));
                if (classType != null)
                {
                    builder.RegisterType(classType).As(repoInterface).InstancePerLifetimeScope();
                }
            }
        }

    }
}
