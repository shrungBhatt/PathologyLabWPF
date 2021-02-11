using GalaSoft.MvvmLight.Command;
using OPMS.Utility;
using OPMS.Views;
using OPMS.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OPMS
{
    /// <summary>
    /// Interaction logic for OPSMWindow.xaml
    /// </summary>
    public partial class OPSMWindow : WindowBase, INotifyPropertyChanged
    {

        #region Properties

        List<JournalEntry> journalEntries;
        public List<JournalEntry> JournalEntries
        {
            get => journalEntries;
            set
            {
                journalEntries = value;
                RaisePropertyChanged(nameof(JournalEntries));
            }
        }

        public ICommand OnJournalClickedCommand { get; set; }


        #endregion

        private void OnJournalClickedCommand_Invoked(JournalEntry data)
        {
            
        }

        public OPSMWindow()
        {
            InitializeComponent();

            ContentFrame.LoadCompleted -= ContentFrame_LoadCompleted;
            ContentFrame.LoadCompleted += ContentFrame_LoadCompleted;
            OnJournalClickedCommand = new RelayCommand<JournalEntry>(OnJournalClickedCommand_Invoked);
            ContentFrame.Navigate(GetUri<Registration>());
            NavigationServiceSingleton.Instance.OPSMWindowFrameNavigationService = ContentFrame.NavigationService;
            App.Logger.Info("Added the navigation service instance");
        }

        private void ContentFrame_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (sender is Frame)
            {
                var view = sender as Frame;
                var list = new List<JournalEntry>();
                if(view.BackStack != null)
                {
                    var enumerator = view.BackStack.GetEnumerator();
                    enumerator.Reset();
                    while (enumerator.MoveNext())
                    {
                        list.Add((JournalEntry)enumerator.Current);
                    }
                    JournalEntries = list;
                    WindowsMenuItem.ItemsSource = JournalEntries;
                }
                
            }
        }

        private void Registration_Clicked(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to registration"); 
            ContentFrame.Navigate(GetUri<Registration>());
        }

        private void TestGroupsManagement_Clicked(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to test group view");
            ContentFrame.Navigate(GetUri<TestGroupView>());
        }

        private void TestTitlesManagement_Clicked(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to test title view");
            ContentFrame.Navigate(GetUri<TestTitleView>());
        }

        private void OtherTestsManagement_Clicked(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to other test view");
            ContentFrame.Navigate(GetUri<OtherTestView>());
        }
        private void UserManagement_Clicked(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to user management view");
            ContentFrame.Navigate(GetUri<UserManagement>());
        }

        private void ChangePassword_Clicked(object sender, RoutedEventArgs e)
        {
            //Show change password popup
        }

        private void AccessLevelManagement_Clicked(object sender, RoutedEventArgs e)
        {
            //Navigate to the access level management screen
        }

        private void TestResults_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to test results view");
            ContentFrame.Navigate(GetUri<TestResultsView>());
        }

        private void FormulaEditor_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to formula editor view");
            ContentFrame.Navigate(GetUri<FormulaEditorPage>());
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            //Extract the logs to desktop.
            //The logs should be of both the folders.
            string opmsFileName = "opms_logs.zip";
            string opmsBackendFileName = "opms_backend_logs.zip";
            try
            {
                //Load the logs of the backend
                string zipPath = GetZipDestPath(opmsBackendFileName);
                string sourcePath = "./OPMS Backend/logs";
                ClearExistingZipFolders(opmsBackendFileName, zipPath);
                ZipFile.CreateFromDirectory(sourcePath, zipPath);
            }
            catch (Exception exception)
            {
                App.Logger.Error(exception);
                MessageBox.Show("Failed to export backend logs", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                string zipPath = GetZipDestPath(opmsFileName);
                string sourcePath = "./logs";
                ClearExistingZipFolders(opmsFileName, zipPath);
                ZipFile.CreateFromDirectory(sourcePath, zipPath);
                MessageBox.Show("Two log zip files of this application are exported to your desktop. Please share both of the zip files to the development team.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                App.Logger.Error(exception);
                MessageBox.Show("Failed to export application logs", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearExistingZipFolders(string fileName, string path)
        {
            if (File.Exists(path))
            {
                App.Logger.Info($"Cleared {fileName} existing zip folder");
                File.Delete(path);
            }
        }
        private string GetZipDestPath(string fileName)
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{fileName}";
        }

        private void PatientBill_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to patient bill view");
            ContentFrame.Navigate(GetUri<PatientBillView>());
        }

        private void PatientBillPayment_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the patient bill payment");
            ContentFrame.Navigate(GetUri<PatientBillPaymentView>());

        }

        private void DrHospitalBill_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the dr/hospital bill view");
            ContentFrame.Navigate(GetUri<DrHospitalBillView>());
        }

        private void DrHospitalBillPayment_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the dr/hospital bill payment view");
            ContentFrame.Navigate(GetUri<DrHospitalBillPaymentView>());
        }

        private void OtherIncome_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the other income view");
            ContentFrame.Navigate(GetUri<NonPatientIncomeView>());
        }

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the expenses view");
            ContentFrame.Navigate(GetUri<ExpensesView>());
        }

        private void SalaryPayment_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the salary payment view");
            ContentFrame.Navigate(GetUri<SalaryPaymentView>());
        }

        private void Reagent_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the reagent view");
            ContentFrame.Navigate(GetUri<ReagentView>());
        }

        private void ReagentTestRelation_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the reagent test relation view");
            ContentFrame.Navigate(GetUri<ReagentTestRelationView>());
        }

        private void DrLabHospitalRegistration_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the Dr lab hospital registration view");
            ContentFrame.Navigate(GetUri<DrLabHospitalRegistrationView>());
        }

        private void DrLabHospitalRateList_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the Dr lab hospital rate list view");
            ContentFrame.Navigate(GetUri<DrLabHospitalRateListView>());
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the inventory view");
            ContentFrame.Navigate(GetUri<InventoryView>());
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the employee view");
            ContentFrame.Navigate(GetUri<EmployeeView>());
        }

        private void Salary_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the salary view");
            ContentFrame.Navigate(GetUri<SalaryView>());
        }

        private void Dealer_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the dealer view");
            ContentFrame.Navigate(GetUri<DealerView>());
        }

        private void ReagentEntry_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the reagent entry view");
            ContentFrame.Navigate(GetUri<ReagentEntryView>());
        }

        private void SignaturePrototype_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the signature prototype view");
            ContentFrame.Navigate(GetUri<SignaturePrototypeView>());
        }

        private void AbbrInterp_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the abbrevation & interpretation view");
            ContentFrame.Navigate(GetUri<AbbrAndInterpView>());
        }

        private void FieldOptions_Click(object sender, RoutedEventArgs e)
        {
            App.Logger.Info("Navigating to the field options view");
            ContentFrame.Navigate(GetUri<FieldOptionsView>());
        }

        

        public override void Init(object data)
        {

        }

        private void WindowsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (e != null && e.OriginalSource != null)
            {
                if (e.OriginalSource is MenuItem)
                {
                    var view = e.OriginalSource as MenuItem;
                    if(view.DataContext is JournalEntry)
                    {
                        var uri = (view.DataContext as JournalEntry).Source;
                        if(uri != null)
                        {
                            ContentFrame.Navigate(uri);
                        }
                    }
                }
            }
        }

        public static Uri GetUri<T>()
        {
            return new Uri($"Views/{typeof(T).Name}.xaml", UriKind.Relative);
        }

        private void SpecializedLabPaymentReport_Click(object sender, RoutedEventArgs e)
        {
            var window = new SpecializedLabReportWindow();
            window.Show();
        }

        private void ReferringPayment_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReferringCutPaymentReportWindow();
            window.Show();
        }

        private void PatientRevenue_Click(object sender, RoutedEventArgs e)
        {
            var window = new PatientRevenueReportWindow();
            window.Show();
        }

        private void PatientReceipt_Click(object sender, RoutedEventArgs e)
        {
            var window = new PatientBillPaymentsReportWindow();
            window.Show();
        }
    }
}
