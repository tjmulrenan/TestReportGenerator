using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{

    public static class EnumConverter
    {
        public enum SpecimenTestType
        {
            Unknown,
            Doorset,
            Partition,
            Ceiling,
            Penetration
        }

        public static SpecimenTestType GetTestType(string specimenTypeNum)
        {

            switch (specimenTypeNum)
            {
                case "1":
                    return SpecimenTestType.Doorset;

                case "2":
                    return SpecimenTestType.Partition;

                case "3":
                    return SpecimenTestType.Ceiling;

                case "4":
                    return SpecimenTestType.Penetration;

                default:
                    return SpecimenTestType.Unknown;
            }
        }

        public enum SpecimenStandardType : int
        {
            Unknown = 0,
            EN = 1,
            BS = 2
        }

        public static SpecimenStandardType? GetStandardType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenStandardType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenStandardType), result))
            {
                var specimenType = (SpecimenStandardType)result;

                if (specimenType != SpecimenStandardType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;

        }

        public enum SpecimenMaterialType : int
        {
            Unknown = 0,
            Timber = 1,
            Steel = 2,
            uPVC = 3
        }


        public static SpecimenMaterialType? GetMaterialType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenMaterialType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenMaterialType), result))
            {
                var specimenType = (SpecimenMaterialType)result;

                if (specimenType != SpecimenMaterialType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }
        public enum SpecimenPanelType : int
        {
            Unknown = 0,
            Overpanels = 1,
            Sidepanels = 2,
            OverpanelsAndSidePanels = 3,
            No = 4,
            Other = 5

        }


        public static SpecimenPanelType? GetPanelType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenPanelType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenPanelType), result))
            {
                var specimenType = (SpecimenPanelType)result;

                if (specimenType != SpecimenPanelType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }

        public static string ToDisplayName(this SpecimenPanelType panelType)
        {
            switch (panelType)
            {
                case SpecimenPanelType.OverpanelsAndSidePanels:
                    return "Overpanels and Side Panels";
            }

            return panelType.ToString();
        }



        public enum SpecimenActingType : int
        {
            Unknown = 0,
            Single = 1,
            Double = 2,
            Fixed = 3
        }


        public static SpecimenActingType? GetActingType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenActingType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenActingType), result))
            {
                var specimenType = (SpecimenActingType)result;

                if (specimenType != SpecimenActingType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }
        public enum SpecimenGlazedInfilledType : int
        {
            Unknown = 0,
            Glazed = 1,
            Infilled = 2,
        }

        public static SpecimenGlazedInfilledType? GetGlazedInfilledType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenGlazedInfilledType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenGlazedInfilledType), result))
            {
                var specimenType = (SpecimenGlazedInfilledType)result;

                if (specimenType != SpecimenGlazedInfilledType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }

        public enum SpecimenInsulatedType : int
        {
            Unknown = 0,
            Insulated = 1,
            Uninsulated = 2,
        }


        public static SpecimenInsulatedType? GetInsulatedType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenInsulatedType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenInsulatedType), result))
            {
                var specimenType = (SpecimenInsulatedType)result;

                if (specimenType != SpecimenInsulatedType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }

        public enum SpecimenLatchedType : int
        {
            Unknown = 0,
            Latched = 1,
            Unlatched = 2,
            LatchedWithTwoPointLock = 3
        }


        public static SpecimenLatchedType? GetLatchedType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenLatchedType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenLatchedType), result))
            {
                var specimenType = (SpecimenLatchedType)result;

                if (specimenType != SpecimenLatchedType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }

        public static string ToDisplayName(this SpecimenLatchedType latchType)
        {
            switch (latchType)
            {
                case SpecimenLatchedType.LatchedWithTwoPointLock:
                    return "Latched with two point lock engaged";
            }

            return latchType.ToString();
        }

        public enum SpecimenShootboltsType : int
        {
            Unknown = 0,
            YesButDisengaged = 1,
            Yes = 2,
            No = 3
        }


        public static SpecimenShootboltsType? GetShootboltsType(string specimenTypeNum)
        {
            Enum.TryParse(typeof(SpecimenShootboltsType), specimenTypeNum, out var result);

            if (result != null && Enum.IsDefined(typeof(SpecimenShootboltsType), result))
            {
                var specimenType = (SpecimenShootboltsType)result;

                if (specimenType != SpecimenShootboltsType.Unknown)
                {
                    return specimenType;
                }
            }

            return null;
        }

        public static string ToDisplayName(this SpecimenShootboltsType shootboltsType)
        {
            switch (shootboltsType)
            {
                case SpecimenShootboltsType.YesButDisengaged:
                    return "Yes but shootbolts disengaged";
            }

            return shootboltsType.ToString();
        }

    }

}