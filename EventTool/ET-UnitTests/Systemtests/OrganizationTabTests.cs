using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace ET_UnitTests.Systemtests
{
    public class OrganizationTabTests
    {
        [Fact]
        public async Task ChangePermissionsForAllMembers()
        {
            // Arrange
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500 // Für visuelles Debugging
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Act
            await page.GotoAsync("https://localhost:7210/"); // ⚠️ URL ggf. anpassen
            await page.ClickAsync("text=Organisation");      // ⚠️ Button-Text ggf. anpassen
            await page.WaitForSelectorAsync(".member-list"); // ⚠️ Selektor ggf. anpassen

            // Zugriff auf alle Mitglieder über Locator
            var memberLocator = page.Locator(".member-item"); // ⚠️ Selektor ggf. anpassen
            int count = await memberLocator.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var member = memberLocator.Nth(i);

                // Öffne Dropdown-Menü
                await member.Locator(".permission-dropdown").ClickAsync(); // ⚠️ Selektor ggf. anpassen

                // Wähle neue Berechtigung aus
                await page.ClickAsync("text=Neue Berechtigung"); // ⚠️ Option-Text ggf. anpassen
            }

            // Assert
            for (int i = 0; i < count; i++)
            {
                var member = memberLocator.Nth(i);
                var permissionText = await member.Locator(".current-permission").TextContentAsync(); // ⚠️ Selektor ggf. anpassen

                Assert.Equal("Neue Berechtigung", permissionText); // ⚠️ Erwarteter Wert ggf. anpassen
            }
        }
    }
}
