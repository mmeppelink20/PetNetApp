/*
 * Created By Asa Armstrong
 * Created on 2023/04/06
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DataObjects;

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for InKindDonationUserControl.xaml
    /// </summary>
    public partial class InKindDonationUserControl : UserControl
    {
        public InKind InKind
        {
            get { return (InKind)GetValue(InKindProperty); }
            set { SetValue(InKindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InKindProperty =
            DependencyProperty.Register("InKind", typeof(InKind), typeof(InKindDonationUserControl), new PropertyMetadata(null));


        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for InKindDonationUserControl.
        /// </summary>
        /// <param name="inKind">The InKind item that the User Control is for.</param>
        public InKindDonationUserControl(InKind inKind)
        {
            InKind = inKind;
            InKind.Quantity = 1;
            InKind.Recieved = true;
            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Removes the User Control and sets the InKind. Recieved to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeleteInKind_Click(object sender, RoutedEventArgs e)
        {
            this.InKind.Recieved = false;
            ((Panel)this.Parent).Children.Remove(this);
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Changes the focus to the textbox txt_ItemName when loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_ItemName_Loaded(object sender, RoutedEventArgs e)
        {
            txt_ItemName.Focus();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Sets the error message if the In-Kind is missing a Description.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uc_InKindDonationUserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (InKind.Description == null || InKind.Description.Length == 0 || InKind.Description.Length > 225)
            {
                lbl_InKindError.Visibility = Visibility.Visible;
                lbl_InKindError.Content = "Must be between 1 and 225 characters.";
            }
            else
            {
                lbl_InKindError.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When a user types in a textbox this will prevent them from entering anything but a digit
        /// this is based on https://stackoverflow.com/a/12721673
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Validates the quantity of the InKind.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
    }
}
