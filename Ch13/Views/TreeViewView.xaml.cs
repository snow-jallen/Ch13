﻿using Ch13.ViewModel;
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

namespace Ch13.Views
{
    /// <summary>
    /// Interaction logic for TreeViewView.xaml
    /// </summary>
    public partial class TreeViewView : UserControl
    {
        public TreeViewView()
        {
            InitializeComponent();
        }

        private void selectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vm = DataContext as TreeViewViewModel;
            if (vm != null)
                vm.SelectedPerson = e.NewValue as Person;
        }
    }
}
