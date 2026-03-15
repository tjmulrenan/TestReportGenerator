using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{
    public class SpecimenSummary
    {
     //   public static string Summary => GetSummary("fixed", true, true, "Timber", "Overpanels",false , "Yes", "Yes", true);
        public static string GetSummary(string action,
            string insulatedResult,
            bool glazed,
            string materialResult,
            string overpanelOrSidepanels,
            string glazedOrInfilled,
            string latched,
            string shootbolts,
            bool opensTowardsHeatConditions,
            bool isPVCuFrame = false)
        {

            string actionResult = "";
            if (action == "Single")
            {
                actionResult = $"{action.ToLower()} acting";
            }
            else if (action == "Double")
            {
                actionResult = ($"{action.ToLower()} acting");
            }
            else if (action == "Fixed")
            {
                actionResult = ($"{action.ToLower()}");
            }
            else
            {
                actionResult = ($"UNKNOWNACTIONRESULT");
            }

            string glazedResult = "";
            var glaze = glazed ? glazedResult = "glazed " : glazedResult = "";

                       
            string PVCResult = "";
            var PVC = isPVCuFrame ? PVCResult = "leaves in PVCu frames" : PVCResult = "doorsets";

            string otherText = "INSERT MORE SPECIFIC TEXT";

            string overpanelOrSidePanelsResult;
            if (overpanelOrSidepanels == "Overpanels")
            {
                overpanelOrSidePanelsResult = $"with {glazedOrInfilled} overpanels ";
            }
            else if (overpanelOrSidepanels == "Side panels")
            {
                overpanelOrSidePanelsResult = $"with {glazedOrInfilled} side panels ";
            }
            else if (overpanelOrSidepanels == "Overpanels/side panels ")
            {
                overpanelOrSidePanelsResult = $"with {glazedOrInfilled} overpanels and side panels ";
            }
            else if (overpanelOrSidepanels == "No")
            {
                overpanelOrSidePanelsResult = "";
            }
            else if (overpanelOrSidepanels == "Other")
            {
                overpanelOrSidePanelsResult = otherText;
            }

            else
            {
                overpanelOrSidePanelsResult = "UNKNOWNOVERPANELRESULT";
            }

            string latchedResult = "";

            if (latched == "Latched")
            {
                latchedResult = ", tested latched";
            }
            else if (latched == "Latched with two point lock engaged")
            {
                latchedResult = ", tested latched with automatic locks engaged";
            }
            else if (latched == "Unlatched")
            {
                latchedResult = ", tested unlatched";
            }
            else if (latched == "No latch fitted")
            {
                latchedResult = ", with no latch fitted";
            }
            else
            {
                latchedResult = "UNKNOWNLATCHEDRESULT";
            }

            string shootboltsResult;

            if (shootbolts == "Yes")
            {
                shootboltsResult = "shootbolts engaged, and with ";
            }
            else if (shootbolts == "Yes but shootbolts disengaged")
            {
                shootboltsResult = "shootbolts disengaged, and with";
            }
            else if (shootbolts == "No")
            {
                shootboltsResult = "";
            }
            else
            {
                shootboltsResult = "UNKNOWNSHOOTBOLTS";
            }

            string conjunction = "";

            if ((latched == "Latched" && shootbolts == "Yes") 
                || (latched == "Unlatched" && shootbolts == "Yes but shootbolts disengaged")
                || (latched == "Unlatched" && shootbolts == "No")
                || (latched == "Latched" && shootbolts == "No"))

            {
                conjunction = " with ";
            }
            else if ((latched == "Latched" && shootbolts == "Yes but shootbolts disengaged") 
                || (latched == "Unlatched" && shootbolts == "Yes") 
                || (latched == "No latched fitted" && shootbolts == "Yes"))
            {
                conjunction = " but with ";
            }
            else if ((latched == "Latched" && shootbolts == "") 
                || (latched == "Latched with two point lock engaged" && shootbolts == "No") 
                || (latched == "No latched fitted" && shootbolts == "No"))
            {
                conjunction = "";
            }
            else if (latched == "Latched with two point lock engaged" && shootbolts == "Yes")
            {
                conjunction = " and ";
            }
            else if ((latched == "Latched with two point lock engaged" && shootbolts == "Yes but shootbolts disengaged") 
                || (latched == "No latched fitted" && shootbolts == "Yes but disengaged"))
            {
                conjunction = "and with";
            }
            else
            {
                conjunction = "UNKNOWNCONNECTOR";
            }
        


            string opensTowardsHeatconditionsResult = "";

            var isTowardsHeatConditions = !opensTowardsHeatConditions ? opensTowardsHeatconditionsResult = "the left hand leaf opening into the furnace and the right hand leaf opening out of the furnace" 
                : opensTowardsHeatconditionsResult = " the right hand leaf opening into the furnace and the left hand leaf opening out of the furnace";

            StringBuilder summary = new StringBuilder("");

            summary.Append($"Two {actionResult} single leaf {insulatedResult?.ToLower()} {glazedResult}{materialResult?.ToLower()} {PVCResult}{latchedResult}{conjunction}{shootboltsResult}{opensTowardsHeatconditionsResult}.");

            return summary.ToString();

        }
    }
}