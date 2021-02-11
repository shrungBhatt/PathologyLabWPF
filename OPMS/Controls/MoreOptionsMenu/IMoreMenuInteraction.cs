using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Controls.MoreOptionsMenu
{
    public interface IMoreMenuInteraction
    {
        void OnExitButtonClicked();
        void OnMenuButtonClicked(string menuId);
    }
}
