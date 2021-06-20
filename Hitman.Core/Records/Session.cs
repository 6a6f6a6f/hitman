using System.Diagnostics.CodeAnalysis;

namespace Hitman.Core.Records
{
    public record Session([NotNull] string Handle,[NotNull] string Jsessionid, [NotNull] string LiAt);
}
