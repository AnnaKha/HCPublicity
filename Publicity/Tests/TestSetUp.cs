using NUnit.Framework;
using Core.DriverCore;
using Protractor;
using OpenQA.Selenium;
using System;
using Core.Configuration;

namespace Publicity.Tests
{
	public class TestSetUp
	{
		IWebDriver driver = Driver.WedDriver;
		public NgWebDriver ngDriver = Driver.Instance;

		[SetUp]
		public void SetUp()
		{
			driver.Navigate().GoToUrl(Config.Url);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
			ngDriver.WaitForAngular();
			ngDriver.Url = driver.Url;
		}

		[TearDown]
		public void CleanUp()
		{
			Driver.QuitBrowser();
		}
	}
}
