using OPMS.Models;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPMS.Windows
{
    public partial class SelectPatientWindow : WindowBase
    {

        const int FirstNameCriteria = 1;
        const int LastNameCriteria = 2;
        const int PatientCodeCriteria = 3;
        const int ReferringByCriteria = 4;
        const int OccupationCriteria = 5;
        const int CivilStatusCriteria = 6;
        List<Patient> cachedPatientsList = new List<Patient>();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token;
        Task SearchTask;

        DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                RaisePropertyChanged(nameof(SelectedDate));
                ShowPatients((CriteriaComboBox.SelectedItem as PickerSearchItem).Id, IsDateChecked, SearchTextBox.Text);
            }
        }

        bool isDateChecked;
        public bool IsDateChecked
        {
            get => isDateChecked;
            set
            {
                isDateChecked = value;
                RaisePropertyChanged(nameof(IsDateChecked));
                ShowPatients((CriteriaComboBox.SelectedItem as PickerSearchItem).Id, IsDateChecked, SearchTextBox.Text);
            }
        }

        public SelectPatientWindow()
        {
            InitializeComponent();
        }

        SelectWindowDataModel<Patient> model;

        public override void Init(object data)
        {
            DataContext = data;
            model = data as SelectWindowDataModel<Patient>;
            cachedPatientsList = model.ItemList;

            CriteriaComboBox.ItemsSource = new List<PickerSearchItem>
            {
                new PickerSearchItem{ Id = 1, Title="First Name"},
                new PickerSearchItem{ Id = 2, Title="Last Name"},
                new PickerSearchItem{ Id = 3, Title="Patient Code"},
                new PickerSearchItem{ Id = 4, Title="Referring By Dr./Hospital"}
            };
            CriteriaComboBox.SelectedIndex = 0;
            RegstDatePicker.SelectedDate = DateTime.Today.Date;
        }

        public event EventHandler OnOkClicked;
        public event EventHandler OnCancelClicked;

        public void OnOkClicked_Invoked(object data)
        {
            OnOkClicked?.Invoke(this, new SelectWindowEventArgs(data));
        }

        public void OnCancelClicked_Invoked()
        {
            OnCancelClicked?.Invoke(this, new EventArgs());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                OnOkClicked_Invoked(ItemsDataGrid.SelectedItem);
                Close();
            }
            else
            {
                MessageBox.Show("Please select a single record from the list", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        protected override void OnClosing(CancelEventArgs e)
        {
            OnOkClicked = null;
            OnCancelClicked = null;
            base.OnClosing(e);
        }


        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var id = (CriteriaComboBox.SelectedItem as PickerSearchItem).Id;
            var text = SearchTextBox.Text;
            await Task.Run(() => { ShowPatients(id, IsDateChecked, text); });
        }

        private void CriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPatients(((sender as ComboBox).SelectedItem as PickerSearchItem).Id, IsDateChecked, SearchTextBox.Text);
        }

        void ShowPatients(int id, bool isDateChecked, string searchQuery)
        {
            switch (id)
            {
                case 1:
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        model.ItemList = cachedPatientsList.FindAll(x => x.FirstName.ToLower().Contains(searchQuery.ToLower()));
                    }
                    else
                    {
                        model.ItemList = cachedPatientsList;
                    }
                    break;
                case 2:
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        model.ItemList = cachedPatientsList.FindAll(x => x.LastName.ToLower().Contains(searchQuery.ToLower()));
                    }
                    else
                    {
                        model.ItemList = cachedPatientsList;
                    }
                    break;
                case 3:
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        model.ItemList = cachedPatientsList.FindAll(x => x.PatientCode.ToLower().Contains(searchQuery.ToLower()));
                    }
                    else
                    {
                        model.ItemList = cachedPatientsList;
                    }
                    break;
                case 4:
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        model.ItemList = cachedPatientsList.FindAll(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ReferredByName))
                            {
                                return (bool)(x.ReferredByName?.ToLower().Contains(searchQuery.ToLower()));
                            }
                            else
                            {
                                return false;
                            }
                        });
                    }
                    else
                    {
                        model.ItemList = cachedPatientsList;
                    }
                    break;
                default:
                    model.ItemList = cachedPatientsList;
                    break;
            }

            if (isDateChecked)
            {
                model.ItemList = model.ItemList.FindAll(x => x.RegistrationDate.Date == SelectedDate.Date);
            }
        }

        private void RegstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = (sender as DatePicker).SelectedDate.Value;
        }

        private void RegstDateCB_Checked(object sender, RoutedEventArgs e)
        {
            IsDateChecked = true;
        }

        private void RegstDateCB_Unchecked(object sender, RoutedEventArgs e)
        {
            IsDateChecked = false;
        }
    }

    public class PickerSearchItem
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }
}
