using OPMS.Controls;
using OPMS.Models;
using OPMS.Models.Test;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.ViewModels.Base
{
    public abstract class GenericViewModel<ViewModelType> : ViewModelBase where ViewModelType : ModelBase
    {

        #region Properties and Fields
        private List<ViewModelType> modelDtos;
        public List<ViewModelType> ModelDtos
        {
            get => modelDtos;
            set
            {
                modelDtos = value;
                RaisePropertyChanged(nameof(ModelDtos));
                if (SelectedModel == null && ModelDtos != null && ModelDtos.Count > 0)
                {
                    SelectedModel = ModelDtos.First();
                }
            }
        }

        private ViewModelType selectedModel;
        public ViewModelType SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                RaisePropertyChanged(nameof(SelectedModel));
                if (SelectedModel != null)
                    OnModelSelected(SelectedModel);
            }
        }

        private List<TestGroups> testGroupDtos;
        public List<TestGroups> TestGroupsDtos
        {
            get => testGroupDtos;
            set
            {
                testGroupDtos = value;
                RaisePropertyChanged(nameof(TestGroupsDtos));
            }
        }

        private TestGroups testGroupSelectedItem;
        public TestGroups SelectedTestGroup
        {
            get => testGroupSelectedItem;
            set
            {
                testGroupSelectedItem = value;
                RaisePropertyChanged(nameof(SelectedTestGroup));
                if (SelectedModel != null && SelectedTestGroup != null)
                {
                    OnGroupIdSelected(SelectedTestGroup.Id);
                }
            }
        }

        private List<TestComboListTypeDto> testGroupTypeDtos;
        public List<TestComboListTypeDto> TestGroupTypeDtos
        {
            get => testGroupTypeDtos;
            set
            {
                testGroupTypeDtos = value;
                RaisePropertyChanged(nameof(TestGroupTypeDtos));
            }
        }

        private TestComboListTypeDto groupTypeSelectedItem;
        public TestComboListTypeDto GroupTypeSelectedItem
        {
            get => groupTypeSelectedItem;
            set
            {
                groupTypeSelectedItem = value;
                RaisePropertyChanged(nameof(GroupTypeSelectedItem));
                if (SelectedModel != null && GroupTypeSelectedItem != null)
                {
                    OnGroupIdSelected(GroupTypeSelectedItem.Id);
                }

            }
        }

        private List<TestTitles> testTitlesDtos;
        public List<TestTitles> TestTitlesDtos
        {
            get => testTitlesDtos;
            set
            {
                testTitlesDtos = value;
                RaisePropertyChanged(nameof(TestTitlesDtos));
            }
        }

        private TestTitles selectedTestTitle;
        public TestTitles SelectedTestTitle
        {
            get => selectedTestTitle;
            set
            {
                selectedTestTitle = value;
                RaisePropertyChanged(nameof(SelectedTestTitle));
                if (SelectedModel != null && SelectedTestTitle != null)
                {
                    OnTestIdSelected(SelectedTestTitle.Id);
                }
            }
        }

        private List<TestComboListTypeDto> testTitleTypeDtos;
        public List<TestComboListTypeDto> TestTitleTypeDtos
        {
            get => testTitleTypeDtos;
            set
            {
                testTitleTypeDtos = value;
                RaisePropertyChanged(nameof(TestTitleTypeDtos));
            }
        }

        private TestComboListTypeDto titleTypeSelectedItem;
        public TestComboListTypeDto TitleTypeSelectedItem
        {
            get => titleTypeSelectedItem;
            set
            {
                titleTypeSelectedItem = value;
                RaisePropertyChanged(nameof(TitleTypeSelectedItem));
                if (SelectedModel != null && TitleTypeSelectedItem != null)
                {
                    OnTestIdSelected(TitleTypeSelectedItem.Id);
                }

            }
        }

        private string searchTitle;
        public string SearchTitle
        {
            get => searchTitle;
            set
            {
                searchTitle = value;
                RaisePropertyChanged(nameof(SearchTitle));
                if (!string.IsNullOrEmpty(SearchTitle) && ModelDtos != null)
                {
                    SetSearch(SearchTitle);
                }
                else
                {
                    ModelDtos = Activator.CreateInstance<List<ViewModelType>>();
                    ModelDtos = oldSelectedModels;
                    RaisePropertyChanged(nameof(ModelDtos));
                    oldSelectedModels = null;
                }
            }
        }



        //private readonly ITestService testService;

        protected List<ViewModelType> oldSelectedModels;
        protected ViewModelType newSelectedModel = Activator.CreateInstance<ViewModelType>();
        protected ViewModelType oldSelectedModel = Activator.CreateInstance<ViewModelType>();
        #endregion

        #region Edit toolbar button clicks
        public override void OnFirstButtonClicked()
        {
            if (ModelDtos != null && ModelDtos.Count > 0)
            {
                SelectedModel = ModelDtos.First();
            }

        }

        public override void OnLastButtonClicked()
        {
            if (ModelDtos != null && ModelDtos.Count > 0)
            {
                SelectedModel = ModelDtos.Last();
            }
        }

        public override void OnNextButtonClicked()
        {
            if (ModelDtos != null)
            {
                var index = ModelDtos.IndexOf(SelectedModel);
                if (index + 1 < ModelDtos.Count)
                    SelectedModel = ModelDtos[index + 1];
            }

        }

        public override void OnPreviousButtonClicked()
        {
            if (ModelDtos != null)
            {
                var index = ModelDtos.IndexOf(SelectedModel);
                if (index - 1 > -1)
                {
                    SelectedModel = ModelDtos[index - 1];
                }
            }

        }

        public override void OnEditButtonClicked()
        {
            base.OnEditButtonClicked();
            oldSelectedModel = (ViewModelType)SelectedModel?.Clone();
        }

        public override void OnCloseButtonClicked()
        {
            SelectedModel = oldSelectedModel;
            base.OnCloseButtonClicked();
        }

        public override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();
            newSelectedModel = Activator.CreateInstance<ViewModelType>();
            oldSelectedModel = SelectedModel;
            SelectedModel = newSelectedModel;

        }

        public override void OnDeleteButtonClicked()
        {
            base.OnDeleteButtonClicked();
            var sure = MessageBox.Show("Are you sure that you want to delete this item?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (sure == MessageBoxResult.Yes)
            {
                DeleteModelItem(SelectedModel);
            }
        }

        public override void OnSaveButtonClicked()
        {
            if (!IsInvalid)
            {
                if (EditButtonActiveStateModel.EditToolbarMode == EditToolbarMode.ADD)
                {
                    AddNewModelItem(SelectedModel);
                }
                else
                {
                    UpdateModelItem(SelectedModel);
                }

                base.OnSaveButtonClicked();
            }
            else
            {
                MessageBox.Show("Please enter values in the fields that has * mark. Place your mouse over that field to get more information.", "Some fields are missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            

            //Save the selected user here
        }
        #endregion

        #region API Calls
        protected abstract void GetModelItems();
        protected abstract void AddNewModelItem(ViewModelType model);
        protected abstract void UpdateModelItem(ViewModelType model);
        protected abstract void DeleteModelItem(ViewModelType model);

        #endregion

        #region Abstract Methods
        protected abstract void OnModelSelected(ViewModelType selectedModel);
        protected virtual void SetSearch(string searchTitle)
        {

        }
        protected virtual void OnGroupIdSelected(int id)
        {

        }
        protected virtual void OnTestIdSelected(int id)
        {

        }
        #endregion

        protected override void OnRefreshButtonClicked()
        {
            base.OnRefreshButtonClicked();
            GetModelItems();
        }

    }
}
