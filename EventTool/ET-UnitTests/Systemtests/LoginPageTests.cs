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
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 500 });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7210/");

            await page.FillAsync(":nth-match(input, 1)", "admin@demo.org");
            await page.FillAsync(":nth-match(input, 2)", "demo");
            await page.ClickAsync("button:has-text('Anmelden')");

            await page.ClickAsync(".mud-tab:has-text('Events')");
            await page.ClickAsync(".mud-tab:has-text('Organisation')");


        }

        [Fact]
        public async Task Login_And_Logout_Works()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 500 });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7210/");
            await page.FillAsync(":nth-match(input, 1)", "admin@demo.org");
            await page.FillAsync(":nth-match(input, 2)", "demo");
            await page.ClickAsync("button:has-text('Anmelden')");

            // Avatar-Menü öffnen
            await page.Locator(".mud-avatar").ClickAsync();

            // Logout klicken
            await page.ClickAsync("text=Logout");

            // Prüfen, ob zur Login-Seite weitergeleitet wurde
            await page.WaitForURLAsync("**/login");
            Assert.Contains("/login", page.Url);
        }

    }
}
