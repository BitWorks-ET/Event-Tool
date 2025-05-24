using Microsoft.AspNetCore.Components.Authorization;
using Moq;
using Xunit;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ET_Frontend.Helpers
{
    public class JwtClaimHelperTests
    {
        [Fact]
        public async Task GetUserDomainAsync_ReturnsDomainClaim_WhenPresent()
        {
            // Arrange: Erstelle einen ClaimsPrincipal mit dem gewünschten Claim
            var claims = new[] { new Claim("org", "orga1.org") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var authState = new AuthenticationState(user);

            var providerMock = new Mock<AuthenticationStateProvider>();
            providerMock
                .Setup(p => p.GetAuthenticationStateAsync())
                .ReturnsAsync(authState);

            // Act
            var result = await JwtClaimHelper.GetUserDomainAsync(providerMock.Object);

            // Assert
            Assert.Equal("orga1.org", result);
        }

        [Fact]
        public async Task GetUserDomainAsync_ReturnsDemoOrg_WhenClaimMissing()
        {
            // Arrange: Kein "org"-Claim
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(user);

            var providerMock = new Mock<AuthenticationStateProvider>();
            providerMock
                .Setup(p => p.GetAuthenticationStateAsync())
                .ReturnsAsync(authState);

            // Act
            var result = await JwtClaimHelper.GetUserDomainAsync(providerMock.Object);

            // Assert
            Assert.Equal("demo.org", result);
        }
    }
}
