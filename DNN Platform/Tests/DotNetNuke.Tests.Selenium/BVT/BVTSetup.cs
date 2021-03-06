﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DotNetNuke.Tests.DNNSelenium.BaseClasses;
using DotNetNuke.Tests.DNNSelenium.CorePages;
using DotNetNuke.Tests.DNNSelenium.Properties;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DotNetNuke.Tests.DNNSelenium.BVT
{
	[SetUpFixture]
	[Category("BVT")] 
	public class BVTSetup
	{
		[SetUp]
		public void RunBeforeBVTTests()
		{
			Trace.WriteLine(BasePage.TraceLevelComposite + "BVT Setup");

			var doc = XDocument.Load(@"BVT\" + Settings.Default.BVTDataFile);

			XElement settings = doc.Document.Element("Tests").Element("settings");

			string testName = settings.Attribute("name").Value;
			string installerLanguage = settings.Attribute("InstallerLanguage").Value;

			IWebDriver driver = TestBase.StartDriver(settings.Attribute("browser").Value);
			string baseUrl = settings.Attribute("baseURL").Value;

			Trace.WriteLine(BasePage.RunningTestKeyWord + "'" + testName + "'");
			Trace.WriteLine(BasePage.PreconditionsKeyWord);

			var installerPage = new InstallerPage(driver);

			installerPage.OpenUsingUrl(baseUrl);

			installerPage.SetInstallerLanguage(installerLanguage);
			installerPage.SetDictionary(installerLanguage);

			installerPage.FillAccountInfo(settings);

			installerPage.ClickOnContinueButton();

			installerPage.WaitForInstallationProcessToFinish();

			installerPage.ClickOnVisitWebsiteButton();

			installerPage.WelcomeScreen();

			AdminSiteSettingsPage adminSiteSettingsPage = new AdminSiteSettingsPage(driver);
			adminSiteSettingsPage.OpenUsingButtons(baseUrl);

			adminSiteSettingsPage.DisablePopups();

			driver.Quit();
		}

		[TearDown]
		public void RunAfterBVTTests()
		{
			Trace.WriteLine(BasePage.TraceLevelComposite + "BVT Teardown");
		}
	}
}
