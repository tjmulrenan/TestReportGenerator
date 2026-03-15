using System;

namespace ReportGenerator.Services
{
    public static class InputValidation
    {        
        public static string TodaysTestNumberAsString = DateTime.Now.ToString("yyMMdd") + "2";
       
        public static int TodaysTestNumberAsInt = Convert.ToInt32(TodaysTestNumberAsString);

        public static bool IsValidTestNum(string reportNum)
        {
            if (Int32.TryParse(reportNum, out int ReportNumAsInt))
            {
                if (ReportNumAsInt < TodaysTestNumberAsInt + 1 && ReportNumAsInt > 1700000)
                {
                    return true;
                }
            }

            return false;
        }

        public static string ValidateTestNum(string reportNum)
        {
            if (IsValidTestNum(reportNum))
            {
                return reportNum;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}