using System.Collections.Generic;
using System.IO;

namespace WebJobsDemo.Shared
{
    public static class StringExtenders
    {
        public static string ToSingleLine(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var reader = new StringReader(value);

            var lines = new List<string>();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add(line.Trim());
            }

            return string.Join("; ", lines);
        }
    }
}
