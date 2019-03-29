using Core.Builders;
using Core.WebElements;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Publicity.TestData;
using System;

namespace Publicity.Pages
{
	public class CreateEditContact
	{
		private readonly static string inputXpath = "/following-sibling::input";

		//Contact info section
		public Input FirstName = new Input(By.XPath($"//label[text()='First Name']{inputXpath}"));
		public Input MiddleName = new Input(By.XPath($"//label[text()='Middle Name']{inputXpath}"));
		public Input LastName = new Input(By.XPath($"//label[text()='Last Name']{inputXpath}"));
		public Input CompanyName = new Input(By.XPath($"//label[text()='Company Name']{inputXpath}"));
		public Input JobTitle = new Input(By.XPath($"//label[text()='Job Title']{inputXpath}"));
		public Input ShowName = new Input(By.XPath($"//label[text()='Show Name']{inputXpath}"));
		public MultiSelect Topics = new MultiSelect(By.XPath($"//label[text()='Topics']{inputXpath}"));
		public MultiSelect Markets = new MultiSelect(By.XPath($"//label[text()='Markets']{inputXpath}"));
		public MultiSelect MediaType = new MultiSelect(By.XPath($"//label[text()='Media Type']{inputXpath}"));
		public Input CompanyWebsite = new Input(By.XPath($"//label[text()='Company Website']{inputXpath}"));
		public MultiSelect Language = new MultiSelect(By.XPath($"//label[text()='Language']{inputXpath}"));
		public ClickElement National = new ClickElement(By.XPath("//app-checkbox[@label='National']"));

		//Contact details section
		public Input Email = new Input(By.XPath($"//label[text()='Email']{inputXpath}"));
		public Input Mobile = new Input(By.XPath($"//label[text()='Mobile']{inputXpath}"));
		public Input Office = new Input(By.XPath($"//label[text()='Office']{inputXpath}"));

		//Contact address section
		public Select AddressType = new Select(By.XPath(".//label[text()='Address Type']"));
		public ClickElement Primary = new ClickElement(By.XPath(".//app-checkbox[@label='Primary']"));
		public Input POBox = new Input(By.XPath($".//label[text()='PO Box']{inputXpath}"));
		public Select Country = new Select(By.XPath(".//label[text()='Country']"));
		public Select State = new Select(By.XPath(".//label[text()='State']"));
		public Input City = new Input(By.XPath($".//label[text()='City']{inputXpath}"));
		public Input Street = new Input(By.XPath($".//label[text()='Street']{inputXpath}"));
		public Input Appartment = new Input(By.XPath($".//label[text()='Suit/Floor/Apt']{inputXpath}"));
		public Input Zip = new Input(By.XPath($".//label[text()='Zip']{inputXpath}"));
		public Select ReasonOfReturnedPackages = new Select(By.XPath(".//label[text()='Reason of Returned Package(Make select)']"));
		public ClickElement Validate = new ClickElement(By.XPath(".//input[@value='Validate']"));
		public ClickElement Remove = new ClickElement(By.XPath(".//input[@value='Remove']"));

		//Social media section
		public Input Facebook = new Input(By.XPath($"//label[text()='Facebook']{inputXpath}"));
		public Input Instagram = new Input(By.XPath($"//label[text()='Instagram']{inputXpath}"));
		public Input Twitter = new Input(By.XPath($"//label[text()='Twitter']{inputXpath}"));
		public Input LinkedIn = new Input(By.XPath($"//label[text()='LinkedIn']{inputXpath}"));
		public Input YouTube = new Input(By.XPath($"//label[text()='YouTube']{inputXpath}"));

		//Pitching profile and Comment section
		public Input PitchingProfile = new Input(By.XPath("(//textarea)[1]"));
		public Input Comment = new Input(By.XPath("(//textarea)[2]"));

		public ClickElement Save = new ClickElement(By.XPath("//input[@type='submit']"));
		public ClickElement Reset = new ClickElement(By.XPath("//input[@type='reset']"));
		public ClickElement AddAdditionalAddresses = new ClickElement(By.XPath("//input[@value='+ Add Additional Addresses']"));

