using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RedLine_Gaia.Application;

/// <summary>
/// Helper to quickly refrence the Application project Assembly.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
