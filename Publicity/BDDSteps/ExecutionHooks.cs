using Core.Configuration;
using Core.DriverCore;
using OpenQA.Selenium;
using Protractor;
using System;
using TechTalk.SpecFlow;

namespace Publicity.BDDSteps
{
	[Binding]
	public sealed class ExecutionHooks
	{
		IWebDriver driver;
		NgWebDriver ngDriver;

		[BeforeScenario("OpenApplication")]
		public void BeforeScenario()
		{
			driver = Driver.WedDriver;
			driver.Navigate().GoToUrl(Config.Url);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
			ngDriver = Driver.Instance;
			ngDriver.WaitForAngular();
			ngDriver.Url = driver.Url;
		}

		[AfterScenario("CloseBrowser")]
		public void AfterScenario()
		{
			Driver.QuitBrowser();
		}
	}
}
