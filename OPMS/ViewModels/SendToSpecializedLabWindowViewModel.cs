using OPMS.Models.Patient;
using OPMS.Repository;
using OPMS.Services.Patient;
using OPMS.Services.WindowManager;
using OPMS.ViewModels.Base;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels
{
    public class SendToSpecializedLabWindowViewModel : ViewModelBase
    {
        #region Properties
        List<HdlRegistration> specializedLabs;
        public List<HdlRegistration> SpecializedLabs
        {
            get => specializedLabs;
            set
            {
                specializedLabs = value;
                RaisePropertyChanged(nameof(SpecializedLabs));
            }
        }

        HdlRegistration selectedSpecializedLab;
        public HdlRegistration SelectedSpecializedLab
        {
            get => selectedSpecializedLab;
            set
            {
                selectedSpecializedLab = value;
                RaisePropertyChanged(nameof(SelectedSpecializedLab));
            }
        }

        List<TestTitles> testTitles;
        public List<TestTitles> TestTitles
        {
            get => testTitles;
            set
            {
                testTitles = value;
                RaisePropertyChanged(nameof(TestTitles));
            }
        }

        ObservableCollection<SpecializedLabSamples> labSamples;
        public ObservableCollection<SpecializedLabSamples> LabSamples
        {
            get => labSamples;
            set
            {
                labSamples = value;
                RaisePropertyChanged(nameof(LabSamples));
            }
        }


        internal void OnItemDeleted()
        {
            if(LabSamples != null && SelectedLabSamples != null && LabSamples.Contains(SelectedLabSamples))
            {
                LabSamples.Remove(SelectedLabSamples);
            }
        }

        internal async void SendSamples()
        {
            if (cachedPatientDto != null)
            {
                IsBusy = Visibility.Visible;
                var response = await patientService.UpdateSpecializedSamples(new Models.Dto.SpecializedLabSampleDto { PatientId = cachedPatientDto.Patient.Id, SentSamples = LabSamples.ToList() });
                IsBusy = Visibility.Hidden;
                if (response != null)
                {
                    SpecializedLabs = response.SpecializedLabs;
                    if (SpecializedLabs != null && SpecializedLabs.Count > 0)
                    {
                        SelectedSpecializedLab = SpecializedLabs.First();
                    }

                    OnItemsSelected(response.SentSamples);
                    TestTitles = cachedPatientDto.SelectedTestTitles;

                    GetModelItems();

                }
            }
        }

        SpecializedLabSamples selectedLabSamples;
        public SpecializedLabSamples SelectedLabSamples
        {
            get => selectedLabSamples;
            set
            {
                selectedLabSamples = value;
                RaisePropertyChanged(nameof(SelectedLabSamples));
            }
        }

        internal void OnItemsSelected(List<TestTitles> selectedTestTitles)
        {
            if (LabSamples == null) LabSamples = new ObservableCollection<SpecializedLabSamples>();

            if(selectedTestTitles != null)
            {
                foreach(var test in selectedTestTitles)
                {
                    LabSamples.Add(new SpecializedLabSamples
                    {
                        LabId = SelectedSpecializedLab.Id,
                        LabName = SelectedSpecializedLab.Name,
                        ModifiedBy = userRepository.UserName,
                        ModifiedDate = DateTime.Now,
                        TestName = test.Name,
                        TestTitleId = test.Id,
                        PatientId = cachedPatientDto.Patient.Id
                    });
                }
            }
        }

        internal void OnItemsSelected(List<SpecializedLabSamples> samples)
        {
            if (LabSamples == null) LabSamples = new ObservableCollection<SpecializedLabSamples>();
            LabSamples.Clear();
            if (samples != null)
            {
                foreach (var test in samples)
                {
                    LabSamples.Add(test);
                }
            }
        }



        #endregion


        #region Fields 
        private readonly IWindowManager _windowManager;
        private readonly IPatientService patientService;
        readonly UserRepository userRepository;
        PatientDto cachedPatientDto;

        #endregion

        public SendToSpecializedLabWindowViewModel(IWindowManager windowManager,
                                     IPatientService patientService,
                                     UserRepository userRepository)
        {
            this._windowManager = windowManager;
            this.patientService = patientService;
            this.userRepository = userRepository;
        }


        async void GetModelItems()
        {
            
            if(cachedPatientDto != null)
            {
                IsBusy = Visibility.Visible;
                var response = await patientService.GetSpecializedLabSamples(new Models.Dto.SpecializedLabSampleDto { PatientId = cachedPatientDto.Patient.Id });
                IsBusy = Visibility.Hidden;
                if (response != null)
                {
                    SpecializedLabs = response.SpecializedLabs;
                    if(SpecializedLabs != null && SpecializedLabs.Count > 0)
                    {
                        SelectedSpecializedLab = SpecializedLabs.First();
                    }

                    OnItemsSelected(response.SentSamples);
                    TestTitles = cachedPatientDto.SelectedTestTitles;

                }
            }
           
        }

        public void OnPatientSelected(PatientDto patient)
        {
            cachedPatientDto = patient;
            GetModelItems();
        }


    }
}
