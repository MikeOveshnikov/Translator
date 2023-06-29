using OpenQA.Selenium;

namespace TranslatorApi.Extensions;

public enum FindType
{
	Id,
	Name,
	XPath,
	ClassName,
	TagName,
	CssSelector
}

public static class WebDriverExtensions
{
	public static IWebElement? GetElement(this ISearchContext driver,
		FindType findType, string value, int waitingTimeSecond = 5)
	{
		while (waitingTimeSecond > 0)
		{
			var webElements = driver.GetElements(findType, value);
			if (webElements.Any())
			{
				return webElements.First();
			}

			Thread.Sleep(1000);
			waitingTimeSecond--;
		}

		return null;
	}

	public static IEnumerable<IWebElement> GetElements(this ISearchContext driver,
		FindType findType, string value)
	{
		var by = findType switch
		{
			FindType.Id => By.Id(value),
			FindType.Name => By.Name(value),
			FindType.XPath => By.XPath(value),
			FindType.ClassName => By.ClassName(value),
			FindType.TagName => By.TagName(value),
			FindType.CssSelector => By.CssSelector(value),
			_ => throw new NotImplementedException(),
		};

		return driver.FindElements(by);
	}

	public static void InputText(this IWebElement element, string value, int waitingTimeSecond = 10)
	{
		while (waitingTimeSecond > 0)
		{
			if (element.Enabled)
				break;

			Thread.Sleep(1000);
			waitingTimeSecond--;
		}

		element.SendKeys(value);
	}
}
