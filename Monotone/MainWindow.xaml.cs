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

namespace Monotone
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
            //ConsoleManager.Show ();
            //theme_Checked(null, null);
            var ad = tab.Items[2] as TabItem;
            if(ad!=null)
            {
                ad.DataContext = Workspace.This;

                //this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
                //this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
            }
            
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\AvalonDock.config"))
                serializer.Deserialize(@".\AvalonDock.config");
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AvalonDock.config");
        }


        private void theme_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict = (ResourceDictionary)System.Windows.Application.LoadComponent(new System.Uri("/Monotone;component/Monotone.xaml", System.UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void theme_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
        }

    }


    [SuppressUnmanagedCodeSecurity]
public static class ConsoleManager
{
    private const string Kernel32_DllName = "kernel32.dll";

    [DllImport(Kernel32_DllName)]
    private static extern bool AllocConsole();

    [DllImport(Kernel32_DllName)]
    private static extern bool FreeConsole();

    [DllImport(Kernel32_DllName)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport(Kernel32_DllName)]
    private static extern int GetConsoleOutputCP();

    public static bool HasConsole
    {
        get { return GetConsoleWindow() != IntPtr.Zero; }
    }

    /// <summary>
    /// Creates a new console instance if the process is not attached to a console already.
    /// </summary>
    public static void Show()
    {
        //#if DEBUG
        if (!HasConsole)
        {
            AllocConsole();
            InvalidateOutAndError();
        }
        //#endif
    }

    /// <summary>
    /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
    /// </summary>
    public static void Hide()
    {
        //#if DEBUG
        if (HasConsole)
        {
            SetOutAndErrorNull();
            FreeConsole();
        }
        //#endif
    }

    public static void Toggle()
    {
        if (HasConsole)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    static void InvalidateOutAndError()
    {
        Type type = typeof(System.Console);

        System.Reflection.FieldInfo _out = type.GetField("_out",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

        System.Reflection.FieldInfo _error = type.GetField("_error",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

        System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod("InitializeStdOutError",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

        Debug.Assert(_out != null);
        Debug.Assert(_error != null);

        Debug.Assert(_InitializeStdOutError != null);

        _out.SetValue(null, null);
        _error.SetValue(null, null);

        _InitializeStdOutError.Invoke(null, new object[] { true });
    }

    static void SetOutAndErrorNull()
    {
        Console.SetOut(TextWriter.Null);
        Console.SetError(TextWriter.Null);
    }
}

}
