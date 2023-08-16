/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
using DataObjects;
using LogicLayer;
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


namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for SurrenderFormsPage.xaml
    /// Code created by Alex Oetken
    /// </summary>
    public partial class SurrenderFormsPage : Page
    {
        private MasterManager _masterManager = null;
        private SurrenderFormManager _surrenderFormManager;
        private List<SurrenderForm> _surrenderForms;
        private static SurrenderFormsPage _page;

        /// <summary>
        /// Alex Oetken
        /// Created: 2023/02/20
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        public SurrenderFormsPage()
        {
            _surrenderFormManager = new SurrenderFormManager();
            InitializeComponent();
        }
        /// <summary>
        /// Alex Oetken
        /// Created: 2023/02/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        private void RefreshSurrenderPage()
        {
            try
            {
                _surrenderForms = _surrenderFormManager.RetrieveAllSurrenderForms();
                datSurrenderForms.ItemsSource = _surrenderForms;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Missing Data", "Something went wrong", ButtonMode.Ok);
                return;
            }
        }
        /// <summary>
        /// Alex Oetken
        /// Created: 2023/02/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Surrenders_Loaded(object sender, RoutedEventArgs e)
        {
            if(_surrenderForms == null)
            {
                RefreshSurrenderPage();
            }
        }

        /// <summary>
        /// Alex Oetken
        /// Created: 2023/02/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (datSurrenderForms.SelectedItem != null)
            {
                var selectedSurrenderForm = (SurrenderForm)datSurrenderForms.SelectedItem;
                try
                {
                    PromptSelection result = PromptWindow.ShowPrompt("Delete this form?", "Are you sure you want to delete this form?", ButtonMode.YesNo);
                    if(result == PromptSelection.Yes)
                    {
                        _surrenderFormManager.RemoveSurrenderForm((int)selectedSurrenderForm.SurrenderFormID);
                    }
                }
                catch (Exception)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to Remove.", ButtonMode.Ok);
                }
            }

            RefreshSurrenderPage(); 

        }
    }
}
