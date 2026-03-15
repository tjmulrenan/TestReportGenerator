using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{
    public static class TemplateTags
    {
        public const string ReportNum = "<ReportNum>";
        public const string SponsorName = "<SponsorName>";
        public const string Address = "<Address>";
        public const string Date = "<Date>";
        public const string Standard = "<Standard>";
        public const string LHSpecimen = "<LHSpecimen>";
        public const string RHSpecimen = "<RHSpecimen>";
        public const string SponsorSummary = "<Summary>";
        public const string OneTwoTitle = "<1.2Title>";
        public const string OneTwoTopParaA = "<1.2TopParaA>";
        public const string OneTwoTopParaB = "<1.2TopParaB>";
        public const string OneTwoBtmParaA = "<1.2BtmParaA>";
        public const string OneTwoBtmParaB = "<1.2BtmParaB>";
        public const string OneFiveA = "<1.5TextA>";
        public const string OneFiveB = "<1.5TextB>";
        public const string OneSixText = "<1.6>";
        public const string MechPreTestTitle = "<MechPreTestTitle>";
        public const string MechPreTestTextA = "<MechPreTestTextA>";
        public const string MechPreTestTextB = "<MechPreTestTextB>";
        public const string GapMeasurementsTitle = "<GapMeasurementsTitle>";
        public const string LeafFrameGapTitle = "<LeafFrameGapTitle>";
        public const string LeafFrameGapText = "<LeafFrameGapText>";
        public const string LHLeafFramePos = "<LHLeafFramePos>";
        public const string RHLeafFramePos = "<RHLeafFramePos>";
        public const string MaximumGapsTitle = "<MaximumGapsTitle>";
        public const string MaximumGapsText = "<MaximumGapsText>";
        public const string LeafStopGapTitle = "<LeafStopGapTitle>";
        public const string LeafStopGapA = "<LeafStopGapTextA>";
        public const string LeafStopGapB = "<LeafStopGapTextB>";
        public const string LHLeafStopPos = "<LHLeafStopPos>";
        public const string RHLeafStopPos = "<RHLeafStopPos>";
        public const string FrameConstructionGapTitle = "<FrameConstructionGapTitle>";
        public const string ConstructionStandard = "<ConstructionStandard>";
        public const string ENCloserText = "<ENCloserText>";
        public const string BSCloserText = "<BSCloserText>";
        public const string TwoFourFinalSettingTitle = "<2.4FinalSettingTitle>";
        public const string TwoFourFinalSettingText = "<2.4FinalSettingText>";
        public const string LHUnexTCText = "<LHUnexTCText>";
        public const string RHUnexTCText = "<RHUnexTCText>";
        public const string LHObsText = "<LHObsText>";
        public const string RHObsText = "<RHObsText>";
        public const string LimitationsOneTextA = "<LimitationsOneTextA>";
        public const string LimitationsOneTextB = "<LimitationsOneTextB>";


    }

    public static class TemplateNames
    {
        public const string Standard = "Standard";
        public const string OneTwoTitle = "1.2Title";
        public const string OneTwoTopParaA = "1.2TopParaA";
        public const string OneTwoTopParaBDifferent = "1.2TopParaBDifferent";
        public const string OneTwoTopParaBIdentical = "1.2TopParaBIdentical";        
        public const string OneTwoBtmPara = "1.2BtmPara";
        public const string OneFiveTextA = "1.5TextA";
        public const string OneSixTextY = "1.6Y";
        public const string OneSixTextN = "1.6N";
        public const string MechPreTestTitle = "MechPreTestTitle";
        public const string MechPreTestText = "MechPreTestText";
        public const string GapMeasurementsTitle = "GapMeasurementsTitle";
        public const string LeafFrameGapTitle = "LeafFrameGapTitle";
        public const string LeafFrameGapText = "LeafFrameGapText";
        public const string MaximumGapsTitle = "MaximumGapsTitle";
        public const string MaximumGapsText = "MaximumGapsText";
        public const string LeafStopGapTitle = "LeafStopGapTitle";
        public const string LeafStopGapText = "LeafStopGapText";
        public const string FrameConstructionGapTitle = "FrameConstructionGapTitle";
        public const string ConstructionStandard = "ConstructionStandard";
        public const string ENCloserText = "ENCloserText";
        public const string BSCloserText = "BSCloserText";
        public const string TwoFourFinalSettingTitle = "2.4FinalSettingTitle";
        public const string TwoFourFinalSettingText = "2.4FinalSettingText";
        public const string UnexTCText = "UnexTCText";
        public const string ObsText = "LHObsText";
        public const string LimitationsOneText = "Limitations1Text";
    }
}
