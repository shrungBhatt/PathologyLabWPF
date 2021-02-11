using Newtonsoft.Json;
using OPMS;
using OPMS.Utility.Attributes;
using OPMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OPSMBackend.DataEntities
{
    public partial class TestResultsView : BaseEntity
    {
        [JsonIgnore]
        [IgnoreColumn]
        public int? PatientId { get; set; }

        [IgnoreColumn]
        public int GroupId { get; set; }

        [IgnoreColumn]
        public int TitleId { get; set; }

        [IgnoreColumn]
        public int OtherTestId { get; set; }

        public string GroupName { get; set; }

        public string TitleName { get; set; }
        public string TestName { get; set; }

        [JsonIgnore]
        private string testResult;
        public string TestResult
        {
            get => testResult;
            set
            {
                testResult = value;
                RaisePropertyChanged(nameof(TestResult));
                RaisePropertyChanged(nameof(Abnormal));
            }
        }
        public string Unit { get; set; }

        public string NormalRange { get; set; }

        [JsonIgnore]
        public bool Abnormal
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(NormalRange))
                    {
                        var regex = new Regex(@"(<[0-9.]*)|([0-9.]*-[0-9.]*)|(>[0-9.]*)");
                        var matches = regex.Matches(NormalRange);
                        if (matches.Count > 0)
                        {
                            var firstMatch = matches[0].Value;
                            if (firstMatch.Contains(">"))
                            {
                                float num1;
                                float.TryParse(firstMatch.Replace(">", "").Trim(), out num1);

                                if (testResult != null && !string.IsNullOrEmpty(testResult))
                                {
                                    var result = (float.Parse(testResult) > num1);
                                    if (result)
                                    {
                                        IsHigh = true;
                                    }
                                    else
                                    {
                                        IsHigh = false;
                                    }
                                    return !result;
                                }
                                else
                                {
                                    return false;
                                }

                            }
                            else if (firstMatch.Contains("<"))
                            {
                                float num2;
                                float.TryParse(firstMatch.Replace("<", "").Trim(), out num2);

                                if (testResult != null && !string.IsNullOrEmpty(testResult))
                                {
                                    var result = (float.Parse(testResult) < num2);
                                    if (result)
                                    {
                                        IsHigh = false;
                                    }
                                    else
                                    {
                                        IsHigh = true;
                                    }
                                    return !result;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else if (firstMatch.Contains("-"))
                            {
                                float num3;
                                float num4;
                                float.TryParse(firstMatch.Split('-')[0], out num3);
                                float.TryParse(firstMatch.Split('-')[1], out num4);

                                if (testResult != null && !string.IsNullOrEmpty(testResult))
                                {
                                    if (float.Parse(testResult) < num3)
                                    {
                                        IsHigh = false;
                                    }
                                    else if (float.Parse(testResult) > num4)
                                    {
                                        IsHigh = true;
                                    }
                                    return !(float.Parse(testResult) >= num3 && float.Parse(testResult) <= num4);
                                }
                                else
                                {
                                    return false;
                                }

                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    App.Logger.Warn(e.Message);
                    return false;
                }
                
            }
        }

        bool exclude;
        [JsonIgnore]
        public bool Exclude
        {
            get => exclude;
            set
            {
                exclude = value;
                RaisePropertyChanged(nameof(Exclude));
            }
        }

        public string LargeTestResult { get; set; }
        [IgnoreColumn]
        public bool IsHigh { get; set; }

        [IgnoreColumn]
        public int TestResultId { get; set; }

        [IgnoreColumn]
        public Formulas Formula { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public ParsedFormula ParsedFormula { get; set; }
        [IgnoreColumn]
        public TestGroups TestGroup { get; set; }
        [IgnoreColumn]
        public TestTitles TestTitle { get; set; }

        [IgnoreColumn]
        public OtherTests OtherTest { get; set; }

        [IgnoreColumn]
        [JsonIgnore]
        public bool ShowTestGroupTitle { get => (bool)(TestGroup?.ShowTitleInReports); }

        [IgnoreColumn]
        [JsonIgnore]
        public bool ShowBoldHeaderForOtherTest { get => (bool)OtherTest?.DisplayInBoldFontInReport; }

        [IgnoreColumn]
        [JsonIgnore]
        public bool DisplayOtherTestTitle { get => (bool)OtherTest?.DisplayInTestResult; }

        [IgnoreColumn]
        [JsonIgnore]
        public string HeaderText { get => TestTitle?.HeaderNote; }

        [IgnoreColumn]
        [JsonIgnore]
        public string FooterText { get => TestTitle?.FooterNote; }

        [IgnoreColumn]
        public int OtherTestOrderId { get; set; }

        [IgnoreColumn]
        public int TestGroupOrderId { get; set; }

        [IgnoreColumn]
        public int TestTitleOrderId { get; set; }
    }
}
