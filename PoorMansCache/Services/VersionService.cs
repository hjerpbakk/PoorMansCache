using System.Diagnostics;
using System.IO;

namespace PoorMansCache.Services
{
    public class VersionService
    {
        static bool debugging;

        static VersionService() => CheckIfDEBUG();

        public string GetCurrentVersion() => 
            $"PoorMansCache - {File.ReadAllText("VERSION.txt")} - {(debugging ? "DEBUG" : "RELEASE")}";

        [Conditional("DEBUG")]
        static void CheckIfDEBUG() => debugging = true;
    }
}
