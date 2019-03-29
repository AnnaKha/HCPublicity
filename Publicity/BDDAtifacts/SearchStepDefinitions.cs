using NUnit.Framework;
using Publicity.Pages;
using Publicity.Tests;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace Publicity.BDDAtifacts
{
	[Binding]
	public class SearchStepDefinitions : TestSetUp
	{
		HomePage landingPage = new HomePage();
		SearchForContacts searchPage = new SearchForContacts();

		[Given(@"I have opened Search for Contacts")]
		public void GivenIHaveOpenedSearchForContacts()
		{
			landingPage.OpenSearchContacts();
		}

		[Given(@"search criteria for line (.*) are entered: (.*), (.*), (.*)")]
		public void GivenSearchCriteria__ForLine_AreEntered(int index, string field, string value, string condtion = "and")
		{
			if (field == "Media Type" || field == "Markets" || field == "Topics" || field == "Language")
			{
				searchPage.SetSearchLineForMultiSelectField(index, field, value, condtion);
			}
			else if (field == "National USA")
			{ searchPage.SetSearchLineForNationalField(index, field, value, condtion); }
			else { searchPage.SetSearchLine(index, field, value, condtion); }
		}

		[Given(@"search line (.*) is removed")]
		public void GivenSearchLineIsRemoved(int index)
		{
			searchPage.RemoveSearchLine(index);
		}

		[When(@"click on Search button")]
		public void WhenClickOnSearchButton()
		{
			searchPage.Search.Click();
		}

		[Then(@"default search criteria are displayed")]
		public void ThenDefaultSearchCriteriaAreDisplayed()
		{
			Assert.AreEqual(searchPage.GetSearchLine(1).First(), "Media Type");
			Assert.AreEqual(searchPage.GetSearchLine(1)[1], string.Empty);
			Assert.AreEqual(searchPage.GetSearchLine(1).Last(), "and");

			Assert.AreEqual(searchPage.GetSearchLine(2).First(), "Company");
			Assert.AreEqual(searchPage.GetSearchLine(2)[1], string.Empty);
			Assert.AreEqual(searchPage.GetSearchLine(2).Last(), "and");

			Assert.AreEqual(searchPage.GetSearchLine(3).First(), "Name");
			Assert.AreEqual(searchPage.GetSearchLine(3)[1], string.Empty);
			Assert.AreEqual(searchPage.GetSearchLine(3).Last(), "and");
		}

		[Then(@"search results in table satisfy all searching criteria: name - (.*) and company name - (.*) and email - (.*)")]
		public void ThenSearchResultsInTableSatisfyThreeAndSearchingCriteria(string firstCriteria, string secondCriteria, string thirdCriteria)
		{
			Assert.That(searchPage.ContactsSearch.Rows.First().Name.Text.ToLower().Contains(firstCriteria.ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.First().Company.ToLower().Contains(secondCriteria.ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.First().Email.ToLower().Contains(thirdCriteria.ToLower()));

			Assert.That(searchPage.ContactsSearch.Rows.Last().Name.Text.ToLower().Contains(firstCriteria.ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.Last().Company.ToLower().Contains(secondCriteria.ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.Last().Email.ToLower().Contains(thirdCriteria.ToLower()));
		}

		[Then(@"search results in table satisfy all searching criteria: by Job Title - (.*), (.*), (.*)")]
		public void ThenSearchResultsInTableSatisfyThreeOrSearchingCriteria(string firstCriteria, string secondCriteria, string thirdCriteria)
		{
			ScrollTable();
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.JobTitle.ToLower()).ToList().All(x => x.Contains(firstCriteria.ToLower()) || x.Contains(secondCriteria.ToLower()) || x.Contains(thirdCriteria.ToLower())));
		}

		[Then(@"search results in table satisfy all searching criteria: company name - (.*) and  name - (.*) or name - (.*)")]
		public void ThenSearchResultsInTableSatisfyOneOrAndTwoAndSearchingCriteria(string firstCriteria, string secondCriteria, string thirdCriteria)
		{
			ScrollTable();
			ScrollTable();
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Company.ToLower()).ToList().All(x => x.Contains(firstCriteria.ToLower())));
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Name.Text.ToLower()).Distinct().ToList().Any(x => x.Contains(secondCriteria.ToLower()) && x.Contains(thirdCriteria.ToLower())));
		}

		[Then(@"search results in table satisfy all searching criteria: by National USA - (.*)")]
		public void ThenSearchResultsInTableSatisfyAllSearchingCriteriaByNationalUSA_No(string value)
		{
		searchPage.EditColumnsSelectionForSearchResultsTable("National USA");
		ScrollTable();
		Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.National.ToLower()).ToList().All(x => x.Contains(value.ToLower())));

		}

		[Then(@"search results in table satisfy all searching criteria: by Media Type - (.*) and Markets - (.*)")]
		public void ThenSearchResultsInTableSatisfyAllSearchingCriteriaByMediaType_BlogAndMarkets_First(string mediaType, string markets)
		{
			ScrollTable();
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.MediaTypes.ToLower()).ToList().All(x => x.Contains(mediaType.ToLower())));
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Markets.ToLower()).ToList().All(x => x.Contains(markets.ToLower())));
		}

		private void ScrollTable()
		{
			ngDriver.WaitForAngular();
			ngDriver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
			Thread.Sleep(1000);
		}
	}
}