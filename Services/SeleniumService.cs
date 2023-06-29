using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Microsoft.VisualBasic.FileIO;
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
        //options.AddArguments("--no-sandbox", "--width=1920", "--height=1080");

#if !DEBUG
        options.AddArguments("--headless");
        //options.AddArguments("--headless", "--disable-gpu");
#endif

        var service = FirefoxDriverService.CreateDefaultService("Drivers/", "geckodriver");
        return new FirefoxDriver(service, options, TimeSpan.FromSeconds(180));
    }

    /// <summary>
    /// Перевести текст в Google
    /// </summary>
    /// <param name="driver">Драйвер</param>
    /// <param name="html">Текст который надо перевести</param>
    private static string TranslateHtml(WebDriver driver, string html)
    {
        // Открываем страницу входа
        driver.Navigate().GoToUrl("https://translate.google.com/?hl=ru");
        Thread.Sleep(1000);
        // Ищем поле "username" и записываем туда адес электронной почты
        driver.GetElement(FindType.ClassName, "er8xn")!
            .InputText(html);

        Thread.Sleep(5000);
        // Ищем кнопку "Далее" и нажимаем её
        var text = driver.GetElement(FindType.ClassName, "HwtZe")!.Text;

        return text;

    }

}
