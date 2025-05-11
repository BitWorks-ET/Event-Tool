using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_UnitTests.Systemtests
{
    public class OrganizationManagementTests
    {
        [Fact]
        public async Task ChangeRolesAndRemoveMember()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 300 });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7210/");

            // 1. Tab "ORGANISATION" öffnen
            await page.ClickAsync("text=ORGANISATION");

            // 2. Rollen aller Mitglieder ändern (Dropdowns finden)
            var dropdowns = await page.QuerySelectorAllAsync("select");
            foreach (var dropdown in dropdowns)
            {
                await dropdown.SelectOptionAsync(new[] { "Organisator" }); // Beispiel: Alle auf Organisator setzen
            }

            // 3. Ersten "ENTFERNEN"-Button klicken
            await page.ClickAsync("button:has-text('ENTFERNEN')");

            // Optional: Überprüfen ob das Mitglied entfernt wurde (z. B. Anzahl der Zeilen checken)
            var rows = await page.QuerySelectorAllAsync("tr");
            Assert.True(rows.Count < 4); // Es waren 4 Zeilen: Header + 3 Mitglieder → jetzt < 4
        }
    }
}
