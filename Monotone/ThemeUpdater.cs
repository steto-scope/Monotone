using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Monotone
{
    /// <summary>
    /// Util-Class for Monotone
    /// </summary>
    public class ThemeUpdater
    {
        #region Updater

        /// <summary>
        /// Updates the applied Monotone-Theme to a new color-scheme.
        /// </summary>
        public static void Update()
        {
            if (UseSystemPreferences)
                Update(GetSystemColor().H);
        }

        /// <summary>
        /// Updates the applied Monotone-Theme to a new color-scheme.
        /// </summary>
        /// <param name="hue">New color</param>
        public static void Update(double hue)
        {
            Update(RootDictionary, hue, BrushesDictionary);
        }

        /// <summary>
        /// Updates the applied Monotone-Theme to a new color-scheme.
        /// </summary>
        /// <param name="color">New color</param>
        public static void Update(Color color)
        {
            Update(RootDictionary, color, BrushesDictionary);
        }

        /// <summary>
        /// Updates the applied Monotone-Theme to a new color-scheme
        /// </summary>
        /// <param name="dictionary">Dictionary which holds the Monotone-Resources. Common value is Application.Current.Resources</param>
        /// <param name="color">The new color. Only the hue is used, no saturation or brightness!</param>
        /// <param name="brushesResource">The Uri to the Brushes-Dictionary. A re-apply of this Resource is used to update the Bindings</param>
        public static void Update(ResourceDictionary dictionary, Color color, Uri brushesResource)
        {
            Update(dictionary, HSV.FromColor(color).H, brushesResource);
        }

        /// <summary>
        /// Updates the applied Monotone-Theme to a new color-scheme
        /// </summary>
        /// <param name="dictionary">Dictionary which holds the Monotone-Resources. Common value is Application.Current.Resources</param>
        /// <param name="hue">The new color. Hue-value of the HSL/HSV colorspace. Range: [0.0,1.0]</param>
        /// <param name="brushesResource">The Uri to the Brushes-Dictionary. A re-apply of this Resource is used to update the Bindings</param>
        public static void Update(ResourceDictionary dictionary, double hue, Uri brushesResource)
        {
            if (brushesResource == null)
                return;
            if (dictionary == null)
                return;

            HSV baseborder = HSV.FromColor(Color.FromRgb(102, 102, 102));
            baseborder.H = hue;
            
            Color transm3 = baseborder.ToColor(0, 0.3, 0);
            transm3.A = 119;
            Color transm2t = baseborder.ToColor(0, 0.1, -0.25);
            transm2t.A = 34;


            ResourceDictionary dict = new ResourceDictionary();
            dict = (ResourceDictionary)System.Windows.Application.LoadComponent(brushesResource);
            dict.Source = brushesResource;

            if (!dictionary.Contains("BaseColor"))
                return;

            dictionary["BaseColor"] = HSV.FromHSV(hue, 0.4, 0.2);
            dictionary["SelectedColor"] = HSV.FromHSV(hue, 1, 1);
            dictionary["DarkerSelectedColor"] = HSV.FromHSV(hue, 1, 0.7);
            dictionary["BaseM2Color"] = baseborder.ToColor(0, 0.1, -0.25);
            dictionary["BaseM2TColor"] = transm2t;
            dictionary["BaseM1Color"] = baseborder.ToColor(0, 0.1, -0.2);
            dictionary["Base1Color"] = baseborder.ToColor(0, 0.1, 0);
            dictionary["Base2Color"] = baseborder.ToColor(0, 0.2, 0);
            dictionary["Base3Color"] = baseborder.ToColor(0, 0.3, 0);
            dictionary["Base3TColor"] = transm3;
            dictionary["Base4Color"] = baseborder.ToColor(0, 0.4, 0);
            dictionary["Base5Color"] = baseborder.ToColor(0, 0.5, 0);
            dictionary["Base6Color"] = baseborder.ToColor(0, 0.6, 0);

            int index = -1;
            for (int i = 0; i < dictionary.MergedDictionaries.Count; i++)
            {
                if (brushesResource.OriginalString.ToLower().EndsWith(dictionary.MergedDictionaries[i].Source.OriginalString.ToLower()))
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                dictionary.MergedDictionaries.RemoveAt(index);
                dictionary.MergedDictionaries.Insert(index, dict);
            }
        }

#endregion

        #region Properties


        /// <summary>
        /// Tries to find the dictionary in the Application Resources
        /// </summary>
        /// <param name="name">filename without extension</param>
        /// <returns></returns>
        private static Uri TryGetDictionaryUri(string name)
        {
            Assembly assembly = Application.ResourceAssembly;
            var resourcesName = assembly.GetName().Name + ".g";
            var manager = new System.Resources.ResourceManager(resourcesName, assembly);
            var resourceSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var allXamlFiles =
              from entry in resourceSet.OfType<DictionaryEntry>()
              let fileName = (string)entry.Key
              where fileName.EndsWith(name.ToLower()+".baml")
              select fileName.Substring(0, fileName.Length - 5) + ".xaml";
            var f = allXamlFiles.FirstOrDefault();
            if (f != null)
            {
                Uri u = new Uri(f, UriKind.Relative);
                return u;
            }
            return null;
        }


        private static Uri brushes;

        public static Uri BrushesDictionary
        {
            get
            {
                if (brushes == null)
                    return TryGetDictionaryUri("monotone.brushes");
                return brushes;
            }
            set { brushes = value; }
        }


        private static ResourceDictionary rootdict;

        public static ResourceDictionary RootDictionary
        {
            get
            {
                if (rootdict == null)
                    return Application.Current.Resources;
                return rootdict; 
            }
            set { rootdict = value; }
        }



        private static bool usesystempreferences;

        /// <summary>
        /// If enabled, Monotone gets updated automatically if the user changed the system colors
        /// </summary>
        public static bool UseSystemPreferences
        {
            get { return usesystempreferences; }
            set 
            {
                if(value && !usesystempreferences)
                    SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
                if(!value && usesystempreferences)
                    SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;

                usesystempreferences = value;
                Update();
            }
        }

        #endregion

        #region Read System Color Scheme


        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(out bool enabled);

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            Update(Application.Current.Resources, GetSystemColor().H, new System.Uri("/Monotone;component/Monotone.Brushes.xaml", System.UriKind.Relative));
            Console.WriteLine("Color changed");
        }

        public static  HSV GetSystemColor()
        {
            bool dwm = false;
            DwmIsCompositionEnabled(out dwm);
            if (dwm)
            {
                int val = 0;
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");

                if (regKey != null)
                {
                    val = (int)regKey.GetValue("ColorizationColor");
                }
                uint u = BitConverter.ToUInt32(BitConverter.GetBytes(val), 0);
                Color c = DWORDtoColor(u);
                return HSV.FromColor(c);
            }
            return new HSV();
        }

        static Color DWORDtoColor(ulong dwColor)
        {
            Color tmp = new Color(); /* why did you declare it static??? */
            tmp.B = (byte)(dwColor % 256); dwColor /= 256;
            tmp.G = (byte)(dwColor % 256); dwColor /= 256;
            tmp.R = (byte)(dwColor % 256); dwColor /= 256;
            tmp.A = (byte)(dwColor % 256); dwColor /= 256;
            return tmp;
        }

        #endregion

        #region HSV<->Color Conversion


        /// <summary>
        /// Repräsentiert eine Farbe im HSL/HSV-Farbraum.
        /// Enthält statische Methode zum Erzeugen. Alphakanal geht verloren.
        /// </summary>
        public class HSV
        {
            private double h;
            private double s;
            private double l;
            /// <summary>
            /// Farbwert [0.0,1.0]
            /// </summary>
            public double H { get { return h; } set { if (h >= 0 && h <= 1) h = value; else { if (h < 0) h = 0; else h = 1; } } }
            /// <summary>
            /// Sättigungswert {0.0,1.0}
            /// </summary>
            public double S { get { return s; } set { if (s >= 0 && s <= 1) s = value; else { if (s < 0) s = 0; else s = 1; } } }
            /// <summary>
            /// Hellwert [0.0,1.0]
            /// </summary>
            public double V { get { return l; } set { if (l >= 0 && l <= 1) l = value; else { if (l < 0) l = 0; else l = 1; } } }

            struct ColorPair
            {
                public double val;
                public string channel;
            }

            public HSV()
            {

            }


            public HSV(double h, double s, double v)
            {
                H = h; S = s; V = v;
            }

            /// <summary>
            /// Konvertiert eine (RGB)Farbe System.Windows.Media.Color in den HSL-Farbraum. Alphakanal geht verloren
            /// </summary>
            /// <param name="rgb"></param>
            /// <returns></returns>
            public static HSV FromColor(Color rgb)
            {
                HSV hsl = new HSV();
                hsl.H = 0; // default to black
                hsl.S = 0;
                hsl.V = 0;

                double deg1 = 1.0 / 360.0;
                double h = 0.0;

                ColorPair r = new ColorPair() { val = (double)rgb.R / 255.0, channel = "R" };
                ColorPair g = new ColorPair() { val = (double)rgb.G / 255.0, channel = "G" };
                ColorPair b = new ColorPair() { val = (double)rgb.B / 255.0, channel = "B" };
                ColorPair[] values = new ColorPair[] { r, g, b };

                ColorPair max = values.OrderByDescending(v => v.val).First();
                ColorPair min = values.OrderBy(v => v.val).First();

                if (min.channel == max.channel)
                    h = 0;
                else
                    switch (max.channel)
                    {
                        case "R":
                            h = 60 * deg1 * (0 + (g.val - b.val) / (max.val - min.val));
                            break;
                        case "G":
                            h = 60 * deg1 * (2 + (b.val - r.val) / (max.val - min.val));
                            break;
                        case "B":
                            h = 60 * deg1 * (4 + (r.val - g.val) / (max.val - min.val));
                            break;
                        default:
                            h = 0;
                            break;
                    }
                if (h < 0)
                    h += 1;
                hsl.H = h;

                if (max.val == 0)
                    hsl.S = 0;
                else
                    hsl.S = (max.val - min.val) / max.val;

                hsl.V = max.val;





                return hsl;

            }

            /// <summary>
            /// Generiert aus dem HSL-Farb-Objekt wieder ein (RGB)Color-Objekt
            /// </summary>
            /// <returns></returns>
            public Color ToColor()
            {
                Color rgb = new Color();


                int r = (int)Math.Truncate((H / (1.0 / 6.0)));
                double f = (H / (1.0 / 6.0)) - r;
                double p = V * (1 - S);
                double q = V * (1 - S * f);
                double t = V * (1 - S * (1 - f));

                switch (r)
                {
                    case 0:
                    case 6:
                    default:
                        rgb = new Color() { R = (byte)Math.Round(V * 255), G = (byte)Math.Round(t * 255), B = (byte)Math.Round(p * 255), A = 255 };
                        break;
                    case 1:
                        rgb = new Color() { R = (byte)Math.Round(q * 255), G = (byte)Math.Round(V * 255), B = (byte)Math.Round(p * 255), A = 255 };
                        break;
                    case 2:
                        rgb = new Color() { R = (byte)Math.Round(p * 255), G = (byte)Math.Round(V * 255), B = (byte)Math.Round(t * 255), A = 255 };
                        break;
                    case 3:
                        rgb = new Color() { R = (byte)Math.Round(p * 255), G = (byte)Math.Round(q * 255), B = (byte)Math.Round(V * 255), A = 255 };
                        break;
                    case 4:
                        rgb = new Color() { R = (byte)Math.Round(t * 255), G = (byte)Math.Round(p * 255), B = (byte)Math.Round(V * 255), A = 255 };
                        break;
                    case 5:
                        rgb = new Color() { R = (byte)Math.Round(V * 255), G = (byte)Math.Round(p * 255), B = (byte)Math.Round(q * 255), A = 255 };
                        break;
                }

                return rgb;
            }

            public Color ToColor(double offsetH, double offsetS, double offsetV)
            {
                double h=H, s=S, v=V;
                H += offsetH; S += offsetS; V += offsetV;

                Color res = ToColor();

                H = h; S = s; V = v;

                return res;
            }


            public static Color FromHSV(double h, double s, double v)
            {
                return new HSV(h, s, v).ToColor();
            }

            /// <summary>
            /// Gibt den RGB-Farbwert zurück
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return ToColor().ToString();
            }
        }

        #endregion
    }




}
