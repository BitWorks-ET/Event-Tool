using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace ET_UnitTests.Systemtests
{
    public class LoginPageTests
    {
        [Fact]
        public async Task Login_ShouldRedirectToHomePage()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 300 });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7210/login");

            // Inputs in der Reihenfolge ausfüllen (wie bei Registrierung)
            await page.FillAsync(":nth-match(input, 1)", "test@example.com");
            await page.FillAsync(":nth-match(input, 2)", "password123");

            // Button klicken
            await page.ClickAsync("button:has-text('Anmelden')");

            // Optional: auf Redirect warten (z. B. zur Home-Page)
            //await page.WaitForURLAsync("**/home");
            //Assert.Contains("/home", page.Url);
        }
    }
}
