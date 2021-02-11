using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPMS.Models
{
    public class ReportPrintingOptionsModel : ModelBase
    {
        public bool HideNormalRange { get; set; }
        public bool ShowGroupTitle { get; set; }
        public bool ShowTestTitle { get; set; }
        public bool IncludeDigitalSignature { get; set; }
        public bool LetterHead { get; set; }
        public bool BlankLetterHead { get; set; }
        public bool MergeAllTestGroups { get; set; }
        public bool MergeSingleTestGroups { get; set; }


        public ReportPrintingOptionsModel SetupOptions()
        {
            HideNormalRange = Properties.ReportPrintingOptions.Default.HideNormalRange;
            ShowGroupTitle = Properties.ReportPrintingOptions.Default.ShowGroupTitle;
            ShowTestTitle = Properties.ReportPrintingOptions.Default.ShowTestTitle;
            IncludeDigitalSignature = Properties.ReportPrintingOptions.Default.IncudeDigitalSignature;
            LetterHead = Properties.ReportPrintingOptions.Default.LetterHead;
            BlankLetterHead = Properties.ReportPrintingOptions.Default.BlankLetterHead;
            MergeAllTestGroups = Properties.ReportPrintingOptions.Default.MergeAllTestGroups;
            MergeSingleTestGroups = Properties.ReportPrintingOptions.Default.MergeSingleTestGroup;

            return this;
        }
    }
}
