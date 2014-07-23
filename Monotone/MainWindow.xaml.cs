using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Packaging;
using System.IO;
using Ionic.Zip;
using System.Security;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MahApps.Metro.Controls;

namespace Monotone
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow ()
        {
            InitializeComponent ();
            DataContext = Workspace.This;
            /*var ad = tab.Items[2] as TabItem;
            if(ad!=null)
            {
                ad.DataContext = Workspace.This;
            }*/
            
        }

        private void theme_Checked(object sender, RoutedEventArgs e)
        {
            MonotoneUtils.UseSystemPreferences = true;
        }

        private void theme_Unchecked(object sender, RoutedEventArgs e)
        {
            MonotoneUtils.UseSystemPreferences = false;
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            MonotoneUtils.Update(e.NewValue);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonotoneUtils.Update();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Xceed.Wpf.Toolkit.MessageBox mb = new Xceed.Wpf.Toolkit.MessageBox();
            mb.Caption = "Caption";
            mb.Text = "Text";
            mb.ShowDialog();
        }

      

    }
    
}
