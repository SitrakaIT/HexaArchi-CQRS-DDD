using System.ComponentModel.DataAnnotations;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Shared.AppSettings;

public sealed class ConnectionOptions : IAppOptions
{
    public static string ConfigurationSectionPath => "ConnectionStrings";

    [Required]
    public string SqlServerConnection { get; private init; }
}