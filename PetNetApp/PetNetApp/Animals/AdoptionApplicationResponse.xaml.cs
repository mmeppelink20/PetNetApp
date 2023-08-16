/// <summary>
/// Molly Meister
/// Created: 04/14/2023
/// 
/// AdoptionApplicationResponse class
/// </summary>
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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AdoptionApplicationResponse.xaml
    /// </summary>
    public partial class AdoptionApplicationResponse : Page
    {
        private MasterManager _manager = MasterManager.GetMasterManager();
        private DataObjects.AdoptionApplicationResponseVM _response = null;
        private AdoptionApplicationVM _adoptionApplication = null;

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Custom constructor for AdoptionApplicationResponse that requires an AdoptionApplicationVM object.
        /// </summary>
        ///
        /// <param name="adoptionApplication"></param>
        public AdoptionApplicationResponse(AdoptionApplicationVM adoptionApplication)
        {
            _adoptionApplication = adoptionApplication;
            InitializeComponent();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Cancel button click handler to cancel the AdoptionApplicationResponse and return to ViewApplication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Confirm", "Are you sure you want to cancel? Your response will not be saved.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            PromptSelection result = PromptWindow.ShowPrompt("Confirm", "Are you sure you want to cancel? \n\n Your response will not be saved.", ButtonMode.YesNo);
            if(result == PromptSelection.Yes)
            {
                var window = Window.GetWindow(this);
                window.Close();
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Save button click handler to save the AdoptionApplicationResponse and return to ViewApplication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if((bool)radApprovedNo.IsChecked)
            {
                if(txt_Comments.Text == null || txt_Comments.Text == "")
                {
                    PromptWindow.ShowPrompt("Alert", "Comments are required for a denied application.");
                    return;
                }
            }
            _response = new DataObjects.AdoptionApplicationResponseVM()
            {
                AdoptionApplicationId = _adoptionApplication.AdoptionApplicationId,
                ResponderUserId = _manager.User.UsersId,
                Approved = (bool)radApprovedYes.IsChecked ? true : false,
                AdoptionApplicationResponseDate = DateTime.Now,
                AdoptionApplicationResponseNotes = txt_Comments.Text == null ? "" : txt_Comments.Text
            };

            if(_response.Approved)
            {
                try
                {
                    _manager.AdoptionApplicationResponseManager.AddAdoptionApplicationResponseByAdoptionApplicationId(_response);
                    List<AdoptionApplicationVM> toDenyList = _manager.AdoptionApplicationManager.RetrieveAllAdoptionApplicationsByAnimalId(_adoptionApplication.AnimalId);

                    foreach (AdoptionApplicationVM app in toDenyList)
                    {
                        if (app.AdoptionApplicationId != _response.AdoptionApplicationId)
                        {
                            DataObjects.AdoptionApplicationResponse autoDeny = new DataObjects.AdoptionApplicationResponse()
                            {
                                AdoptionApplicationId = app.AdoptionApplicationId,
                                ResponderUserId = _manager.User.UsersId,
                                Approved = false,
                                AdoptionApplicationResponseDate = DateTime.Now,
                                AdoptionApplicationResponseNotes = "This animal has found another loving home."
                            };
                            _manager.AdoptionApplicationManager.EditAdoptionApplicationStatusByAnimalIdForApprovedApplication(autoDeny);
                        }
                    }
                    PromptWindow.ShowPrompt("Success", "The response was successfully submitted! \n\n All other applications for this animal have been denied.");
                }
                catch (Exception up)
                {
                    PromptWindow.ShowPrompt("Error", "There was an issue inserting the response. \n\n" + up.InnerException.Message);
                }
            }
            else 
            {
                try
                {
                    if (_manager.AdoptionApplicationResponseManager.AddAdoptionApplicationResponseByAdoptionApplicationId(_response))
                    {
                        PromptWindow.ShowPrompt("Success", "The response was successfully submitted.");
                    }
                }
                catch (Exception up)
                {
                    PromptWindow.ShowPrompt("Error", "There was an issue inserting the response. \n\n" + up.InnerException.Message);
                    PromptWindow.ShowPrompt("title", "mesaage", ButtonMode.YesNo);

                }
            }
            var window = Window.GetWindow(this);
            window.Close();
        }
    }
}
