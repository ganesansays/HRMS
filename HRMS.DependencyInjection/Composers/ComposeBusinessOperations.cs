using Microsoft.Practices.Unity;

namespace Hrms.DependencyInjection
{
    /// <summary>
    /// Compose Business operations
    /// </summary>
    class ComposeBusinessOperations
    {
        /// <summary>
        /// Compose Business Operations
        /// </summary>
        /// <param name="container"></param>
        public static void Compose(IUnityContainer container)
        {
            //BusinessOperations Bindings
            /*container.RegisterType(typeof(IReferenceOperations), typeof(ReferenceOperations), new InjectionConstructor(container.Resolve(typeof(IReferenceRepository))));*/
        }
    }
}
