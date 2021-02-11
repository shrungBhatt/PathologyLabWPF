using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using OPMS.Models.Report;
using OPSMBackend.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPMS.Windows
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : WindowBase
    {
        public ReportWindow()
        {
            InitializeComponent();
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.FullPage;
        }

        public override void Init(object data)
        {

            if(data != null)
            {
                var reportModel = data as BaseReportModel;
                
                ReportViewer.LocalReport.ReportEmbeddedResource = reportModel.ReportResourcePath;

                switch (reportModel.ReportType)
                {
                    case BaseReportModel.REPORT_TYPE_TEST_RESULT:
                        LoadTestResultReport(reportModel as TestResultsReportModel);
                        break;
                    case BaseReportModel.REPORT_TYPE_PATIENT_BILL:
                        LoadPatientBillReport(reportModel as PatientBillReportModel);
                        break;
                    case BaseReportModel.REPORT_TYPE_SPECIALIZED_LAB_REPORT:
                        LoadSpecLabReport(reportModel as SpecializedLabReportModel);
                        break;
                    case BaseReportModel.REPORT_TYPE_REFERRING_CUT_PAYMENT_REPORT:
                        LoadRefCutPaymentReport(reportModel as ReferringCutPaymentReportModel);
                        break;
                    case BaseReportModel.REPORT_PATIENT_REVENUE_REPORT:
                        LoadPatientRevenueReport(reportModel as PatientRevenueReportModel);
                        break;
                    case BaseReportModel.REPORT_PATIENT_BILL_PAYMENT_REPORT:
                        LoadPatientBillPaymentsReport(reportModel as PatientBillPaymentsReportModel);
                        break;
                }
            }

           
        }

        private void LoadTestResultReport(TestResultsReportModel testResultModel)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestResults", new BindingSource().DataSource = testResultModel.TestResultsViews));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Patient", new BindingSource().DataSource = testResultModel.Patient));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Options", new BindingSource().DataSource = testResultModel.OptionsModel));
            if (testResultModel.LeftSignature != null)
            {
                ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("LeftSignature", new BindingSource().DataSource = testResultModel.LeftSignature));
            }

            if (testResultModel.RightSignature != null)
            {
                ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("RightSignature", new BindingSource().DataSource = testResultModel.RightSignature));
            }

            ReportViewer.RefreshReport();
        }

        private void LoadPatientBillReport(PatientBillReportModel patientBillReportModel)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("TestTitles", new BindingSource().DataSource = patientBillReportModel.TestTitles));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Patient", new BindingSource().DataSource = patientBillReportModel.Patient));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("PatientBill", new BindingSource().DataSource = patientBillReportModel.PatientBill));

            ReportViewer.RefreshReport();
        }

        private void LoadSpecLabReport(SpecializedLabReportModel specializedReportModel)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("LabSamples", new BindingSource().DataSource = specializedReportModel.LabSamples));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Options", new BindingSource().DataSource = specializedReportModel.Options));

            ReportViewer.RefreshReport();
        }

        private void LoadRefCutPaymentReport(ReferringCutPaymentReportModel model)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReferringCuts", new BindingSource().DataSource = model.ReferringCutModelDtos));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Options", new BindingSource().DataSource = model.Options));

            ReportViewer.RefreshReport();
        }

        private void LoadPatientRevenueReport(PatientRevenueReportModel model)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("RevenueItems", new BindingSource().DataSource = model.RevenueItems));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Options", new BindingSource().DataSource = model.Options));

            ReportViewer.RefreshReport();
        }

        private void LoadPatientBillPaymentsReport(PatientBillPaymentsReportModel model)
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("BillPayments", new BindingSource().DataSource = model.BillPayments));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Options", new BindingSource().DataSource = model.Options));

            ReportViewer.RefreshReport();
        }
    }
}
