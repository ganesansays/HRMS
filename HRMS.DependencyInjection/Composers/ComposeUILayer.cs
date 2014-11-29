using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Configuration;
using Hrms.Repository;
using System.Data.Entity;

namespace Hrms.DependencyInjection
{
    /// <summary>
    /// Compose UI Layers
    /// </summary>
    class ComposeUILayer
    {
        /// <summary>
        /// Controller instantiation
        /// </summary>
        /// <param name="Kernel"></param>
        public static void Compose(IUnityContainer container)
        {
            //Controller injection
            container.RegisterType<HrmsStore>(new InjectionConstructor(false));
            container.RegisterType<DbContext, HrmsStore>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType<IRepositoryContext, DBRepositoryContext>();
        }
    }
}
