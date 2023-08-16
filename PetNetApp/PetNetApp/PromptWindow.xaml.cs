
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
using System.Windows.Shapes;

namespace WpfPresentation
{
    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/01/31
    /// 
    /// Replacement for Messagebox
    /// </summary>
    public partial class PromptWindow : Window
    {
        public string PromptText { get; set; } = "";
        public ButtonMode ButtonMode { get; set; }
        public PromptSelection PromptSelection { get; private set; }

        private PromptWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn1.IsDefault = true;
            btn2.IsCancel = true;
            switch(ButtonMode)
            {
                case ButtonMode.YesNo:
                    btn1.Content = "Yes";
                    btn1.Style = (Style)Application.Current.Resources["rsrcDefaultButton"];
                    btn2.Content = "No";
                    btn2.Style = (Style)Application.Current.Resources["rsrcSafeButton"];
                    PromptSelection = PromptSelection.No;
                    break;
                case ButtonMode.DeleteCancel:
                    btn1.Content = "Delete";
                    btn1.Style = (Style)Application.Current.Resources["rsrcWarningButton"];
                    btn2.Content = "Cancel";
                    btn2.Style = (Style)Application.Current.Resources["rsrcSafeButton"];
                    PromptSelection = PromptSelection.Cancel;
                    break;
                case ButtonMode.SaveCancel:
                    btn1.Content = "Save";
                    btn1.Style = (Style)Application.Current.Resources["rsrcDefaultButton"];
                    btn2.Content = "Cancel";
                    btn2.Style = (Style)Application.Current.Resources["rsrcSafeButton"];
                    PromptSelection = PromptSelection.Cancel;
                    break;
                case ButtonMode.Ok:
                    btn1.Content = "Ok";
                    btn1.Style = (Style)Application.Current.Resources["rsrcDefaultButton"];
                    Grid.SetColumnSpan(btn1, 2);
                    btn2.Visibility = Visibility.Hidden;
                    PromptSelection = PromptSelection.Ok;
                    break;
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            switch (ButtonMode)
            {
                case ButtonMode.YesNo:
                    PromptSelection = PromptSelection.Yes;
                    break;
                case ButtonMode.SaveCancel:
                    PromptSelection = PromptSelection.Save;
                    break;
                case ButtonMode.Ok:
                    PromptSelection = PromptSelection.Ok;
                    break;
                case ButtonMode.DeleteCancel:
                    PromptSelection = PromptSelection.Delete;
                    break;
            }
            this.Close();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            switch (ButtonMode)
            {
                case ButtonMode.YesNo:
                    PromptSelection = PromptSelection.No;
                    break;
                case ButtonMode.SaveCancel:
                case ButtonMode.DeleteCancel:
                    PromptSelection = PromptSelection.Cancel;
                    break;
                case ButtonMode.Ok:
                    PromptSelection = PromptSelection.Ok;
                    break;
                default:
                    PromptSelection = PromptSelection.Cancel;
                    break;
            }
            this.Close();
        }
    
        public static PromptSelection ShowPrompt(string title, string prompt, ButtonMode buttonMode = ButtonMode.Ok)
        {
            PromptWindow promptWindow = new PromptWindow()
            {
                PromptText = prompt,
                Title = title,
                ButtonMode = buttonMode
            };
            promptWindow.ShowDialog();
            return promptWindow.PromptSelection;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public enum ButtonMode
    {
        YesNo,
        DeleteCancel,
        SaveCancel,
        Ok
    }
    public enum PromptSelection
    {
        Yes,
        No,
        Delete,
        Save,
        Cancel,
        Ok
    }
}
