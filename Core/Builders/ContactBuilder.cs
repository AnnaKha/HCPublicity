namespace Core.Builders
{
	public class ContactBuilder 
	{
		private Contact _contact = new Contact();

		public ContactBuilder WithContactWithAllInfo(Contact contact)
		{
			WithContactInfo(contact);
			WithContactDetails(contact);
			WithContactAddress(contact);
			WithContactSocialMedia(contact);
			return this;
		}

		public ContactBuilder WithContactInfo(Contact contact)
		{
			_contact.FirstName = contact.FirstName;
			_contact.MiddleName = contact.MiddleName;
			_contact.LastName = contact.LastName;
			_contact.CompanyName = contact.CompanyName;
			_contact.JobTitle = contact.JobTitle;
			_contact.ShowName = contact.ShowName;
			_contact.Topics = contact.Topics;
			_contact.Markets = contact.Markets;
			_contact.MediaType = contact.MediaType;
			_contact.CompanyWebsite = contact.CompanyWebsite;
			_contact.Language = contact.Language;
			_contact.Language = contact.Language;
			return this;
		}

		public ContactBuilder WithContactDetails(Contact contact)
		{
			_contact.Email = contact.Email;
			_contact.Mobile = contact.Mobile;
			_contact.Office = contact.Office;
			return this;
		}

		public ContactBuilder WithContactAddress(Contact contact)
		{
			_contact.AddressType = contact.AddressType;
			//_contact.Primary = contact.Primary;
			_contact.POBox = contact.POBox;
			_contact.Zip = contact.Zip;
			_contact.State = contact.State;
			_contact.Country = contact.Country;
			_contact.City = contact.City;
			_contact.Street = contact.Street;
			_contact.Appartment = contact.Appartment;
			_contact.ReasonOfReturnedPackages = contact.ReasonOfReturnedPackages;
			return this;
		}

		public ContactBuilder WithContactSocialMedia(Contact contact)
		{
			_contact.Facebook = contact.Facebook;
			_contact.YouTube = contact.YouTube;
			_contact.Twitter = contact.Twitter;
			_contact.Instagram = contact.Instagram;
			_contact.LinkedIn = contact.LinkedIn;
			return this;
		}

		public ContactBuilder ContactWithRequiredInfo(Contact contact)
		{
			_contact.FirstName = contact.FirstName;
			_contact.LastName = contact.LastName;
			_contact.CompanyName = contact.CompanyName;
			_contact.Email = contact.Email;
			_contact.AddressType = contact.AddressType;
			_contact.State = contact.State;
			_contact.Primary = true;
			return this;
		}

		public ContactBuilder ContactWithDefaultSearchInfo(Contact contact)
		{
			_contact.FirstName = contact.FirstName;
			_contact.LastName = contact.LastName;
			_contact.CompanyName = contact.CompanyName;
			_contact.ShowName = contact.ShowName;
			_contact.JobTitle = contact.JobTitle;
			_contact.MediaType = contact.MediaType;
			_contact.Markets = contact.Markets;
			_contact.Email = contact.Email;
			_contact.Mobile = contact.Mobile;
			_contact.Office = contact.Office;
			return this;
		}
		public Contact BuildContact()
		{
			return _contact;
		}
	}
}