using ReportGenerator.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
            Model = new ReportGeneratorModel(new AsyncCommand(CreateReportAsync));
        }

        public ReportGeneratorModel Model { get; }

        public async Task CreateReportAsync(object parameters)
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
                MessageBox.Show($"Test number is invalid!", "Report Creation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var report = new ReportDocument();

            var workingDir = Directory.GetCurrentDirectory();
            var contentDir = Path.Combine(workingDir, @"..\..\..\..\Content");
            var standardisedWording = $@"{contentDir}\Standardized Wording_DONOTMODIFY";
            var standardDir = $@"{standardisedWording}\{Model.SelectedStandard}";

            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoBtmPara)}", new[] { TemplateTags.OneTwoBtmParaA, TemplateTags.OneTwoBtmParaB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoTitle)}", new[] { TemplateTags.OneTwoTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneTwoTopParaA)}", new[] { TemplateTags.OneTwoTopParaA, TemplateTags.OneTwoTopParaB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.OneFiveTextA)}", new[] { TemplateTags.OneFiveA });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.TwoFourFinalSettingText)}", new[] { TemplateTags.TwoFourFinalSettingText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.TwoFourFinalSettingTitle)}", new[] { TemplateTags.TwoFourFinalSettingTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.BSCloserText)}", new[] { TemplateTags.BSCloserText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.ENCloserText)}", new[] { TemplateTags.ENCloserText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.FrameConstructionGapTitle)}", new[] { TemplateTags.FrameConstructionGapTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.GapMeasurementsTitle)}", new[] { TemplateTags.GapMeasurementsTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafFrameGapTitle)}", new[] { TemplateTags.LeafFrameGapTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafStopGapText)}", new[] { TemplateTags.LeafStopGapA, TemplateTags.LeafStopGapB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LeafStopGapTitle)}", new[] { TemplateTags.LeafStopGapTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.LimitationsOneText)}", new[] { TemplateTags.LimitationsOneTextA, TemplateTags.LimitationsOneTextB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MaximumGapsText)}", new[] { TemplateTags.MaximumGapsText });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MaximumGapsTitle)}", new[] { TemplateTags.MaximumGapsTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MechPreTestText)}", new[] { TemplateTags.MechPreTestTextA, TemplateTags.MechPreTestTextB });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.MechPreTestTitle)}", new[] { TemplateTags.MechPreTestTitle });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.Standard)}", new[] { TemplateTags.Standard });
            SafeAttachTextToTags(report, $@"{standardDir}\{TemplateWithExtension(TemplateNames.ConstructionStandard)}", new[] { TemplateTags.ConstructionStandard });

            var sameDir = $@"{standardisedWording}\Same";
            var oneSixFilename = Model.IsSampleReport ? $@"{sameDir}\1.6Y.txt" : $@"{sameDir}\1.6N.txt";
            SafeAttachTextToTags(report, oneSixFilename, new[] { TemplateTags.OneSixText });

            var specimen = new SpecimenData(
                Action: Model.SelectedLHActing,
                Insulated: Model.SelectedLHInsulated,
                Glazed: Model.IsLeftHandGlazed,
                Material: Model.SelectedLHMaterial,
                Panels: Model.SelectedLHPanels,
                GlazedOrInfilled: Model.SelectedLHGlazedInfilled,
                Latched: Model.SelectedLHLatched,
                Shootbolts: Model.SelectedLHShootbolts,
                OpensTowardsHeatConditions: Model.IsLeftHandOpeningTowardsHeatConditions);

            report.AttachTextToTags(new[] { TemplateTags.ReportNum }, Model.TestNumber);
            report.AttachTextToTags(new[] { TemplateTags.SponsorName }, Model.SponsorName);
            report.AttachTextToTags(new[] { TemplateTags.Address }, Model.Address);
            report.AttachTextToTags(new[] { TemplateTags.SponsorSummary }, SpecimenSummary.GetSummary(specimen));
            report.AttachTextToTags(new[] { TemplateTags.Date }, GetDateFromReportNumber(Model.TestNumber));

            var templateFilename = $"{contentDir}\\{TemplateFile}";
            var outputFolder = $"{workingDir}\\CreatedTestDocs";

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            var outputFilename = $"{outputFolder}\\{Model.SponsorName}_{Model.TestNumber}_{DateTime.Now:HHmmss}";

            await RunOnStaThreadAsync(() => report.CreateWordDocument(templateFilename, outputFilename));

            MessageBox.Show($"Report output to: {outputFilename}", "Report Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static Task RunOnStaThreadAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();

            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }

        private string GetDateFromReportNumber(string testNum)
        {
            try
            {
                string dateOnly = testNum.Remove(testNum.Length - 1);
                DateTime testDate = DateTime.ParseExact(dateOnly, "yyMMdd", null);

                string suffix = (testDate.Day % 10, testDate.Day % 100) switch
                {
                    (1, not 11) => "st",
                    (2, not 12) => "nd",
                    (3, not 13) => "rd",
                    _           => "th"
                };

                string full = testDate.ToString("d MMMM yyyy");
                return dateOnly[4] == '0' ? full.Insert(1, suffix) : full.Insert(2, suffix);
            }
            catch (Exception)
            {
                return "Invalid test number";
            }
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
