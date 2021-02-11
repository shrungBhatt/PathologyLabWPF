using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Controls
{
    public interface IEditToolBarInteraction
    {
        void OnSaveButtonClicked();
        void OnEditButtonClicked();
        void OnDeleteButtonClicked();
        void OnCloseButtonClicked();
        void OnAddButtonClicked();
        void OnLastButtonClicked();
        void OnNextButtonClicked();
        void OnPreviousButtonClicked();
        void OnFirstButtonClicked();
    }
}
