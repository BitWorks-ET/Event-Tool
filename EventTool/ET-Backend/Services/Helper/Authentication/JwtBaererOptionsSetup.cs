using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace ET_Backend.Services.Helper.Authentication;

public class JwtBaererOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<JwtBaererOptionsSetup> _logger;

    /// <summary>
    /// Erstellt eine neue Instanz von <see cref="JwtBaererOptionsSetup"/>.
    /// </summary>
    /// <param name="options">Die JWT-Optionen (z. B. Secret, Issuer, Audience).</param>
    /// <param name="logger">Logger für Diagnosen.</param>
    public JwtBaererOptionsSetup(IOptions<JwtOptions> options, ILogger<JwtBaererOptionsSetup> logger)
    {
        _jwtOptions = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// Konfiguriert die <see cref="JwtBearerOptions"/> für einen benannten Dienst.
    /// </summary>
    /// <param name="name">Der Name der Optionsinstanz.</param>
    /// <param name="options">Die zu konfigurierenden Optionen.</param>
    public void Configure(string name, JwtBearerOptions options)
    {
        Configure(options);
    }


    /// <summary>
    /// Konfiguriert die <see cref="JwtBearerOptions"/> mit validierten Parametern.
    /// </summary>
    /// <param name="options">Die zu konfigurierenden Optionen.</param>
    public void Configure(JwtBearerOptions options)
    {
        _logger.LogInformation("JWT Validation mit SecretKey: {Key}", _jwtOptions.SecretKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }
}