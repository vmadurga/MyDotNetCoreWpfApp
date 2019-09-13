﻿using System.Windows.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class BlankPage : Page
    {
        public BlankPage(BlankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}