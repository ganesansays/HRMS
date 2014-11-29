using Microsoft.Practices.Unity;

/**********************************************************************************************************************************/
/* File Name: ALCompositionRoot.cs
 * Created By: Dasaprakash K
 * Created On: 09-May-2014
 * Modification History
 * Modified By      Modified Date       Remarks
 * 
/**********************************************************************************************************************************/
namespace Hrms.DependencyInjection
{
    /// <summary>
    /// Application Layer Composition Toot
    /// </summary>
    public class ALCompositionRoot : CompositionRoot
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ALCompositionRoot() 
        {

        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="container"></param>
        public ALCompositionRoot(IUnityContainer container) : base(container)
        {

        }

        /// <summary>
        /// Compose Repositories and Business Objects 
        /// </summary>
        protected override void Compose()
        {
            ComposeRepositories.Compose(Container);
            ComposeBusinessOperations.Compose(Container);
        }
    }
}