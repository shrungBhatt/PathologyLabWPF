using OPMS.Repository;
using OPMS.Services;
using OPMS.Services.Finance;
using OPMS.Services.Maintenance;
using OPMS.Services.Patient;
using OPMS.Services.Reagent;
using OPMS.Services.RequestProvider;
using OPMS.Services.Test;
using OPMS.Services.User;
using OPMS.Services.WindowManager;
using OPMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinyIoC;

namespace OPMS.ViewModels.Base
{
    public static class DIContainer
    {

        private static TinyIoCContainer _container;

        static DIContainer()
        {
            _container = new TinyIoCContainer();

            _container.Register<HomePageViewModel>();
            _container.Register<LoginViewModel>();
            _container.Register<RegistrationViewModel>();
            _container.Register<TestResultsViewModel>();
            _container.Register<UserManagementViewModel>();
            _container.Register<TestGroupViewModel>();
            _container.Register<TestTitleViewModel>();
            _container.Register<OtherTestViewModel>();
            _container.Register<FormulaEditorViewModel>();

            _container.Register<PatientBillViewModel>();
            _container.Register<PatientBillPaymentViewModel>();
            _container.Register<NonPatientIncomeViewModel>();
            _container.Register<ExpensesViewModel>();
            _container.Register<SalaryPaymentViewModel>();
            _container.Register<DrHospitalBillViewModel>();
            _container.Register<DrHospitalBillPaymentViewModel>();
            _container.Register<ReagentViewModel>();
            _container.Register<ReagentTestRelationViewModel>();

            _container.Register<DrLabHospitalRegistrationViewModel>();
            _container.Register<DrLabHospitalRateListViewModel>();
            _container.Register<EmployeeViewModel>();
            _container.Register<SalaryViewModel>();
            _container.Register<InventoryViewModel>();

            _container.Register<DealerViewModel>();
            _container.Register<ReagentEntryViewModel>();
            _container.Register<SignaturePrototypeViewModel>();
            _container.Register<AbbrAndInterpViewModel>();
            _container.Register<FieldOptionsViewModel>();

            _container.Register<SendToSpecializedLabWindowViewModel>();
            _container.Register<SpecializedLabReportWindowViewModel>();
            _container.Register<ReferringCutPaymentReportWindowViewModel>();
            _container.Register<PatientRevenueReportWindowViewModel>();
            _container.Register<PatientBillPaymentsReportWindowViewModel>();
            _container.Register<SelectToAddTestTitlesToGroupViewModel>();

            _container.Register<IRequestProvider, RequestProvider>();
            _container.Register<IWindowManager, WindowManager>();
            _container.Register<IUserService, UserService>();
            _container.Register<ITestService, TestService>();
            _container.Register<IPatientService, PatientService>();
            _container.Register<IReagentService, ReagentService>();
            _container.Register<IMaintenanceService, MaintenanceService>();
            _container.Register<IFinanceService, FinanceService>();

            _container.Register<UserRepository>().AsSingleton();

        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
