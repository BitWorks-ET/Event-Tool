using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_UnitTests.Systemtests
{
    public class UserProfileTests
    {
        [Fact]
        public async Task EditUserData_FromHomePage()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 300 });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7210/");

            // 1. Klicke auf den Avatar "U"
            await page.ClickAsync("a:has-text('U')");

            // 2. Klicke auf "NUTZERDATEN ÄNDERN"
            await page.ClickAsync("text=NUTZERDATEN ÄNDERN");

            // 3. Eingaben vornehmen
            await page.FillAsync(":nth-match(input, 1)", "Test");       // Vorname
            await page.FillAsync(":nth-match(input, 2)", "Test");       // Nachname
            await page.FillAsync(":nth-match(input, 3)", "Test123");    // Passwort
            await page.FillAsync(":nth-match(input, 4)", "Test123");    // Passwort wiederholen

            // 4. Änderungen speichern
            await page.ClickAsync("button:has-text('ÄNDERUNGEN SPEICHERN')");
        }

    }
}
