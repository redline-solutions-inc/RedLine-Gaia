using System;

namespace RedLine_Gaia.Domain.Entities.Master;

public class Tenant
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ConnectionString { get; set; }
}
