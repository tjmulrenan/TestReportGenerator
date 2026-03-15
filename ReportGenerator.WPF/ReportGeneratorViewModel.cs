using ReportGenerator.Services;
using System;
using System.IO;
using System.Windows;

namespace ReportGeneratorWPF
{
    public class ReportGeneratorViewModel
    {
        private const string TemplateFile = @"Template.docx";

        private readonly CleanupService _cleanupService;

        public ReportGeneratorViewModel()
        {
            _cleanupService = new CleanupService(new ProcessService());
            Model = new ReportGeneratorModel(new Command(CreateReport));
        }


        public ReportGeneratorModel Model { get; } 

        public void CreateReport(object parameters)
        {
            if (_cleanupService.HasActiveWordInstances())
            {
                var result = MessageBox.Show("Word must be closed before report generation. Do you want to proceed?", "Close All Instances Of Word", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _cleanupService.KillWordInstances();
                }
                else
                {
                    return;
                }
            }

            if (!InputValidation.IsValidTestNum(Model.TestNumber))
            {
                MessageBox.Show($"Test number is invalid!","Report Creation Failed",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var report = new ReportDocument();

            // Attach all tags

            var workingDir = Directory.GetCurrentDirectory();
            var contentDir = Path.Combine(workingDir, @"..\..\..\..\Content");
            var standardisedWording = $@"{contentDir}\Standardized Wording_DONOTMODIFY";

            var standardDir = $@"{standardisedWording}\{Model.SelectedStandard}";

            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoBtmPara) }", new[] { TemplateTags.OneTwoBtmParaA, TemplateTags.OneTwoBtmParaB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoTitle) }", new[] { TemplateTags.OneTwoTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoTopParaA) }", new[] { TemplateTags.OneTwoTopParaA, TemplateTags.OneTwoTopParaB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneFiveTextA) }", new[] { TemplateTags.OneFiveA });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.TwoFourFinalSettingText) }", new[] { TemplateTags.TwoFourFinalSettingText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.TwoFourFinalSettingTitle) }", new[] { TemplateTags.TwoFourFinalSettingTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.BSCloserText) }", new[] { TemplateTags.BSCloserText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.ENCloserText) }", new[] { TemplateTags.ENCloserText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.FrameConstructionGapTitle) }", new[] { TemplateTags.FrameConstructionGapTitle});
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.GapMeasurementsTitle) }", new[] { TemplateTags.GapMeasurementsTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafFrameGapTitle) }", new[] { TemplateTags.LeafFrameGapTitle});
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafStopGapText) }", new[] { TemplateTags.LeafStopGapA, TemplateTags.LeafStopGapB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafStopGapTitle) }", new[] { TemplateTags.LeafStopGapTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LimitationsOneText) }", new[] { TemplateTags.LimitationsOneTextA, TemplateTags.LimitationsOneTextB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MaximumGapsText) }", new[] { TemplateTags.MaximumGapsText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MaximumGapsTitle) }", new[] { TemplateTags.MaximumGapsTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MechPreTestText) }", new[] { TemplateTags.MechPreTestTextA, TemplateTags.MechPreTestTextB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MechPreTestTitle) }", new[] { TemplateTags.MechPreTestTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.Standard)}", new[] { TemplateTags.Standard });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.ConstructionStandard) }", new[] { TemplateTags.ConstructionStandard});
           
            var sameDir = $@"{standardisedWording}\Same";

            var oneSixFilename = Model.IsSampleReport ? $@"{sameDir}\1.6Y.txt" : $@"{sameDir}\1.6N.txt";
            
            SafeAttachTextToTags(report, oneSixFilename, new[] { TemplateTags.OneSixText });
         
            var summaryText = SpecimenSummary.GetSummary(Model.SelectedLHActing, 
                Model.SelectedLHInsulated, 
                Model.IsLeftHandGlazed, 
                Model.SelectedLHMaterial,
                Model.SelectedLHPanels,
                Model.SelectedLHGlazedInfilled,
                Model.SelectedLHLatched,
                Model.SelectedLHShootbolts, 
                Model.IsLeftHandOpeningTowardsHeatConditions);

            report.AttachTextToTags(new[] { TemplateTags.ReportNum }, Model.TestNumber);
            report.AttachTextToTags(new[] { TemplateTags.SponsorName }, Model.SponsorName);
            report.AttachTextToTags(new[] { TemplateTags.Address }, Model.Address);
            report.AttachTextToTags(new[] { TemplateTags.SponsorSummary }, summaryText);
            report.AttachTextToTags(new[] { TemplateTags.Date }, GetTestDataFromReportNumber(Model.TestNumber));


            // Create Word Document


            var templateFilename = $"{contentDir}\\{TemplateFile}";
            var outputFolder = $"{workingDir}\\CreatedTestDocs";

            if (!Directory.Exists($"{outputFolder}"))
            {
                Directory.CreateDirectory(outputFolder);
            }

            var outputFilename = $"{outputFolder}\\{Model.SponsorName}_{Model.TestNumber}_{DateTime.Now.ToString("HHmmss")}";

            report.CreateWordDocument(templateFilename, outputFilename);

            MessageBox.Show($"Report output to: {outputFilename}", "Report Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string GetTestDataFromReportNumber(string testNum)
        {
            string testDateFullWithSuffix = string.Empty;

            try
            {
                //removes last number from string that has no connection to test date

                string testNumDateOnly = testNum.Remove(testNum.Length - 1);

                //converts testdate string to a "DateTime" variable

                DateTime testDate = DateTime.ParseExact(testNumDateOnly, "yyMMdd", null);

                string dateSuffix =
                    (testDate.Day % 10 == 1 && testDate.Day % 100 != 11) ? "st"
                    : (testDate.Day % 10 == 2 && testDate.Day % 100 != 12) ? "nd"
                    : (testDate.Day % 10 == 3 && testDate.Day % 100 != 13) ? "rd"
                    : "th";

                //converts test date to full length date - day suffix

                string testDateFull = testDate.ToString("d MMMM yyyy");

                //checks if date starts with 0 so it knows where to put the day suffix

                testDateFullWithSuffix = testNumDateOnly[4] == '0' ? testDateFull.Insert(1, dateSuffix) : testDateFull.Insert(2, dateSuffix);
            }
            catch (Exception)
            {
                return "Invalid test number";
            }

            return testDateFullWithSuffix;
        }

        private string TemplateWithExtension(string templateName) => $"{templateName}.txt";

        private void SafeAttachTextToTags(ReportDocument report, string sourceFile, string[] reportTags)
        {
            if (File.Exists(sourceFile))
            {
                string sectionText = File.ReadAllText(sourceFile);
                report.AttachTextToTags(reportTags, sectionText, true);
            }
        }
    }
}
