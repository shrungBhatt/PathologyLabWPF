using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace OPMS.Utility
{
    public class NavigationServiceSingleton
    {

        private static NavigationServiceSingleton instace;

        public static NavigationServiceSingleton Instance
        {
            get
            {
                if (instace == null)
                {
                    instace = new NavigationServiceSingleton();
                }

                return instace;
            }
        }

        private NavigationServiceSingleton()
        {

        }

        public NavigationService OPSMWindowFrameNavigationService {get; set;}

    }
}
