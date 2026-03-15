using NUnit.Framework;
using ReportGenerator.Services;

namespace ReportGenerator.Tests
{
    public class SpecimenSummaryTests
    {
        [Test]
        public void SingleActing_Latched_NoShootbolts_ContainsExpectedText()
        {
            var specimen = new SpecimenData(
                Action: "Single",
                Insulated: "Insulated",
                Glazed: false,
                Material: "Timber",
                Panels: "No",
                GlazedOrInfilled: "Glazed",
                Latched: "Latched",
                Shootbolts: "No",
                OpensTowardsHeatConditions: true);

            var result = SpecimenSummary.GetSummary(specimen);

            Assert.That(result, Does.Contain("single acting"));
            Assert.That(result, Does.Contain("insulated"));
            Assert.That(result, Does.Contain("timber"));
            Assert.That(result, Does.Contain("tested latched"));
        }

        [Test]
        public void DoubleActing_TwoPointLock_ShootboltsEngaged_ContainsExpectedText()
        {
            var specimen = new SpecimenData(
                Action: "Double",
                Insulated: "Uninsulated",
                Glazed: true,
                Material: "Steel",
                Panels: "Overpanels",
                GlazedOrInfilled: "Glazed",
                Latched: "Latched with two point lock engaged",
                Shootbolts: "Yes",
                OpensTowardsHeatConditions: false);

            var result = SpecimenSummary.GetSummary(specimen);

            Assert.That(result, Does.Contain("double acting"));
            Assert.That(result, Does.Contain("glazed"));
            Assert.That(result, Does.Contain("automatic locks engaged"));
            Assert.That(result, Does.Contain("shootbolts engaged"));
        }

        [Test]
        public void Glazed_True_ContainsGlazedInSummary()
        {
            var specimen = new SpecimenData(
                Action: "Fixed",
                Insulated: "Insulated",
                Glazed: true,
                Material: "Timber",
                Panels: "No",
                GlazedOrInfilled: "Infilled",
                Latched: "Unlatched",
                Shootbolts: "No",
                OpensTowardsHeatConditions: false);

            var result = SpecimenSummary.GetSummary(specimen);

            Assert.That(result, Does.Contain("glazed"));
        }

        [Test]
        public void OpensTowardsHeat_True_ContainsRightHandOpeningIntoFurnace()
        {
            var specimen = new SpecimenData(
                Action: "Single",
                Insulated: "Insulated",
                Glazed: false,
                Material: "Timber",
                Panels: "No",
                GlazedOrInfilled: "Glazed",
                Latched: "Latched",
                Shootbolts: "No",
                OpensTowardsHeatConditions: true);

            var result = SpecimenSummary.GetSummary(specimen);

            Assert.That(result, Does.Contain("right hand leaf opening into the furnace"));
        }

        [Test]
        public void OpensAwayFromHeat_ContainsLeftHandOpeningIntoFurnace()
        {
            var specimen = new SpecimenData(
                Action: "Single",
                Insulated: "Insulated",
                Glazed: false,
                Material: "Timber",
                Panels: "No",
                GlazedOrInfilled: "Glazed",
                Latched: "Latched",
                Shootbolts: "No",
                OpensTowardsHeatConditions: false);

            var result = SpecimenSummary.GetSummary(specimen);

            Assert.That(result, Does.Contain("left hand leaf opening into the furnace"));
        }
    }
}
