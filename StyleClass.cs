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

namespace Onigaku
{
    public static class StyleClass
    {
        public static Style buttonStyle { get; } = new Style();
        public static Style labelStyle { get; } = new Style();
        static StyleClass()
        {
            buttonStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            buttonStyle.Setters.Add(new Setter { Property = Control.MarginProperty, Value = new Thickness(10) });
            buttonStyle.Setters.Add(new Setter { Property = Control.FontSizeProperty, Value = 13.0 });
            buttonStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = new SolidColorBrush(Colors.Black) });
            buttonStyle.Setters.Add(new Setter { Property = Control.ForegroundProperty, Value = new SolidColorBrush(Colors.Lime) });
            buttonStyle.Setters.Add(new Setter { Property = Control.OpacityProperty, Value = 0.5 });
            buttonStyle.Setters.Add(new Setter { Property = Control.PaddingProperty, Value = new Thickness(8) });

            labelStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Tahoma") });
            labelStyle.Setters.Add(new Setter { Property = Control.FontSizeProperty, Value = 25.0 });
        }
    }
}
