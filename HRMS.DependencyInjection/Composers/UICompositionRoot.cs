using System;
using System.Collections.Generic;
using System.ServiceModel;

/**********************************************************************************************************************************/
/* File Name: UICompositionRoot.cs
 * Created By: Dasaprakash K
 * Created On: 09-May-2014
 * Modification History
 * Modified By      Modified Date       Remarks
 * 
/**********************************************************************************************************************************/
namespace Hrms.DependencyInjection
{
    /// <summary>
    /// UI Composition Root
    /// </summary>
    public class UICompositionRoot : CompositionRoot
    {
        //List of channel factory created
        private List<ChannelFactory> factoryList = null;

        /// <summary>
        /// Constructor
        /// </summary>
        protected override void Compose()
        {
            factoryList = ComposeApplicationLayer.Compose(Container);
            ComposeUILayer.Compose(Container);
        }

        /// <summary>
        /// Destructor
        /// </summary>

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                //Best practise is to close the channels after communication, but as per some performance issues
                //it is proposed to keep the client channel open
                //Reference: http://stackoverflow.com/questions/866302/channelfactory-close-vs-iclientchannel-close
            }
            // free native resources if there are any.
        }
    }
}