		public Contact GetContactInfo()
		{
			Contact contactDetails = new Contact();
			contactDetails.FirstName = FirstName.GetValue;
			contactDetails.MiddleName = MiddleName.GetValue;
			contactDetails.LastName = LastName.GetValue;
			contactDetails.CompanyName = CompanyName.GetValue;
			contactDetails.JobTitle = JobTitle.GetValue;
			contactDetails.ShowName = ShowName.GetValue;
			contactDetails.Topics = Topics.SelectedValues();
			contactDetails.Markets = Markets.SelectedValues();
			contactDetails.MediaType = MediaType.SelectedValues();
			contactDetails.CompanyWebsite = CompanyWebsite.GetValue;
			contactDetails.Language = Language.SelectedValues();
			contactDetails.National = National.IsSelected;

			contactDetails.Email = Email.GetValue;
			contactDetails.Mobile = Mobile.GetValue;
			contactDetails.Office = Office.GetValue;

			if (AddressType.IsDisplayed())
			{
				contactDetails.AddressType = AddressType.SelectedOption();
				contactDetails.Primary = Primary.IsSelected;
				contactDetails.POBox = POBox.GetValue;
				contactDetails.Country = Country.SelectedOption();
				contactDetails.State = State.SelectedOption();
				contactDetails.City = City.GetValue;
				contactDetails.Street = Street.GetValue;
				contactDetails.Appartment = Appartment.GetValue;
				contactDetails.Zip = Zip.GetValue;
				contactDetails.ReasonOfReturnedPackages = ReasonOfReturnedPackages.SelectedOption();
			}

			contactDetails.Facebook = Facebook.GetValue;
			contactDetails.Instagram = Instagram.GetValue;
			contactDetails.Twitter = Twitter.GetValue;
			contactDetails.LinkedIn = LinkedIn.GetValue;
			contactDetails.YouTube = YouTube.GetValue;

			contactDetails.PitchingProfile = PitchingProfile.GetValue;
			if (Comment.IsDisplayed())
			{
				contactDetails.Comment = PitchingProfile.GetValue;
			}
			return contactDetails;
		}

		public Contact EnterOnlyRequiredDataForContact()
		{
			var rawTestData = JObject.Parse(TestDataFiles.ContactTestData);
			Contact newContact = rawTestData["NewContact"][0].ToObject<Contact>();
			Contact contactWithReqiredInfo = new ContactBuilder().ContactWithRequiredInfo(newContact).BuildContact();
			string uniqueName = contactWithReqiredInfo.FirstName + Guid.NewGuid();

			FirstName.SetText(uniqueName);
			LastName.SetText(contactWithReqiredInfo.LastName);
			CompanyName.SetText(contactWithReqiredInfo.LastName);
			Email.SetText(contactWithReqiredInfo.Email);
			AddressType.SelectByText(contactWithReqiredInfo.AddressType);
			State.SelectByText(contactWithReqiredInfo.State);
			Primary.Click();

			contactWithReqiredInfo.FirstName = uniqueName;
			return contactWithReqiredInfo;
		}

