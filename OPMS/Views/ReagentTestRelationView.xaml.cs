﻿using OPMS.ViewModels;
using OPMS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPMS.Views
{
    /// <summary>
    /// Interaction logic for ReagentTestRelationView.xaml
    /// </summary>
    public partial class ReagentTestRelationView : Page
    {
        public ReagentTestRelationView()
        {
            InitializeComponent();
            DataContext = DIContainer.Resolve<ReagentTestRelationViewModel>();
        }
    }
}