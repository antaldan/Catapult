using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlphaLaunch.Core.Selecta
{
    public static class SafeWalk
    {
        public static IEnumerable<string> EnumerateFiles(string path, HashSet<string> ignoredDirectories)
        {
            try
            {
                return Directory.EnumerateDirectories(path)
                    .Where(x => !ignoredDirectories.Contains(Path.GetFileName(x)))
                    .SelectMany(x => EnumerateFiles(x, ignoredDirectories))
                    .Concat(Directory.EnumerateFiles(path));
            }
            catch (PathTooLongException)
            {
                return Enumerable.Empty<string>();
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}