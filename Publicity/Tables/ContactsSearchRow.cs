using Core.Tables;
using OpenQA.Selenium;

namespace Publicity.Tables
{
	public class ContactsSearchRow : RowBase
	{
		public ContactsSearchRow(IWebElement element) : base(element) { }
		public IWebElement Checkbox => GetElement(0, "input");
		public IWebElement Name => GetElement(1, ".//a");
		public string Company => GetTdText(2);
		public string ShowName => GetTdText(3);
		public string JobTitle => GetTdText(4);
		public string MediaTypes => GetTdText(5);
		public string Markets => GetTdText(6);
		public string Email => GetTdText(7);
		public string MobileNumber => GetTdText(8);
		public string OfficeNumber => GetTdText(9);
		public string ListSpecific => GetTdText(10);
		public string Notes => GetTdText(11);
		public IWebElement EditIcon => GetElement(12, ".//i[contains(@class, 'fa-pencil')]");
		public string National => GetTdText(12);
	}
}
