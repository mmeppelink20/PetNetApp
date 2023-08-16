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

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ViewEvents.xaml
    /// </summary>
    public partial class ViewEvents : Page
    {
        private MasterManager _manager;
        private List<SponsorEvent> _sponsorEvents;
        private SponsorEvent _sponsorEvent;
    
        //public ViewEvents(string Name)//old attempt
        public ViewEvents(SponsorEvent sponsorEvent , MasterManager masterManager)
        {

            //_sponsorEvent = new SponsorEvent() { CompanyName = Name };
            //_manager = MasterManager.GetMasterManager();
            _sponsorEvent = sponsorEvent;
            _manager = masterManager;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datVeiwEventsGrid.ItemsSource = null;
            try
            {
                _sponsorEvents = _manager.InstitutionalEntityManager.RetrieveSponsorEventByName(_sponsorEvent.CompanyName);
               
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
            }
            if (_sponsorEvents.Count!=0)
            {
                datVeiwEventsGrid.ItemsSource = _sponsorEvents;
                try
                {
                    //datVeiwEventsGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(0);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                    //datMedicalRecordGrid.Columns.RemoveAt(2);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                }
                lblEventName.Content = _sponsorEvent.CompanyName + " Events";
            }
            else
            {
                List<string> noRecordMessage = new List<string>();
                datVeiwEventsGrid.ItemsSource = noRecordMessage;
                datVeiwEventsGrid.Columns[0].Header = "No Entitys Available";
            }
            
        }
    }
}
