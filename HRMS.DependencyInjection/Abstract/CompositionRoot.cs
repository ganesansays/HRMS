using System;
using Microsoft.Practices.Unity;

namespace Hrms.DependencyInjection
{
    public abstract class CompositionRoot : IDisposable
    {
        /// <summary>
        /// Unity Container
        /// </summary>
        protected IUnityContainer Container {get; private set;}
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CompositionRoot()
        {
            Container = new UnityContainer();
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="container"></param>
        public CompositionRoot(IUnityContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Create UI object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T CreateComposer<T>() where T: CompositionRoot, new() 
        {
            return new T();
        }

        /// <summary>
        /// Get object of type T for the specified interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
       private static object Get<T>(Type type) where T : CompositionRoot, new()
        {
            T composer = CreateComposer<T>();
            composer.Compose();

            return composer.Get(type);
        }

        public static T GetTypeFromUIContainer<T>(Type type)
        {
            return (T) CompositionRoot.Get<UICompositionRoot>(type);
        }

        public static T GetTypeFromUIContainer<T>()
        {
            return (T)CompositionRoot.Get<UICompositionRoot>(typeof(T));
        }


        /// <summary>
        /// Get object for the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Get(Type type)
        {
            return type == null ? null :
                Container.Resolve(type);
        }

        /// <summary>
        /// Overridable method
        /// </summary>
        protected abstract void Compose();

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                Container.Dispose();
            }

            // free native resources if there are any.

        }
    }
}
