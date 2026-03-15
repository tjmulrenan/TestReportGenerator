using ReportGenerator.Services;
using System;
using System.IO;
using static ReportGenerator.Services.EnumConverter;

namespace ReportGenerator.CLI
{
    class Program
    {

        private const string InvalidSelection = "Invalid selection, please try again";
        private const string TemplateFile = @"Template.docx";

        static void Main(string[] args)
        {

            var cleanupService = new CleanupService(new ProcessService());

            if (cleanupService.HasActiveWordInstances())
            {
                Console.WriteLine("Detected active instances of Word");
                Console.WriteLine("Push Y to kill all instances and continue");
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Y)
                {
                    cleanupService.KillWordInstances();
                    Console.WriteLine("");
                }
                else
                {
                    return;
                }
            }

            var report = new ReportDocument();

            //Console.WriteLine("Welcome to the Report-o-matic!");





            var testNum = AskQuestion("Enter report number:", InputValidation.ValidateTestNum);
            report.AttachTextToTags(new[] { TemplateTags.ReportNum }, testNum);




            //if (isValidTestNum)
            //{
            //    //removes last number from string that has no connection to test date

            //    string testNumDateOnly = testNum.Remove(testNum.Length - 1);

            //    //converts testdate string to a "DateTime" variable

            //    DateTime testDate = DateTime.ParseExact(testNumDateOnly, "yyMMdd", null);

            //    string dateSuffix =
            //        (testDate.Day % 10 == 1 && testDate.Day % 100 != 11) ? "st"
            //        : (testDate.Day % 10 == 2 && testDate.Day % 100 != 12) ? "nd"
            //        : (testDate.Day % 10 == 3 && testDate.Day % 100 != 13) ? "rd"
            //        : "th";

            //    //converts test date to full length date - day suffix

            //    string testDateFull = testDate.ToString("d MMMM yyyy");

            //    //checks if date starts with 0 so it knows where to put the day suffix

            //    string testDateFullWithSuffix = testNumDateOnly[4] == '0' ? testDateFull.Insert(1, dateSuffix) : testDateFull.Insert(2, dateSuffix);
            //    report.AttachTextToTags(new[] { ReportTags.Date }, testDateFullWithSuffix);

            //}


            // reading text file (first 255 chars)

            var specimenStandard = AskQuestion("What standard ? \n1 = EN \n2 = BS", GetStandardType);


            var contentDir = Directory.GetCurrentDirectory() + "\\content";
            var standardizedTextDir = $"{contentDir}\\Standardized Wording_DONOTMODIFY";
            var sameTextDir = $"{standardizedTextDir}\\Same";
            string standardTextDir = string.Empty;

            if (specimenStandard == SpecimenStandardType.EN)
            {
                standardTextDir = @$"{standardizedTextDir}\\EN\\";
            }
            else if (specimenStandard == SpecimenStandardType.BS)
            {
                standardTextDir = @$"{standardizedTextDir}\\BS\\";
            }


            string standard = File.ReadAllText($"{standardTextDir}{TemplateNames.Standard}.txt");
            report.AttachTextToTags(new[] { TemplateTags.Standard }, standard);

