using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaskOrganizer.Helpers
{
    public static class FileHelpers
    {
        public static string getFilePath(this string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string filePath = fi.FullName;
            return filePath;
        }

        public static string getRelativeFilePath(this string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDir.Contains("bin"))
            {
                int index = baseDir.IndexOf("bin");
                baseDir = baseDir.Substring(0, index);
            }
            return baseDir;
        }

    }
}
