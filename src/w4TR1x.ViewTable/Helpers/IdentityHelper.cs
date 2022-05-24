using System;

namespace w4TR1x.ViewTable.Helpers
{
    public static class IdentityHelper
    {
        public static string CreateIfNull(string? identity, string prefix, int totalCount = 8)
        {
            return string.IsNullOrWhiteSpace(identity)
                ? Create(prefix, totalCount)
                : identity;
        }

        public static string Create(string prefix, int totalCount = 8)
        {
            prefix = prefix.ToLower();
            return string.Concat(prefix, Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, totalCount - prefix.Length));
        }
    }
}
