using System.Diagnostics;
using System.Threading;
using DNNSelenium.Common.BaseClasses;
using DNNSelenium.Common.BaseClasses.BasePages;
using OpenQA.Selenium;

namespace DNNSelenium.Common.CorePages
{
	public class AdminAdvancedSettingsPage : BasePage
	{
		public AdminAdvancedSettingsPage (IWebDriver driver) : base (driver) {}

		public static string AdminAdvancedSettingsUrl = "/Admin/AdvancedSettings";

		public override string PageTitleLabel
		{
			get { return "Advanced Configuration Settings"; }
		}

		public override string PageHeaderLabel
		{
			get { return "Advanced Configuration Settings"; }
		}

		public static string LanguagePacksTab = "//a[@href = '#asLanguagePacks']";
		public static string LanguagePackTable = "//table[contains(@id, '_AdvancedSettings_languagePacks')]";
		public static string NextButtonStep = "//a[contains(@id, '_nextButtonStep')]";
		public static string Return1Button = "//a[contains(@id, '_finishButtonStep')]";
		public static string AcceptCheckBox = "//input[contains(@id, 'wizInstall_chkAcceptLicense')]";

		public static string PackageInfoScreenTitle = "//h2/span[text() = 'Package Information']";
		public static string ReleaseNotesScreenTitle = "//h2/span[text() = 'Release Notes']";
		public static string ReviewLicenseScreenTitle = "//h2/span[text() = 'Review License']";
		public static string PackageReportScreenTitle = "//h2/span[text() = 'Package Installation Report']";

		public void OpenUsingUrl(string baseUrl)
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Open Admin '" + PageTitleLabel + "' page:");
			GoToUrl(baseUrl + AdminAdvancedSettingsUrl);
		}

		public void OpenUsingButtons(string baseUrl)
		{
			GoToUrl(baseUrl);
			Trace.WriteLine(BasePage.TraceLevelPage + "Open Admin '" + PageTitleLabel + "' page:");
			WaitAndClick(By.XPath(AdminBasePage.AdminTopMenuLink));
			WaitAndClick(By.XPath(AdminBasePage.AdvancedConfigurationSettingsLink));
		}

		public void OpenUsingControlPanel(string baseUrl)
		{
			GoToUrl(baseUrl);
			Trace.WriteLine(BasePage.TraceLevelPage + "Open Admin '" + PageTitleLabel + "' page:");
			SelectSubMenuOption(ControlPanelIDs.ControlPanelAdminOption, ControlPanelIDs.ControlPanelAdminAdvancedSettings, ControlPanelIDs.AdminAdvancedSettingsOption);
		}

		public string SetLanguageName(string language)
		{
			string option = null;

			switch (language)
			{
				case "de":
					{
						option = "Deutsch (Deutschland)";
						break;
					}
				case "es":
					{
						option = "Espa�ol (Espa�a, alfabetizaci�n internacional)";
						break;
					}
				case "fr":
					{
						option = "fran�ais (France)";
						break;
					}
				case "it":
					{
						option = "italiano (Italia)";
						break;
					}
				case "nl":
					{
						option = "Nederlands (Nederland)";
						break;
					}
			}

			return option;
		} 

		public void DeployLanguagePack(string packName)
		{
			OpenTab(By.XPath(LanguagePacksTab));

			WaitForElement(
				By.XPath("//table[contains(@id, '_AdvancedSettings_languagePacks')]//tr[td/span[contains(text(), '" + packName + "')]]/td/a")).Click();

			WaitForElement(By.XPath(PackageInfoScreenTitle));

			Trace.WriteLine(BasePage.TraceLevelPage + "Click on Next Button: ");
			WaitForElement(By.XPath(NextButtonStep), 60).Click();

			WaitForElement(By.XPath(ReleaseNotesScreenTitle));

			Trace.WriteLine(BasePage.TraceLevelPage + "Click on Next Button: ");
			WaitForElement(By.XPath(NextButtonStep), 60).Click();

			WaitForElement(By.XPath(ReviewLicenseScreenTitle));

			Trace.WriteLine(BasePage.TraceLevelPage + "Click on Accept CheckBox: ");
			WaitForElement(By.XPath(AcceptCheckBox)).WaitTillEnabled(30);
			WaitForElement(By.XPath(AcceptCheckBox)).Info();
			CheckBoxCheck(By.XPath(AcceptCheckBox));

			Trace.WriteLine(BasePage.TraceLevelPage + "Click on Next Button: ");
			WaitForElement(By.XPath(NextButtonStep), 60).Click();

			WaitForElement(By.XPath(PackageReportScreenTitle));
			Trace.WriteLine(BasePage.TraceLevelPage + "Click on Return Button: ");
			WaitForElement(By.XPath(Return1Button), 60).Click();

			Thread.Sleep(1000);
		}
	}
}
 