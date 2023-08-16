using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfPresentation.UserControls
{
    public class PlaceholderTextBox : TextBox
    {
        //source https://putridparrot.com/blog/basics-of-extending-a-wpf-control/
        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
            TextProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextPropertyChanged)));
        }

        static void TextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            PlaceholderTextBox placeholderTextBox = (PlaceholderTextBox)sender;

            bool textExists = placeholderTextBox.Text.Length > 0;
            if (textExists != placeholderTextBox.RemoveDefaultText)
            {
                placeholderTextBox.SetValue(RemoveDefaultTextKey, textExists);
            }
        }


        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        public static readonly DependencyProperty DefaultTextProperty = DependencyProperty.Register("DefaultText", typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        private static readonly DependencyPropertyKey RemoveDefaultTextKey = DependencyProperty.RegisterReadOnly("RemoveDefaultText", typeof(bool), typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty RemoveDefaultTextProperty = RemoveDefaultTextKey.DependencyProperty;

        public bool RemoveDefaultText
        {
            get { return (bool)GetValue(RemoveDefaultTextProperty); }
        }
    }
}
