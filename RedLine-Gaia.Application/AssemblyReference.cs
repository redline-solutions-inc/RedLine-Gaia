using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RedLine_Gaia.Application;

[ExcludeFromCodeCoverage]
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