		public Contact EnterAllDataForContact()
		{
			var rawTestData = JObject.Parse(TestDataFiles.ContactTestData);
			Contact newContact = rawTestData["NewContact"][0].ToObject<Contact>();
			Contact contactWithAllInfo = new ContactBuilder().WithContactWithAllInfo(newContact).BuildContact();

			string uniqueName = contactWithAllInfo.FirstName + Guid.NewGuid();
			FirstName.SetText(uniqueName);
			MiddleName.SetText(contactWithAllInfo.MiddleName);
			LastName.SetText(contactWithAllInfo.LastName);
			CompanyName.SetText(contactWithAllInfo.LastName);
			JobTitle.SetText(contactWithAllInfo.JobTitle);
			ShowName.SetText(contactWithAllInfo.ShowName);
			Topics.SetValue(contactWithAllInfo.Topics);
			MediaType.SetValue(contactWithAllInfo.MediaType);
			Markets.SetValue(contactWithAllInfo.Markets);
			CompanyWebsite.SetText(contactWithAllInfo.CompanyWebsite);
			Language.SetValue(contactWithAllInfo.Language);
			if(contactWithAllInfo.National==true)
			{ National.Click(); };

			Email.SetText(contactWithAllInfo.Email);
			Mobile.SetText(contactWithAllInfo.Mobile);
			Office.SetText(contactWithAllInfo.Office);

			AddressType.SelectByText(contactWithAllInfo.AddressType);
			Primary.Click();
			POBox.SetText(contactWithAllInfo.POBox);
			Country.SelectByText(contactWithAllInfo.Country);
			State.SelectByText(contactWithAllInfo.State);
			City.SetText(contactWithAllInfo.City);
			Street.SetText(contactWithAllInfo.Street);
			Appartment.SetText(contactWithAllInfo.Appartment);
			Zip.SetText(contactWithAllInfo.Zip);
			ReasonOfReturnedPackages.SelectByText(contactWithAllInfo.ReasonOfReturnedPackages);

			Facebook.SetText(contactWithAllInfo.Facebook);
			Instagram.SetText(contactWithAllInfo.Instagram);
			Twitter.SetText(contactWithAllInfo.Twitter);
			LinkedIn.SetText(contactWithAllInfo.LinkedIn);
			YouTube.SetText(contactWithAllInfo.YouTube);

			contactWithAllInfo.FirstName = uniqueName;
			return contactWithAllInfo;
		}

		public Contact AddAddressForContact(int index=1)
		{
			var rawTestData = JObject.Parse(TestDataFiles.ContactTestData);
			Contact newContact = rawTestData["AdditionalAddress"][0].ToObject<Contact>();
			Contact contactWithAddress = new ContactBuilder().WithContactAddress(newContact).BuildContact();
			AddAdditionalAddresses.Click();

			Element addressSection = new Element(By.XPath($"(//app-edit-address)[{index}]"));
			addressSection.Child<Select>(AddressType.GetSelector()).SelectByText(contactWithAddress.AddressType);
			addressSection.Child(Primary.GetSelector()).Click();
			addressSection.Child<Input>(POBox.GetSelector()).SetText(contactWithAddress.POBox);
			addressSection.Child<Select>(Country.GetSelector()).SelectByText(contactWithAddress.Country);
			addressSection.Child<Select>(State.GetSelector()).SelectByText(contactWithAddress.State);
			addressSection.Child<Input>(City.GetSelector()).SetText(contactWithAddress.City);
			addressSection.Child<Input>(Street.GetSelector()).SetText(contactWithAddress.Street);
			addressSection.Child<Input>(Appartment.GetSelector()).SetText(contactWithAddress.Appartment);
			addressSection.Child<Input>(Zip.GetSelector()).SetText(contactWithAddress.Zip);
			addressSection.Child<Select>(ReasonOfReturnedPackages.GetSelector()).SelectByText(contactWithAddress.ReasonOfReturnedPackages);
			return contactWithAddress;
		}

		public Contact GetContactAdditionalAddress(int index = 1)
		{
			Contact contact = new Contact();
			Element addressSection = new Element(By.XPath($"(//app-edit-address)[{index}]"));
			contact.AddressType = addressSection.Child<Select>(AddressType.GetSelector()).GetValue;
			addressSection.Child(Primary.GetSelector()).Click();
			contact.POBox = addressSection.Child<Input>(POBox.GetSelector()).GetValue;
			contact.Country = addressSection.Child<Select>(Country.GetSelector()).GetValue;
			contact.State = addressSection.Child<Select>(State.GetSelector()).GetValue;
			contact.City = addressSection.Child<Input>(City.GetSelector()).GetValue;
			contact.Street = addressSection.Child<Input>(Street.GetSelector()).GetValue;
			contact.Appartment = addressSection.Child<Input>(Appartment.GetSelector()).GetValue;
			contact.Zip = addressSection.Child<Input>(Zip.GetSelector()).GetValue;
			contact.ReasonOfReturnedPackages = addressSection.Child<Select>(ReasonOfReturnedPackages.GetSelector()).GetValue;
			Contact address = new ContactBuilder().WithContactAddress(contact).BuildContact();

			return address;
		}
	}
}
