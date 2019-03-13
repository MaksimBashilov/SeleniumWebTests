using System;
using System.IO;

namespace Maksim.Web.SeleniumTests.Resources
{
    internal struct TestFile
    {
        public string Filename;
        public string Path;
    }

    internal static class ResourceManager
    {
        private const string ResourcePath = "Resources\\";

        public static string GenerateFullPath(TestFile testFile)
        {
            string AssemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(AssemblyPath, ResourcePath, testFile.Path, testFile.Filename);
        }

        /// <summary>
        /// returns relative path for scorm package
        /// </summary>
        /// <returns></returns>
        public static TestFile GetJpgFile()
        {
            return new TestFile
            {
                Path = "Files",
                Filename = "SeleniumJpg.jpg"
            };
        }
    }
}
