using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TranslatorApi.Extensions;

namespace TranslatorApi.Services;

internal static class SeleniumService
{
    /// <summary>
    /// Метод перевода текста в Google
    /// </summary>
    /// <param name="html">Адрес</param>
    /// <exception cref="Exception">Ошибка</exception>
    public static string Translate(string html)
    {
        var driver = RunFirefox();

        try
        {
            var text = TranslateHtml(driver, html);
            return text;
        }
        catch (Exception)
        {
            return string.Empty;
        }
        finally
        {
            driver.Quit();
        }
    }

    /// <summary>
    /// Запустить Firefox
    /// </summary>
    /// <returns></returns>
    private static WebDriver RunFirefox()
    {
        var options = new FirefoxOptions();
        //options.LogLevel = FirefoxDriverLogLevel.Trace;
        //options.AddArguments("--no-sandbox", "--width=1920", "--height=1080");

#if !DEBUG
        options.AddArguments("--headless");
        //options.AddArguments("--headless", "--disable-gpu");
#endif

        var service = FirefoxDriverService.CreateDefaultService("Drivers/", "geckodriver");
        return new FirefoxDriver(service, options);
    }

    /// <summary>
    /// Перевести текст в Google
    /// </summary>
    /// <param name="driver">Драйвер</param>
    /// <param name="html">Текст который надо перевести</param>
    private static string TranslateHtml(WebDriver driver, string html)
    {
        // Открываем страницу google переводчика
        driver.Navigate().GoToUrl("https://translate.google.com/?hl=ru");
        Thread.Sleep(2000);
        // Ищем поле "textarea" и записываем туда не переведенный текст
        driver.GetElement(FindType.ClassName, "er8xn")!
            .InputText(html);

        Thread.Sleep(5000);
        // Ищем поле "textarea" с переводом и возвращаем его
        var text = driver.GetElement(FindType.ClassName, "HwtZe")!.Text;

        return text;

    }

}
