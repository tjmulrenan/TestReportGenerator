namespace ReportGenerator.Services
{
    public record SpecimenData(
        string? Action,
        string? Insulated,
        bool Glazed,
        string? Material,
        string? Panels,
        string? GlazedOrInfilled,
        string? Latched,
        string? Shootbolts,
        bool OpensTowardsHeatConditions,
        bool IsPVCuFrame = false);
}
