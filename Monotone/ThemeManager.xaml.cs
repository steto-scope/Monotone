using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Monotone
{
    /// <summary>
    /// Interaktionslogik für ThemeManager.xaml
    /// </summary>
    public partial class ThemeManager : Window
    {
        List<string> accents = new List<string>();

        private MainWindow mw;

        public ThemeManager(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            StreamReader sr = new StreamReader("Monotone.Colors.xaml");
            coloreditor.Text = sr.ReadToEnd();
            StreamReader sr2 = new StreamReader("Monotone.Brushes.xaml");
            brusheseditor.Text = sr2.ReadToEnd();

            try
            {
                var dirs = Directory.GetDirectories(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\accents");
                foreach(string x in dirs)
                if(File.Exists(x+"\\Monotone.Colors.xaml") && File.Exists(x+"\\Monotone.Brushes.xaml"))
                {
                    accentsList.Items.Add(new FileInfo(x).Name);
                }
            }
            catch
            {

            }
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

        private void enabledcontrols_Checked(object sender, RoutedEventArgs e)
        {
            var mg = mw.FindName("maingrid") as Grid;
            if(mg!=null)
                mg.IsEnabled = false;
        }

        private void enabledcontrols_Unchecked(object sender, RoutedEventArgs e)
        {
            var mg = mw.FindName("maingrid") as Grid;
            if (mg != null)
                mg.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ResourceDictionary colors = (ResourceDictionary)XamlReader.Parse(coloreditor.Text);
                ResourceDictionary brushes = (ResourceDictionary)XamlReader.Parse(brusheseditor.Text);

                MonotoneUtils.Update(Application.Current.Resources, colors, brushes);
            }
            catch { }
        }


        private void accentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a = accentsList.SelectedItem.ToString();

            coloreditor.Text = File.ReadAllText("./accents/" + a + "/Monotone.Colors.xaml");
            brusheseditor.Text = File.ReadAllText("./accents/" + a + "/Monotone.Brushes.xaml");
            Button_Click_1(null, null);
        }
    }
}
