using System;
using System.Configuration;
using Microsoft.Practices.Unity;

/**********************************************************************************************************************************/
/* File Name: ComposeRepositories.cs
 * Created By: Dasaprakash K
 * Created On: 09-May-2014
 * Modification History
 * Modified By      Modified Date       Remarks
 * 
/**********************************************************************************************************************************/
namespace Hrms.DependencyInjection
{
    /// <summary>
    /// Class to Compose Repositories
    /// </summary>
    class ComposeRepositories
    {
        /// <summary>
        /// Compose Repositories
        /// </summary>
        /// <param name="container"></param>
        public static void Compose(IUnityContainer container)
        {

            /*container.RegisterType(typeof(IReferenceRepository), typeof(DatabaseReferenceRepository));*/
        }
    }
}
