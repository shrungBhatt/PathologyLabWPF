using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OPMS.Controls.ProgressView
{
    public class ProgressView : Grid
    {

        public ProgressView()
        {
            Background = (Brush)(new BrushConverter().ConvertFromString("#1A000000"));
            Children.Add(GetProgressBar());
        }

        private ProgressBar GetProgressBar()
        {
            var progressBar = new ProgressBar();
            progressBar.VerticalAlignment = VerticalAlignment.Top;
            progressBar.Height = 4;
            progressBar.IsIndeterminate = true;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.SetResourceReference(ProgressBar.ForegroundProperty, "PrimaryColor");
            progressBar.SetResourceReference(ProgressBar.BackgroundProperty, "SecondaryColor");

            return progressBar;

        }

    }
}
