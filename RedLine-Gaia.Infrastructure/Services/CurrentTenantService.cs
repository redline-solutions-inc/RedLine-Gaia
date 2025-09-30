using System;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Domain.Models;

namespace RedLine_Gaia.Infrastructure.Services;

public class CurrentTenantService : ICurrentTenantService
{
    private readonly ITenantService _tenantService;
    public int? TenantId { get; set; }
    public string? ConnectionString { get; set; }
    public Locale Locale { get; set; } = Locale.EN;
    public Guid? UserId { get; set; }

    public CurrentTenantService(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public bool SetTenant(int tenantId, string? userId, string? locale)
    {
        var foundConnectionString = _tenantService.GetConnectionString(tenantId);

        if (foundConnectionString is null)
            return false;

        TenantId = tenantId;
        ConnectionString = foundConnectionString;

        if (!string.IsNullOrEmpty(userId))
            UserId = Guid.Parse(userId);

        // Set locale, defaulting to EN if not provided or invalid
        if (
            !string.IsNullOrEmpty(locale)
            && Enum.TryParse<Locale>(locale, true, out var parsedLocale)
        )
        {
            Locale = parsedLocale;
        }
        else
        {
            Locale = Locale.EN;
        }

        return true;
    }
}