            string oneTwoTitle = File.ReadAllText($"{standardTextDir}{TemplateNames.OneTwoTitle}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneTwoTitle }, oneTwoTitle);

            string oneTwoBtmPara = File.ReadAllText($"{standardTextDir}{TemplateNames.OneTwoBtmPara}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneTwoBtmParaA, TemplateTags.OneTwoBtmParaB }, oneTwoBtmPara, true);

            string oneFiveFileName = "1.5.txt";
            string oneFive = File.ReadAllText(standardTextDir + oneFiveFileName);
            report.AttachTextToTags(new[] { TemplateTags.OneFiveA, TemplateTags.OneFiveB }, oneFive, true);







            string SponsorQuestion = "Sponsor name:";
            Console.WriteLine(SponsorQuestion);
            string sponsorName = Console.ReadLine();
            report.AttachTextToTags(new[] { TemplateTags.SponsorName }, sponsorName);


            //Console.WriteLine("Address (eg. Brewery Rd,Pampisford,Cambridge,CB22 3HG):");
            //string sponsorAddress = Console.ReadLine();
            //string sponsorAddressWithReturns = sponsorAddress.Replace(",", ",\r");
            //report.AttachTextToTags(new[] { ReportTags.Address }, sponsorAddressWithReturns);



            var templateFilename = $"{contentDir}\\{TemplateFile}";
            var outputFileName = $"{contentDir}\\CreatedTestDocs\\{sponsorName}_{testNum}_{DateTime.Now.ToString("HHmmss")}";           
            
            
            report.CreateWordDocument(templateFilename, outputFileName);

            Console.WriteLine($"File output to: {outputFileName}");


            Console.ReadLine();

        }

        private static void SetENReportTags(string standardDir, ReportDocument report)
        {
            string oneTwoTitle = File.ReadAllText($"{standardDir}{TemplateNames.OneTwoTitle}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneTwoTitle }, oneTwoTitle);

            string oneTwoTopParaA = File.ReadAllText($"{standardDir}{TemplateNames.OneTwoTopParaA}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneTwoTopParaA }, oneTwoTopParaA);

            string oneTwoBtmPara = File.ReadAllText($"{standardDir}{TemplateNames.OneTwoBtmPara}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneTwoBtmParaA, TemplateTags.OneTwoBtmParaB }, oneTwoBtmPara, true);

            string oneFiveTextA = File.ReadAllText($"{standardDir}{TemplateNames.OneFiveTextA}.txt");
            report.AttachTextToTags(new[] { TemplateTags.OneFiveA }, oneFiveTextA);

            string gapMeasurementsTitle = File.ReadAllText($"{standardDir}{TemplateNames.GapMeasurementsTitle}.txt");
            report.AttachTextToTags(new[] { TemplateTags.GapMeasurementsTitle }, gapMeasurementsTitle);

        }

        private static void SetBSReportTags(string standardDir, ReportDocument report)
        {

        }

        private static void SetGeneralReportTags(string sameTextDir, ReportDocument report)
        {
            //change bool later
            bool isSameSize = false;

            if (isSameSize)
            {
                string oneTwoBtmParaB = File.ReadAllText($"{sameTextDir}{TemplateNames.OneTwoTopParaBIdentical}.txt");
                report.AttachTextToTags(new[] { TemplateTags.OneTwoTopParaB }, oneTwoBtmParaB);
            }
            else
            {
                string oneTwoBtmParaB = File.ReadAllText($"{sameTextDir}{TemplateNames.OneTwoTopParaBDifferent}.txt");
                report.AttachTextToTags(new[] { TemplateTags.OneTwoTopParaB }, oneTwoBtmParaB);
            }

            report.AttachTextToTags(new[] { TemplateTags.OneFiveB }, oneFiveBTextB(true, true, true, true, true));



            //TEST, CHANGE LATER
            bool isSpecimenSampled = false;
            string oneSixText;

            if (isSpecimenSampled)
            {
                oneSixText = File.ReadAllText($"{sameTextDir}{TemplateNames.OneSixTextY}.txt");
            }
            else
            {
                oneSixText = File.ReadAllText($"{sameTextDir}{TemplateNames.OneSixTextN}.txt");
            }

            report.AttachTextToTags(new[] { TemplateTags.OneSixText }, oneSixText);

            

            
            string leafFrameGapTextA = "The gaps between the leaf edges and the frame and between the base of the leaf and the threshold were measured on ";
            //TEST, CHANGE LATER
            string fullLeafFrameGapText = leafFrameGapTextA + leafFrameGapTextB(true, true, true);
            report.AttachTextToTags(new[] { TemplateTags.LeafFrameGapText }, fullLeafFrameGapText);
        }
        public static string leafFrameGapTextB(bool RHSpecimenExists, bool LHSpecimenOpensTowardsFurn, bool RHSpecimenOpensTowardsFurn)
        {

            if (RHSpecimenExists)
            {
                if (LHSpecimenOpensTowardsFurn == RHSpecimenOpensTowardsFurn && LHSpecimenOpensTowardsFurn)
                {
                    return "exposed face of both leaves prior to the start of the test.";
                }
                else if (LHSpecimenOpensTowardsFurn == RHSpecimenOpensTowardsFurn && !LHSpecimenOpensTowardsFurn)
                {
                    return "unexposed face of both leaves prior to the start of the test.";
                }
                if (LHSpecimenOpensTowardsFurn != RHSpecimenOpensTowardsFurn && LHSpecimenOpensTowardsFurn)
                {
                    return "exposed face of the left hand leaf and unexposed face of the right hand leaf prior to the start of the test.";
                }
                else if (LHSpecimenOpensTowardsFurn != RHSpecimenOpensTowardsFurn && !LHSpecimenOpensTowardsFurn)
                {
                    return "unexposed face of the left hand leaf and exposed face of the right hand leaf prior to the start of the test.";
                }
            }
            else if (!RHSpecimenExists)
            {
                if (LHSpecimenOpensTowardsFurn)
                {
                    return "exposed face of the leaf prior to the start of the test.";
                }
                if (LHSpecimenOpensTowardsFurn)
                {
                    return "unexposed face of the leaf prior to the start of the test.";
                }
            }
            return "INVALID GAPTEXT";
        }
        public static string oneFiveBTextB(bool RHSpecimenExists, bool LHSpecimenOpensTowardsFurn, bool RHSpecimenOpensTowardsFurn, bool LHSpecimenLatched, bool RHSpecimenLatched)
        {
            string specimenDirectionText(bool RHSpecimenExists, bool LHSpecimenTowardsFurn, bool RHSpecimenOpensTowardsFurn)
            {
                if (RHSpecimenExists)
                {
                    if (LHSpecimenTowardsFurn == RHSpecimenOpensTowardsFurn && LHSpecimenTowardsFurn)
                    {
                        return "both leaves opened towards the heating conditions of the test.";
                    }
                    else if (LHSpecimenTowardsFurn == RHSpecimenOpensTowardsFurn && !LHSpecimenTowardsFurn)
                    {
                        return "both leaves opened away from the heating conditions of the test.";
                    }
                    else if (LHSpecimenTowardsFurn != RHSpecimenOpensTowardsFurn && LHSpecimenTowardsFurn)
                    {
                        return "the left hand leaf opened towards the heat conditions of the test and right hand leaf opened away from the heat conditions of the test.";
                    }
                    else if (LHSpecimenTowardsFurn != RHSpecimenOpensTowardsFurn && !LHSpecimenTowardsFurn)
                    {
                        return "the left hand leaf opened away from the heat conditions of the test and right hand leaf towards the heat conditions of the test.";
                    }
                }
                else
                {
                    if (LHSpecimenTowardsFurn)
                    {
                        return "the left hand leaf opened towards the heat conditions of the test";
                    }
                    if (!LHSpecimenTowardsFurn)
                    {
                        return "the left hand leaf opened away from the heat conditions of the test";
                    }
                }
                return "INVALIDDIRECTION";
            }

            string specimenLatchingText(bool RHSpecimenExists, bool LHSpecimenLatched, bool RHSpecimenLatched)
            {
                if (RHSpecimenExists)
                {
                    if (LHSpecimenLatched == RHSpecimenLatched && LHSpecimenLatched)
                    {
                        return "The leaves were latched prior to the start of the test.";
                    }
                    else if (LHSpecimenLatched == RHSpecimenLatched && !LHSpecimenLatched)
                    {
                        return "The leaves were unlatched prior to the start of the test.";
                    }
                    if (LHSpecimenLatched != RHSpecimenLatched && LHSpecimenLatched)
                    {
                        return "The left hand leaf was latched and the right hand leaf was unlatched prior to the start of the test.";
                    }
                    else if (LHSpecimenLatched != RHSpecimenLatched && !LHSpecimenLatched)
                    {
                        return "The left hand leaf was unlatched and the right hand leaf was latched prior to the start of the test.";
                    }
                }
                else
                {
                    if (LHSpecimenLatched)
                    {
                        return "The leaf was latched prior to the start of the test.";
                    }
                    else
                    {
                        return "The leaf was unlatched prior to the start of the test.";
                    }
                }
                return "INVALIDLATCHING";
            }
            //TEST, CHANGE LATER
            return specimenDirectionText(RHSpecimenExists, LHSpecimenOpensTowardsFurn, RHSpecimenOpensTowardsFurn) + specimenLatchingText(RHSpecimenExists, LHSpecimenLatched, RHSpecimenLatched);
        }

        public static T AskQuestion<T>(string question, Func<string, T> ValidateAnswer)
        {
            T validAnswer = default(T);

            while (validAnswer == null)
            {
                Console.WriteLine(question);

                validAnswer = ValidateAnswer(Console.ReadLine());

                if (validAnswer == null)
                {
                    Console.WriteLine(InvalidSelection);
                }
            }

            return validAnswer;
        }
    }

}