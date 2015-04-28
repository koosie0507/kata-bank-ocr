using KataBankOCR.Tests.Properties;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Linq;

namespace KataBankOCR.Tests.BasicScanning.StepDefinitions
{
    [Binding]
    public class BasicScanningSteps
    {
        private readonly ScanViewModel scanViewModel = new ScanViewModel();

        [Given(@"the machine produces (.*) valid entry\(ies\)")]
        public void GivenTheMachineProducesOneEntry(int entries)
        {
            string fileName = string.Format("FileWith{0}Entries", entries);
            ScenarioContext.Current.Add("file", Resources.ResourceManager.GetString(fileName));
        }

        [When(@"I scan")]
        public void WhenIScanTheEntry()
        {
            scanViewModel.Scan(ScenarioContext.Current.Get<string>("file"));
        }

        [Then(@"the result should (.*) valid bank account number\(s\)")]
        public void ThenTheResultShouldABankAccountNumber(int expectedAccountNumberCount)
        {
            var accountNumbersList = this.scanViewModel.AccountNumbers.ToList();

            Assert.That(accountNumbersList, Has.Count.EqualTo(expectedAccountNumberCount));
            Assert.That(accountNumbersList, Has.All.Matches<string>(x=>x.Length == 9));
        }
    }
}