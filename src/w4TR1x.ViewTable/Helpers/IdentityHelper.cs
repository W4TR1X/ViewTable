using System;

namespace w4TR1x.ViewTable.Helpers
{
    public static class IdentityHelper
    {
        public static string Create(string prefix, int totalCount = 8)
        {
            prefix = prefix.ToLower();
            return string.Concat(prefix, Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, totalCount - prefix.Length));
        }
    }
}
