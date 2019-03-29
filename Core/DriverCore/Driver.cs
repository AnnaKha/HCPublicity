using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Core.Configuration;
using Protractor;

namespace Core.DriverCore
{
	public enum Browsers
	{
		Chrome,
		Firefox,
		IE
	}

	public static class Driver
	{
		private static IWebDriver driver;
		private static NgWebDriver ngDriver;
		public static NgWebDriver Instance
		{
			get
			{
				return ngDriver ?? GetNgWebDriver();
			}
		}
		public static IWebDriver WedDriver
		{
			get
			{
				return driver ?? GetWebDriver();
			}
		}
		public static IWebDriver GetWebDriver()
		{
			var browser = Config.BrowserName;

			var browserName = (Browsers)Enum.Parse(typeof(Browsers), browser);

			switch (browserName)
			{
				case Browsers.Chrome:
					return driver = new ChromeDriver();
				case Browsers.Firefox:
					return driver = new FirefoxDriver();
				case Browsers.IE:
					return driver  = new InternetExplorerDriver();
				default:
					throw new Exception("Unknown driver: " + browser);
			}
		}

		public static NgWebDriver GetNgWebDriver()
		{
			return ngDriver = new NgWebDriver(driver);
		}

		public static void QuitBrowser()
		{
			if (driver != null)
			{
				WedDriver.Quit();
				WedDriver.Dispose();
			}
			driver = null;
		}
	}
}
