using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
namespace WebScrappingNH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new EdgeDriver();
            var path = "ListaNH.txt";
            string nhUrl = "example";
            string username = "user";
            string password = "password";
            driver.Navigate().GoToUrlAsync(nhUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var Webusername = driver.FindElement(By.Name("username_or_email"));
            var Webpassword = driver.FindElement(By.Name("password"));
            Webusername.SendKeys(username);
            Webpassword.SendKeys(password);
            Console.ReadKey();
            var button = driver.FindElement(By.TagName("button"));
            button.Click();
            driver.Navigate().GoToUrlAsync(nhUrl + "favorites/");
            var cantidad = driver.FindElement(By.ClassName("count"));
            var limite = Convert.ToDouble(cantidad.Text.Trim('(', ')'));
            int i = 0;
            
            while (i <= limite)
            {
                var text = driver.FindElements(By.ClassName("caption"));
                i += text.Count();
                using (StreamWriter writer = File.AppendText(path))
                {
                    foreach (var element in text)
                    {
                        writer.WriteLine(element.Text);
                    }
                    writer.Close();
                }
                var n = driver.FindElement(By.ClassName("next"));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
                n.Click();
            }
            Console.ReadKey();
            driver.Quit();
        }
    }
}
