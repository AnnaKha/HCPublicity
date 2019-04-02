using Core.Builders;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using Publicity.Pages;
using System.Linq;

namespace Publicity.Tests
{
	[TestFixture]
	public class Test : TestSetUp
	{
		HomePage landingPage = new HomePage();
		SearchForContacts searchPage = new SearchForContacts();
		CreateEditContact contactPage = new CreateEditContact();

		[Test]
		public void ClickContacts()
		{
			landingPage.OpenSearchContacts();
			searchPage.RemoveSearchLine(1);
			searchPage.RemoveSearchLine(2);
			searchPage.SetSearchLine(1, "Name", "s");
			searchPage.Search.Click();
			Assert.IsTrue(searchPage.ContactsSearch.Rows.First().Name.Text.Contains("s"));
		}

		[Test]
		public void GetContactInfo()
		{
			landingPage.OpenSearchContacts();
			searchPage.RemoveSearchLine(1);
			searchPage.RemoveSearchLine(2);
			searchPage.SetSearchLine(1, "Name", "Ojus");
			searchPage.Search.Click();
			string name = searchPage.ContactsSearch.Rows.First().Name.Text;
			Contact firstContact = searchPage.GetContactInfoForRow(1);
			searchPage.ContactsSearch.Rows.First().EditIcon.Click();
			Contact edited = new ContactBuilder().ContactWithDefaultSearchInfo(contactPage.GetContactInfo()).BuildContact();
			Assert.AreEqual(name, $"{edited.LastName}, {edited.FirstName}");
			firstContact.Should().BeEquivalentTo(edited);
		}

		[Test]
		public void CreateContact()
		{
			landingPage.OpenCreateNewContact();
			contactPage.EnterAllDataForContact();
			contactPage.Save.Click();
		}

		[Test]
		public void ThreeAndCriteriaSearch()
		{
			landingPage.OpenSearchContacts();
			searchPage.SetSearchLine(1, "Email", "eugene");
			searchPage.SetSearchLine(2, "Company", "springfield");
			searchPage.SetSearchLine(3, "Name", "simpson");
			searchPage.Search.Click();
			Assert.That(searchPage.ContactsSearch.Rows.First().Name.Text.ToLower().Contains("simpson".ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.First().Company.ToLower().Contains("springfield".ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.First().Email.ToLower().Contains("eugene".ToLower()));

			Assert.That(searchPage.ContactsSearch.Rows.Last().Name.Text.ToLower().Contains("simpson".ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.Last().Company.ToLower().Contains("springfield".ToLower()));
			Assert.That(searchPage.ContactsSearch.Rows.Last().Email.ToLower().Contains("eugene".ToLower()));
		}

		[Test]
		public void CheckAllRowsAgainstSearchCriteria()
		{
			landingPage.OpenSearchContacts();
			searchPage.RemoveSearchLine(1);
			searchPage.RemoveSearchLine(2);
			searchPage.SetSearchLine(1, "Email", "eugene");
			searchPage.Search.Click();
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Email.ToLower()).ToList().All(x=>x.Contains("eugene".ToLower())));
			Assert.That(searchPage.ContactsSearch.Rows.Select(i => i.Email.ToLower()).ToList().All(x => x.Contains("eugene".ToLower()) || x.Contains("eug".ToLower()) || x.Contains("gene".ToLower())));
		}
	}
}