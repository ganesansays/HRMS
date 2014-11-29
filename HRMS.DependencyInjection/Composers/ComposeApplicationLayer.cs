using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using Microsoft.Practices.Unity;

/**********************************************************************************************************************************/
/* File Name: ComposeApplicationLayer.cs
 * Created By: Dasaprakash K
 * Created On: 09-May-2014
 * Modification History
 * Modified By      Modified Date       Remarks
 * 
/**********************************************************************************************************************************/
namespace Hrms.DependencyInjection
{
    /// <summary>
    /// Compose Application Layer
    /// </summary>
    class ComposeApplicationLayer
    {
        /// <summary>
        /// Instantiate Business in-proc or through WCF based on the flag
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static List<ChannelFactory> Compose(IUnityContainer container)
        {
            string IsNativeConfig = ConfigurationManager.AppSettings["Inject:IsNative"];
            bool IsNative = false;
            Boolean.TryParse(IsNativeConfig, out IsNative);

            //In-proc instantiation
            if(IsNative)
            {
                new ALCompositionRoot(container);
                return null;
            }
            else
            {
                //WCF instantiation
                List<ChannelFactory> channelFactoryList = new List<ChannelFactory>();
                /*channelFactoryList.Add(
                    AddBinding<IReferenceOperations, ReferenceOperations>(container));*/
                return channelFactoryList;
            }
        }

        /// <summary>
        /// ECF channel factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="Kernel"></param>
        /// <returns></returns>
        private static ChannelFactory<T> AddBinding<T, F>(IUnityContainer Kernel)
        {
            string endPointConfigurationName = "NetTcpBinding_" + typeof(T).Name;
            ChannelFactory<T> factory = null;

            Kernel.RegisterType(typeof(T), new InjectionFactory((ctx) => GetWCFImplementation<T>(endPointConfigurationName, out factory)));
            return factory;
        }

        /// <summary>
        /// Get proxy for WCF Implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPointConfigurationName"></param>
        /// <param name="channelFactory"></param>
        /// <returns></returns>
        private static T GetWCFImplementation<T>(string endPointConfigurationName, out ChannelFactory<T> channelFactory)  
        {
            channelFactory = new ChannelFactory<T>(
                endPointConfigurationName); 
            T proxy = (T)channelFactory.CreateChannel();
            return proxy;
        }
    }
}
