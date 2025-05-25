using ET_Backend.Services.Helper.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;
using System.Text;

namespace ET_UnitTests.Unittests
{
    public class JwtBaererOptionsSetupTests
    {
        [Fact]
        public void Configure_SetsTokenValidationParameters()
        {
            // Arrange
            var jwtOptions = new JwtOptions
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                SecretKey = "supergeheimespasswort1234567890!!",
                ExpirationTime = 1
            };
            var options = Options.Create(jwtOptions);
            var logger = new Mock<ILogger<JwtBaererOptionsSetup>>();
            var setup = new JwtBaererOptionsSetup(options, logger.Object);

            var jwtBearerOptions = new JwtBearerOptions();

            // Act
            setup.Configure(jwtBearerOptions);

            // Assert
            Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateAudience);
            Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateIssuer);
            Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateLifetime);
            Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey);
            Assert.Equal("TestIssuer", jwtBearerOptions.TokenValidationParameters.ValidIssuer);
            Assert.Equal("TestAudience", jwtBearerOptions.TokenValidationParameters.ValidAudience);

            var key = jwtBearerOptions.TokenValidationParameters.IssuerSigningKey as SymmetricSecurityKey;
            Assert.NotNull(key);
            Assert.Equal(Encoding.UTF8.GetBytes(jwtOptions.SecretKey), key.Key);
        }
    }
}
