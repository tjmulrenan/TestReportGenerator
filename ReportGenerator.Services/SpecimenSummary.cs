namespace ReportGenerator.Services
{
    public static class SpecimenSummary
    {
        public static string GetSummary(SpecimenData specimen)
        {
            string actionResult = specimen.Action switch
            {
                "Single" => "single acting",
                "Double" => "double acting",
                "Fixed"  => "fixed",
                _        => "UNKNOWNACTIONRESULT"
            };

            string glazedResult = specimen.Glazed ? "glazed " : "";
            string pvcResult = specimen.IsPVCuFrame ? "leaves in PVCu frames" : "doorsets";

            string panelsResult = specimen.Panels switch
            {
                "Overpanels"                 => $"with {specimen.GlazedOrInfilled?.ToLower()} overpanels ",
                "Sidepanels"                 => $"with {specimen.GlazedOrInfilled?.ToLower()} side panels ",
                "Overpanels and Side Panels" => $"with {specimen.GlazedOrInfilled?.ToLower()} overpanels and side panels ",
                "No"                         => "",
                "Other"                      => "INSERT MORE SPECIFIC TEXT",
                _                            => "UNKNOWNOVERPANELRESULT"
            };

            string latchedResult = specimen.Latched switch
            {
                "Latched"                             => ", tested latched",
                "Latched with two point lock engaged" => ", tested latched with automatic locks engaged",
                "Unlatched"                           => ", tested unlatched",
                _                                     => "UNKNOWNLATCHEDRESULT"
            };

            string shootboltsResult = specimen.Shootbolts switch
            {
                "Yes"                           => "shootbolts engaged, and with ",
                "Yes but shootbolts disengaged" => "shootbolts disengaged, and with ",
                "No"                            => "",
                _                               => "UNKNOWNSHOOTBOLTS"
            };

            string conjunction = (specimen.Latched, specimen.Shootbolts) switch
            {
                ("Latched",                             "Yes")                           => " with ",
                ("Latched",                             "No")                            => " with ",
                ("Unlatched",                           "Yes but shootbolts disengaged") => " with ",
                ("Unlatched",                           "No")                            => " with ",
                ("Latched",                             "Yes but shootbolts disengaged") => " but with ",
                ("Unlatched",                           "Yes")                           => " but with ",
                ("Latched with two point lock engaged", "No")                            => "",
                ("Latched with two point lock engaged", "Yes")                           => " and ",
                ("Latched with two point lock engaged", "Yes but shootbolts disengaged") => " and with ",
                _                                                                        => "UNKNOWNCONNECTOR"
            };

            string heatConditionsResult = specimen.OpensTowardsHeatConditions
                ? "the right hand leaf opening into the furnace and the left hand leaf opening out of the furnace"
                : "the left hand leaf opening into the furnace and the right hand leaf opening out of the furnace";

            return $"Two {actionResult} single leaf {specimen.Insulated?.ToLower()} {glazedResult}{specimen.Material?.ToLower()} {pvcResult}{latchedResult}{conjunction}{shootboltsResult}{heatConditionsResult}.";
        }
    }
}
