using Bunit;
using Xunit;
using ET_Frontend.Pages.AccountManagement;
using MudBlazor.Services;

namespace ET_UnitTests.Frontendtests
{
    public class RegisterPageTests : TestContext
    {
        public RegisterPageTests()
        {
            Services.AddMudServices();
            JSInterop.SetupVoid("mudElementRef.addOnBlurEvent", _ => true);
        }

        [Fact]
        public void RegisterPage_ShouldRenderAllElements()
        {
            var cut = RenderComponent<Register>();

            // �berschrift pr�fen
            Assert.Contains("Registrieren", cut.Markup);

            // Felder pr�fen (z.B. E-Mail, Passwort, Name)
            Assert.Contains("E-Mail", cut.Markup);
            Assert.Contains("Passwort", cut.Markup);

            // Button pr�fen
            Assert.Contains("Registrieren", cut.Markup);
        }
    }
}
