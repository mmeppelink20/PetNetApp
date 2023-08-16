/// <summary>
/// Asa Armstrong
/// Created: 2023/02/02
/// 
/// WPF UI logic for adding and editing Animal Deaths.
/// </summary>
///
/// <remarks>
/// Asa Armstrong
/// Updated: 2023/03/01
/// Added Comments.
/// </remarks>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
/// 
/// FinalQA
/// </remarks>
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
using DataObjects;
using LogicLayer;

namespace WpfPresentation.Animals
{
    public partial class AddAnimalDOD513 : Page
    {
        private DeathVM _death = new DeathVM();
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private Animal _animal = new Animal();  // the currently selected animal
        private Kennel _kennel = null;
        private bool isEditMode = false;
        private DeathVM _oldDeathVM = new DeathVM();

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page AddAnimalDOD513
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="animal"></param>
        public AddAnimalDOD513(Animal animal, Kennel kennel = null)
        {
            _animal = animal;
            _kennel = kennel;

            InitializeComponent();

            populateControls();


            retrieveOldDeath();
            if (!_oldDeathVM.DeathId.Equals(0))
            {
                setEditMode();
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        private void retrieveOldDeath()
        {
            try
            {
                _oldDeathVM = _masterManager.DeathManager.RetrieveAnimalDeath(_animal);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }
        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        private void populateControls()
        {
            txt_Name.Text = (_oldDeathVM.AnimalName == null || _oldDeathVM.AnimalName.Length == 0) ?
                                ((_animal.AnimalName == null || _animal.AnimalName.Length == 0) ? "UNKNOWN" : _animal.AnimalName) :
                                _oldDeathVM.AnimalName;
            txt_AnimalID.Text = _animal.AnimalId.Equals(0) ? (_oldDeathVM.AnimalId.Equals(0) ? "UNKNOWN" : _oldDeathVM.AnimalId.ToString()) :
                                _animal.AnimalId.ToString();
        }
        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        private void setEditMode()
        {
            // _death = _oldDeathVM; // just creates a pointer instead of a new DeathVM
            _death.DeathDate = _oldDeathVM.DeathDate;
            _death.AnimalBreed = _oldDeathVM.AnimalBreed;
            _death.AnimalGender = _oldDeathVM.AnimalGender;
            _death.AnimalId = _oldDeathVM.AnimalId;
            _death.AnimalName = _oldDeathVM.AnimalName;
            _death.AnimalType = _oldDeathVM.AnimalType;
            _death.DeathCause = _oldDeathVM.DeathCause;
            _death.DeathDisposal = _oldDeathVM.DeathDisposal;
            _death.DeathDisposalDate = _oldDeathVM.DeathDisposalDate;
            _death.DeathId = _oldDeathVM.DeathId;
            _death.DeathNotes = _oldDeathVM.DeathNotes;

            populateControls();
            isEditMode = true;
            lbl_Title.Content = "Edit Animal Death Record";
            txt_Cause.Text = _oldDeathVM.DeathCause;
            date_DOD.SelectedDate = _oldDeathVM.DeathDate;
            txt_Notes.Text = _oldDeathVM.DeathNotes;
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Adds a new record to the DB or Edits an existing one.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (date_DOD.SelectedDate == null)
                {
                    throw new ApplicationException("Death Date cannot be empty.");
                }
                _death.DeathDate = date_DOD.SelectedDate.Value;
                _death.DeathCause = txt_Cause.Text;
                _death.DeathNotes = txt_Notes.Text;
                _death.AnimalId = _animal.AnimalId;

                _death.UsersId = _masterManager.User.UsersId;

                if (_death.DeathDate.Equals(null))
                {
                    throw new ApplicationException("Death Date cannot be empty.");
                }

                if (_death.DeathCause.Equals(null) || _death.DeathCause.Length < 1 || _death.DeathCause.Length > 100)
                {
                    throw new ApplicationException("Death Cause must be between 1 and 100 characters.");
                }

                if (_death.DeathNotes.Length > 500)
                {
                    throw new ApplicationException("Death Notes cannot be greater than 500 characters.");
                }

                if (_death.DeathDate > DateTime.Now)
                {
                    throw new ApplicationException("Death Date cannot be in the future.");
                }

                bool success = false;
                string successString = "";
                if (!isEditMode) // if not in edit mode, create new death record
                {
                    //No field for these in UI?
                    _death.DeathDisposal = "UNKNOWN";
                    _death.DeathDisposalDate = DateTime.Now;
                    if (success = _masterManager.DeathManager.AddAnimalDeath(_death))
                    {
                        successString += "Death record created. ";
                    }
                }
                else if (isEditMode) // if in edit mode, update existing death record
                {
                    if (success = _masterManager.DeathManager.EditAnimalDeath(_death, _oldDeathVM))
                    {
                        successString += "Death record updated. ";
                    }

                }

                if (success)
                {
                    
                    if(
                        (_kennel != null && _kennel.KennelId != 0) &&
                        PromptSelection.Yes == PromptWindow.ShowPrompt("Animal Kenneling", "Remove " + _animal.AnimalName + " from kennel?", ButtonMode.YesNo)
                        )
                    {
                        try
                        {
                            if(_masterManager.KennelManager.RemoveAnimalKennelingByKennelIdAndAnimalId(_kennel.KennelId, _animal.AnimalId))
                            {
                                successString += _animal.AnimalName + " removed from kennel.";
                            }
                            else
                            {
                                throw new ApplicationException();
                            }
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Something went wrong. " + _animal.AnimalName + " wasn't removed from kennel. " + ex.Message);
                        }
                    }
                    PromptWindow.ShowPrompt("Success", successString, ButtonMode.Ok);
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", isEditMode ? "Death Record Not Edited" : "Death Record Not Created", ButtonMode.Ok);
                }

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    retrieveOldDeath();
                    setEditMode();
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Cancels the Add/Edit and returns from the page.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Confirm Cancel", "Cancel and return?", ButtonMode.YesNo).Equals(PromptSelection.Yes))
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    // return to a base page
                }
            }
        }
    }
}
