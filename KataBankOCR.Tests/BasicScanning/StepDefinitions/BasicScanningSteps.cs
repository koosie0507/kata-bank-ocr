using KataBankOCR.Tests.Properties;
using TechTalk.SpecFlow;
using KataBankOCR;
using NUnit.Framework;
using System.Linq;

namespace KataBankOCR.Tests.BasicScanning.StepDefinitions
{
    [Binding]
    public class BasicScanningSteps
    {
        private readonly ScanViewModel scanViewModel = new ScanViewModel();

        [Given(@"the machine produces one entry")]
        public void GivenTheMachineProducesOneEntry()
        {
            ScenarioContext.Current.Add("file", Resources.OneLine);
        }

        [Given(@"the entry has exactly (.*) lines")]
        public void GivenTheEntryHasExactlyLines(int lines)
        {
            ScenarioContext.Current.ContainsKey("file");
        }

        [Given(@"each line has exactly (.*) characters")]
        public void GivenEachLineHasExactlyCharacters(int chars)
        {
            ScenarioContext.Current.ContainsKey("file");
        }

        [When(@"I scan the entry")]
        public void WhenIScanTheEntry()
        {
            scanViewModel.Scan(ScenarioContext.Current.Get<string>("file"));
        }

        [Then(@"the result should a bank account number")]
        public void ThenTheResultShouldABankAccountNumber()
        {
            Assert.DoesNotThrow(() => scanViewModel.AccountNumbers.Single());
        }

        [Then(@"the account number should be (.*) digits long")]
        public void ThenTheAccountNumberShouldBeDigitsLong(int p0)
        {
            Assert.AreEqual(p0, scanViewModel.AccountNumbers.Single().Length);
        }
    }
}