using Core.Builders;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using Publicity.Pages;
using Publicity.Tests;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace Publicity.BDDSteps
{
	[Binding]
	public class CreateContactStepDefinitions : TestSetUp
	{
		HomePage landingPage = new HomePage();
		SearchForContacts searchPage = new SearchForContacts();
		CreateEditContact createContactPage = new CreateEditContact();
		Contact createdContact;

		[Given(@"I have opened Create new Contact")]
		public void GivenIHaveOpenedCreateNewContact()
		{
			landingPage.OpenCreateNewContact();
		}


		[Given(@"entered all information for contact")]
		public void GivenEnteredAllInformationForContact()
		{
			createdContact = createContactPage.EnterAllDataForContact();
		}

		[Given(@"entered only required information for contact")]
		public void GivenEnteredOnlyRequiredlInformationForContact()
		{
			createdContact = createContactPage.EnterOnlyRequiredDataForContact();
		}

		[Given(@"I press save")]
		public void GivenIPressSave()
		{
			createContactPage.Save.Click();
		}

		[When(@"I press save")]
		public void WhenIPressSave()
		{
			createContactPage.Save.Click();
		}

		[Then(@"check created contact is available in search results")]
		public void CheckCreatedContactIsAvailableInSearchResults()
		{
			landingPage.OpenSearchContacts();
			searchPage.SetSearchLine(1, "Name", createdContact.FirstName);
			searchPage.RemoveSearchLine(3);
			searchPage.RemoveSearchLine(2);
			searchPage.Search.Click();
			ScrollTable();
			Contact newContact = searchPage.GetContactInfoForRow(1);
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Name.Text.ToLower()).ToList().All(x => x.Contains(createdContact.FirstName.ToLower())));
			newContact.Should().BeEquivalentTo(new ContactBuilder().ContactWithDefaultSearchInfo(createdContact).BuildContact());// check when search works
		}


		[Given(@"click on Search button")]
		public void GivenClickOnSearchButton()
		{
			searchPage.Search.Click();
		}

		[Given(@"click edit icon for created contact")]
		public void GivenClickEditIconForLine()
		{
			landingPage.OpenSearchContacts();
			searchPage.SetSearchLine(1, "Name", createdContact.FirstName);
			searchPage.RemoveSearchLine(3);
			searchPage.RemoveSearchLine(2);
			searchPage.Search.Click();
			searchPage.ContactsSearch.Rows.First().EditIcon.Click();
		}

		[Given(@"add additional address")]
		public void GivenAddAdditionalAddress()
		{
			createContactPage.AddAddressForContact(2);
		}

		[Then(@"check edited contact contains additional address")]
		public void ThenCheckEditedContactContainsNewInformation()
		{
			createContactPage.GetContactAdditionalAddress().Should().BeEquivalentTo(new ContactBuilder().WithContactAddress(createdContact).BuildContact());
			//check when search works
		}

		private void ScrollTable()
		{
			ngDriver.WaitForAngular();
			ngDriver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
			Thread.Sleep(1000);
		}
	}
}