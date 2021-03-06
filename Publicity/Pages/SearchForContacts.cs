﻿using Core.Models;
using Core.WebElements;
using OpenQA.Selenium;
using Publicity.Tables;
using System.Collections.Generic;
using System.Linq;

namespace Publicity.Pages
{
	public class SearchForContacts
	{
		public const string fieldXpath = "(//label[text()='Field'])";
		public const string valueXpath = "(//label[text()='Value'])";
		public const string conditionXpath = "(//label[text()='Condition'])";

		public ClickElement AddSearchLineIcon = new ClickElement(By.XPath("//i[contains(@class,'fa-plus-circle')]"));
		public ClickElement Search = new ClickElement(By.XPath("//button[@type='submit']"));
		public ClickElement Reset = new ClickElement(By.XPath("//button[@type='reset']"));
		public ContactsSearchTable ContactsSearch = new ContactsSearchTable(By.TagName("table"));
		public ClickElement ManageColumns = new ClickElement(By.TagName("app-manage-columns"));

		public void AddOneMoreSearcLine()
		{
			AddSearchLineIcon.Click();
		}

		public void SetSearchLine(int index, string fieldName, string criteria, string conditionValue = "and")
		{
			Select field = new Select(By.XPath($"{fieldXpath}[{index}]"));
			Input value = new Input(By.XPath($"{valueXpath}[{index}]/following-sibling::input"));
			Select condition = new Select(By.XPath($"{conditionXpath}[{index}]"));
			field.SelectByText(fieldName);
			value.SetText(criteria);
			condition.SelectByText(conditionValue);
			field.Click();
		}

		public void SetSearchLineForMultiSelectField(int index, string fieldName, string criteria, string conditionValue = "and")
		{
			Select field = new Select(By.XPath($"{fieldXpath}[{index}]"));
			MultiSelect value = new MultiSelect(By.XPath($"{valueXpath}[{index}]/following-sibling::input"));
			Select condition = new Select(By.XPath($"{conditionXpath}[{index}]"));
			field.SelectByText(fieldName);
			value.SetValue(criteria);
			condition.SelectByText(conditionValue);
			field.Click();
		}

		public void SetSearchLineForNationalField(int index, string fieldName, string criteria, string conditionValue = "and")
		{
			Select field = new Select(By.XPath($"{fieldXpath}[{index}]"));
			ClickElement value = new ClickElement(By.XPath($"//app-radio-group//label[text()=' {criteria} ']/.."));
			Select condition = new Select(By.XPath($"{conditionXpath}[{index}]"));
			field.SelectByText(fieldName);
			value.Click();
			condition.SelectByText(conditionValue);
			field.Click();
		}

		public List<string> GetSearchLine(int index)
		{
			Select field = new Select(By.XPath($"{fieldXpath}[{index}]"));
			Input value = new Input(By.XPath($"{valueXpath}[{index}]/following-sibling::input"));
			Select condition = new Select(By.XPath($"{conditionXpath}[{index}]"));
			List<string> searchCriteria = new List<string>
			{
				field.SelectedOption(),
				value.GetValue,
				condition.SelectedOption()
			};
			return searchCriteria;
		}

		public void RemoveSearchLine(int index)
		{
			ClickElement removeIcon = new ClickElement(By.XPath($"(//i[contains(@class,'fa-minus-circle')])[{index}]"));
			removeIcon.Click();
		}

		public Contact GetContactInfoForRow(int index)
		{
			string[] fullName = ContactsSearch.Rows[index - 1].Name.Text.Split(',');
			Contact row = new Contact();
			row.FirstName = fullName[1].Trim();
			row.LastName = fullName[0].Trim();
			row.CompanyName = ContactsSearch.Rows[index - 1].Company;
			row.ShowName = ContactsSearch.Rows[index - 1].ShowName;
			row.JobTitle = ContactsSearch.Rows[index - 1].JobTitle;
			row.MediaType = ContactsSearch.Rows[index - 1].MediaTypes;
			row.Markets = ContactsSearch.Rows[index - 1].Markets;
			row.Email = ContactsSearch.Rows[index - 1].Email;
			row.Mobile = ContactsSearch.Rows[index - 1].MobileNumber;
			row.Office = ContactsSearch.Rows[index - 1].OfficeNumber;
			switch (ContactsSearch.Rows[index - 1].ListSpecific)
			{
				case "No":
					row.ListSpecific = "Public";
					break;
				case "Yes":
					row.ListSpecific = "List Specific";
					break;
				default:
					row.ListSpecific = string.Empty;
					break;
			};
			if (row.Mobile == string.Empty)
			{ row.Mobile = "+___-___-____"; }
			if (row.Office == string.Empty)
			{ row.Office = "+___-___-____"; }
			return row;
		}

		public void EditColumnsSelectionForSearchResultsTable(string columnName)
		{
			ManageColumns.Click();
			ClickElement column = new ClickElement(By.XPath($"//div[contains(@class, 'manage-columns-alert')]//div[text()='{columnName}']/ancestor::div[contains(@class, 'manage-columns-alert')]"));
			column.Child(By.XPath("./following-sibling::div[contains(@class, 'manage-columns-alert')]//input")).Click();
		}

		public List<string> NameValuesInTable()
		{
			return ContactsSearch.Rows.Select(i => i.Name.Text.ToLower()).ToList();
		}

		public List<string> CompanyValuesInTable()
		{
			return ContactsSearch.Rows.Select(i => i.Company.ToLower()).ToList();
		}
		public List<string> EmailValuesInTable()
		{
			return ContactsSearch.Rows.Select(i => i.Email.ToLower()).ToList();
		}
	}
}